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

using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Xaml;
namespace Sample.RecycleItemsView
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class FocusChainTest : ContentPage
	{
		public FocusChainTest ()
		{
			InitializeComponent ();
		}

        protected override void OnAppearing()
        {
            // TODO : specific
            //Xamarin.Forms.PlatformConfiguration.TizenSpecific.VisualElement.SetNextFocusLeftView(ItemsView1, ItemsView4);
            //Xamarin.Forms.PlatformConfiguration.TizenSpecific.VisualElement.SetNextFocusLeftView(ItemsView2, ItemsView1);
            //Xamarin.Forms.PlatformConfiguration.TizenSpecific.VisualElement.SetNextFocusLeftView(ItemsView3, ItemsView2);
            //Xamarin.Forms.PlatformConfiguration.TizenSpecific.VisualElement.SetNextFocusLeftView(ItemsView4, ItemsView3);

            //Xamarin.Forms.PlatformConfiguration.TizenSpecific.VisualElement.SetNextFocusRightView(ItemsView1, ItemsView2);
            //Xamarin.Forms.PlatformConfiguration.TizenSpecific.VisualElement.SetNextFocusRightView(ItemsView2, ItemsView3);
            //Xamarin.Forms.PlatformConfiguration.TizenSpecific.VisualElement.SetNextFocusRightView(ItemsView3, ItemsView4);
            //Xamarin.Forms.PlatformConfiguration.TizenSpecific.VisualElement.SetNextFocusRightView(ItemsView4, ItemsView1);
        }
    }
}