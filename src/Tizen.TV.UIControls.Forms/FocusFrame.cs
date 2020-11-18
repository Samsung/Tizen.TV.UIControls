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

using System;
using System.Runtime.CompilerServices;
using System.Windows.Input;
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
        /// Identifies the ContentFocusedCommand bindable property
        /// </summary>
        public static readonly BindableProperty ContentFocusedCommandProperty = BindableProperty.Create(nameof(ContentFocusedCommand), typeof(ICommand), typeof(FocusFrame), null);

        /// <summary>
        /// Identifies the ContentFocusedCommandParameter bindable property
        /// </summary>
        public static readonly BindableProperty ContentFocusedCommandParameterProperty = BindableProperty.Create(nameof(ContentFocusedCommandParameter), typeof(object), typeof(FocusFrame), null);

        /// <summary>
        /// Identifies the ContentUnfocusedCommand bindable property
        /// </summary>
        public static readonly BindableProperty ContentUnfocusedCommandProperty = BindableProperty.Create(nameof(ContentUnfocusedCommand), typeof(ICommand), typeof(FocusFrame), null);

        /// <summary>
        /// Identifies the ContentUnfocusedCommandParameter bindable property
        /// </summary>
        public static readonly BindableProperty ContentUnfocusedCommandParameterProperty = BindableProperty.Create(nameof(ContentUnfocusedCommandParameter), typeof(object), typeof(FocusFrame), null);

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
        /// Raise when one of descendants view are focused
        /// </summary>
        public event EventHandler<FocusEventArgs> ContentFocused;

        /// <summary>
        /// Raise when one of descendants view are unfocused
        /// </summary>
        public event EventHandler<FocusEventArgs> ContentUnfocused;


        /// <summary>
        /// Gets or sets the command to invoke when the content is focused. This is a bindable property.
        /// </summary>
        public ICommand ContentFocusedCommand
        {
            get => (ICommand)GetValue(ContentFocusedCommandProperty);
            set => SetValue(ContentFocusedCommandProperty, value);
        }

        /// <summary>
        /// Gets or sets the parameter to pass to the ContentFocusedCommand property. This is a bindable property.
        /// </summary>
        public object ContentFocusedCommandParameter
        {
            get => GetValue(ContentFocusedCommandParameterProperty);
            set => SetValue(ContentFocusedCommandParameterProperty, value);
        }

        /// <summary>
        /// Gets or sets the command to invoke when the content is unfocused. This is a bindable property.
        /// </summary>
        public ICommand ContentUnfocusedCommand
        {
            get => (ICommand)GetValue(ContentUnfocusedCommandProperty);
            set => SetValue(ContentUnfocusedCommandProperty, value);
        }

        /// <summary>
        /// Gets or sets the parameter to pass to the ContentUnfocusedCommand property. This is a bindable property.
        /// </summary>
        public object ContentUnfocusedCommandParameter
        {
            get => GetValue(ContentUnfocusedCommandParameterProperty);
            set => SetValue(ContentUnfocusedCommandParameterProperty, value);
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
                    Content.DescendantAdded -= OnDescendantAdded;
                    Content.DescendantRemoved -= OnDescendantRemoved;

                    foreach (var child in Content.Descendants())
                    {
                        if (child is VisualElement ve)
                        {
                            ve.Focused -= OnContentFocused;
                            ve.Unfocused -= OnContentFocused;
                        }
                    }
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
                    Content.DescendantAdded += OnDescendantAdded;
                    Content.DescendantRemoved += OnDescendantRemoved;

                    foreach (var child in Content.Descendants())
                    {
                        if (child is VisualElement ve)
                        {
                            ve.Focused += OnContentFocused;
                            ve.Unfocused += OnContentFocused;
                        }
                    }
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
            if (e.IsFocused)
            {
                ContentFocusedCommand?.Execute(ContentFocusedCommandParameter);
                ContentFocused?.Invoke(this, e);
            }
            else
            {
                ContentUnfocusedCommand?.Execute(ContentUnfocusedCommandParameter);
                ContentUnfocused?.Invoke(this, e);
            }
        }

        void OnDescendantAdded(object sender, ElementEventArgs e)
        {
            if (e.Element is VisualElement ve)
            {
                ve.Focused += OnContentFocused;
                ve.Unfocused += OnContentFocused;
            }
        }

        void OnDescendantRemoved(object sender, ElementEventArgs e)
        {
            if (e.Element is VisualElement ve)
            {
                ve.Focused -= OnContentFocused;
                ve.Unfocused -= OnContentFocused;
            }
        }

    }
}
