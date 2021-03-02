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

namespace Tizen.Theme.Common
{
    /// <summary>
    /// The MediaView class is an abstract class whose subclasses can be used to display the video output on the screen.
    /// </summary>
    [ContentProperty("Player")]
    public abstract class MediaView : Layout<View>, IVideoOutput
    {
        /// <summary>
        /// Identifies the Player bindable property.
        /// </summary>
        public static readonly BindableProperty PlayerProperty = BindableProperty.Create("Player", typeof(MediaPlayer), typeof(MediaView), default(MediaPlayer), propertyChanged: (b, o, n) => ((MediaView)b).OnPlayerChanged());
        
        View _controller;

        /// <summary>
        /// Gets or sets the media player.
        /// </summary>
        public MediaPlayer Player
        {
            get { return (MediaPlayer)GetValue(PlayerProperty); }
            set { SetValue(PlayerProperty, value); }
        }

        /// <summary>
        /// Gets the video output type.
        /// </summary>
        public virtual VideoOuputType OuputType => VideoOuputType.Buffer;

        VisualElement IVideoOutput.MediaView => this;
        View IVideoOutput.Controller
        {
            get
            {
                return _controller;
            }
            set
            {
                if (_controller != null)
                {
                    Children.Remove(_controller);
                }

                _controller = value;

                if (_controller != null)
                {
                    Children.Add(_controller);
                }
            }
        }

        protected override void LayoutChildren(double x, double y, double width, double height)
        {
            _controller?.Layout(new Rectangle(x, y, width, height));
        }

        protected override SizeRequest OnMeasure(double widthConstraint, double heightConstraint)
        {
            return _controller?.Measure(widthConstraint, heightConstraint) ?? base.OnMeasure(widthConstraint, heightConstraint);
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
    }
}
