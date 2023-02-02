﻿/*
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
using System.Linq;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Platform;
using MButton = Microsoft.Maui.Controls.Button;
using MApplication = Microsoft.Maui.Controls.Application;

namespace Tizen.TV.UIControls.Forms.Handler
{
    public class PlatformAccessKeyEffect : PlatformEffect
    {
        RemoteControlKeyNames _targetKeyName;

        protected override void OnAttached()
        {
            try
            {
                WindowKeyEvents.Instance.KeyDown += OnKeyDown;
                _targetKeyName = InputEvents.GetAccessKey(Element);
            }
            catch(Exception e)
            {
                Log.Error(UIControls.Tag, $"Failed to attach the effect : {e.Message}");
            }
        }

        protected override void OnDetached()
        {
            WindowKeyEvents.Instance.KeyDown -= OnKeyDown;
        }

        Page GetParentPage()
        {
            var _targetElement = Element;

            if (_targetElement is Page page) return page;
            while (_targetElement != null && _targetElement.Parent != null)
            {
                if (_targetElement.Parent is Page parentPage) return parentPage;
                _targetElement = _targetElement.Parent;
            }
            return null;
        }

        void OnKeyDown(object sender, Tizen.NUI.Window.KeyEventArgs e)
        {
            var targetName = _targetKeyName.ToString();
            if (targetName == e.Key.KeyPressedName || targetName == "NUM" + e.Key.KeyPressedName)
            {
                var targetPage = GetParentPage();
                if(IsOnMainPage(targetPage))
                {
                    ActiveOrFocusElement();
                }
            }
        }

        bool IsOnMainPage(Page targetPage)
        {
            var mainPage = MApplication.Current.MainPage;
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

        void ActiveOrFocusElement()
        {
            (Element as VisualElement).Focus();
            if (Element is MButton)
                (Element as MButton).SendClicked();
        }
    }
}