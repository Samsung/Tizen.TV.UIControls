using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Sample.RecycleItemsView
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ImageCellTest : ContentPage
    {
        public ImageCellTest ()
        {
            InitializeComponent ();
        }
        void OnSelected(object sender, EventArgs args)
        {
            var text = ((sender as Tizen.TV.UIControls.Forms.RecycleItemsView).SelectedItem as PosterModel).Text;
            System.Console.WriteLine("{0} is selected", text);
            this.DisplayAlert("Selected", text, "OK");
        }
    }
}