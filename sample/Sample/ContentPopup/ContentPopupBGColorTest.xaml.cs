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
using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Xaml;
using Microsoft.Maui.Graphics;
using Tizen.Theme.Common;
using CPopup = Tizen.Theme.Common.ContentPopup;

namespace Sample.ContentPopup
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ContentPopupBGColorTest : ContentPage
    {
        public ContentPopupBGColorTest()
        {
            InitializeComponent();
        }

        void OnPopupButtonClicked(object sender, EventArgs e)
        {
            var popup = new CPopup
            {
                BackgroundColor = Color.FromHex("#CCF0F8FF"),
                Content = new StackLayout
                {
                    Children =
                    {
                        new Label
                        {
                            Text = "This ContentPopup is dismissed as a back key.",
                            HorizontalTextAlignment = TextAlignment.Center,
                        }
                    }
                }
            };

            this.ShowPopup(popup);
        }

        void OnFullscreenPopupButtonClicked(object sender, EventArgs e)
        {
            MyPopup popup = new MyPopup();
            popup.BackgroundColor = Colors.Black;

            var defaultButton = new Button
            {
                Text = "Set BackgroundColor to Default (Transparent)",
                TextColor = Colors.White,
            };
            defaultButton.Clicked += (s, ee) =>
            {
                popup.BackgroundColor = Colors.Transparent;
            };

            var grayButton = new Button
            {
                Text = "Set Background Color to Gray",
                TextColor = Colors.White,
            };
            grayButton.Clicked += (s, ee) =>
            {
                popup.BackgroundColor = Colors.Gray;
            };

            var dismiss = new Button
            {
                Text = "Dismiss",
                TextColor = Colors.White,
            };
            dismiss.Clicked += (s, ee) =>
            {
                popup?.Dismiss();
            };

            var label = new Label
            {
                Text = "This ContentPopup is dismissed as a below dismiss button.",
                TextColor = Colors.White,
                HorizontalTextAlignment = TextAlignment.Center,
            };

            var stackLayout = new StackLayout();

            stackLayout.Children.Add(label);
            stackLayout.Children.Add(defaultButton);
            stackLayout.Children.Add(grayButton);
            stackLayout.Children.Add(dismiss);

            //TODO : Need to fix the issue of Grid
            //var grid = new Grid();
            //grid.RowDefinitions.Add(new RowDefinition());
            //grid.RowDefinitions.Add(new RowDefinition());
            //grid.RowDefinitions.Add(new RowDefinition());
            //grid.RowDefinitions.Add(new RowDefinition());
            //grid.RowDefinitions.Add(new RowDefinition());

            //grid.Children.Add(label);
            //grid.Children.Add(defaultButton);
            //grid.Children.Add(grayButton);
            //grid.Children.Add(dismiss);

            popup.Content = stackLayout;

            this.ShowPopup(popup);
        }
    }
}