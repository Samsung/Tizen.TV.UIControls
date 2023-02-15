/*
 * Copyright (c) 2018 Samsung Electronics Co., Ltd All Rights Reserved
 *
 * Licensed under the Flora License, Version 1.1 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://floralicense.org/license/
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System;
using System.Threading.Tasks;
using Microsoft.Maui;
using Microsoft.Maui.Handlers;
using Microsoft.Maui.Platform;
using Tizen.NUI;
using Tizen.UIExtensions.NUI;

namespace Tizen.Theme.Common.Handler
{
    public class ContentPopupHandler : ElementHandler<IContentPopup, Popup>
    {
        public static IPropertyMapper<IContentPopup, ContentPopupHandler> Mapper = new PropertyMapper<IContentPopup, ContentPopupHandler>(ElementMapper)
        {
            [nameof(IContentPopup.Content)] = MapContent,
            [nameof(IContentPopup.IsOpen)] = MapIsOpen,
            [nameof(IContentPopup.BackgroundColor)] = MapBackgroundColor,
        };

        TaskCompletionSource<bool> _tcs;

        public ContentPopupHandler() : base(Mapper)
        {
        }

        protected override Popup CreatePlatformElement()
        {
            var popup = new Popup()
            {
                Layout = new LinearLayout
                {
                    HorizontalAlignment = HorizontalAlignment.Center
                },
            };

            return popup;
        }

        protected override void ConnectHandler(Popup platformView)
        {
            platformView.Closed += OnClosed;
            base.ConnectHandler(platformView);
        }

        protected override void DisconnectHandler(Popup platformView)
        {
            platformView.Closed -= OnClosed;
            base.DisconnectHandler(platformView);
        }

        public static void MapContent(ContentPopupHandler handler, IContentPopup popup)
        {
            var content = popup.Content?.ToPlatform(handler.MauiContext);
            handler.PlatformView.Content = content;
            handler.PlatformView.Content?.RaiseToTop();
        }

        public static void MapIsOpen(ContentPopupHandler handler, IContentPopup popup)
        {
            if (popup.IsOpen)
            {
                handler.PlatformView.Open();
            }
            else
            {
                handler.PlatformView.Close();
            }
        }

        public static void MapBackgroundColor(ContentPopupHandler handler, IContentPopup popup)
        {
            handler.PlatformView.BackgroundColor = popup.BackgroundColor.ToNUIColor();
        }

        void OnClosed(object sender, EventArgs e)
        {
            (VirtualView as ContentPopup)?.SendDismissed();
            VirtualView.IsOpen = false;
        }
    }
}
