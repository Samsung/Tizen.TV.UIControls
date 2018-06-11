using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Tizen.TV.UIControls.Forms;

namespace Sample
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class TestMasterDetailPage : MasterDetailPage
	{
        public Command<RemoteControlKeyEventArgs> MasterDetailPageHandler
        {
            get
            {
                return new Command<RemoteControlKeyEventArgs>((arg) =>
                {
                    Console.WriteLine("MasterDetailPage => arg.KeyType : {0} , arg.KeyName : {1}", arg.KeyType, arg.KeyName);
                    Title = $"{arg.KeyName} was pressed";
                });
            }
        }

        public Command<RemoteControlKeyEventArgs> MasterPageHandler
        {
            get
            {
                return new Command<RemoteControlKeyEventArgs>((arg) => {
                    Console.WriteLine("Master Page => arg.KeyType : {0} , arg.KeyName : {1}", arg.KeyType, arg.KeyName);
                    MasterLabel.Text = $"{arg.KeyName} was pressed";
                });
            }
        }

        public Command<RemoteControlKeyEventArgs> DetailPageHandler
        {
            get
            {
                return new Command<RemoteControlKeyEventArgs>((arg) =>
                {
                    Console.WriteLine("Detail Page => arg.KeyType : {0} , arg.KeyName : {1}", arg.KeyType, arg.KeyName);
                    DetailLabel.Text = $"{arg.KeyName} was pressed";
                });
            }
        }

        public TestMasterDetailPage ()
		{
			InitializeComponent ();
		}

        void OnClicked(object sender, EventArgs e)
        {
            if (MasterBehavior == MasterBehavior.Popover)
                MasterBehavior = MasterBehavior.Split;
            else
                MasterBehavior = MasterBehavior.Popover;
        }

        void OnShowMaster(object sender, EventArgs e)
        {
            IsPresented = true;
        }
    }
}