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
using Tizen.Theme.Common;
using Microsoft.Maui.Controls;

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
                new AudioItem { Path = "https://developer.samsung.com/onlinedocs/tv/SmartView/sample/audio/Beverly_-_01_-_You_Said_It.mp3", Text = "You said it"},
                new AudioItem { Path = "https://developer.samsung.com/onlinedocs/tv/SmartView/sample/audio/Every_Now_and_Every_Then_-_08_-_Bergwald_Bergwald.mp3", Text = "Bergwald Bergwald"},
                new AudioItem { Path = "https://developer.samsung.com/onlinedocs/tv/SmartView/sample/audio/Ketsa_-_11_-_Retake.mp3", Text = "Retake"},
            };
        }
    }
    public class PlayerMainPageModel
    {
        public PlayerMainPageModel()
        {
            TestList = new List<PlayerTestModel>
            {
                new PlayerTestModel("Embedding control Page test", typeof(TestEmbeddingControlOnPage), MediaSource.FromFile("tvcm.mp4")),
                new AudioPlayerTestModel("Audio player test", typeof(TestAudioPlayer)),
                new PlayerTestModel("Simple Player test", typeof(SimplePlayerPage), MediaSource.FromFile("gear-sport.mp4")),
                new PlayerTestModel("Simple Player test2", typeof(SimplePlayerPage2), MediaSource.FromFile("tvcm.mp4")),
                new PlayerTestModel("Overlay page test", typeof(TestOverlayPage), MediaSource.FromFile("tvcm.mp4")),
                new PlayerTestModel("Overlay page test with code", typeof(TestOverlayPage2), MediaSource.FromFile("tvcm.mp4")),
                new PlayerTestModel("Overlay view test", typeof(TestOverlayView), MediaSource.FromFile("iu.mp4")),
                new PlayerTestModel("Aspect test", typeof(TestAspect), MediaSource.FromFile("pixel2-cf.mp4")),
                new PlayerTestModel("Url test", typeof(TestOverlayPage), MediaSource.FromUri(new System.Uri("http://download.blender.org/demo/movies/caminandes_gran_dillama.mp4"))),
            };

            if (Device.Idiom == TargetIdiom.TV)
            {
                TestList.Add(new PlayerTestModel("Overlay page test with code(no DRM)", typeof(TestOverlayPage3), MediaSource.FromFile("tvcm.mp4")));
                TestList.Add(new PlayerTestModel("Overlay page test with code(DRM)", typeof(TestOverlayPage4), MediaSource.FromFile("tvcm.mp4")));
                TestList.Add(new PlayerTestModel("Overlay page test with code(no DRM after DRM)", typeof(TestOverlayPage5), MediaSource.FromFile("tvcm.mp4")));
            }
        }

        public List<PlayerTestModel> TestList { get; set; }
    }
}
