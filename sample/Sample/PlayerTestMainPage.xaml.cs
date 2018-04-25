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
    public partial class PlayerTestMainPage : ContentPage
    {
        public PlayerTestMainPage ()
        {
            InitializeComponent();
            BindingContext = new MainPageModel();
        }

        async void ItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            System.Console.WriteLine("ItemSelected!! ");
            TestModel model = (TestModel)args.SelectedItem;
            Page page = (Page)Activator.CreateInstance(model.Page);

            System.Console.WriteLine("Page was created");
            page.BindingContext = model;

            System.Console.WriteLine("Data was binding");

            await Navigation.PushAsync(page);

            System.Console.WriteLine("Page was pushed");
        }
    }
}