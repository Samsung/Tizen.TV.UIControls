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
using Microsoft.Maui.Controls;
using Microsoft.Maui.Handlers;

namespace Tizen.TV.UIControls.Forms.Renderer
{
    public class TVEntryHandler : EntryHandler
    {
        const string _doneKeyName = "Select";
        const string _cancelKeyName = "Cancel";

        protected override void ConnectHandler(UIExtensions.ElmSharp.Entry platformView)
        {
            base.ConnectHandler(platformView);

            if (platformView != null)
            {
                InputEvents.GetEventHandlers(VirtualView as BindableObject)?.Add(new RemoteKeyHandler(new Action<RemoteControlKeyEventArgs>((args) =>
                {
                    if (args.PlatformKeyName.Equals(_doneKeyName))
                    {
                        //FocusSearch(true)?.SetFocus(true);
                        Device.BeginInvokeOnMainThread(() =>
                        {
                            VirtualView.Text = platformView.Text;
                            (VirtualView as IEntryController)?.SendCompleted();
                        });
                    }
                    else if (args.PlatformKeyName.Equals(_cancelKeyName))
                    {
                        platformView.HideInputPanel();
                    }
                }), RemoteControlKeyTypes.KeyDown));

                if (platformView is Tizen.UIExtensions.ElmSharp.Entry entry)
                {
                    entry.EntryLayoutFocused += OnFocused;
                    entry.EntryLayoutUnfocused += OnUnfocused;
                }
            }
        }
    }
}
