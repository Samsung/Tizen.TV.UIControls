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
	public partial class TestMasterDetailPage : MasterDetailPage
	{
        public Command<RemoteControlKeyEventArgs> MasterDetailPageHandler
        {
            get
            {
                return new Command<RemoteControlKeyEventArgs>((arg) =>
                {
                    Console.WriteLine("MasterDetailPage => arg.KeyType : {0} , arg.KeyName : {1}", arg.KeyType, arg.KeyName);
                    Title = $"{arg.KeyName} was pressed";
                });
            }
        }

        public Command<RemoteControlKeyEventArgs> MasterPageHandler
        {
            get
            {
                return new Command<RemoteControlKeyEventArgs>((arg) => {
                    Console.WriteLine("Master Page => arg.KeyType : {0} , arg.KeyName : {1}", arg.KeyType, arg.KeyName);
                    MasterLabel.Text = $"{arg.KeyName} was pressed";
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

        public TestMasterDetailPage ()
		{
			InitializeComponent ();
		}

        void OnClicked(object sender, EventArgs e)
        {
            if (MasterBehavior == MasterBehavior.Popover)
                MasterBehavior = MasterBehavior.Split;
            else
                MasterBehavior = MasterBehavior.Popover;
        }

        void OnShowMaster(object sender, EventArgs e)
        {
            IsPresented = true;
        }
    }
}