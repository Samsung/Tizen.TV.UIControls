using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Sample
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PlayerMainPage : ContentPage
    {
        public PlayerMainPage()
        {
            InitializeComponent();
            BindingContext = new PlayerMainPageModel();
        }

        async void ItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            PlayerTestModel model = (PlayerTestModel)args.SelectedItem;
            Page page = (Page)Activator.CreateInstance(model.Page);
            page.BindingContext = model;
            await Navigation.PushAsync(page);
        }
    }
}
