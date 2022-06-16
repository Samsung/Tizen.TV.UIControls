/*
 * Copyright (c) 2020 Samsung Electronics Co., Ltd All Rights Reserved
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
using Microsoft.Maui.Handlers;
using Microsoft.Maui.Platform;
using Tizen.UIExtensions.Common;
using Tizen.UIExtensions.ElmSharp;
using TButton = Tizen.UIExtensions.ElmSharp.Button;

namespace Tizen.Theme.Common.Renderer
{
    public class ContentButtonHandler : ContentViewHandler
    {
        TButton _button;

        ContentButton Button => VirtualView as ContentButton;

        protected override void ConnectHandler(ContentCanvas platformView)
        {
            base.ConnectHandler(platformView);
            Initialize();
        }

        void Initialize()
        {
            if (_button == null)
            {
                _button = new TButton(PlatformParent);
                _button.SetTransparentStyle();
                _button.Opacity = 0;
                _button.Show();

                _button.Pressed += OnPressed;
                _button.Released += OnReleased;
                _button.Clicked += OnClicked;
                _button.Focused += OnButtonFocused;
                _button.Unfocused += OnButtonFocused;
                PlatformView.PackEnd(_button);
            }

            PlatformView.LayoutUpdated += OnLayoutUpdated;
        }

        private void OnLayoutUpdated(object sender, LayoutEventArgs e)
        {
            _button.Geometry = PlatformView.Geometry;
            _button.RaiseTop();
        }

        void OnButtonFocused(object sender, EventArgs e)
        {
            if (_button.IsFocused)
                OnFocused(this, e);
            else 
                OnUnfocused(this, e);
        }

        void OnPressed(object sender, EventArgs args)
        {
            Button?.SendPressed();
        }

        void OnReleased(object sender, EventArgs args)
        {
            Button?.SendReleased();
        }

        void OnClicked(object sender, EventArgs args)
        {
            Button?.SendClicked();
        }
    }
}
