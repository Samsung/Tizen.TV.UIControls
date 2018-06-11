using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Tizen.TV.UIControls.Forms;

namespace Sample
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TestRemoteControl_xaml : ContentPage
    {
        public Command<RemoteControlKeyEventArgs> ButtonHandler
        {
            get
            {
                return new Command<RemoteControlKeyEventArgs>((arg) => 
                {
                    Console.WriteLine("Control => arg.KeyType : {0} , arg.KeyName : {1}", arg.KeyType, arg.KeyName);
                    Label1.Text = $"Button1 Keyevent : KeyType {arg.KeyType}, KeyName {arg.KeyName}";
                });
            }
        }

        public Command<RemoteControlKeyEventArgs> PageHandler
        {
            get
            {
                return new Command<RemoteControlKeyEventArgs>((arg) =>
                {
                    Console.WriteLine("Page => arg.KeyType : {0} , arg.KeyName : {1}", arg.KeyType, arg.KeyName);
                    Label2.Text = $"Page Keyevent : KeyType {arg.KeyType}, KeyName {arg.KeyName}";
                });
            }
        }

        public TestRemoteControl_xaml ()
        {
            InitializeComponent ();
        }

        void OnClicked(object sender, EventArgs e)
        {
            this.DisplayAlert("alert", "Button2 clicked", "ok");
        }
    }
}