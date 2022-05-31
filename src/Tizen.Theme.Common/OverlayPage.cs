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
using System.Runtime.CompilerServices;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;

namespace Tizen.Theme.Common
{
    /// <summary>
    /// The OverlayPage class is used to display the video output on a page.
    /// </summary>
    public class OverlayPage : ContentPage, IOverlayOutput
    {
        /// <summary>
        /// Identifies the OverlayArea bindable property.
        /// </summary>
        public static readonly BindableProperty OverlayAreaProperty = BindableProperty.Create("OverlayArea", typeof(Rect), typeof(OverlayPage), default(Rect));
        /// <summary>
        /// Identifies the Player bindable property.
        /// </summary>
        public static readonly BindableProperty PlayerProperty = BindableProperty.Create("Player", typeof(MediaPlayer), typeof(OverlayPage), default(MediaPlayer), propertyChanged: (b, o, n) => ((OverlayPage)b).OnPlayerChanged());

        View _controller;

        /// <summary>
        /// Gets or sets the overlay area.
        /// </summary>
        public Rect OverlayArea
        {
            get { return (Rect)GetValue(OverlayAreaProperty); }
            set { SetValue(OverlayAreaProperty, value); }
        }

        /// <summary>
        /// Gets or sets the media player.
        /// </summary>
        public MediaPlayer Player
        {
            get { return (MediaPlayer)GetValue(PlayerProperty); }
            set { SetValue(PlayerProperty, value); }
        }

        VisualElement IVideoOutput.MediaView => this;

        View IVideoOutput.Controller
        {
            get { return _controller; }
            set
            {
                if (_controller != null)
                {
                    InternalChildren.Remove(_controller);
                }
                    
                _controller = value;
                if (_controller != null)
                {
                    InternalChildren.Insert(0, _controller);
                    OnChildrenReordered();
                }
            }
        }

        /// <summary>
        /// Occurs when the overlay area is updated.
        /// </summary>
        public event EventHandler AreaUpdated;

        protected override void LayoutChildren(double x, double y, double width, double height)
        {
            base.LayoutChildren(x, y, width, height);
            (this as IVideoOutput).Controller?.Layout(new Rect(x, y, width, height));
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);
            if (propertyName == nameof(OverlayArea))
            {
                AreaUpdated?.Invoke(this, EventArgs.Empty);
            }

            if (propertyName == nameof(Content) || propertyName == nameof(ControlTemplate))
            {
                if (_controller != null)
                {
                    var controller = _controller;
                    (this as IOverlayOutput).Controller = null;
                    controller.Layout(new Rect(0, 0, -1, -1));
                    Device.BeginInvokeOnMainThread(() => (this as IOverlayOutput).Controller = controller);
                }
            }
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            if (Player != null)
            {
                SetInheritedBindingContext(Player, BindingContext);
            }
        }

        void OnPlayerChanged()
        {
            if (Player != null)
            {
                Player.VideoOutput = this;
                SetInheritedBindingContext(Player, BindingContext);
            }
        }

        /// <summary>
        /// Gets the video output type.
        /// </summary>
        public VideoOuputType OuputType => VideoOuputType.Overlay;
    }
}