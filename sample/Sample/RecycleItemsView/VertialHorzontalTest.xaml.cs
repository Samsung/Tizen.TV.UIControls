/*
 * Copyright (c) 2018 Samsung Electronics Co., Ltd All Rights Reserved
 *
 * Licensed under the Apache License, Version 2.0 (the License);
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an AS IS BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Sample.RecycleItemsView
{

    public class MenuItemsView : Tizen.TV.UIControls.Forms.RecycleItemsView
    {
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
                layout.BackgroundColor = Color.FromRgb(5, 5, 5);
            }
        }
    }

    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class VertialHorzontalTest : ContentPage
	{
        public VertialHorzontalTest ()
        {
            InitializeComponent();
            MenuList.Focused += OnMenuFocused;
            MenuList.Unfocused += OnMenuFocused;

        }

        void OnMenuFocused(object sender, FocusEventArgs e)
        {
            if (e.IsFocused)
            {
                double start = (Sidebar.WidthRequest - 1) / 800.0;
                var animation = new Animation((r) =>
                {
                    Sidebar.WidthRequest = 800 * r;
                }, start, 1, Easing.SpringIn);
                animation.Commit(Sidebar, "Focus", length: (uint)(250 * (1 - start)));
            }
            else
            {
                double start = (800 - Sidebar.WidthRequest) / 800.0;
                var animation = new Animation((r) =>
                {
                    Sidebar.WidthRequest = 800 * (1 - r) + 1;
                }, start, 1, Easing.SpringIn);
                animation.Commit(Sidebar, "Focus", length: (uint)(250 * (1 - start)));
            }
        }

        void OnSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem is ColorModel item)
            {
                DisplayAlert("Selected", item.Text, "OK");
            }
        }
    }
}