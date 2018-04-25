using System.Collections.Generic;
using Tizen.TV.UIControls.Forms;

namespace Sample
{
    public class MainPageModel
    {
        public MainPageModel()
        {
            TestList = new List<TestModel>();
            TestList.Add(new TestModel("Overlay page test", typeof(TestOverlayPage), MediaSource.FromFile("b.mp4")));
            TestList.Add(new TestModel("Overlay page test with code", typeof(TestOverlayPage2), MediaSource.FromFile("a.mp4")));
            TestList.Add(new TestModel("Overlay view test", typeof(TestOverlayView), MediaSource.FromFile("c.mp4")));
            TestList.Add(new TestModel("Media view test", typeof(TestMediaView), MediaSource.FromFile("c.mp4")));
            TestList.Add(new TestModel("Aspect test", typeof(TestAspect), MediaSource.FromFile("d.mp4")));
            TestList.Add(new TestModel("Url test(slow)", typeof(TestOverlayPage), MediaSource.FromUri(new System.Uri("http://www.html5videoplayer.net/videos/toystory.mp4"))));
            TestList.Add(new TestModel("Url test(fast)", typeof(TestOverlayPage), MediaSource.FromUri(new System.Uri("http://10.113.111.170/~abyss/d.mp4"))));
        }

        public List<TestModel> TestList { get; set; }
    }
}
