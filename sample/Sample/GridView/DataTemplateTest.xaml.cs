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
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Sample.GridView
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DataTemplateTest : ContentPage
    {
        public DataTemplateTest()
        {
            InitializeComponent();
        }

        void GridView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Tizen.TV.UIControls.Forms.GridView recycleView = sender as Tizen.TV.UIControls.Forms.GridView;
            if (recycleView.SelectedItem is PosterModel poster)
            {
                myLabel.Text = e.SelectedItemIndex+ "th "+poster.Text + " is selected";
            }
        }
    }
}