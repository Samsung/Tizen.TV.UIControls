using System;
using Tizen.TV.UIControls.Forms;
using Xamarin.Forms;

namespace TMDb
{
    public class VideoPage : ContentPage
    {
        public VideoPage(string id)
        {
            NavigationPage.SetHasNavigationBar(this, false);
            var layout = new AbsoluteLayout
            {
                BackgroundColor = Color.Black,
            };
            var view = new OverlayMediaView
            {
                Player = new MediaPlayer()
                {
                    AspectMode = DisplayAspectMode.AspectFit,
                    AutoPlay = true,
                    Source = $"/opt/usr/home/owner/media/Downloads/{id}.mp4"
                },
            };
            layout.Children.Add(view, new Rectangle(0, 0, 1, 1), AbsoluteLayoutFlags.All);
            Content = layout;
        }
    }
}