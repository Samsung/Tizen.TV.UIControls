using System;
using Tizen.TV.UIControls.Forms;

namespace Sample
{
    public class TestModel
    {
        public TestModel(string name, Type page, MediaSource source) : this(name, page, source, null)
        {
        }

        public TestModel(string name, Type page, MediaSource source, object submodel)
        {
            Name = name;
            Page = page;
            Source = source;
            SubModel = submodel;
        }

        public string Name { get; }
        public Type Page { get; }

        public MediaSource Source { get; }

        public object SubModel { get; }
    }
}
