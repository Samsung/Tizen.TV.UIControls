using System;
using Xamarin.Forms;
using Tizen.TV.UIControls.Forms;

namespace Sample
{
    public class TestRemoteControl : ContentPage
    {
        int _clickedTimes = 0;
        public TestRemoteControl()
        {
            Button button1 = new Button { Text = "Button1" };
            Switch toggle = new Switch();

            RemoteKeyHandler buttonHandler = new RemoteKeyHandler(new Action<RemoteControlKeyEventArgs>((arg) =>
            {
                Console.WriteLine(" Control => arg.KeyType : {0} , arg.KeyName : {1}", arg.KeyType, arg.KeyName);
                button1.Text = $"Button1 : {arg.KeyType} {arg.KeyName}";
                arg.Handled = toggle.IsToggled;
            }));
            InputEvents.GetEventHandlers(button1).Add(buttonHandler);


            Button button2 = new Button { Text = "Button2 (Accesskey 1)" };
            button2.Clicked += (s, e) =>
            {
                button2.Text = $"Button2 (Accesskey 1): {++_clickedTimes} clicked";
            };
            InputEvents.SetAccessKey(button2, RemoteControlKeyNames.NUM1);

            SetBinding(TitleProperty, new Binding("Name"));


            Label label;
            Content = new StackLayout
            {
                Children =
                {
                    new StackLayout {
                        Orientation = StackOrientation.Horizontal,
                        Children = { toggle, new Label { Text = "Consume event" } }
                    },
                    button1,
                    button2,
                    (label = new Label())
                }
            };

            RemoteKeyHandler PageHandler = new RemoteKeyHandler(new Action<RemoteControlKeyEventArgs>((arg) =>
            {
                Console.WriteLine("Page1 => arg.KeyType : {0} , arg.KeyName : {1}", arg.KeyType, arg.KeyName);
                label.Text = $"Page Key event : KeyType {arg.KeyType}, KeyName {arg.KeyName}";
            }));
            InputEvents.GetEventHandlers(this).Add(PageHandler);
        }
    }
}
