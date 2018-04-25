using System;
using Tizen.TV.UIControls.Forms;

namespace Sample
{
    public class TestModel
    {
        public TestModel(string name, Type page, MediaSource source)
        {
            Name = name;
            Page = page;
            Source = source;
        }

        public string Name { get; }
        public Type Page { get; }

        public MediaSource Source { get; }
    }
}
