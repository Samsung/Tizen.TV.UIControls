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
	public partial class TestTabbedPage : TabbedPage
	{
        public Command TabbedPageHandler_Tabbed
        {
            get
            {
                return new Command<RemoteControlKeyEventArgs>((arg) =>
                {
                    Console.WriteLine("TabbedPage => arg.KeyType : {0} , arg.KeyName : {1}", arg.KeyType, arg.KeyName);
                    if (arg.KeyType == RemoteControlKeyTypes.KeyUp)
                    {
                        if (arg.KeyName == RemoteControlKeyNames.NUM1)
                        {
                            CurrentPage = Page1;
                        }
                        else if (arg.KeyName == RemoteControlKeyNames.NUM2)
                        {
                            CurrentPage = Page2;
                        }
                        else if (arg.KeyName == RemoteControlKeyNames.NUM3)
                        {
                            CurrentPage = Page3;
                        }
                    }
                });
            }
        }

        public Command PageHandler1_Tabbed
        {
            get
            {
                return new Command<RemoteControlKeyEventArgs>((arg) =>
                {
                    Console.WriteLine("ContentPage => arg.KeyType : {0} , arg.KeyName : {1}", arg.KeyType, arg.KeyName);
                    Label1.Text = $"{arg.KeyName} was pressed";
                });
            }
        }

        public Command PageHandler2_Tabbed
        {
            get
            {
                return new Command<RemoteControlKeyEventArgs>((arg) =>
                {
                    Console.WriteLine("ContentPage => arg.KeyType : {0} , arg.KeyName : {1}", arg.KeyType, arg.KeyName);
                    Label2.Text = $"{arg.KeyName} was pressed";
                });
            }
        }

        public Command PageHandler3_Tabbed
        {
            get
            {
                return new Command<RemoteControlKeyEventArgs>((arg) =>
                {
                    Console.WriteLine("ContentPage => arg.KeyType : {0} , arg.KeyName : {1}", arg.KeyType, arg.KeyName);
                    Label3.Text = $"{arg.KeyName} was pressed";
                });
            }
        }

        public Command ButtonHandler_Tabbed
        {
            get
            {
                return new Command<RemoteControlKeyEventArgs>((arg) =>
                {
                    Console.WriteLine("Control => arg.KeyType : {0} , arg.KeyName : {1}", arg.KeyType, arg.KeyName);
                });
            }
        }

		public TestTabbedPage ()
		{
			InitializeComponent ();
		}
	}
}