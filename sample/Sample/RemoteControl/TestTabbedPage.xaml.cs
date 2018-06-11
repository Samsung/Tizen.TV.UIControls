using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Tizen.TV.UIControls.Forms;

namespace Sample
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class TestTabbedPage : TabbedPage
	{
        public Command TabbedPageHandler_Tabbed
        {
            get
            {
                return new Command<RemoteControlKeyEventArgs>((arg) =>
                {
                    Console.WriteLine("TabbedPage => arg.KeyType : {0} , arg.KeyName : {1}", arg.KeyType, arg.KeyName);
                    if (arg.KeyType == RemoteControlKeyTypes.KeyUp)
                    {
                        if (arg.KeyName == RemoteControlKeyNames.NUM1)
                        {
                            CurrentPage = Page1;
                        }
                        else if (arg.KeyName == RemoteControlKeyNames.NUM2)
                        {
                            CurrentPage = Page2;
                        }
                        else if (arg.KeyName == RemoteControlKeyNames.NUM3)
                        {
                            CurrentPage = Page3;
                        }
                    }
                });
            }
        }

        public Command PageHandler1_Tabbed
        {
            get
            {
                return new Command<RemoteControlKeyEventArgs>((arg) =>
                {
                    Console.WriteLine("ContentPage => arg.KeyType : {0} , arg.KeyName : {1}", arg.KeyType, arg.KeyName);
                    Label1.Text = $"{arg.KeyName} was pressed";
                });
            }
        }

        public Command PageHandler2_Tabbed
        {
            get
            {
                return new Command<RemoteControlKeyEventArgs>((arg) =>
                {
                    Console.WriteLine("ContentPage => arg.KeyType : {0} , arg.KeyName : {1}", arg.KeyType, arg.KeyName);
                    Label2.Text = $"{arg.KeyName} was pressed";
                });
            }
        }

        public Command PageHandler3_Tabbed
        {
            get
            {
                return new Command<RemoteControlKeyEventArgs>((arg) =>
                {
                    Console.WriteLine("ContentPage => arg.KeyType : {0} , arg.KeyName : {1}", arg.KeyType, arg.KeyName);
                    Label3.Text = $"{arg.KeyName} was pressed";
                });
            }
        }

        public Command ButtonHandler_Tabbed
        {
            get
            {
                return new Command<RemoteControlKeyEventArgs>((arg) =>
                {
                    Console.WriteLine("Control => arg.KeyType : {0} , arg.KeyName : {1}", arg.KeyType, arg.KeyName);
                });
            }
        }

		public TestTabbedPage ()
		{
			InitializeComponent ();
		}
	}
}