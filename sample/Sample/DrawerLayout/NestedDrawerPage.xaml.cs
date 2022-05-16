using System;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Xaml;

namespace Sample.DrawerLayout
{
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