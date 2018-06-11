using Xamarin.Forms;
using Tizen.TV.UIControls.Forms;

namespace Sample
{
    public class TestNavigationPage : NavigationPage
    {
        public TestNavigationPage()
        {
            SetHasNavigationBar(this, false);

            InputEvents.GetEventHandlers(this).Add(new RemoteKeyHandler((args) =>
            {
                if (args.KeyName == RemoteControlKeyNames.Up && args.KeyType == RemoteControlKeyTypes.KeyUp)
                    DisplayAlert("KeyEvent", "Up Pressed on NavigationPage", "ok");
            }));

            var page1 = new ContentPage
            {
                Title = "child page1",
                Content = new StackLayout
                {
                    Children =
                    {
                        new Label { Text = "`Up` is triggering NavigationPage EventHandler" },
                        new Label { Text = "`Down` is triggering child page1 EventHandler" },
                    }

                }
            };

            InputEvents.GetEventHandlers(page1).Add(new RemoteKeyHandler((args) =>
            {
                if (args.KeyName == RemoteControlKeyNames.Down && args.KeyType == RemoteControlKeyTypes.KeyUp)
                    DisplayAlert("KeyEvent", "Down Pressed on child page1", "ok");
            }));

            var page2 = new ContentPage
            {
                Title = "child page2",
                Content = new StackLayout
                {
                    Children =
                    {
                        new Label { Text = "`Up` is triggering NavigationPage EventHandler" },
                        new Label { Text = "`Down` is triggering child page2 EventHandler" },
                    }

                }
            };
            InputEvents.GetEventHandlers(page2).Add(new RemoteKeyHandler((args) =>
            {
                if (args.KeyName == RemoteControlKeyNames.Down && args.KeyType == RemoteControlKeyTypes.KeyUp)
                    DisplayAlert("KeyEvent", "Down Pressed on child page2", "ok");
            }));

            this.PushAsync(page1);
            this.PushAsync(page2);
        }
    }
}
