using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Xaml;

namespace Sample
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MainPage : ContentPage
	{
		public MainPage ()
		{
			InitializeComponent();
            BindingContext = new MainPageModel();
		}

        async void ItemTapped(object sender, ItemTappedEventArgs args)
        {
            TestCategory model = (TestCategory)args.Item;
            Page page = (Page)Activator.CreateInstance(model.PageType);
            await Navigation.PushAsync(page);
        }
    }
}