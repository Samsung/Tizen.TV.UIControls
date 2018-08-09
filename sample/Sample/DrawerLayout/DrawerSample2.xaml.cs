using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Sample.DrawerLayout
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class DrawerSample2 : ContentPage
	{
		public DrawerSample2 ()
		{
			InitializeComponent();
            var list = new List<string>();
            for (int i = 0; i < 100; i++)
            {
                list.Add($"{i + 1} item");
            }
            ListView.ItemsSource = list;
		}

        private void MenuList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Drawer.IsOpen = false;
        }

        private void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            Drawer.IsOpen = true;
        }

        private void Panning_Clicked(object sender, EventArgs e)
        {
            Drawer.DrawerMode = Tizen.TV.UIControls.Forms.DrawerMode.Panning;
        }

        private void Resize_Clicked(object sender, EventArgs e)
        {
            Drawer.DrawerMode = Tizen.TV.UIControls.Forms.DrawerMode.Resize;
        }

        private void Overlap_Clicked(object sender, EventArgs e)
        {
            Drawer.DrawerMode = Tizen.TV.UIControls.Forms.DrawerMode.Overlap;
        }
    }
}