using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Sample
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class RemoteControlMainPage : ContentPage
	{
		public RemoteControlMainPage ()
		{
			InitializeComponent ();
            BindingContext = new RemoteControlMainPageModel();
        }

        async void ItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            RemoteControlTestModel model = (RemoteControlTestModel)args.SelectedItem;
            Page page = (Page)Activator.CreateInstance(model.Page);
            page.BindingContext = model;
            await Navigation.PushAsync(page);
        }
    }
}