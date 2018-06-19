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
using System.Collections.Generic;
using Tizen.TV.UIControls.Forms;

namespace Sample
{
    public class RemoteControlTestModel
    {
        public RemoteControlTestModel(string name, Type page)
        {
            Name = name;
            Page = page;
        }

        public string Name { get; }
        public Type Page { get; }
    }

    public class RemoteControlMainPageModel
    {
        public List<RemoteControlTestModel> TestList { get; set; }

        public RemoteControlMainPageModel()
        {
            TestList = new List<RemoteControlTestModel>()
            {
                new RemoteControlTestModel("Remote Control test", typeof(TestRemoteControl)),
                new RemoteControlTestModel("Remote Control Xaml test", typeof(TestRemoteControl_xaml)),
                new RemoteControlTestModel("NavigationPage test", typeof(TestNavigationPage)),
                new RemoteControlTestModel("TabbedPage test", typeof(TestTabbedPage)),
                new RemoteControlTestModel("MasterDetailPage test", typeof(TestMasterDetailPage)),
            };
        }
    }
}
