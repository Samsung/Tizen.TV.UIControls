using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Sample.RecycleItemsView;

namespace Sample
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RIVMainPage : ContentPage
    {
        public RIVMainPage ()
        {
            InitializeComponent();
            BindingContext = new RecycleItemsView.MainPageModel();
        }

        async void ItemSelected(object sender, ItemTappedEventArgs args)
        {
            TestModel model = (TestModel)args.Item;
            Page page = (Page)Activator.CreateInstance(model.PageType);
            page.BindingContext = model;
            await Navigation.PushAsync(page);
        }
    }
}