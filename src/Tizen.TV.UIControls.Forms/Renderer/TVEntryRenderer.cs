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
using Tizen.TV.UIControls.Forms.Renderer;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Tizen;

[assembly: ExportRenderer(typeof(Entry), typeof(TVEntryRenderer))]
namespace Tizen.TV.UIControls.Forms.Renderer
{
    class TVEntryRenderer : EntryRenderer
    {
        const string _doneKeyName = "Select";
        const string _cancelKeyName = "Cancel";

        public TVEntryRenderer() : base()
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                InputEvents.GetEventHandlers(Element).Add(new RemoteKeyHandler(new Action<RemoteControlKeyEventArgs>((args) =>
                {
                    Console.WriteLine("@@@@ KEYNAME : " + args.PlatformKeyName);
                    if (args.PlatformKeyName.Equals(_doneKeyName))
                    {
                        Control.SetFocus(false);
                        Device.BeginInvokeOnMainThread(() =>
                        {
                            Element.Text = Control.Text;
                            Element.SendCompleted();
                        });
                    }
                    else if (args.PlatformKeyName.Equals(_cancelKeyName))
                    {
                        Control.HideInputPanel();
                    }
                }), RemoteControlKeyTypes.KeyDown));
            }
        }
    }
}
