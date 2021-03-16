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

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Sample.RecycleItemsView
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ItemFocusedEventTest : ContentPage
    {
        public ItemFocusedEventTest()
        {
            InitializeComponent ();
        }

        void OnFirstItemsItemFocused(object sender, Tizen.TV.UIControls.Forms.FocusedItemChangedEventArgs e)
        {
            var item = e.FocusedItem as ColorModel;
            if(item != null)
            {
                firstItems.BackgroundColor = item.Color;
            }
        }

        void OnSecondItemsItemFocused(object sender, Tizen.TV.UIControls.Forms.FocusedItemChangedEventArgs e)
        {
            itemIndex.Text = e.FocusedItemIndex.ToString();
        }
    }
}
