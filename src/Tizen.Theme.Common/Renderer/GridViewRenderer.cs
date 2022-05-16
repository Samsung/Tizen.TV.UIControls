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
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using ElmSharp;
using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Compatibility;
using Microsoft.Maui.Controls.Compatibility.Platform.Tizen;
using Microsoft.Maui.Controls.Platform;
//using Xamarin.Forms.Platform.Tizen;

using Tizen.Theme.Common;
using Tizen.Theme.Common.Renderer;

//TODO gridview

[assembly: ExportRenderer(typeof(GridView), typeof(GridViewRenderer))]
namespace Tizen.Theme.Common.Renderer
{
    class GengridItemContext
    {
        public object Data { get; set; }
        public View RealizedView { get; set; }
    }

    public class GridViewRenderer : ViewRenderer<GridView, GenGrid>
    {
        Dictionary<object, GenGridItem> _gengridItemDic = new Dictionary<object, GenGridItem>();
        Dictionary<IntPtr, GenGridItem> _itemHandleDic = new Dictionary<IntPtr, GenGridItem>();
        List<object> _itemList = new List<object>();
        INotifyCollectionChanged _collectionChanged = null;
        GenItemClass _gridItemClass = new GenItemClass("full");

        protected override void OnElementChanged(ElementChangedEventArgs<GridView> e)
        {
            if (Control == null)
            {
                SetNativeControl(new GenGrid(Forms.NativeParent));
                //Control.HorizontalScrollBarVisiblePolicy = Element.HorizontalScrollBarVisible.ToScrollBarVisiblePolicy();
                //Control.VerticalScrollBarVisiblePolicy = Element.VerticalScrollBarVisible.ToScrollBarVisiblePolicy();
                Control.IsHorizontal = Element.Orientation == ItemsLayoutOrientation.Horizontal ? true : false;
                Control.ItemWidth = Forms.ConvertToScaledPixel(Element.ItemWidth);
                Control.ItemHeight = Forms.ConvertToScaledPixel(Element.ItemHeight);
                Control.ItemAlignmentX = Element.ItemHorizontalAlignment.LayoutAlignmentToDouble();
                Control.ItemAlignmentY = Element.ItemVerticalAlignment.LayoutAlignmentToDouble();
                //Control.Style = Element.ThemeStyle;
                Control.ItemSelected += OnItemSelected;
                Control.ItemRealized += OnItemRealized;

                SmartEvent<GenGridItemEventArgs> _focused = new SmartEvent<GenGridItemEventArgs>(Control, Control.RealHandle, "item,focused", OnSmartEvent);
                SmartEvent<GenGridItemEventArgs> _unfocused = new SmartEvent<GenGridItemEventArgs>(Control, Control.RealHandle, "item,unfocused", OnSmartEvent);
                _focused.On += OnItemFocused;
                _unfocused.On += OnItemUnfocused;

                _gridItemClass.GetContentHandler = GetGridViewContent;
                if (Element.ItemsSource != null)
                {
                    UpdateItemsSource();
                }
            }
            base.OnElementChanged(e);
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == GridView.ItemWidthProperty.PropertyName)
            {
                Control.ItemWidth = Forms.ConvertToScaledPixel(Element.ItemWidth);
            }
            else if (e.PropertyName == GridView.ItemHeightProperty.PropertyName)
            {
                Control.ItemHeight = Forms.ConvertToScaledPixel(Element.ItemHeight);
            }
            else if (e.PropertyName == GridView.ItemVerticalAlignmentProperty.PropertyName)
            {
                Control.ItemAlignmentX = Element.ItemVerticalAlignment.LayoutAlignmentToDouble();
            }
            else if (e.PropertyName == GridView.ItemHorizontalAlignmentProperty.PropertyName)
            {
                Control.ItemAlignmentY = Element.ItemHorizontalAlignment.LayoutAlignmentToDouble();
            }
            //else if (e.PropertyName == GridView.HorizontalScrollBarVisibleProperty.PropertyName)
            //{
            //    Control.HorizontalScrollBarVisiblePolicy = Element.HorizontalScrollBarVisible.ToScrollBarVisiblePolicy();
            //}
            //else if (e.PropertyName == GridView.VerticalScrollBarVisibleProperty.PropertyName)
            //{
            //    Control.VerticalScrollBarVisiblePolicy = Element.VerticalScrollBarVisible.ToScrollBarVisiblePolicy();
            //}
            else if (e.PropertyName == GridView.OrientationProperty.PropertyName)
            {
                Control.IsHorizontal = Element.Orientation == ItemsLayoutOrientation.Horizontal ? true : false;
            }
            else if (e.PropertyName == GridView.ItemsSourceProperty.PropertyName ||
                e.PropertyName == GridView.ItemTemplateProperty.PropertyName)
            {
                UpdateItemsSource();
            }
            else if (e.PropertyName == GridView.SelectedItemProperty.PropertyName)
            {
                UpdateSelectedItem();
            }
            base.OnElementPropertyChanged(sender, e);
        }

        protected override void UpdateThemeStyle()
        {
            //Control.Style = Element.ThemeStyle;
        }

        void OnItemSelected(object sender, GenGridItemEventArgs e)
        {
            GengridItemContext context = e.Item.Data as GengridItemContext;
            Element.SelectedItem = context.Data;
            (Element as IGridViewController)?.SendItemSelected(new SelectedItemChangedEventArgs(context.Data, _itemList.IndexOf(context.Data)));
        }

        EvasObject GetGridViewContent(object data, string part)
        {
            GengridItemContext context = (GengridItemContext)data;

            if (context.RealizedView == null)
            {
                context.RealizedView = CreateOrGetRealView(context);
            }

            if (part == "elm.swallow.icon")
            {
                var renderer = Platform.GetOrCreateRenderer(context.RealizedView);
                (renderer as LayoutRenderer)?.RegisterOnLayoutUpdated();
                return renderer.NativeView;
            }
            return null;
        }

        void OnItemUnfocused(object sender, GenGridItemEventArgs e)
        {
            if (e.Item == null)
                return;

            GengridItemContext context = e.Item.Data as GengridItemContext;
            if (context.RealizedView == null)
                context.RealizedView = CreateOrGetRealView(context);
            (Element as IGridViewController)?.SendItemFocused(new GridViewFocusedEventArgs(context.Data, context.RealizedView, false));
        }

        void OnItemFocused(object sender, GenGridItemEventArgs e)
        {
            if (e.Item == null)
                return;

            GengridItemContext context = e.Item.Data as GengridItemContext;
            if (context.RealizedView == null)
                context.RealizedView = CreateOrGetRealView(context);
            (Element as IGridViewController)?.SendItemFocused(new GridViewFocusedEventArgs(context.Data, context.RealizedView, true));
        }

        void OnItemRealized(object sender, GenGridItemEventArgs e)
        {
            e.Item.EmitSignal("elm,state,5p,scale", "");
            e.Item.SetPartColor("effect", new ElmSharp.Color(0, 0, 0, 0));
        }

        GenGridItemEventArgs OnSmartEvent(IntPtr data, IntPtr obj, IntPtr info)
        {
            return new GenGridItemEventArgs
            {
                Item = _itemHandleDic.ContainsKey(info) ? _itemHandleDic[info] : null
            };
        }

        void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            UpdateItemsSource();
        }

        void UpdateItemsSource()
        {
            Control.Clear();
            _gengridItemDic.Clear();
            _itemHandleDic.Clear();
            _itemList.Clear();

            if (_collectionChanged != null)
            {
                _collectionChanged.CollectionChanged -= OnCollectionChanged;
                _collectionChanged = null;
            }

            if (Element.ItemsSource == null)
            {
                return;
            }

            foreach (var item in Element.ItemsSource)
            {
                var context = new GengridItemContext
                {
                    Data = item,
                    RealizedView = null,
                };
                var gridItem = Control.Append(_gridItemClass, context);
                IntPtr handle = gridItem;
                _itemHandleDic.Add(handle, gridItem);
                _gengridItemDic.Add(item, gridItem);
                _itemList.Add(item);
            }

            if (Element.ItemsSource is INotifyCollectionChanged collection)
            {
                _collectionChanged = collection;
                collection.CollectionChanged += OnCollectionChanged;
            }
        }

        View CreateOrGetRealView(GengridItemContext context)
        {
            if (context.RealizedView != null)
            {
                return context.RealizedView;
            }
            var realView = CreateView(Element.ItemTemplate);
            realView.BindingContext = context.Data;
            realView.Parent = Element;
            context.RealizedView = realView;
            return realView;
        }

        View CreateView(DataTemplate template)
        {
            if (template != null)
            {
                var content = template.CreateContent();
                if (content is View view)
                    return view;
            }
            return null;
        }

        void UpdateSelectedItem()
        {
            if (Element.SelectedItem == null)
            {
                Control.SelectedItem.IsSelected = false;
                return;
            }
            GenGridItem item = null;
            _gengridItemDic.TryGetValue(Element.SelectedItem, out item);
            if (item != null)
            {
                item.IsSelected = true;
            }
        }
    }

    public static class GridViewConvertExtensions
    {
        public static double LayoutAlignmentToDouble(this LayoutAlignment alignment)
        {

            double alignValue = 0.5;
            switch (alignment)
            {
                case LayoutAlignment.Start:
                    alignValue = 0.0;
                    break;
                case LayoutAlignment.End:
                    alignValue = 1.0;
                    break;
            }
            return alignValue;
        }

        public static ScrollBarVisiblePolicy ToScrollBarVisiblePolicy(this ScrollBarVisibility visibility)
        {

            ScrollBarVisiblePolicy policy = ScrollBarVisiblePolicy.Invisible;
            switch (visibility)
            {
                case ScrollBarVisibility.Always:
                    policy = ScrollBarVisiblePolicy.Visible;
                    break;
                case ScrollBarVisibility.Default:
                    policy = ScrollBarVisiblePolicy.Invisible;
                    break;
            }
            return policy;
        }
    }
}