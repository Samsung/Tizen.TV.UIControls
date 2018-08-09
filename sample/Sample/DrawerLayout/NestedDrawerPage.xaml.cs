using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Sample.DrawerLayout
{
    public class FocusedMenuItemsView : MenuItemsView
    {
        public event EventHandler ItemFocused;
        protected override void OnItemFocused(object data, View targetView, bool isFocused)
        {
            base.OnItemFocused(data, targetView, isFocused);
            if (isFocused)
            {
                ItemFocused?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NestedDrawerPage : ContentPage
    {
        public NestedDrawerPage ()
        {
            InitializeComponent();
            Tizen.TV.UIControls.Forms.InputEvents.GetEventHandlers(MainMenuList).Add(
                new Tizen.TV.UIControls.Forms.RemoteKeyHandler((t) => {
                    if (t.KeyName == Tizen.TV.UIControls.Forms.RemoteControlKeyNames.Right)
                    {
                        SubMenuList.Focus();
                        t.Handled = true;
                    }
                },
                Tizen.TV.UIControls.Forms.RemoteControlKeyTypes.KeyDown));
            Tizen.TV.UIControls.Forms.InputEvents.GetEventHandlers(SubMenuList).Add(
                new Tizen.TV.UIControls.Forms.RemoteKeyHandler((t) => {
                    if (t.KeyName == Tizen.TV.UIControls.Forms.RemoteControlKeyNames.Left)
                    {
                        MainMenuList.Focus();
                        t.Handled = true;
                    }
                },
                Tizen.TV.UIControls.Forms.RemoteControlKeyTypes.KeyDown));
            FocusHolder.Focused += (s, e) =>
            {
                SubDrawer.IsOpen = false;
            };
            FocusHolder.Unfocused += (s, e) =>
            {
                SubDrawer.IsOpen = true;
            };
        }
        protected override void OnAppearing()
        {
            MainMenuList.Focus();
        }

        void MainMenuList_ItemFocused(object sender, EventArgs e)
        {
            SubMenuList.ItemsSource = (MainMenuList.FocusedItem as MenuListModel).Items;
        }

        void SubMenuList_ItemFocused(object sender, EventArgs e)
        {
            TargetImage.Source = (SubMenuList.FocusedItem as MenuModel).Path;
        }
    }
}