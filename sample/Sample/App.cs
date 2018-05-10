using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace Sample
{
    public class App : Application
    {
        public App()
        {
            var navi = new NavigationPage();
            navi.PushAsync(new MainPage());
            MainPage = navi;
        }
    }
}
