using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Sample.DrawerLayout
{
    public class MenuItemsView : Tizen.TV.UIControls.Forms.RecycleItemsView
    {
        public MenuItemsView()
        {
            ItemTemplate = new DataTemplate(() =>
            {
                var label = new Label
                {
                    FontAttributes = FontAttributes.Bold,
                    VerticalOptions = LayoutOptions.FillAndExpand,
                    VerticalTextAlignment = TextAlignment.Center
                };
                label.SetBinding(Label.TextProperty, new Binding("Text"));
                return new StackLayout
                {
                    VerticalOptions = LayoutOptions.FillAndExpand,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    Padding = 20,
                    Children =
                    {
                        label,
                    }
                };
            });
        }
        protected override void OnItemFocused(object data, View targetView, bool isFocused)
        {
            StackLayout layout = (StackLayout)targetView;
            Label label = (Label)layout.Children[0];

            if (isFocused)
            {
                layout.BackgroundColor = Color.FromRgb(198, 201, 206);
                label.TextColor = Color.FromRgb(5, 5, 5);
            }
            else
            {
                label.TextColor = Color.FromRgb(198, 201, 206);
                layout.BackgroundColor = Color.Transparent;
            }
        }
    }

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DrawerSample1 : ContentPage
    {
        public DrawerSample1 ()
        {
            InitializeComponent();
        }

        void MenuItemsView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            ImageHolder.Source = (e.SelectedItem as MenuModel).Path;
        }
    }
}