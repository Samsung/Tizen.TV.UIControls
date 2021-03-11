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
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace Tizen.Theme.Common
{
    /// <summary>
    /// A Frame with shadow effects
    /// </summary>
    public class ShadowFrame : Frame
    {
        /// <summary>
        /// Identifies the CornerRadius bindable property.
        /// </summary>
        public static new readonly BindableProperty CornerRadiusProperty = BindableProperty.Create(nameof(CornerRadius), typeof(CornerRadius), typeof(ShadowFrame), default(CornerRadius));

        /// <summary>
        /// Identifies the BorderWidth bindable property.
        /// </summary>
        public static readonly BindableProperty BorderWidthProperty = BindableProperty.Create(nameof(BorderWidth), typeof(double), typeof(ShadowFrame), 1.0);

        /// <summary>
        /// Identifies the ShadowOffsetX bindable property.
        /// </summary>
        public static readonly BindableProperty ShadowOffsetXProperty = BindableProperty.Create(nameof(ShadowOffsetX), typeof(double), typeof(ShadowFrame), 0d);

        /// <summary>
        /// Identifies the ShadowOffsetY bindable property.
        /// </summary>
        public static readonly BindableProperty ShadowOffsetYProperty = BindableProperty.Create(nameof(ShadowOffsetY), typeof(double), typeof(ShadowFrame), 8.0);

        /// <summary>
        /// Identifies the ShadowColor bindable property.
        /// </summary>
        public static readonly BindableProperty ShadowColorProperty = BindableProperty.Create(nameof(ShadowColor), typeof(Color), typeof(ShadowFrame), Color.FromHex("#3E000000"));

        /// <summary>
        /// Identifies the ShadowOpacity bindable property.
        /// </summary>
        public static readonly BindableProperty ShadowOpacityProperty = BindableProperty.Create(nameof(ShadowOpacity), typeof(double), typeof(ShadowFrame), .24d, coerceValue: (bindable, value) => ((double)value).Clamp(0, 1));


        /// <summary>
        /// Identifies the ShadowBlur bindable property.
        /// </summary>
        public static readonly BindableProperty ShadowBlurRadiusProperty = BindableProperty.Create(nameof(ShadowBlurRadius), typeof(double), typeof(ShadowFrame), 10d);

        /// <summary>
        /// Gets or sets a value that represents CornerRadius.
        /// </summary>
        public new CornerRadius CornerRadius
        {
            get => (CornerRadius)GetValue(CornerRadiusProperty);
            set => SetValue(CornerRadiusProperty, value);
        }

        /// <summary>
        /// Gets or sets a value that represents BorderWidth.
        /// </summary>
        public double BorderWidth
        {
            get => (double)GetValue(BorderWidthProperty);
            set => SetValue(BorderWidthProperty, value);
        }

        /// <summary>
        /// Gets or sets a shadow offset x.
        /// </summary>
        public double ShadowOffsetX
        {
            get => (double)GetValue(ShadowOffsetXProperty);
            set => SetValue(ShadowOffsetXProperty, value);
        }

        /// <summary>
        /// Gets or sets a shadow offset y.
        /// </summary>
        public double ShadowOffsetY
        {
            get => (double)GetValue(ShadowOffsetYProperty);
            set => SetValue(ShadowOffsetYProperty, value);
        }

        /// <summary>
        /// Gets or sets a shadow color.
        /// </summary>
        public Color ShadowColor
        {
            get => (Color)GetValue(ShadowColorProperty);
            set => SetValue(ShadowColorProperty, value);
        }

        /// <summary>
        /// Gets or sets a shadow opacity.
        /// </summary>
        public double ShadowOpacity
        {
            get => (double)GetValue(ShadowOpacityProperty);
            set => SetValue(ShadowOpacityProperty, value);
        }

        /// <summary>
        /// Gets or sets a shadow blur radius.
        /// </summary>
        public double ShadowBlurRadius
        {
            get => (double)GetValue(ShadowBlurRadiusProperty);
            set => SetValue(ShadowBlurRadiusProperty, value);
        }
    }
}
