using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Tizen.TV.UIControls.Forms;
namespace Sample
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class TestAudioPlayer : ContentPage
	{
        MediaPlayer player;
		public TestAudioPlayer ()
		{
			InitializeComponent ();
            player = new MediaPlayer();
            PlayerArea.BindingContext = player;
		}

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            player.Stop();
            player.Source = (e.SelectedItem as AudioItem).Path;
            await player.Start();
            albumArts.Source = new StreamImageSource
            {
                Stream = (token) =>
                {
                    return player.GetAlbumArts();
                }
            };

            var metadata = await player.GetMetadata();
            string info = "";
            foreach (var item in metadata)
            {
                info += $"{item.Key} : {item.Value}\n";
            }
            metaDataLabels.Text = info;
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            player.Stop();
        }
    }
}