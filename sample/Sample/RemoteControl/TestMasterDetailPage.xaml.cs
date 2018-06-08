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
	public partial class TestMasterDetailPage : MasterDetailPage
	{
        public Command<RemoteControlKeyEventArgs> masterDetailPageHandler
        {
            get
            {
                return new Command<RemoteControlKeyEventArgs>((arg) => Console.WriteLine("MasterDetailPage => arg.KeyType : {0} , arg.KeyName : {1}", arg.KeyType, arg.KeyName));
            }
        }

        public Command<RemoteControlKeyEventArgs> masterPageHandler
        {
            get
            {
                return new Command<RemoteControlKeyEventArgs>((arg) => Console.WriteLine("Master Page => arg.KeyType : {0} , arg.KeyName : {1}", arg.KeyType, arg.KeyName));
            }
        }

        public Command<RemoteControlKeyEventArgs> detailPageHandler
        {
            get
            {
                return new Command<RemoteControlKeyEventArgs>((arg) => Console.WriteLine("Detail Page => arg.KeyType : {0} , arg.KeyName : {1}", arg.KeyType, arg.KeyName));
            }
        }

        public TestMasterDetailPage ()
		{
			InitializeComponent ();
		}
    }
}