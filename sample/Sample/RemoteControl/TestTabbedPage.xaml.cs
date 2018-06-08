using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tizen.TV.UIControls.Forms;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Sample
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class TestTabbedPage : TabbedPage
	{
        public Command tabbedPageHandler_Tabbed
        {
            get
            {
                return new Command((arg) => Console.WriteLine("TabbedPage => arg.KeyType : {0} , arg.KeyName : {1}", (arg as RemoteControlKeyEventArgs).KeyType, (arg as RemoteControlKeyEventArgs).KeyName));
            }
        }

        public Command pageHandler_Tabbed
        {
            get
            {
                return new Command((arg) => Console.WriteLine("ContentPage => arg.KeyType : {0} , arg.KeyName : {1}", (arg as RemoteControlKeyEventArgs).KeyType, (arg as RemoteControlKeyEventArgs).KeyName));
            }
        }

        public Command buttonHandler_Tabbed
        {
            get
            {
                return new Command((arg) => Console.WriteLine("Control => arg.KeyType : {0} , arg.KeyName : {1}", (arg as RemoteControlKeyEventArgs).KeyType, (arg as RemoteControlKeyEventArgs).KeyName));
            }
        }

		public TestTabbedPage ()
		{
			InitializeComponent ();
		}
	}
}