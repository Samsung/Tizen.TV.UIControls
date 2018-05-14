using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Tizen.TV.UIControls.Forms;

namespace Sample
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class TestRemoteControl_xaml : ContentPage
	{
        public Command<RemoteControlKeyEventArgs> buttonHandler
        {
            get
            {
                return new Command<RemoteControlKeyEventArgs>((arg) => Console.WriteLine("Control => arg.KeyType : {0} , arg.KeyName : {1}", arg.KeyType, arg.KeyName));
            }
        }

        public TestRemoteControl_xaml ()
		{
			InitializeComponent ();
		}
	}
}