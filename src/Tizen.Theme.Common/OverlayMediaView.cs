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

#nullable enable

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using Microsoft.Maui.Controls.Internals;

namespace Tizen.Theme.Common
{
    /// <summary>
    /// An overlay MediaView.
    /// </summary>
    public class OverlayMediaView : MediaView, IOverlayOutput
    {
        internal static readonly BindablePropertyKey OverlayAreaPropertyKey = BindableProperty.CreateReadOnly(nameof(OverlayArea), typeof(Rect), typeof(OverlayMediaView), default(Rect));
        /// <summary>
        /// Identifies the OverlayArea bindable property.
        /// </summary>
        public static readonly BindableProperty OverlayAreaProperty = OverlayAreaPropertyKey.BindableProperty;

        /// <summary>
        /// Initializes a new instance of the OverlayMediaView class.
        /// </summary>
        public OverlayMediaView()
        {
            BatchCommitted += OnBatchCommitted;
        }

        /// <summary>
        /// Occurs when the overlay area is updated.
        /// </summary>
        public event EventHandler AreaUpdated;

        /// <summary>
        /// Gets the overlay area.
        /// </summary>
        public Rect OverlayArea
        {
            get { return (Rect)GetValue(OverlayAreaProperty); }
            private set { SetValue(OverlayAreaPropertyKey, value); }
        }

        /// <summary>
        /// Gets the video output type.
        /// </summary>
        public override VideoOuputType OuputType => VideoOuputType.Overlay;


        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);
            if (new List<string> { nameof(X), nameof(Y), nameof(Width), nameof(Height)}.Contains(propertyName) && !Batched)
            {
                OverlayArea = Bounds;
            }
            if (propertyName == nameof(OverlayArea))
            {
                AreaUpdated?.Invoke(this, EventArgs.Empty);
            }
        }

        void OnBatchCommitted(object? sender, EventArg<VisualElement> e)
        {
            OverlayArea = Bounds;
        }
    }
}
