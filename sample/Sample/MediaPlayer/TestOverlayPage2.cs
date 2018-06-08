using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;
using Tizen.TV.UIControls.Forms;

namespace Sample
{
    public class TestOverlayPage2 : OverlayPage
    {
        public TestOverlayPage2 ()
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

            var player = new MediaPlayer
            {
                Source = "tvcm.mp4",
                VideoOutput = this,
                AutoPlay = true,
                UsesEmbeddingControls = false,
            };
            label.SetBinding(Label.TextProperty, new Binding("Duration", source: player, stringFormat:"{0} duration"));
        }
    }
}