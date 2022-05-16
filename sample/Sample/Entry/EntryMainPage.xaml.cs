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
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Xaml;

namespace Sample
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class EntryMainPage : ContentPage
	{
		public EntryMainPage()
		{
			InitializeComponent ();
            BindingContext = new EntryMainPageModel();
        }

        async void ItemSelected(object sender, ItemTappedEventArgs args)
        {
            EntryTestModel model = (EntryTestModel)args.Item;
            Page page = (Page)Activator.CreateInstance(model.Page);
            page.BindingContext = model;
            await Navigation.PushAsync(page);
        }
    }
}