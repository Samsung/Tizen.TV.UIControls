/*
 * Copyright (c) 2018 Samsung Electronics Co., Ltd All Rights Reserved
 *
 * Licensed under the Apache License, Version 2.0 (the License);
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an AS IS BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System;
using ElmSharp;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Tizen;
using Tizen.TV.UIControls.Forms.Renderer;

[assembly: ResolutionGroupName("TizenTVUIControl")]
[assembly: ExportEffect(typeof(RemoteKeyEventEffect), "RemoteKeyEventEffect")]
namespace Tizen.TV.UIControls.Forms.Renderer
{
    public class RemoteKeyEventEffect : PlatformEffect
    {
        protected override void OnAttached()
        {
            try
            {
                if (Element is Page)
                {
                    EcoreKeyEvents.Instance.KeyDown += OnPageKeyDown;
                    EcoreKeyEvents.Instance.KeyUp += OnPageKeyUp;
                }
                else
                {
                    Control.KeyDown += OnViewKeyDown;
                    Control.KeyUp += OnViewKeyUp;
                }
            }
            catch (Exception e)
            {
                Log.Error(UIControls.Tag, $"Failed to attach the effect : {e.Message}");
            }
        }

        protected override void OnDetached()
        {
            if (Element is Page)
            {
                EcoreKeyEvents.Instance.KeyDown -= OnPageKeyDown;
                EcoreKeyEvents.Instance.KeyUp -= OnPageKeyUp;
            }
            else
            {
                Control.KeyDown -= OnViewKeyDown;
                Control.KeyUp -= OnViewKeyUp;
            }
        }

        void OnPageKeyDown(object sender, EcoreKeyEventArgs e)
        {
            InvokeActionAndEvent(RemoteControlKeyTypes.KeyDown, e.KeyName);
        }

        void OnPageKeyUp(object sender, EcoreKeyEventArgs e)
        {
            InvokeActionAndEvent(RemoteControlKeyTypes.KeyUp, e.KeyName);
        }

        void OnViewKeyDown(object sender, EvasKeyEventArgs e)
        {
            if (InvokeActionAndEvent(RemoteControlKeyTypes.KeyDown, e.KeyName))
                e.Flags = EvasEventFlag.OnHold;
        }

        void OnViewKeyUp(object sender, EvasKeyEventArgs e)
        {
            if (InvokeActionAndEvent(RemoteControlKeyTypes.KeyUp, e.KeyName))
                e.Flags = EvasEventFlag.OnHold;
        }

        static RemoteControlKeyEventArgs CreateArgs(RemoteControlKeyTypes keyType, string keyName)
        {
            RemoteControlKeyNames key = RemoteControlKeyNames.Undefined;
            if (Enum.TryParse(keyName, out key))
                return new RemoteControlKeyEventArgs(keyType, key);
            if (Enum.TryParse("NUM" + keyName, out key))
                return new RemoteControlKeyEventArgs(keyType, key);

            return new RemoteControlKeyEventArgs(keyType, key);
        }

        bool InvokeActionAndEvent(RemoteControlKeyTypes keyType, string keyName)
        {
            RemoteControlKeyEventArgs args = CreateArgs(keyType, keyName);
            if (args == null)
                return false;

            if (Element is Page targetPage)
            {
                if (!IsOnCurrentPage(Application.Current.MainPage, targetPage))
                {
                    return false;
                }
            }

            var handlers = InputEvents.GetEventHandlers(Element);
            foreach (RemoteKeyHandler item in handlers)
            {
                item.SendKeyEvent(args);
            }
            return args.Handled;
        }

        bool IsOnCurrentPage(Page currentPage, Page targetPage)
        {
            if (currentPage == targetPage)
                return true;

            while (currentPage is IPageContainer<Page>)
            {
                currentPage = (currentPage as IPageContainer<Page>).CurrentPage;
                if (currentPage == targetPage)
                    return true;
            }

            if (currentPage is MasterDetailPage masterDetailPage)
            {
                if (masterDetailPage.IsPresented)
                {
                    if (IsOnCurrentPage(masterDetailPage.Master, targetPage))
                        return true;
                }
                if (!(masterDetailPage.MasterBehavior == MasterBehavior.Popover && masterDetailPage.IsPresented))
                {
                    if (IsOnCurrentPage(masterDetailPage.Detail, targetPage))
                        return true;
                }
            }

            return false;
        }
    }
}
