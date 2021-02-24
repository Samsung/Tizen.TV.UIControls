using Tizen.TV.UIControls.Forms;
using Xamarin.Forms;

namespace Sample
{
    public class AnimatedContentPageTransitTest : AnimatedContentPage
    {
        PageTransition _pageTransition = PageTransition.SlideFromRight;

        public AnimatedContentPageTransitTest()
        {
            var radio1 = new RadioButton
            {
                Content = "SlideFromRight",
                TextColor = Color.SkyBlue,
                GroupName = "Transit"
            };
            radio1.CheckedChanged += (s, e) =>
            {
                if (e.Value)
                {
                    _pageTransition = PageTransition.SlideFromRight;
                }
            };

            var radio2 = new RadioButton
            {
                Content = "SlideFromLeft",
                TextColor = Color.SkyBlue,
                GroupName = "Transit"
            };
            radio2.CheckedChanged += (s, e) =>
            {
                if (e.Value)
                {
                    _pageTransition = PageTransition.SlideFromLeft;
                }
            };

            var radio3 = new RadioButton
            {
                Content = "SlideFromBottom",
                TextColor = Color.SkyBlue,
                GroupName = "Transit"
            };
            radio3.CheckedChanged += (s, e) =>
            {
                if (e.Value)
                {
                    _pageTransition = PageTransition.SlideFromBottom;
                }
            };

            var radio4 = new RadioButton
            {
                Content = "SlideFromTop",
                TextColor = Color.SkyBlue,
                GroupName = "Transit"
            };
            radio4.CheckedChanged += (s, e) =>
            {
                if (e.Value)
                {
                    _pageTransition = PageTransition.SlideFromTop;
                }
            };

            var radio5 = new RadioButton
            {
                Content = "Fade",
                TextColor = Color.SkyBlue,
                GroupName = "Transit"
            };
            radio5.CheckedChanged += (s, e) =>
            {
                if (e.Value)
                {
                    _pageTransition = PageTransition.Fade;
                }
            };

            var radio6 = new RadioButton
            {
                Content = "Scale",
                TextColor = Color.SkyBlue,
                GroupName = "Transit"
            };
            radio6.CheckedChanged += (s, e) =>
            {
                if (e.Value)
                {
                    _pageTransition = PageTransition.Scale;
                }
            };

            var pushBtn = new Button
            {
                Text = "Push",
                HorizontalOptions = LayoutOptions.FillAndExpand,
            };

            pushBtn.Clicked += async (s, e) =>
            {
                var page = new AnimatedContentPage
                {
                    BackgroundColor = Color.Transparent,
                    PageTranistion = _pageTransition,
                    Content = new StackLayout
                    {
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        VerticalOptions = LayoutOptions.FillAndExpand,
                        Orientation = StackOrientation.Vertical,
                        Spacing = 10,
                        Children =
                        {
                            new Image
                            {
                                HeightRequest = Device.Idiom == TargetIdiom.TV ? 1000 : 300,
                                WidthRequest = Device.Idiom == TargetIdiom.TV ? 700 : 200,
                                Source = "poster/01 Jaws.jpg"
                            },
                        }
                    }
                };
                await Navigation.PushAsync(page);
            };


            // Build the page.
            StackLayout layout = new StackLayout
            {
                IsVisible = true,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Orientation = StackOrientation.Vertical,
                Spacing = 10,
                Children =
                {
                    new Image
                    {
                        HeightRequest = Device.Idiom == TargetIdiom.TV ? 1000 : 300,
                        WidthRequest = Device.Idiom == TargetIdiom.TV ? 700 : 200,
                        Source = "poster/02 Raiders of the Lost Ark.jpg"
                    },
                    new Label
                    {
                        IsVisible = true,
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        VerticalOptions = LayoutOptions.FillAndExpand,
                    },
                    new StackLayout
                    {
                        Children =
                        {
                            new StackLayout
                            {
                                HorizontalOptions = LayoutOptions.CenterAndExpand,
                                Orientation = StackOrientation.Horizontal,
                                Spacing = 10,
                                Children = { new FocusFrame { Content = radio1 } , new Label { Text = "SlideFromRight", VerticalOptions = LayoutOptions.Center  }},
                            },
                            new StackLayout
                            {
                                HorizontalOptions = LayoutOptions.CenterAndExpand,
                                Orientation = StackOrientation.Horizontal,
                                Spacing = 10,
                                Children = { new FocusFrame { Content = radio2 }, new Label { Text = "SlideFromLeft", VerticalOptions = LayoutOptions.Center } },
                            },
                            new StackLayout
                            {
                                HorizontalOptions = LayoutOptions.CenterAndExpand,
                                Orientation = StackOrientation.Horizontal,
                                Spacing = 10,
                                Children = { new FocusFrame { Content = radio3 }, new Label { Text = "SlideFromBottom", VerticalOptions = LayoutOptions.Center } },
                            },
                            new StackLayout
                            {
                                HorizontalOptions = LayoutOptions.CenterAndExpand,
                                Orientation = StackOrientation.Horizontal,
                                Spacing = 10,
                                Children = { new FocusFrame { Content = radio4 }, new Label { Text = "SlideFromTop", VerticalOptions = LayoutOptions.Center } },
                            },
                            new StackLayout
                            {
                                HorizontalOptions = LayoutOptions.CenterAndExpand,
                                Orientation = StackOrientation.Horizontal,
                                Spacing = 10,
                                Children = { new FocusFrame { Content = radio5 }, new Label { Text = "Fade", VerticalOptions = LayoutOptions.Center } },
                            },
                            new StackLayout
                            {
                                HorizontalOptions = LayoutOptions.CenterAndExpand,
                                Orientation = StackOrientation.Horizontal,
                                Spacing = 10,
                                Children = { new FocusFrame { Content = radio6 }, new Label { Text = "Scale", VerticalOptions = LayoutOptions.Center } },
                            },
                        }
                    },
                    pushBtn,
                }
            };

            this.Content = new ScrollView { Content = layout };
        }
    }
}