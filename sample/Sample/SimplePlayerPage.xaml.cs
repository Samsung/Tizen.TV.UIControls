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
        async void OnSeekChanged(object sender, ValueChangedEventArgs e)
        {
            await Player.Seek((int)(Player.Duration * e.NewValue));
        }
    }
}