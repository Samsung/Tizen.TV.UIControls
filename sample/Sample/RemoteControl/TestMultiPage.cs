using System;
using System.Collections.Generic;
using System.Text;

using Xamarin.Forms;
using Tizen.TV.UIControls.Forms;

namespace Sample
{
    public class TestNavigationPage : NavigationPage
    {
        public TestNavigationPage()
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
                Console.WriteLine("Button2 is Clicked !");
            };
            InputEvents.SetAccessKey(button2, RemoteControlKeyNames.NUM1);

            Entry entry = new Entry { Placeholder = "This is Entry" };
            InputEvents.SetAccessKey(entry, RemoteControlKeyNames.NUM2);

            var page1 = new ContentPage
            {
                Title = "Page1",
                Content = new StackLayout
                {
                    Children = {
                                new Label { Text = "Welcome to Xamarin.Forms!" },
                                button1,
                                button2,
                                entry
                            }

                }
            };

            var page2 = new ContentPage { Title = "nav.page2" };
            InputEvents.GetEventHandlers(page2).Add(new RemoteKeyHandler(
                new Action<RemoteControlKeyEventArgs>(
                (arg) => { Console.WriteLine(" page2 => arg.KeyType : {0} , arg.KeyName : {1}", arg.KeyType, arg.KeyName); }
                )));

            var page3_btn1 = new Button
            {
                Text = "button1"

            };
            InputEvents.GetEventHandlers(page3_btn1).Add(new RemoteKeyHandler(new Action<RemoteControlKeyEventArgs>(
                (arg) => { Console.WriteLine(" page3_btn1 => arg.KeyType : {0} , arg.KeyName : {1}", arg.KeyType, arg.KeyName); })));
            var page3 = new ContentPage {
                Title = "nav.page3",
                Content = new StackLayout
                {
                    Children =
                    {
                        page3_btn1
                    }
                }
            };
            InputEvents.GetEventHandlers(page3).Add(new RemoteKeyHandler(new Action<RemoteControlKeyEventArgs>(
                (arg) => { Console.WriteLine(" page3 => arg.KeyType : {0} , arg.KeyName : {1}", arg.KeyType, arg.KeyName); })));

            this.Title = "Navigation Page";
            this.PushAsync(page1);
            this.PushAsync(page2);
            this.PushAsync(page3);

            RemoteKeyHandler PageHandler = new RemoteKeyHandler(new Action<RemoteControlKeyEventArgs>((arg) =>
            {
                Console.WriteLine("Navigation Page => arg.KeyType : {0} , arg.KeyName : {1}", arg.KeyType, arg.KeyName);
            }));
            InputEvents.GetEventHandlers(this).Add(PageHandler);

        }
    }
}
