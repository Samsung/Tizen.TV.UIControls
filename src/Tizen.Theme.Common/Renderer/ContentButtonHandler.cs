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
using Tizen.NUI;
using Tizen.UIExtensions.NUI;
using NTouchEventArgs = Tizen.NUI.BaseComponents.View.TouchEventArgs;

namespace Tizen.Theme.Common.Renderer
{
    public class ContentButtonHandler : ContentViewHandler
    {
        bool _isPressed;

        ContentButton Button => VirtualView as ContentButton;

        protected override void ConnectHandler(ContentViewGroup platformView)
        {
            platformView.TouchEvent += OnTouched;
            base.ConnectHandler(platformView);
        }

        bool OnTouched(object source, NTouchEventArgs e)
        {
            var state = e.Touch.GetState(0);

            if (state == PointStateType.Down)
            {
                _isPressed = true;
                OnPressed(this, EventArgs.Empty);
                return true;
            }
            else if (state == PointStateType.Up)
            {
                OnReleased(this, EventArgs.Empty);
                if (_isPressed && PlatformView.IsInside(e.Touch.GetLocalPosition(0)))
                {
                    OnClicked(this, EventArgs.Empty);
                }
                _isPressed = false;
                return true;
            }
            return false;
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
