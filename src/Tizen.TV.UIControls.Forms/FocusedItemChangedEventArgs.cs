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

namespace Tizen.TV.UIControls.Forms
{
    /// <summary>
    /// Event arguments for the ItemFocused event.
    /// </summary>
    public class FocusedItemChangedEventArgs : EventArgs
    {

        /// <summary>
        /// Creates a new FocusedItemChangedEventArgs event that indicates that the user has focused.
        /// </summary>
        /// <param name="focusedItem">The item is focused.</param>
        /// <param name="focusedItemIndex">The item's index is focused.</param>
        public FocusedItemChangedEventArgs(object focusedItem, int focusedItemIndex)
        {
            FocusedItem = focusedItem;
            FocusedItemIndex = focusedItemIndex;
        }

        /// <summary>
        /// Gets the new focused item.
        /// </summary>
        public object FocusedItem { get; private set; }

        /// <summary>
        /// Gets the new focused item's index.
        /// </summary>
        public int FocusedItemIndex { get; private set; }
    }
}
