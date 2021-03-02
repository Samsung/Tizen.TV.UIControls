using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Tizen.Theme.Common;

namespace Sample
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AnimatedNaviMainPage : ContentPage
    {
        public AnimatedNaviMainPage()
        {
            InitializeComponent ();
            BindingContext = new AnimatedNaviPageTestModel();

        }
        async void ItemSelected(object sender, ItemTappedEventArgs args)
        {
            TestModel model = (TestModel)args.Item;
            Page page = (Page)Activator.CreateInstance(model.PageType);
            page.BindingContext = model;
            AnimatedNavigationPage naviPage = new AnimatedNavigationPage(page)
            {
                Title = "Page Transition Test",
                IsPreviousPageVisible = model.IsPreviousVisible
            };
            await Navigation.PushAsync(naviPage);
        }
    }
}