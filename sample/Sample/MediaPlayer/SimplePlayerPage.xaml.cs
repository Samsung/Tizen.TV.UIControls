using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Tizen.TV.UIControls.Forms;

namespace Sample
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SimplePlayerPage : OverlayPage
    {
		public SimplePlayerPage ()
		{
			InitializeComponent ();
		}

        void OnClickPlay(object sender, ClickedEventArgs e)
        {
            if (Player.State == PlaybackState.Playing)
                Player.Pause();
            else
            {
                var unused = Player.Start();
            }
        }

        void OnClickStop(object sender, ClickedEventArgs e)
        {
            Player.Stop();
        }

        async void OnSeekChanged(object sender, ValueChangedEventArgs e)
        {
            await Player.Seek((int)(Player.Duration * e.NewValue));
        }
    }
}