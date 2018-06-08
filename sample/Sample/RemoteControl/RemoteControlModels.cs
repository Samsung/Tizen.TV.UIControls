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
