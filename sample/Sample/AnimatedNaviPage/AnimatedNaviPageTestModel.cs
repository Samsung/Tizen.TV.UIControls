using System;
using System.Collections.Generic;

namespace Sample
{
    class TestModel
    {
        public string Name { get; set; }
        public Type PageType { get; set; }
        public bool IsPreviousVisible { get; set; }
    }

    class AnimatedNaviPageTestModel
    {
        public IList<TestModel> TestList { get; }
        public AnimatedNaviPageTestModel()
        {
            TestList = new List<TestModel>
            {
                new TestModel
                {
                    Name = "Basic Test",
                    PageType = typeof(AnimatedNaviPageBasicTest),
                    IsPreviousVisible = true
                },
                new TestModel
                {
                    Name = "Page Trasintion Test",
                    PageType = typeof(AnimatedContentPageTransitTest),
                    IsPreviousVisible = false
                },
            };
        }
    }
}
