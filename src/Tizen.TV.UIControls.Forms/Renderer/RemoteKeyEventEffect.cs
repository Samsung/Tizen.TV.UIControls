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

using ElmSharp;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Platform;
using System;
using System.Linq;
using Tizen.TV.UIControls.Forms.Renderer;

namespace Tizen.TV.UIControls.Forms.Renderer
{
    public class PlatformRemoteKeyEventEffect : PlatformEffect
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
            if (InvokeActionAndEvent(RemoteControlKeyTypes.KeyDown, e.KeyName, e.Flags.HasFlag(EvasEventFlag.OnHold)))
                e.Flags = EvasEventFlag.OnHold;
        }

        void OnViewKeyUp(object sender, EvasKeyEventArgs e)
        {
            if (InvokeActionAndEvent(RemoteControlKeyTypes.KeyUp, e.KeyName, e.Flags.HasFlag(EvasEventFlag.OnHold)))
                e.Flags = EvasEventFlag.OnHold;
        }

        bool InvokeActionAndEvent(RemoteControlKeyTypes keyType, string keyName, bool isHandled = false)
        {
            RemoteControlKeyEventArgs args = RemoteControlKeyEventArgs.Create((VisualElement)Element, keyType, keyName, isHandled);
            if (args == null)
                return false;

            if (Element is Page targetPage)
            {
                if (!IsOnMainPage(targetPage))
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

        bool IsOnMainPage(Page targetPage)
        {
            var mainPage = Microsoft.Maui.Controls.Application.Current.MainPage;
            var currentPage = mainPage.Navigation.ModalStack.Count > 0 ? mainPage.Navigation.ModalStack.LastOrDefault() : mainPage;
            return IsOnCurrentPage(currentPage, targetPage);
        }

        bool IsOnCurrentPage(Page currentPage, Page targetPage)
        {
            if (currentPage == targetPage)
                return true;

            var pageToCompare = currentPage;
            while (pageToCompare is IPageContainer<Page>)
            {
                pageToCompare = (pageToCompare as IPageContainer<Page>).CurrentPage;
                if (pageToCompare == targetPage)
                    return true;
            }

            if (pageToCompare is FlyoutPage flyoutPage)
            {
                if (flyoutPage.IsPresented)
                {
                    if (IsOnCurrentPage(flyoutPage.Flyout, targetPage))
                        return true;
                }
                if (!(flyoutPage.FlyoutLayoutBehavior == FlyoutLayoutBehavior.Popover && flyoutPage.IsPresented))
                {
                    if (IsOnCurrentPage(flyoutPage.Detail, targetPage))
                        return true;
                }
            }

//#pragma warning disable CS0618 // Type or member is obsolete
//            if (pageToCompare is MasterDetailPage masterDetailPage)
//#pragma warning restore CS0618 // Type or member is obsolete
//            {
//                if (masterDetailPage.IsPresented)
//                {
//                    if (IsOnCurrentPage(masterDetailPage.Master, targetPage))
//                        return true;
//                }
//                if (!(masterDetailPage.MasterBehavior == MasterBehavior.Popover && masterDetailPage.IsPresented))
//                {
//                    if (IsOnCurrentPage(masterDetailPage.Detail, targetPage))
//                        return true;
//                }
//            }

            return false;
        }
    }
}
