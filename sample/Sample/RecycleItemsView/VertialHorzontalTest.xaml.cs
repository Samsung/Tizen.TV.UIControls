using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Sample.RecycleItemsView
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class VertialHorzontalTest : ContentPage
	{
		public VertialHorzontalTest ()
		{
			InitializeComponent ();
		}

        private void OnSelected(object sender, SelectedItemChangedEventArgs e)
        {
            DisplayAlert("Selected", (e.SelectedItem as ColorModel).Text, "OK");
        }
    }
}