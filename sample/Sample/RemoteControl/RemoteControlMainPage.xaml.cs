using System;
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

        async void ItemSelected(object sender, ItemTappedEventArgs args)
        {
            RemoteControlTestModel model = (RemoteControlTestModel)args.Item;
            Page page = (Page)Activator.CreateInstance(model.Page);
            page.BindingContext = model;
            await Navigation.PushAsync(page);
        }
    }
}