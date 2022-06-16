using System;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Xaml;

namespace Sample.DrawerLayout
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DrawerMainPage : ContentPage
    {
        public DrawerMainPage ()
        {
            InitializeComponent ();
            BindingContext = new DrawerTestModel();

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