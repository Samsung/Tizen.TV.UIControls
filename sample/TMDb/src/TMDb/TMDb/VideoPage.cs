using Tizen.TV.UIControls.Forms;
using Xamarin.Forms;

namespace TMDb
{
    public class VideoPage : OverlayPage
    {
        public VideoPage(string id)
        {
            NavigationPage.SetHasNavigationBar(this, false);
            Player = new MediaPlayer()
            {
                AutoPlay = true,
                Source = $"https://github.sec.samsung.net/pages/sngnlee/youtubedata/{id}.mp4"
            };
            Player.PlaybackCompleted += (s, e) =>
            {
                Navigation.PopAsync();
            };
        }
    }
}