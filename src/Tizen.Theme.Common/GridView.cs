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
using System.ComponentModel;
using System.Collections;
using Microsoft.Maui.Controls;
//using Specific = Xamarin.Forms.PlatformConfiguration.TizenSpecific.VisualElement;

namespace Tizen.Theme.Common
{
    /// <summary>
    /// The GridView is a view to efficiently display a collections of data using DataTemplate
    /// </summary>
    public class GridView : View, IGridViewController
    {
        /// <summary>
        /// Identifies the ItemsSource bindable property.
        /// </summary>
        public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create(nameof(ItemsSource), typeof(IEnumerable), typeof(GridView), null);

        /// <summary>
        /// Identifies the ItemTemplate bindable property.
        /// </summary>
        public static readonly BindableProperty ItemTemplateProperty = BindableProperty.Create(nameof(ItemTemplate), typeof(DataTemplate), typeof(ItemsView<GridView>), null, validateValue: (b, v) => ((GridView)b).ValidateItemTemplate((DataTemplate)v));

        /// <summary>
        /// Identifies the ItemHeight bindable property.
        /// </summary>
        public static readonly BindableProperty ItemHeightProperty = BindableProperty.Create(nameof(ItemHeight), typeof(double), typeof(GridView), 0.0);

        /// <summary>
        /// Identifies the ItemWidth bindable property.
        /// </summary>
        public static readonly BindableProperty ItemWidthProperty = BindableProperty.Create(nameof(ItemWidth), typeof(double), typeof(GridView), 0.0);

        /// <summary>
        /// Identifies the Orientation bindable property.
        /// </summary>
        public static readonly BindableProperty OrientationProperty = BindableProperty.Create(nameof(Orientation), typeof(ItemsLayoutOrientation), typeof(GridView), ItemsLayoutOrientation.Horizontal);

        ///// <summary>
        ///// Identifies the HorizontalScrollBarVisible bindable property.
        ///// </summary>
        //public static readonly BindableProperty HorizontalScrollBarVisibleProperty = BindableProperty.Create(nameof(HorizontalScrollBarVisible), typeof(ScrollBarVisibility), typeof(GridView), ScrollBarVisibility.Never);

        ///// <summary>
        ///// Identifies the VerticalScrollBarVisible bindable property.
        ///// </summary>
        //public static readonly BindableProperty VerticalScrollBarVisibleProperty = BindableProperty.Create(nameof(VerticalScrollBarVisible), typeof(ScrollBarVisibility), typeof(GridView), ScrollBarVisibility.Never);

        /// <summary>
        /// Identifies the SelectedItem bindable property.
        /// </summary>
        public static readonly BindableProperty SelectedItemProperty = BindableProperty.Create(nameof(SelectedItem), typeof(object), typeof(GridView), null);

        /// <summary>
        /// Identifies the ItemHorizontalAlignment bindable property.
        /// </summary>
        public static readonly BindableProperty ItemHorizontalAlignmentProperty = BindableProperty.Create(nameof(ItemHorizontalAlignment), typeof(LayoutAlignment), typeof(GridView), LayoutAlignment.Center);

        /// <summary>
        /// Identifies the ItemVerticalAlignment bindable property.
        /// </summary>
        public static readonly BindableProperty ItemVerticalAlignmentProperty = BindableProperty.Create(nameof(ItemVerticalAlignment), typeof(LayoutAlignment), typeof(GridView), LayoutAlignment.Center);

        /// <summary>
        /// ItemSelected is raised when one item of GridView is selected.
        /// </summary>
        public event EventHandler<SelectedItemChangedEventArgs> ItemSelected;

        /// <summary>
        /// ItemFocused is raised when one item of GridView has received focus.
        /// </summary>
        public event EventHandler<GridViewFocusedEventArgs> ItemFocused;

        /// <summary>
        /// Gets or sets the source of items to template and display.
        /// </summary>
        public IEnumerable ItemsSource
        {
            get => (IEnumerable)GetValue(ItemsSourceProperty);
            set => SetValue(ItemsSourceProperty, value);
        }

        /// <summary>
        /// Gets or sets the DataTemplate to apply to the ItemsSource.
        /// </summary>
        public DataTemplate ItemTemplate
        {
            get => (DataTemplate)GetValue(ItemTemplateProperty);
            set => SetValue(ItemTemplateProperty, value);
        }

        /// <summary>
        /// Gets or sets a value that represents the height of an item.
        /// </summary>
        public double ItemHeight
        {
            get { return (double)GetValue(ItemHeightProperty); }
            set { SetValue(ItemHeightProperty, value); }
        }

        /// <summary>
        /// Gets or sets a value that represents the width of an item.
        /// </summary>
        public double ItemWidth
        {
            get { return (double)GetValue(ItemWidthProperty); }
            set { SetValue(ItemWidthProperty, value); }
        }

        /// <summary>
        /// Gets or sets the item's layout alignment along the horizon.
        /// </summary>
        public LayoutAlignment ItemHorizontalAlignment
        {
            get { return (LayoutAlignment)GetValue(ItemHorizontalAlignmentProperty); }
            set { SetValue(ItemHorizontalAlignmentProperty, value); }
        }

        /// <summary>
        /// Gets or sets the item's layout alignment along the vertical.
        /// </summary>
        public LayoutAlignment ItemVerticalAlignment
        {
            get { return (LayoutAlignment)GetValue(ItemVerticalAlignmentProperty); }
            set { SetValue(ItemVerticalAlignmentProperty, value); }
        }

        /// <summary>
        /// Gets or sets the currently selected item from the ItemsSource.
        /// </summary>
        public object SelectedItem
        {
            get { return GetValue(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); }
        }

        ///// <summary>
        ///// Gets or sets the style of GridView.
        ///// </summary>
        //public string ThemeStyle
        //{
        //    get { return (string)GetValue(Specific.StyleProperty); }
        //    set { SetValue(Specific.StyleProperty, value); }
        //}

        /// <summary>
        /// Gets or sets the value which indicates the direction which items are positioned.
        /// </summary>
        public ItemsLayoutOrientation Orientation
        {
            get { return (ItemsLayoutOrientation)GetValue(OrientationProperty); }
            set { SetValue(OrientationProperty, value); }
        }

        ///// <summary>
        ///// Gets or sets the horizontal scroll bar visibility.
        ///// </summary>
        //public ScrollBarVisibility HorizontalScrollBarVisible
        //{
        //    get { return (ScrollBarVisibility)GetValue(HorizontalScrollBarVisibleProperty); }
        //    set { SetValue(HorizontalScrollBarVisibleProperty, value); }
        //}

        ///// <summary>
        ///// Gets or sets the vertical scroll bar visibility.
        ///// </summary>
        //public ScrollBarVisibility VerticalScrollBarVisible
        //{
        //    get { return (ScrollBarVisibility)GetValue(VerticalScrollBarVisibleProperty); }
        //    set { SetValue(VerticalScrollBarVisibleProperty, value); }
        //}

        /// <summary>
        /// For internal use.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void SendItemFocused(GridViewFocusedEventArgs args)
        {
            ItemFocused?.Invoke(this, args);
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public void SendItemSelected(SelectedItemChangedEventArgs args)
        {
            ItemSelected?.Invoke(this, args);
        }

        protected bool ValidateItemTemplate(DataTemplate template)
        {
            if (template == null)
                return true;
            if (template is DataTemplateSelector)
                return false;

            object content = template.CreateContent();
            if (content is View || content is Cell)
                return true;

            return false;
        }
    }
}
