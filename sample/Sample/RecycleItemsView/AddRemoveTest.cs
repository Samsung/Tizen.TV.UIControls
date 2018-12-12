using TV = Tizen.TV.UIControls.Forms;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using System;

namespace Sample
{
    class MyRecycleItemsView : TV.RecycleItemsView
    {
        protected override void OnItemFocused(object data, View targetView, bool isFocused)
        {
            if (data == Header || data == Footer)
            {
                if (isFocused)
                {
                    var old = (targetView as StackLayout).BackgroundColor;
                    (targetView as StackLayout).BackgroundColor = Color.FromRgb(Math.Min(1.0, old.R + 0.2), Math.Min(1.0, old.G + 0.2), Math.Min(1.0, old.B + 0.2));
                }
                else
                {
                    (targetView as StackLayout).BackgroundColor = data == Header ? Color.Red : Color.Blue;
                }
                return;
            }
            if (isFocused)
            {
                (targetView as StackLayout).BackgroundColor = Color.CadetBlue;
            }
            else
            {
                (targetView as StackLayout).BackgroundColor = Color.DarkCyan;
            }
        }
    }
    class MyData
    {
        public int Index { get; set; }
    }
    public class AddRemoveTest : ContentPage
    {
        int _currentIndex = 0;
        MyData _lastFocused = null;
        public AddRemoveTest ()
        {

            var itemsview = new MyRecycleItemsView()
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                BackgroundColor = Color.AliceBlue,
                ItemWidth = 200,
            };
            var items = new ObservableCollection<MyData>();

            itemsview.ItemTemplate = new DataTemplate(() =>
            {
                var stack = new StackLayout()
                {
                    BackgroundColor = Color.DarkCyan
                };
                var label = new Label()
                {
                    HorizontalOptions = LayoutOptions.CenterAndExpand,
                    VerticalOptions = LayoutOptions.CenterAndExpand,
                };
                stack.Children.Add(label);

                label.SetBinding(Label.TextProperty, new Binding("Index"));
                return stack;
            });
            itemsview.ItemsSource = items;
            itemsview.ItemSelected += (s, e) =>
            {
                items.Remove(e.SelectedItem as MyData);
            };

            itemsview.HeaderTemplate = new DataTemplate(() =>
            {
                var stack = new StackLayout()
                {
                    WidthRequest = 300,
                    BackgroundColor = Color.Red
                };
                var label = new Label()
                {
                    Text = "Head",
                    HorizontalOptions = LayoutOptions.CenterAndExpand,
                    VerticalOptions = LayoutOptions.CenterAndExpand,
                };
                stack.Children.Add(label);
                return stack;
            });

            itemsview.FooterTemplate = new DataTemplate(() =>
            {
                var stack = new StackLayout()
                {
                    WidthRequest = 300,
                    BackgroundColor = Color.Blue
                };
                var label = new Label()
                {
                    Text = "Footer",
                    HorizontalOptions = LayoutOptions.CenterAndExpand,
                    VerticalOptions = LayoutOptions.CenterAndExpand,
                };
                stack.Children.Add(label);
                return stack;
            });

            var add = new Button()
            {
                Text = "Add"
            };
            itemsview.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == "FocusedItem" && itemsview.FocusedItem != null)
                {
                    _lastFocused = itemsview.FocusedItem as MyData;
                }
            };

            add.Clicked += (s, e) =>
            {
                var item = new MyData
                {
                    Index = _currentIndex++
                };
                if (_lastFocused != null)
                {
                    var index = items.IndexOf(_lastFocused);
                    if (index != -1)
                        items.Insert(index, item);
                    else
                        items.Add(item);
                }
                else
                {
                    items.Add(item);
                }

            };

            var replace = new Button { Text = "Replace first" };
            replace.Clicked += (s, e) =>
            {
                try
                {
                    items[0] = new MyData() { Index = _currentIndex++ };
                }
                catch { }
            };

            var move = new Button { Text = "Move first and second" };
            move.Clicked += (s, e) =>
            {
                try
                {
                    items.Move(0, 1);
                }
                catch { }
            };

            var reset = new Button { Text = "Reset" };
            reset.Clicked += (s, e) =>
            {
                items.Clear();
            };

            var source = new Button { Text = "SourceChange" };
            source.Clicked += (s, e) =>
            {
                var newsource = new ObservableCollection<MyData>();
                for (int i = 0; i < 10; i++)
                {
                    newsource.Add(new MyData { Index = _currentIndex++ });
                }
                items = newsource;
                itemsview.ItemsSource = items;
            };

            var enableHeader = new Switch()
            {
                IsToggled = false
            };

            var enableFooter = new Switch()
            {
                IsToggled = false
            };

            enableHeader.Toggled += (s, e) =>
            {
                System.Console.WriteLine($"Update header toggle {e.Value}");
                if (e.Value)
                    itemsview.Header = "header";
                else
                    itemsview.Header = null;
            };

            enableFooter.Toggled += (s, e) =>
            {
                System.Console.WriteLine($"Update footer toggle {e.Value}");
                if (e.Value)
                    itemsview.Footer = "footer";
                else
                    itemsview.Footer = null;
            };

            var enablefocusHeader = new Switch()
            {
                IsToggled = true
            };
            enablefocusHeader.Toggled += (s, e) => itemsview.AllowFocusHeader = e.Value;

            var enablefocusFooter = new Switch()
            {
                IsToggled = true
            };
            enablefocusFooter.Toggled += (s, e) => itemsview.AllowFocusFooter = e.Value;


            Content = new StackLayout {
                Children = {
                    itemsview,
                    add,
                    replace,
                    move,
                    reset,
                    source,
                    new StackLayout { Orientation = StackOrientation.Horizontal, Children = {new Label { Text = "Header : "}, enableHeader }},
                    new StackLayout { Orientation = StackOrientation.Horizontal, Children = {new Label { Text = "Footer : "}, enableFooter }},
                    new StackLayout { Orientation = StackOrientation.Horizontal, Children = {new Label { Text = "FocusableOnHeader : "}, enablefocusHeader }},
                    new StackLayout { Orientation = StackOrientation.Horizontal, Children = {new Label { Text = "FocusableOnFooter : " }, enablefocusFooter }},
                }
            };
        }
    }
}