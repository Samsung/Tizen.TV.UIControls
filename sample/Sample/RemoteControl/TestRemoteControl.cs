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
using Xamarin.Forms;
using Tizen.TV.UIControls.Forms;

namespace Sample
{
    public class TestRemoteControl : ContentPage
    {
        int _clickedTimes = 0;
        public TestRemoteControl()
        {
            Button button1 = new Button { Text = "Button1" };
            Switch toggle = new Switch();

            RemoteKeyHandler buttonHandler = new RemoteKeyHandler(new Action<RemoteControlKeyEventArgs>((arg) =>
            {
                Console.WriteLine(" Control => arg.KeyType : {0} , arg.KeyName : {1}, arg.PlatformKeyName : {2}", arg.KeyType, arg.KeyName, arg.PlatformKeyName);
                button1.Text = $"Button1 : {arg.KeyType} {arg.KeyName} {arg.PlatformKeyName}";
                arg.Handled = toggle.IsToggled;
            }));
            InputEvents.GetEventHandlers(button1).Add(buttonHandler);


            Button button2 = new Button { Text = "Button2 (Accesskey 1)" };
            button2.Clicked += (s, e) =>
            {
                button2.Text = $"Button2 (Accesskey 1): {++_clickedTimes} clicked";
            };
            InputEvents.SetAccessKey(button2, RemoteControlKeyNames.NUM1);

            SetBinding(TitleProperty, new Binding("Name"));


            Label label;
            Content = new StackLayout
            {
                Children =
                {
                    new StackLayout {
                        Orientation = StackOrientation.Horizontal,
                        Children = { toggle, new Label { Text = "Consume event" } }
                    },
                    button1,
                    button2,
                    (label = new Label())
                }
            };

            RemoteKeyHandler PageHandler = new RemoteKeyHandler(new Action<RemoteControlKeyEventArgs>((arg) =>
            {
                Console.WriteLine("Page1 => arg.KeyType : {0} , arg.KeyName : {1} , arg.PlatformKeyName : {2}", arg.KeyType, arg.KeyName, arg.PlatformKeyName);
                label.Text = $"Page Key event : KeyType {arg.KeyType}, KeyName {arg.KeyName}, PlatformKeyName {arg.PlatformKeyName}";
            }));
            InputEvents.GetEventHandlers(this).Add(PageHandler);
        }
    }
}
