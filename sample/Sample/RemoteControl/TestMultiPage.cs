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

using Xamarin.Forms;
using Tizen.TV.UIControls.Forms;

namespace Sample
{
    public class TestNavigationPage : NavigationPage
    {
        public TestNavigationPage()
        {
            SetHasNavigationBar(this, false);

            InputEvents.GetEventHandlers(this).Add(new RemoteKeyHandler((args) =>
            {
                if (args.KeyName == RemoteControlKeyNames.Up)
                    DisplayAlert("KeyEvent", "Up Pressed on NavigationPage", "ok");
            }, RemoteControlKeyTypes.KeyUp));

            var page1 = new ContentPage
            {
                Title = "child page1",
                Content = new StackLayout
                {
                    Children =
                    {
                        new Label { Text = "`Up` is triggering NavigationPage EventHandler" },
                        new Label { Text = "`Down` is triggering child page1 EventHandler" },
                    }

                }
            };

            InputEvents.GetEventHandlers(page1).Add(new RemoteKeyHandler((args) =>
            {
                if (args.KeyName == RemoteControlKeyNames.Down)
                    DisplayAlert("KeyEvent", "Down Pressed on child page1", "ok");
            }, RemoteControlKeyTypes.KeyUp));

            var page2 = new ContentPage
            {
                Title = "child page2",
                Content = new StackLayout
                {
                    Children =
                    {
                        new Label { Text = "`Up` is triggering NavigationPage EventHandler" },
                        new Label { Text = "`Down` is triggering child page2 EventHandler" },
                    }

                }
            };
            InputEvents.GetEventHandlers(page2).Add(new RemoteKeyHandler((args) =>
            {
                if (args.KeyName == RemoteControlKeyNames.Down)
                    DisplayAlert("KeyEvent", "Down Pressed on child page2", "ok");
            }, RemoteControlKeyTypes.KeyUp));

            this.PushAsync(page1);
            this.PushAsync(page2);
        }
    }
}
