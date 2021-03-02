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
using Xamarin.Forms;
using Tizen.TV.UIControls.Forms;
using OverlayPage = Tizen.Theme.Common.OverlayPage;
using MediaPlayer = Tizen.Theme.Common.MediaPlayer;

namespace Sample
{
    public class TestOverlayPage4 : OverlayPage
    {
        public TestOverlayPage4 ()
        {
            Label label = null;
            Content = new StackLayout {
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center,
                Children = {
                    (label = new Label
                    {
                        Text = "Content area",
                        VerticalOptions = LayoutOptions.Center,
                        HorizontalOptions = LayoutOptions.Center,
                    })
                }
            };

            var myUri = new Uri("http://profficialsite.origin.mediaservices.windows.net/c51358ea-9a5e-4322-8951-897d640fdfd7/tearsofsteel_4k.ism/manifest(format=mpd-time-csf)");
            var DRMSource = new DRMMediaSource
            {
                Uri = myUri,
                LicenseUrl = "http://playready-testserver.azurewebsites.net/rightsmanager.asmx?PlayRight=1&UseSimpleNonPersistentLicense=1",
            };

            var player = new MediaPlayer
            {
                IsLooping = true,
                Source = DRMSource,
                VideoOutput = this,
                AutoPlay = true,
                UsesEmbeddingControls = false,
            };
            label.SetBinding(Label.TextProperty, new Binding("Duration", source: player, stringFormat:"{0} duration"));
        }
    }
}