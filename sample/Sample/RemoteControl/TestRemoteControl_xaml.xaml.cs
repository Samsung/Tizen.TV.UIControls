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
    public partial class TestRemoteControl_xaml : ContentPage
    {
        public Command<RemoteControlKeyEventArgs> ButtonHandler
        {
            get
            {
                return new Command<RemoteControlKeyEventArgs>((arg) => 
                {
                    Console.WriteLine("Control => arg.KeyType : {0} , arg.KeyName : {1}", arg.KeyType, arg.KeyName);
                    Label1.Text = $"Button1 Keyevent : KeyType {arg.KeyType}, KeyName {arg.KeyName}";
                });
            }
        }

        public Command<RemoteControlKeyEventArgs> PageHandler
        {
            get
            {
                return new Command<RemoteControlKeyEventArgs>((arg) =>
                {
                    Console.WriteLine("Page => arg.KeyType : {0} , arg.KeyName : {1}", arg.KeyType, arg.KeyName);
                    Label2.Text = $"Page Keyevent : KeyType {arg.KeyType}, KeyName {arg.KeyName}";
                });
            }
        }

        public TestRemoteControl_xaml ()
        {
            InitializeComponent ();
        }

        void OnClicked(object sender, EventArgs e)
        {
            this.DisplayAlert("alert", "Button2 clicked", "ok");
        }
    }
}