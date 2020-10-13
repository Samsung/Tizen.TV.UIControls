/*
 * Copyright (c) 2020 Samsung Electronics Co., Ltd All Rights Reserved
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

using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace Tizen.TV.UIControls.Forms
{
    /// <summary>
    /// It is a container to decorate focused state
    /// </summary>
    public class FocusFrame : Frame
    {
        /// <summary>
        /// Identifies the FocusedColor bindable property.
        /// </summary>
        public static readonly BindableProperty FocusedColorProperty = BindableProperty.Create(nameof(FocusedColor), typeof(Color), typeof(FocusFrame), Color.Orange);


        /// <summary>
        /// Creates and initializes a new instance of the FocusFrame class.
        /// </summary>
        public FocusFrame()
        {
            Padding = 10;
            BorderColor = Color.Transparent;
            HasShadow = false;
        }

        /// <summary>
        /// Gets or sets a value that represents FocusedColor, it used for decorating focused state on content
        /// </summary>
        public Color FocusedColor
        {
            get => (Color)GetValue(FocusedColorProperty);
            set => SetValue(FocusedColorProperty, value);
        }

        protected override void OnPropertyChanging([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanging(propertyName);
            if (propertyName == nameof(Content))
            {
                if (Content != null)
                {
                    Content.Focused -= OnContentFocused;
                    Content.Unfocused -= OnContentFocused;
                }
            }
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);
            if (propertyName == nameof(Content))
            {
                if (Content != null)
                {
                    Content.Focused += OnContentFocused;
                    Content.Unfocused += OnContentFocused;
                }
            }
        }

        /// <summary>
        /// This method was called when content's focused state was changed.
        /// To change behavior of decorating, overriding this method
        /// </summary>
        /// <param name="isFocused">This parameter indicates whether the content is focused.</param>
        protected virtual void OnContentFocused(bool isFocused)
        {
            BackgroundColor = isFocused ? FocusedColor : Color.Transparent;
        }

        void OnContentFocused(object sender, FocusEventArgs e)
        {
            OnContentFocused(e.IsFocused);
        }
    }
}
