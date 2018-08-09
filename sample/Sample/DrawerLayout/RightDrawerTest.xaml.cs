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
	public partial class RightDrawerTest : ContentPage
	{
		public RightDrawerTest ()
		{
			InitializeComponent ();
            RightSwitch.IsToggled = true;
            RightSwitch.Toggled += (s, e) =>
            {
                if (RightSwitch.IsToggled)
                    Drawer.DrawerPosition = Tizen.TV.UIControls.Forms.DrawerPosition.Right;
                else
                    Drawer.DrawerPosition = Tizen.TV.UIControls.Forms.DrawerPosition.Left;
            };
            Drawer.DrawerOpened += (s, e) => { System.Console.WriteLine("Drawer is opened"); };
            Drawer.DrawerClosed += (s, e) => { System.Console.WriteLine("Drawer is closed"); };
        }
	}
}