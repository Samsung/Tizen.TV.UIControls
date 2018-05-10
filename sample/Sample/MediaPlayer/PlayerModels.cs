using System;
using System.Collections.Generic;
using Tizen.TV.UIControls.Forms;

namespace Sample
{
    public class PlayerTestModel
    {
        public PlayerTestModel(string name, Type page, MediaSource source)
        {
            Name = name;
            Page = page;
            Source = source;
        }

        public string Name { get; }
        public Type Page { get; }

        public MediaSource Source { get; }

        public object SubModel { get; }
    }

    public class AudioItem
    {
        public string Path { get; set; }
        public string Text { get; set; }
    }
    public class AudioPlayerTestModel : PlayerTestModel
    {
        public IList<AudioItem> Items { get; }
        public AudioPlayerTestModel(string name, Type page) : base(name, page, null)
        {
            Items = new List<AudioItem>{
                new AudioItem { Path = "Kalimba.mp3", Text = "Kalimba"},
                new AudioItem { Path = "Maid with the Flaxen Hair.mp3", Text = "Maid with the Flaxen Hair"},
                new AudioItem { Path = "Sleep Away.mp3", Text = "Sleep Away.mp3"},
            };
        }
    }
    public class PlayerMainPageModel
    {
        public PlayerMainPageModel()
        {
            TestList = new List<PlayerTestModel>
            {
                new PlayerTestModel("Embedding control test", typeof(TestEmbeddingControl), MediaSource.FromUri(new System.Uri("http://10.113.111.170/~abyss/d.mp4"))),
                new PlayerTestModel("Embedding control Page test", typeof(TestEmbeddingControl2), MediaSource.FromUri(new System.Uri("http://10.113.111.170/~abyss/a.mp4"))),
                new AudioPlayerTestModel("Audio player test", typeof(TestAudioPlayer)),
                new PlayerTestModel("Simple Player test", typeof(SimplePlayerPage), MediaSource.FromUri(new System.Uri("http://10.113.111.170/~abyss/d.mp4"))),
                new PlayerTestModel("Simple Player test2", typeof(SimplePlayerPage), MediaSource.FromUri(new System.Uri("http://www.html5videoplayer.net/videos/madagascar3.mp4"))),
                new PlayerTestModel("Overlay page test", typeof(TestOverlayPage), MediaSource.FromFile("c.mp4")),
                new PlayerTestModel("Overlay page test with code", typeof(TestOverlayPage2), MediaSource.FromFile("c.mp4")),
                new PlayerTestModel("Overlay view test", typeof(TestOverlayView), MediaSource.FromFile("c.mp4")),
                new PlayerTestModel("Media view test", typeof(TestMediaView), MediaSource.FromFile("d.mp4")),
                new PlayerTestModel("Aspect test", typeof(TestAspect), MediaSource.FromFile("d.mp4")),
                new PlayerTestModel("Url test(slow)", typeof(TestOverlayPage), MediaSource.FromUri(new System.Uri("http://www.html5videoplayer.net/videos/toystory.mp4"))),
                new PlayerTestModel("Url test(fast)", typeof(TestOverlayPage), MediaSource.FromUri(new System.Uri("http://10.113.111.170/~abyss/d.mp4")))
            };
        }

        public List<PlayerTestModel> TestList { get; set; }
    }
}
