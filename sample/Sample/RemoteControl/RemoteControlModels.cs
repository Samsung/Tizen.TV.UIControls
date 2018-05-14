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
                new RemoteControlTestModel("Multi Pages test", typeof(TestMultiPage)),
                new RemoteControlTestModel("Remote Control test Xaml", typeof(TestRemoteControl_xaml)),
            };
        }
    }
}
