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
using Xamarin.Forms.Xaml;
using Tizen.TV.UIControls.Forms;

namespace Sample
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class TestFlyoutPage : FlyoutPage
	{
        public Command<RemoteControlKeyEventArgs> FlyoutPageHandler
        {
            get
            {
                return new Command<RemoteControlKeyEventArgs>((arg) =>
                {
                    Console.WriteLine("FlyoutPage => arg.KeyType : {0} , arg.KeyName : {1}", arg.KeyType, arg.KeyName);
                    Title = $"{arg.KeyName} was pressed";
                });
            }
        }

        public Command<RemoteControlKeyEventArgs> FlyoutHandler
        {
            get
            {
                return new Command<RemoteControlKeyEventArgs>((arg) => {
                    Console.WriteLine("Flyout => arg.KeyType : {0} , arg.KeyName : {1}", arg.KeyType, arg.KeyName);
                    FlyoutLabel.Text = $"{arg.KeyName} was pressed";
                });
            }
        }

        public Command<RemoteControlKeyEventArgs> DetailPageHandler
        {
            get
            {
                return new Command<RemoteControlKeyEventArgs>((arg) =>
                {
                    Console.WriteLine("Detail Page => arg.KeyType : {0} , arg.KeyName : {1}", arg.KeyType, arg.KeyName);
                    DetailLabel.Text = $"{arg.KeyName} was pressed";
                });
            }
        }

        public TestFlyoutPage()
		{
			InitializeComponent ();
		}

        void OnClicked(object sender, EventArgs e)
        {
            if (FlyoutLayoutBehavior == FlyoutLayoutBehavior.Popover)
                FlyoutLayoutBehavior = FlyoutLayoutBehavior.Split;
            else
                FlyoutLayoutBehavior = FlyoutLayoutBehavior.Popover;
        }

        void OnShowFlyout(object sender, EventArgs e)
        {
            IsPresented = true;
        }
    }
}