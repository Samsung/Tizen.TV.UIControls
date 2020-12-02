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

namespace Tizen.TV.UIControls.Forms
{
    /// <summary>
    /// A Page that manages the navigation with custom animation and user-experience of a stack of other pages.
    /// </summary>
    public class AnimatedNavigationPage : NavigationPage
    {
        /// <summary>
        /// Creates and initializes a new instance of the AnimatedNavigationPage class.
        /// </summary>
        public AnimatedNavigationPage() : base()
        {
            SetHasNavigationBar(this, false);
        }

        /// <summary>
        /// Creates and initializes a new instance of the AnimatedNavigationPage class.
        /// </summary>
        /// <param name="root">The root page</param>
        public AnimatedNavigationPage(Page root) : base(root)
        {
            SetHasNavigationBar(this, false);
        }
    }
}
