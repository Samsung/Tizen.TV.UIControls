using System;
using Tizen.TV.UIControls.Forms;
using Xamarin.Forms;

namespace Sample
{
    public class AnimatedNaviPageBasicTest : ContentPage
    {
        private int _depth;
        public AnimatedNaviPageBasicTest() : this(1)
        {
        }

        public AnimatedNaviPageBasicTest(int depth)
        {
            BackgroundColor = depth % 2 == 0 ? Color.LightGray : Color.White;

            // Left-Right
            var pushAnim = new Animation {
                { 0, 1, new Animation (v => TranslationX = (1 - v) * this.Width, 0, 1) },
                { 0, 0.5, new Animation (v => Opacity = v, 0.3, 1) },
            };

            var popAnim = new Animation {
                { 0, 1, new Animation (v => TranslationX = (1 - v) * this.Width, 1, 0) },
                { 0.5, 1, new Animation (v => Opacity = v, 1, 0.3) },
            };

            this.SetPushAnimation(pushAnim);
            this.SetPopAnimation(popAnim);


            var primaryItem = new ToolbarItem
            {
                Text = "P:"+ depth,
                Order = ToolbarItemOrder.Primary,
            };

            var secondaryItem = new ToolbarItem
            {
                Text = "S:" + depth,
                Order = ToolbarItemOrder.Secondary,
            };

            ToolbarItems.Add(primaryItem);
            ToolbarItems.Add(secondaryItem);

            _depth = depth;
            var pushBtn = new Button
            {
                Text = "Push",
                HorizontalOptions = LayoutOptions.FillAndExpand,
            };

            var popBtn = new Button
            {
                Text = "Pop",
                HorizontalOptions = LayoutOptions.FillAndExpand,
            };

            var PopAndPush = new Button
            {
                Text = "Pop and push",
                HorizontalOptions = LayoutOptions.FillAndExpand,
            };

            var poppop = new Button
            {
                Text = "Pop and pop",
                HorizontalOptions = LayoutOptions.FillAndExpand,
            };

            var pushAndPop = new Button
            {
                Text = "Push and pop",
                HorizontalOptions = LayoutOptions.FillAndExpand,
            };
            var pushpush = new Button
            {
                Text = "Push and push",
                HorizontalOptions = LayoutOptions.FillAndExpand,
            };
            var popToRoot = new Button
            {
                Text = "Pop to Root",
                HorizontalOptions = LayoutOptions.FillAndExpand,
            };

            pushBtn.Clicked += async (s, e) =>
            {
                await Navigation.PushAsync(new AnimatedNaviPageBasicTest(_depth + 1));
            };
            popBtn.Clicked += async (s, e) =>
            {
                if (Navigation.NavigationStack.Count > 1)
                {
                    await Navigation.PopAsync();
                }
            };

            PopAndPush.Clicked += async (s, e) =>
            {
                await Navigation.PopAsync();
                await Navigation.PushAsync(new AnimatedNaviPageBasicTest(_depth + 1));
            };

            poppop.Clicked += async (s, e) =>
            {
                await Navigation.PopAsync();
                Console.WriteLine("Pop");
                await Navigation.PopAsync();
            };

            pushAndPop.Clicked += async (s, e) =>
            {
                await Navigation.PushAsync(new AnimatedNaviPageBasicTest(_depth + 1));
                await Navigation.PopAsync();
            };
            pushpush.Clicked += async (s, e) =>
            {
                await Navigation.PushAsync(new AnimatedNaviPageBasicTest(_depth + 1));
                await Navigation.PushAsync(new AnimatedNaviPageBasicTest(_depth + 2));
            };
            popToRoot.Clicked += async (s, e) =>
            {
                await Navigation.PopToRootAsync();
            };

            // Build the page.
            Title = "Page " + _depth;
            StackLayout layout = new StackLayout
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Orientation = StackOrientation.Vertical,
                Spacing = 10,
                Children =
                {
                    new Image
                    {
                        HeightRequest = 1000,
                        WidthRequest = 700,
                        Source = _depth % 2 == 0 ? "poster/01 Jaws.jpg" : "poster/02 Raiders of the Lost Ark.jpg"
                    },
                    new Label
                    {
                        Text = "Depth : " + _depth,
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        VerticalOptions = LayoutOptions.FillAndExpand,
                    },
                    pushBtn,
                    popBtn,
                    PopAndPush,
                    poppop,
                    pushAndPop,
                    pushpush,
                    popToRoot
                }
            };

            this.Content = layout;
        }
    }
}