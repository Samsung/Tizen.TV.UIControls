using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Sample
{
    public class TestCategory
    {
        public string Name { get; set; }
        public Type PageType { get; set; }
    }

    public class MainPageModel
    {
        public List<TestCategory> TestCategories { get; }

        public MainPageModel()
        {
            TestCategories = new List<TestCategory>
            {
                new TestCategory
                {
                    Name = "MediaPlayer Test",
                    PageType = typeof(PlayerMainPage),
                }
            };
        }
    }
}
