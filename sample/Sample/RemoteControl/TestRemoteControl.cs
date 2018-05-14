using System;
using System.Collections.Generic;
using System.Text;

using Xamarin.Forms;
using Tizen.TV.UIControls.Forms;

namespace Sample
{
    public class TestRemoteControl : ContentPage
    {
        public TestRemoteControl()
        {
            Button button1 = new Button { Text = "Button1" };
            RemoteKeyHandler buttonHandler = new RemoteKeyHandler(new Action<RemoteControlKeyEventArgs>((arg) =>
            {
                Console.WriteLine(" Control => arg.KeyType : {0} , arg.KeyName : {1}", arg.KeyType, arg.KeyName);
            }));
            InputEvents.GetEventHandlers(button1).Add(buttonHandler);


            Button button2 = new Button { Text = "Button2" };
            button2.Clicked += (s, e) =>
            {
                Console.WriteLine(" Button2 is Clicked !");
            };
            InputEvents.SetAccessKey(button2, RemoteControlKeyNames.NUM1);

            Title = "Page1";
            Content = new StackLayout
            {
                Children = {
                            new Label { Text = "Welcome to Xamarin.Forms!" },
                            button1,
                            button2
                        }

            };

            RemoteKeyHandler PageHandler = new RemoteKeyHandler(new Action<RemoteControlKeyEventArgs>((arg) =>
            {
                Console.WriteLine("Page1 => arg.KeyType : {0} , arg.KeyName : {1}", arg.KeyType, arg.KeyName);
            }));
            InputEvents.GetEventHandlers(this).Add(PageHandler);
        }
    }
}
