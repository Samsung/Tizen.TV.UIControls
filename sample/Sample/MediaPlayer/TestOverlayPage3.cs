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

using Microsoft.Maui.Controls;
using Tizen.Theme.Common;

namespace Sample
{
    public class TestOverlayPage3 : OverlayPage
    {
        public TestOverlayPage3()
        {
            Label label = null;
            Content = new StackLayout
            {
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
            var source = "https://bitdash-a.akamaihd.net/content/sintel/sintel.mpd";
            var player = new MediaPlayer
            {
                IsLooping = true,
                Source = source,
                VideoOutput = this,
                AutoPlay = true,
                UsesEmbeddingControls = false,
            };

            label.SetBinding(Label.TextProperty, new Binding("Duration", source: player, stringFormat: "{0} duration"));

        }
    }
}