﻿/*
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
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Xaml;
using Microsoft.Maui.Graphics;

namespace Sample.GridView
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DataTemplateTest2 : ContentPage
    {
        DataTemplate Old = null;
        bool IsOld = true;

        public DataTemplateTest2()
        {
            InitializeComponent();

        }

        void GridView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var recycleView = sender as Tizen.Theme.Common.GridView;
            if (recycleView.SelectedItem is PosterModel poster)
            {
                myLabel.Text = poster.Text + " is selected";
            }
        }

        void OnClicked(object sender, EventArgs e)
        {
            if (Old == null)
            {
                Old = myGridView.ItemTemplate;
            }

            if (IsOld)
            {
                myGridView.ItemTemplate = new DataTemplate(() =>
                {
                    return new StackLayout
                    {
                        BackgroundColor = Colors.LightBlue,
                        Children =
                        {
                            new Label(){ Text = "Text 1", TextColor = Colors.Purple },
                            new Button(){ Text = "Button 1",TextColor = Colors.Green, VerticalOptions = LayoutOptions.FillAndExpand }
                        }
                    };
                });
            }
            else 
            {
                myGridView.ItemTemplate = Old;
            }
            IsOld = !IsOld;


        }
    }
}