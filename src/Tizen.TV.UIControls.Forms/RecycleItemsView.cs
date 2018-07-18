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
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Tizen.TV.UIControls.Forms
{
    /// <summary>
    /// The orientations the a RecycleItemsView can have.
    /// </summary>
    public enum RecycleItemsViewOrientation
    {
        /// <summary>
        /// should be vertically oriented.
        /// </summary>
        Vertical,

        /// <summary>
        /// RecycleItemsView should be horizontally oriented.
        /// </summary>
        Horizontal,
    }

    /// <summary>
    /// This interface is for internal use.
    /// </summary>
    public interface IRecycleItemsViewController
    {
        bool SendKeyDown(string keyname);
        View FocusedView { get; }
        Rectangle ScrollBounds { get; }
    }

    /// <summary>
    /// A ScrollView that efficiently displays a collections of data using DataTemplate
    /// </summary>
    public class RecycleItemsView : ContentView, IRecycleItemsViewController
    {
        /// <summary>
        /// Backing store for the ItemWidth property.
        /// </summary>
        public static readonly BindableProperty ItemWidthProperty = BindableProperty.Create(nameof(ItemWidth), typeof(double), typeof(RecycleItemsView), 100d, propertyChanged: (b, o, n) => ((RecycleItemsView)b).UpdateItemSize());
        /// <summary>
        /// Backing store for the ItemHeight property.
        /// </summary>
        public static readonly BindableProperty ItemHeightProperty = BindableProperty.Create(nameof(ItemHeight), typeof(double), typeof(RecycleItemsView), 100d, propertyChanged: (b, o, n) => ((RecycleItemsView)b).UpdateItemSize());
        /// <summary>
        /// Backing store for the Orientation property.
        /// </summary>
        public static readonly BindableProperty OrientationProperty = BindableProperty.Create(nameof(Orientation), typeof(RecycleItemsViewOrientation), typeof(RecycleItemsView), RecycleItemsViewOrientation.Horizontal, propertyChanged: (b, o, n) => ((RecycleItemsView)b).UpdateOrientation());
        /// <summary>
        /// Backing store for the Spacing property.
        /// </summary>
        public static readonly BindableProperty SpacingProperty = BindableProperty.Create(nameof(Spacing), typeof(double), typeof(RecycleItemsView), 6d, propertyChanged: (b, o, n) => ((RecycleItemsView)b).UpdateSpacing());
        /// <summary>
        /// Backing store for the ItemsSource property.
        /// </summary>
        public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create(nameof(ItemsSource), typeof(IEnumerable), typeof(RecycleItemsView), null, propertyChanged: (b, o, n) => ((RecycleItemsView)b).UpdateItemsSource());
        /// <summary>
        /// Backing store for the ItemTemplate property.
        /// </summary>
        public static readonly BindableProperty ItemTemplateProperty = BindableProperty.Create(nameof(ItemTemplate), typeof(DataTemplate), typeof(RecycleItemsView), null, validateValue: (b, v) => ((RecycleItemsView)b).ValidateItemTemplate((DataTemplate)v), propertyChanged: (b, o, n) => ((RecycleItemsView)b).UpdateItemTemplate());
        /// <summary>
        /// Backing store for the FocusedItem property.
        /// </summary>
        public static readonly BindableProperty FocusedItemProperty = BindableProperty.Create(nameof(FocusedItem), typeof(object), typeof(RecycleItemsView), null, BindingMode.OneWayToSource, propertyChanging: (b, o, n) => ((RecycleItemsView)b).OnFocusedItemChanging(), propertyChanged: (b, o, n) => ((RecycleItemsView)b).OnFocusedItemChanged());
        /// <summary>
        /// Backing store for the SelectedItem property.
        /// </summary>
        public static readonly BindableProperty SelectedItemProperty = BindableProperty.Create(nameof(SelectedItem), typeof(object), typeof(RecycleItemsView), null, BindingMode.OneWayToSource, propertyChanged: (b, o, n) => ((RecycleItemsView)b).UpdateSelectedItem());
        /// <summary>
        /// Backing store for the ContentMargin property.
        /// </summary>
        public static readonly BindableProperty ContentMarginProperty = BindableProperty.Create(nameof(ContentMargin), typeof(Thickness), typeof(RecycleItemsView), default(Thickness), propertyChanged: (b, o, n) => ((RecycleItemsView)b).UpdateContentMargin());
        /// <summary>
        /// Backing store for the ScrollPolicy property.
        /// </summary>
        public static readonly BindableProperty ScrollPolicyProperty = BindableProperty.Create(nameof(ScrollPolicy), typeof(ScrollToPosition), typeof(RecycleItemsView), ScrollToPosition.Center);
        /// <summary>
        /// Backing store for the ScrollBarVisibility property.
        /// </summary>
        public static readonly BindableProperty ScrollBarVisibilityProperty = BindableProperty.Create(nameof(ScrollBarVisibility), typeof(ScrollBarVisibility), typeof(RecycleItemsView), ScrollBarVisibility.Default, propertyChanged: (b, o, n) => ((RecycleItemsView)b).UpdateScrollBarVisibility());
        /// <summary>
        /// Backing store for the ColumnCount property.
        /// </summary>
        public static readonly BindableProperty ColumnCountProperty = BindableProperty.Create(nameof(ColumnCount), typeof(int), typeof(RecycleItemsView), 1, propertyChanged: (b, o, n) => ((RecycleItemsView)b).UpdateColumnCount());
        /// <summary>
        /// Backing store for the Header property.
        /// </summary>
        public static readonly BindableProperty HeaderProperty = BindableProperty.Create(nameof(Header), typeof(object), typeof(RecycleItemsView), null, propertyChanged: (b, o, n) => ((RecycleItemsView)b).UpdateHeaderElement());
        /// <summary>
        /// Backing store for the Footer property.
        /// </summary>
        public static readonly BindableProperty FooterProperty = BindableProperty.Create(nameof(Footer), typeof(object), typeof(RecycleItemsView), null, propertyChanged: (b, o, n) => ((RecycleItemsView)b).UpdateFooterElement());
        /// <summary>
        /// Backing store for the HeaderTemplate property.
        /// </summary>
        public static readonly BindableProperty HeaderTemplateProperty = BindableProperty.Create(nameof(HeaderTemplate), typeof(DataTemplate), typeof(RecycleItemsView), null, validateValue: (b, v) => ((RecycleItemsView)b).ValidateItemTemplate((DataTemplate)v), propertyChanged: (b, o, n) => ((RecycleItemsView)b).UpdateHeaderElement());
        /// <summary>
        /// Backing store for the FooterTemplate property.
        /// </summary>
        public static readonly BindableProperty FooterTemplateProperty = BindableProperty.Create(nameof(FooterTemplate), typeof(DataTemplate), typeof(RecycleItemsView), null, validateValue: (b, v) => ((RecycleItemsView)b).ValidateItemTemplate((DataTemplate)v), propertyChanged: (b, o, n) => ((RecycleItemsView)b).UpdateFooterElement());

        static readonly BindableProperty HeaderElementProperty = BindableProperty.Create(nameof(HeaderElement), typeof(View), typeof(RecycleItemsView), null, propertyChanging: (b, o, n) => ((RecycleItemsView)b).OnHeaderElementChanging(), propertyChanged: (b, o, n) => ((RecycleItemsView)b).OnHeaderElementChanged());
        static readonly BindableProperty FooterElementProperty = BindableProperty.Create(nameof(FooterElement), typeof(View), typeof(RecycleItemsView), null, propertyChanging: (b, o, n) => ((RecycleItemsView)b).OnFooterElementChanging(), propertyChanged: (b, o, n) => ((RecycleItemsView)b).OnFooterElementChanged());


        static readonly BindablePropertyKey FocusedViewPropertyKey = BindableProperty.CreateReadOnly("FocusedView", typeof(View), typeof(RecycleItemsView), null);
        static readonly BindableProperty FocusedViewProperty = FocusedViewPropertyKey.BindableProperty;


        IList<ItemContext> _itemsContext;
        ContentLayout _contentLayout;
        LinkedList<View> _recycleViews = new LinkedList<View>();
        Dictionary<View, ItemContext> _viewToItemTable = new Dictionary<View, ItemContext>();
        Rectangle _lastViewPort;
        int _lastStart = -1;
        int _lastEnd = -1;
        int _focusedItemIndex = -1;

        /// <summary>
        /// Creates and initializes a new instance of the RecycleItemsView class.
        /// </summary>
        public RecycleItemsView()
        {
            InitializeComponent();
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
        /// Gets or sets a value that represents the height of an item.
        /// </summary>
        public double ItemHeight
        {
            get { return (double)GetValue(ItemHeightProperty); }
            set { SetValue(ItemHeightProperty, value); }
        }

        /// <summary>
        /// Gets or sets the value which indicates the direction which items are positioned.
        /// </summary>
        public RecycleItemsViewOrientation Orientation
        {
            get { return (RecycleItemsViewOrientation)GetValue(OrientationProperty); }
            set { SetValue(OrientationProperty, value); }
        }

        /// <summary>
        /// Gets or sets a value which indicates the amount of space between each item.
        /// </summary>
        public double Spacing
        {
            get { return (double)GetValue(SpacingProperty); }
            set { SetValue(SpacingProperty, value); }
        }

        /// <summary>
        /// Gets or sets the source of items to template and display.
        /// </summary>
        public IEnumerable ItemsSource
        {
            get { return (IEnumerable)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        /// <summary>
        /// Gets or sets the DataTemplate to apply to the ItemsSource.
        /// </summary>
        public DataTemplate ItemTemplate
        {
            get { return (DataTemplate)GetValue(ItemTemplateProperty); }
            set { SetValue(ItemTemplateProperty, value); }
        }

        /// <summary>
        /// Gets or sets the currently focused item from the ItemsSource.
        /// </summary>
        public object FocusedItem
        {
            get { return GetValue(FocusedItemProperty); }
            set { SetValue(FocusedItemProperty, value); }
        }

        /// <summary>
        /// Gets or sets the currently selected item from the ItemsSource.
        /// </summary>
        public object SelectedItem
        {
            get { return GetValue(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); }
        }

        /// <summary>
        /// Gets or sets the margin for the content.
        /// </summary>
        public Thickness ContentMargin
        {
            get { return (Thickness)GetValue(ContentMarginProperty); }
            set { SetValue(ContentMarginProperty, value); }
        }

        /// <summary>
        /// Gets or sets the count of columns.
        /// </summary>
        public int ColumnCount
        {
            get { return (int)GetValue(ColumnCountProperty); }
            set { SetValue(ColumnCountProperty, value); }
        }

        /// <summary>
        /// Gets or sets the binding, or view that will be displayed at the first of the items.
        /// </summary>
        public object Header
        {
            get { return GetValue(HeaderProperty); }
            set { SetValue(HeaderProperty, value); }
        }

        /// <summary>
        /// Gets or sets a data template to use to format a data object for display Header.
        /// </summary>
        public DataTemplate HeaderTemplate
        {
            get { return (DataTemplate)GetValue(HeaderTemplateProperty); }
            set { SetValue(HeaderTemplateProperty, value); }
        }

        /// <summary>
        /// Gets instantiated Header view
        /// </summary>
        public View HeaderElement
        {
            get { return (View)GetValue(HeaderElementProperty); }
            protected set { SetValue(HeaderElementProperty, value); }
        }

        /// <summary>
        /// Gets or sets the binding, or view that will be displayed at the last of the items.
        /// </summary>
        public object Footer
        {
            get { return GetValue(FooterProperty); }
            set { SetValue(FooterProperty, value); }
        }

        /// <summary>
        /// Gets or sets a data template to use to format a data object for display Footer.
        /// </summary>
        public DataTemplate FooterTemplate
        {
            get { return (DataTemplate)GetValue(FooterTemplateProperty); }
            set { SetValue(FooterTemplateProperty, value); }
        }

        /// <summary>
        /// Gets instantiated Footer view
        /// </summary>
        public View FooterElement
        {
            get { return (View)GetValue(FooterElementProperty); }
            protected set { SetValue(FooterElementProperty, value); }
        }

        /// <summary>
        /// Gets or sets the scroll position policy.
        /// </summary>
        public ScrollToPosition ScrollPolicy
        {
            get { return (ScrollToPosition)GetValue(ScrollPolicyProperty); }
            set { SetValue(ScrollPolicyProperty, value); }
        }

        /// <summary>
        /// Gets or sets the scroll bar visibility.
        /// </summary>
        public ScrollBarVisibility ScrollBarVisibility
        {
            get { return (ScrollBarVisibility)GetValue(ScrollBarVisibilityProperty); }
            set { SetValue(ScrollBarVisibilityProperty, value); }
        }

        /// <summary>
        /// declare the number of items to make redundant.
        /// </summary>
        protected virtual int RedundantItemCount
        {
            get { return 5; }
        }

        /// <summary>
        /// Gets the ScrollView internally created.
        /// </summary>
        protected ScrollView ScrollView
        {
            get;
            private set;
        }

        Size ContentSize
        {
            get
            {
                if (IsHorizontal)
                {
                    return new Size
                    {
                        Width = (ItemWidth + Spacing) * RowsCount - Spacing + HeaderSizeWithSpacing + FooterSizeWithSpacing,
                        Height = (ItemHeight + Spacing) * ColumnCount - Spacing
                    };
                }
                else
                {
                    return new Size
                    {
                        Width = (ItemWidth + Spacing) * ColumnCount - Spacing,
                        Height = (ItemHeight + Spacing) * RowsCount - Spacing + HeaderSizeWithSpacing + FooterSizeWithSpacing
                    };
                }
            }
        }

        View IRecycleItemsViewController.FocusedView => (View)GetValue(FocusedViewProperty);
        Rectangle IRecycleItemsViewController.ScrollBounds => ViewPort;

        Rectangle ViewPort => new Rectangle(ScrollView.ScrollX, ScrollView.ScrollY, ScrollView.Width, ScrollView.Height);

        double MaringForViewPort
        {
            get { return Math.Max(100, ItemWidth * RedundantItemCount); }
        }

        Rectangle ViewPortWithMargin
        {
            get
            {
                var viewport = ViewPort;
                if (IsHorizontal)
                {
                    viewport.X = Math.Max(viewport.X - MaringForViewPort, 0);
                    viewport.Width += (MaringForViewPort * 2);
                }
                else
                {
                    viewport.Y = Math.Max(viewport.Y - MaringForViewPort, 0);
                    viewport.Height += (MaringForViewPort * 2);
                }
                return viewport;
            }
        }

        double ViewPortStartPoint
        {
            get
            {
                if (IsHorizontal)
                {
                    return ViewPortWithMargin.X;
                }
                else
                {
                    return ViewPortWithMargin.Y;
                }
            }
        }

        double ViewPortSize
        {
            get
            {
                if (IsHorizontal)
                {
                    return ViewPortWithMargin.Width;
                }
                else
                {
                    return ViewPortWithMargin.Height;
                }
            }
        }

        double HeaderSize { get; set; }
        double HeaderSizeWithSpacing => HeaderSize == 0 ? 0 : HeaderSize + Spacing;
        double FooterSize { get; set; }
        double FooterSizeWithSpacing => FooterSize == 0 ? 0 : FooterSize + Spacing;

        double ItemSize => IsHorizontal ? ItemWidth + Spacing : ItemHeight + Spacing;
        double ColumnSize => IsHorizontal? ItemHeight + Spacing : ItemWidth + Spacing;

        int ItemsCount {
            get
            {
                var count = _itemsContext.Count;
                if (HasHeader)
                    count -= 1;
                if (HasFooter)
                    count -= 1;
                return count;
            }
        }
        bool IsHorizontal => Orientation == RecycleItemsViewOrientation.Horizontal;

        int RowsCount => (int)Math.Ceiling(ItemsCount / (double)ColumnCount);

        bool HasFooter => FooterElement != null;
        bool HasHeader => HeaderElement != null;

        bool HasFocusOnHeader => HasHeader && FocusedItemIndex == 0;
        bool HasFocusOnFooter => HasFooter && FocusedItemIndex == _itemsContext.Count - 1;

        int FocusedItemIndex
        {
            get => _focusedItemIndex;
            set
            {
                _focusedItemIndex = value;
                UpdateFocus(_focusedItemIndex);
            }
        }

        /// <summary>
        /// Event that is raised when a new item is selected.
        /// </summary>
        public event EventHandler<SelectedItemChangedEventArgs> ItemSelected;

        /// <summary>
        /// Event that is raised when a item's view is attached the RecycleItemsView.
        /// </summary>
        public event EventHandler<ItemRealizedEventArgs> ItemRealized;

        /// <summary>
        /// Event that is raised when a item's view is detached the RecycleItemsView.
        /// </summary>
        public event EventHandler<ItemUnrealizedEventArgs> ItemUnrealized;

        /// <summary>
        /// Returns a task that scrolls the scroll view to an item asynchronously.
        /// </summary>
        /// <param name="item">The item to scroll.</param>
        /// <param name="position">The scroll position.</param>
        /// <param name="animation">Whether or not to animate the scroll.</param>
        /// <returns>Task</returns>
        public Task ScrollToAsync(object item, ScrollToPosition position, bool animation)
        {
            var point = GetScrollPositionForItem(GetItemIndex(item), ScrollPolicy);
            return ScrollView.ScrollToAsync(point.X, point.Y, animation);
        }

        /// <summary>
        /// Returns a task that scrolls the scroll view to a position asynchronously.
        /// </summary>
        /// <param name="x">The X position of the finished scroll.</param>
        /// <param name="y">The Y position of the finished scroll.</param>
        /// <param name="animation">Whether or not to animate the scroll.</param>
        /// <returns>Task</returns>
        public Task ScrollToAsync(double x, double y, bool animation)
        {
            return ScrollView.ScrollToAsync(x, y, animation);
        }

        bool IRecycleItemsViewController.SendKeyDown(string keyname)
        {
            if (ItemsCount == 0)
                return false;

            string prevRows = IsHorizontal ? "Left" : "Up";
            string nextRows = IsHorizontal ? "Right" : "Down";
            string prevCols = IsHorizontal ? "Up" : "Left";
            string nextCols = IsHorizontal ? "Down" : "Right";

            Point rowcols = IndexToPosition(FocusedItemIndex);
            int rows = (int)rowcols.X;
            int cols = (int)rowcols.Y;

            if (keyname == prevRows)
            {
                if (HasFocusOnHeader)
                    return false;
                if (rows == 0 && !HasHeader)
                    return false;

                FocusedItemIndex = Math.Max(0, PositionToIndex(rows - 1, cols));
                return true;
            }
            else if (keyname == nextRows)
            {
                if (FocusedItemIndex == _itemsContext.Count - 1)
                    return false;

                if (rows == RowsCount - 1 && !HasFooter)
                    return false;

                FocusedItemIndex = Math.Min(PositionToIndex(rows + 1, cols), _itemsContext.Count - 1);
                return true;
            }
            else if (keyname == nextCols)
            {
                if (HasFocusOnHeader || HasFocusOnFooter)
                    return false;

                if (cols == ColumnCount - 1)
                    return false;

                FocusedItemIndex = Math.Min(PositionToIndex(rows, cols + 1), _itemsContext.Count - 1);
                return true;
            }
            else if (keyname == prevCols)
            {
                if (HasFocusOnHeader || HasFocusOnFooter)
                    return false;

                if (cols == 0)
                    return false;

                FocusedItemIndex = Math.Max(0, PositionToIndex(rows, cols - 1));
                return true;
            }
            else if (keyname == "Return")
            {
                if (FocusedItem != null)
                {
                    SelectedItem = FocusedItem;
                    return true;
                }
                FocusedItem = _itemsContext[FocusedItemIndex].Data;
                return true;
            }
            return false;
        }

        protected void InitializeComponent()
        {
            _contentLayout = new ContentLayout(this)
            {
                Padding = ContentMargin
            };

            ScrollView = CreateScrollView();
            ScrollView.Orientation = IsHorizontal ? ScrollOrientation.Horizontal : ScrollOrientation.Vertical;
            ScrollView.Scrolled += OnScrolled;
            ScrollView.Content = _contentLayout;

            ScrollView.Focused += OnScrollViewFocused;
            ScrollView.Unfocused += OnScrollViewFocused;
            Focused += OnFocused;

            UpdateScrollBarVisibility();

            Content = ScrollView;
            _itemsContext = new List<ItemContext>();
        }

        /// <summary>
        /// Create a ScrollView that used in RecycleItemsView.
        /// </summary>
        /// <returns>ScrollView</returns>
        protected virtual ScrollView CreateScrollView()
        {
            return new ScrollView();
        }

        /// <summary>
        /// Called when Item is focused
        /// </summary>
        /// <param name="data">The focused item from the ItemsSource.</param>
        /// <param name="targetView">Instantiated View</param>
        /// <param name="isFocused">Whether or not focused</param>
        protected virtual void OnItemFocused(object data, View targetView, bool isFocused)
        {
            if (isFocused)
            {
                targetView.ScaleTo(1.2, 200);
            }
            else
            {
                targetView.ScaleTo(1.0, 200);
            }
        }

        protected override void LayoutChildren(double x, double y, double width, double height)
        {
            base.LayoutChildren(x, y, width, height);
            LayoutItems();
        }

        protected override SizeRequest OnMeasure(double widthConstraint, double heightConstraint)
        {
            var request = base.OnMeasure(widthConstraint, heightConstraint);
            request.Minimum = new Size
            {
                Width = Math.Max(ItemWidth + ContentMargin.HorizontalThickness, request.Minimum.Width),
                Height = Math.Max(ItemHeight + ContentMargin.VerticalThickness, request.Minimum.Height)
            };
            return request;
        }

        void SendItemFocused(object data, View targetView, bool isFocused)
        {
            if (isFocused && data == FocusedItem)
            {
                var item = GetItemContext(data);
                if (item.IsRealized && item.RealizedView == targetView)
                    SetValue(FocusedViewPropertyKey, targetView);
            }
            OnItemFocused(data, targetView, isFocused);
        }

        bool ShouldReArrange()
        {
            if (_lastViewPort.Size != ViewPort.Size)
                return true;

            var diff = IsHorizontal ? Math.Abs(_lastViewPort.X - ViewPort.X) : Math.Abs(_lastViewPort.Y - ViewPort.Y);

            if (diff > (MaringForViewPort / 2))
                return true;

            return false;
        }

        void RealizeItem(int index)
        {
            var bounds = CalculateCellBounds(index);
            if (HasHeader) index += 1;
            RealizeItem(_itemsContext[index], bounds);
        }

        void RealizeItem(ItemContext item, Rectangle bounds)
        {
            if (item.IsRealized)
                return;

            item.IsRealized = true;
            var view = GetFromRecycleViews();
            if (view != null)
            {
                AbsoluteLayout.SetLayoutBounds(view, bounds);
            }
            else
            {
                view = CreateContent(item.Data);

                view.Focused += OnViewFocused;
                _contentLayout.Children.Add(view, bounds);
            }
            _viewToItemTable[view] = item;

            view.BindingContext = item.Data;
            item.RealizedView = view;

            if (item.Data == FocusedItem)
            {
                SendItemFocused(item.Data, view, true);
            }
            ItemRealized?.Invoke(this, new ItemRealizedEventArgs { Item = item.Data, RealizedView = view });
        }

        void UnrealizeItem(int index)
        {
            if (HasHeader) index += 1;
            UnrealizeItem(_itemsContext[index]);
        }

        void UnrealizeItem(ItemContext item)
        {
            if (!item.IsRealized || item.IsPersistent)
                return;

            item.IsRealized = false;
            var view = item.RealizedView;
            item.RealizedView = null;
            if (view != null)
            {
                if (item.Data == FocusedItem)
                {
                    SendItemFocused(item.Data, view, false);
                }
                _recycleViews.AddLast(view);
                _viewToItemTable.Remove(view);
            }
            ItemUnrealized?.Invoke(this, new ItemUnrealizedEventArgs { Item = item.Data, UnrealizedView = view });
        }

        View GetFromRecycleViews()
        {
            if (_recycleViews.First != null)
            {
                var view = _recycleViews.First;
                _recycleViews.RemoveFirst();
                return view.Value;
            }
            return null;
        }

        void ClearRecycleViews()
        {
            foreach (var view in _recycleViews)
            {
                _contentLayout.Children.Remove(view);
            }
            _recycleViews.Clear();
        }

        View CreateContent(object data)
        {
            if (ItemTemplate != null)
            {
                var content = ItemTemplate.CreateContent();
                if (content is View view) return view;
                else if (content is ViewCell viewCell) return viewCell.View;
                else if (content is ImageCell imageCell) return CreateContent(imageCell);
                else if (content is TextCell textCell) return CreateContent(textCell);
            }
            return CreateContent(new TextCell { Text = data.ToString() });
        }

        View CreateContent(ImageCell cell)
        {
            Label text = new Label
            {
                HorizontalTextAlignment = TextAlignment.Start,
                VerticalTextAlignment = TextAlignment.Start,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                FontAttributes = FontAttributes.Bold,
            };
            Label detailLabel = new Label
            {
                HorizontalTextAlignment = TextAlignment.Start,
                VerticalTextAlignment = TextAlignment.Start,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
            };
            detailLabel.FontSize = Device.GetNamedSize(NamedSize.Micro, detailLabel);

            text.SetBinding(Label.TextProperty, new Binding("Text", source: cell));
            text.SetBinding(Label.TextColorProperty, new Binding("TextColor", source: cell));

            detailLabel.SetBinding(Label.TextProperty, new Binding("Detail", source: cell));
            detailLabel.SetBinding(Label.TextColorProperty, new Binding("DetailColor", source: cell));

            Image image = new Image
            {
                HeightRequest = ItemHeight,
                WidthRequest = ItemWidth,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Aspect = Aspect.Fill
            };
            image.SetBinding(Image.SourceProperty, new Binding("ImageSource", source: cell));

            var view = new AbsoluteLayout();
            view.Children.Add(image, new Rectangle(0, 0, 1, 1), AbsoluteLayoutFlags.All);
            view.Children.Add(new StackLayout
            {
                VerticalOptions = LayoutOptions.EndAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Children = {
                    new StackLayout {
                        VerticalOptions = LayoutOptions.EndAndExpand,
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        Padding = 15,
                        Spacing = 0,
                        BackgroundColor = Color.FromHex("#2b7c87"),
                        Children = { text, detailLabel }
                    }
                }
            }, new Rectangle(0, 0, 1, 1), AbsoluteLayoutFlags.All);

            cell.SetBinding(BindingContextProperty, new Binding("BindingContext", source: view));
            return view;
        }

        View CreateContent(TextCell cell)
        {
            Label text = new Label
            {
                HorizontalTextAlignment = TextAlignment.Start,
                VerticalTextAlignment = TextAlignment.Start,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                FontAttributes = FontAttributes.Bold,
            };
            Label detailLabel = new Label
            {
                HorizontalTextAlignment = TextAlignment.Start,
                VerticalTextAlignment = TextAlignment.Start,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
            };
            detailLabel.FontSize = Device.GetNamedSize(NamedSize.Micro, detailLabel);

            text.SetBinding(Label.TextProperty, new Binding("Text", source: cell));
            text.SetBinding(Label.TextColorProperty, new Binding("TextColor", source: cell));

            detailLabel.SetBinding(Label.TextProperty, new Binding("Detail", source: cell));
            detailLabel.SetBinding(Label.TextColorProperty, new Binding("DetailColor", source: cell));

            var view = new AbsoluteLayout();
            view.Children.Add(new StackLayout
            {
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Children = {
                    new StackLayout {
                        VerticalOptions = LayoutOptions.Center,
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        Padding = 15,
                        Spacing = 0,
                        BackgroundColor = Color.FromHex("#2b7c87"),
                        Children = { text, detailLabel }
                    }
                }
            }, new Rectangle(0, 0, 1, 1), AbsoluteLayoutFlags.All);

            cell.SetBinding(BindingContextProperty, new Binding("BindingContext", source: view));
            return view;
        }

        Rectangle CalculateCellBounds(int index)
        {
            int rowIndex = index / ColumnCount;
            int colIndex = index % ColumnCount;

            if (IsHorizontal)
                return new Rectangle(HeaderSizeWithSpacing + rowIndex * ItemSize, colIndex * ColumnSize, ItemWidth, ItemHeight);
            else
                return new Rectangle(colIndex * ColumnSize, HeaderSizeWithSpacing + rowIndex * ItemSize, ItemWidth, ItemHeight);
        }

        void LayoutInvalidate()
        {
            _lastViewPort = new Rectangle(0, 0, -1, -1);
            _lastEnd = _lastStart = -1;

            foreach (var item in _itemsContext)
            {
                if (item.IsRealized && !item.IsPersistent)
                {
                    UnrealizeItem(item);
                }
            }

            InvalidateLayout();
            LayoutItems();
        }

        void LayoutItems()
        {
            if (ViewPort.Height <= 0 || ViewPort.Width <= 0 || ItemsCount <= 0 || !ShouldReArrange())
            {
                ClearRecycleViews();
                return;
            }

            _lastViewPort = ViewPort;

            int startIndex = (int)(Math.Max(ViewPortStartPoint - HeaderSize, 0) / (ItemSize / ColumnCount) );
            int endIndex = startIndex + (int)Math.Ceiling(ViewPortSize / (ItemSize / ColumnCount));

            startIndex = Math.Min(ItemsCount - 1, startIndex);
            endIndex = Math.Min(ItemsCount - 1, endIndex);

            if (_lastStart != -1 && _lastEnd != -1)
            {
                if (_lastStart < startIndex)
                {
                    for (int i = _lastStart; i <= Math.Min(Math.Max(startIndex - 1, 0), _lastEnd); i++)
                    {
                        UnrealizeItem(i);
                    }
                }

                if (_lastEnd > endIndex)
                {
                    for (int i = endIndex + 1; i <= _lastEnd; i++)
                    {
                        UnrealizeItem(i);
                    }
                }
            }

            for (int i = startIndex; i <= endIndex; i++)
            {
                RealizeItem(i);
            }
            _lastStart = startIndex;
            _lastEnd = endIndex;
            ClearRecycleViews();
        }

        bool ValidateItemTemplate(DataTemplate template)
        {
            if (template == null)
                return true;
            if (template is DataTemplateSelector)
                return false;


            object content = template.CreateContent();
            if (content is View)
                return true;
            else if (content is Cell)
                return true;

            return false;
        }

        ItemContext GetItemContext(object data)
        {
            return _itemsContext.FirstOrDefault(d => d.Data == data);
        }

        int GetItemIndex(object data)
        {
            for (int i = 0; i < _itemsContext.Count; i++)
            {
                if (_itemsContext[i].Data == data)
                {
                    return i;
                }
            }
            return -1;
        }

        Point GetScrollPositionForItem(int index, ScrollToPosition pos)
        {
            ScrollToPosition position = pos;
            Rectangle bound;
            if (index == 0 && HasHeader)
            {
                bound = HeaderElement.Bounds;
            }
            else if (index == _itemsContext.Count -1 && HasFooter)
            {
                bound = FooterElement.Bounds;
            }
            else
            {
                bound = CalculateCellBounds(index + (HasHeader ? -1 : 0));
            }

            double y = bound.Y;
            double x = bound.X;

            if (position == ScrollToPosition.MakeVisible)
            {
                if (ViewPort.Contains(bound))
                    return new Point(ScrollView.ScrollX, ScrollView.ScrollY);
                switch (ScrollView.Orientation)
                {
                    case ScrollOrientation.Vertical:
                        position = y > ScrollView.ScrollY ? ScrollToPosition.End : ScrollToPosition.Start;
                        break;
                    case ScrollOrientation.Horizontal:
                        position = x > ScrollView.ScrollX ? ScrollToPosition.End : ScrollToPosition.Start;
                        break;
                    case ScrollOrientation.Both:
                        position = x > ScrollView.ScrollX || y > ScrollView.ScrollY ? ScrollToPosition.End : ScrollToPosition.Start;
                        break;
                }
            }
            switch (position)
            {
                case ScrollToPosition.Center:
                    y = y - Height / 2 + bound.Height / 2;
                    x = x - Width / 2 + bound.Width / 2;
                    break;
                case ScrollToPosition.End:
                    y = y - Height + bound.Height;
                    x = x - Width + bound.Width;
                    break;
            }
            return new Point(x, y);
        }

        void OnViewFocused(object sender, FocusEventArgs e)
        {
            if (e.IsFocused)
            {
                // workaround, When view was focused, scroll does not working
                Device.BeginInvokeOnMainThread(() =>
                {
                    if (sender is View view && view.IsFocused)
                    {
                        FocusedItem = _viewToItemTable[(View)sender].Data;
                    }
                });
            }
        }

        void OnFocused(object sender, FocusEventArgs e)
        {
            ScrollView.Focus();
        }

        void OnScrollViewFocused(object sender, FocusEventArgs e)
        {
            if (e.IsFocused && ItemsCount > 0)
            {
                if (FocusedItemIndex == -1)
                {
                    FocusedItemIndex = 0;
                }
                else if (FocusedItemIndex >= _itemsContext.Count)
                {
                    FocusedItemIndex = _itemsContext.Count - 1;
                }
                else
                {
                    // To update scrolled area
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        // To trigger UpdateFocus
                        FocusedItemIndex = _focusedItemIndex;
                    });
                }
            }
            else
            {
                FocusedItem = null;
            }
        }

        void OnScrolled(object sender, ScrolledEventArgs e)
        {
            LayoutItems();
        }

        void OnFocusedItemChanging()
        {
            if (FocusedItem != null && GetItemContext(FocusedItem)?.RealizedView != null)
            {
                SendItemFocused(FocusedItem, GetItemContext(FocusedItem).RealizedView, false);
            }
        }

        async void UpdateFocus(int focusedIndex)
        {
            if (focusedIndex < 0 || focusedIndex >= _itemsContext.Count)
                return;

            var context = _itemsContext[focusedIndex];
            FocusedItem = context.Data;

            if (!context.IsRealized)
            {
                var point = GetScrollPositionForItem(focusedIndex, ScrollPolicy);
                await ScrollView.ScrollToAsync(point.X, point.Y, true);
            }
            var view = context.RealizedView;
            if (view != null)
            {
                view.Focus();
                SendItemFocused(FocusedItem, view, true);
                await ScrollView.ScrollToAsync(view, ScrollPolicy, true);
            }
        }

        void OnFocusedItemChanged()
        {
            if (FocusedItem != null)
            {
                FocusedItemIndex = GetItemIndex(FocusedItem);
            }
        }

        void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                int idx = e.NewStartingIndex;
                int headerIndex = HasHeader ? 1 : 0;
                foreach (var item in e.NewItems)
                {
                    _itemsContext.Insert(headerIndex + idx++, new ItemContext { Data = item });
                }
            }
            else if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                int idx = e.OldStartingIndex;
                int headerIndex = HasHeader ? 1 : 0;
                foreach (var item in e.OldItems)
                {
                    var index = headerIndex + idx--;
                    UnrealizeItem(_itemsContext[index]);
                    _itemsContext.RemoveAt(index);
                }
            }
            else if (e.Action == NotifyCollectionChangedAction.Move)
            {
                int headerIndex = HasHeader ? 1 : 0;
                var v = _itemsContext[e.OldStartingIndex + headerIndex];
                _itemsContext.RemoveAt(e.OldStartingIndex + headerIndex);
                _itemsContext.Insert(e.NewStartingIndex + headerIndex, v);
            }
            else if (e.Action == NotifyCollectionChangedAction.Replace)
            {
                int headerIndex = HasHeader ? 1 : 0;
                var old = _itemsContext[headerIndex + e.NewStartingIndex];
                UnrealizeItem(old);
                _itemsContext[headerIndex + e.NewStartingIndex] = new ItemContext { Data = e.NewItems[0] };
            }
            else if (e.Action == NotifyCollectionChangedAction.Reset)
            {
                ItemContext header = null;
                ItemContext footer = null;
                if (HasHeader)
                    header = _itemsContext[0];
                if (HasFooter)
                    footer = _itemsContext[_itemsContext.Count - 1];

                ClearItemContext();

                if (header != null)
                    _itemsContext.Add(header);
                if (footer != null)
                    _itemsContext.Add(footer);
            }
            LayoutInvalidate();
        }

        void UpdateContentMargin()
        {
            _contentLayout.Padding = ContentMargin;
        }

        void UpdateSelectedItem()
        {
            if (FocusedItem != SelectedItem)
            {
                FocusedItem = SelectedItem;
            }
            ItemSelected?.Invoke(this, new SelectedItemChangedEventArgs(SelectedItem));
        }

        void UpdateItemTemplate()
        {
            LayoutInvalidate();
        }

        void UpdateItemsSource()
        {
            ItemContext header = null;
            ItemContext footer = null;
            if (HasHeader)
                header = _itemsContext[0];
            if (HasFooter)
                footer = _itemsContext[_itemsContext.Count - 1];

            ClearItemContext();

            if (header != null)
                _itemsContext.Add(header);

            if (ItemsSource != null)
            {
                foreach (var item in ItemsSource)
                {
                    _itemsContext.Add(new ItemContext
                    {
                        Data = item
                    });
                }

                if (ItemsSource is INotifyCollectionChanged collection)
                {
                    collection.CollectionChanged += OnCollectionChanged;
                }
            }
            if (footer != null)
                _itemsContext.Add(footer);
            LayoutInvalidate();
        }

        void UpdateSpacing()
        {
            LayoutInvalidate();
        }

        void UpdateOrientation()
        {
            ScrollView.Orientation = IsHorizontal ? ScrollOrientation.Horizontal : ScrollOrientation.Vertical;
            LayoutInvalidate();
        }

        void UpdateItemSize()
        {
            LayoutInvalidate();
        }

        void UpdateScrollBarVisibility()
        {
            if (IsHorizontal)
            {
                ScrollView.HorizontalScrollBarVisibility = ScrollBarVisibility;
                ScrollView.VerticalScrollBarVisibility = ScrollBarVisibility.Never;
            }
            else
            {
                ScrollView.HorizontalScrollBarVisibility = ScrollBarVisibility.Never;
                ScrollView.VerticalScrollBarVisibility = ScrollBarVisibility;
            }
        }

        void UpdateColumnCount()
        {
            LayoutInvalidate();
        }

        void UpdateHeaderElement()
        {
            if (Header != null && HeaderTemplate != null)
            {
                HeaderElement = (View)HeaderTemplate.CreateContent();
                HeaderElement.BindingContext = Header;
            }
            else if (Header != null && Header is View view)
            {
                HeaderElement = view;
            }
            else
            {
                HeaderElement = null;
            }
        }

        void UpdateFooterElement()
        {
            if (Footer != null && FooterTemplate != null)
            {
                FooterElement = (View)FooterTemplate.CreateContent();
                FooterElement.BindingContext = Footer;
            }
            else if (Footer != null && Footer is View view)
            {
                FooterElement = view;
            }
            else
            {
                FooterElement = null;
            }
        }

        void OnHeaderElementChanging()
        {
            if (HeaderElement != null)
            {
                _contentLayout.Children.Remove(HeaderElement);
                _itemsContext.RemoveAt(0);
            }
        }

        void OnHeaderElementChanged()
        {
            if (HeaderElement != null)
            {
                double widthConstraint = IsHorizontal ? double.PositiveInfinity : ItemWidth;
                double heightConstraint = IsHorizontal ? ItemHeight : double.PositiveInfinity;
                var size = HeaderElement.Measure(widthConstraint, heightConstraint);
                HeaderSize = IsHorizontal ? size.Request.Width : size.Request.Height;

                AbsoluteLayoutFlags layoutFlags = AbsoluteLayoutFlags.YProportional | AbsoluteLayoutFlags.XProportional;
                Rectangle bounds = new Rectangle(0, 0, 0, 0);

                if (IsHorizontal)
                {
                    bounds.Width = HeaderSize;
                    bounds.Height = 1.0;
                    bounds.Y = 0.5;
                    layoutFlags |= AbsoluteLayoutFlags.HeightProportional;
                }
                else
                {
                    bounds.Height = HeaderSize;
                    bounds.Width = 1.0;
                    bounds.X = 0.5;
                    layoutFlags |= AbsoluteLayoutFlags.WidthProportional;
                }

                _contentLayout.Children.Add(HeaderElement, bounds, layoutFlags);

                var item = new ItemContext
                {
                    Data = Header,
                    RealizedView = HeaderElement,
                    IsRealized = true,
                    IsPersistent = true,
                };
                _itemsContext.Insert(0, item);
                _viewToItemTable[HeaderElement] = item;
            }
            else
            {
                HeaderSize = 0.0;
            }
            LayoutInvalidate();
        }

        void OnFooterElementChanging()
        {
            if (FooterElement != null)
            {
                _contentLayout.Children.Remove(FooterElement);
                _itemsContext.RemoveAt(_itemsContext.Count - 1);
            }
        }

        void OnFooterElementChanged()
        {
            if (FooterElement != null)
            {
                double widthConstraint = IsHorizontal ? double.PositiveInfinity : ItemWidth;
                double heightConstraint = IsHorizontal ? ItemHeight : double.PositiveInfinity;
                var size = FooterElement.Measure(widthConstraint, heightConstraint);
                FooterSize = IsHorizontal ? size.Request.Width : size.Request.Height;

                Rectangle bounds = new Rectangle(1, 1, ItemWidth, ItemHeight);
                AbsoluteLayoutFlags layoutFlags = AbsoluteLayoutFlags.YProportional | AbsoluteLayoutFlags.XProportional;

                if (IsHorizontal)
                {
                    bounds.Width = FooterSize;
                    bounds.Height = 1.0;
                    bounds.Y = 0.5;
                    layoutFlags |= AbsoluteLayoutFlags.HeightProportional;
                }
                else
                {
                    bounds.Height = FooterSize;
                    bounds.Width = 1.0;
                    bounds.X = 0.5;
                    layoutFlags |= AbsoluteLayoutFlags.WidthProportional;
                }
                _contentLayout.Children.Add(FooterElement, bounds, layoutFlags);
                var item = new ItemContext
                {
                    Data = Footer,
                    RealizedView = FooterElement,
                    IsRealized = true,
                    IsPersistent = true,
                };
                _itemsContext.Add(item);
                _viewToItemTable[FooterElement] = item;
            }
            else
            {
                FooterSize = 0.0;
            }
        }

        int PositionToIndex(int row, int cols)
        {
            if (row == -1)
                return 0;
            return row * ColumnCount + cols + (HasHeader ? 1 : 0);
        }

        Point IndexToPosition(int index)
        {
            if (HasHeader)
                index -= 1;
            if (index == -1 && HasHeader)
            {
                return new Point(-1, 0);
            }
            return new Point(index / ColumnCount, index % ColumnCount);
        }

        void ClearItemContext()
        {
            _focusedItemIndex = -1;
            foreach (var item in _itemsContext)
            {
                if (item.IsRealized && !item.IsPersistent)
                {
                    UnrealizeItem(item);
                }
            }
            ClearRecycleViews();
            _itemsContext.Clear();
        }

        internal class ContentLayout : AbsoluteLayout, IRecycleItemsViewController
        {
            RecycleItemsView RecycleItemsView { get; }

            public View FocusedView => ((IRecycleItemsViewController)RecycleItemsView).FocusedView;

            public Rectangle ScrollBounds => ((IRecycleItemsViewController)RecycleItemsView).ScrollBounds;

            public ContentLayout(RecycleItemsView itemsView)
            {
                RecycleItemsView = itemsView;
            }

            protected override SizeRequest OnMeasure(double widthConstraint, double heightConstraint)
            {
                return new SizeRequest(RecycleItemsView.ContentSize);
            }

            public bool SendKeyDown(string keyname)
            {
                return ((IRecycleItemsViewController)RecycleItemsView).SendKeyDown(keyname);
            }
        }
    }

    /// <summary>
    /// Event arguments for the ItemRealized event.
    /// </summary>
    public class ItemRealizedEventArgs : EventArgs
    {
        /// <summary>
        /// The attached item.
        /// </summary>
        public object Item { get; set; }
        /// <summary>
        /// The attached view.
        /// </summary>
        public View RealizedView { get; set; }
    }

    /// <summary>
    /// Event arguments for the ItemUnrealized event.
    /// </summary>
    public class ItemUnrealizedEventArgs : EventArgs
    {
        /// <summary>
        /// The detached item.
        /// </summary>
        public object Item { get; set; }
        /// <summary>
        /// The detached view.
        /// </summary>
        public View UnrealizedView { get; set; }
    }

    class ItemContext
    {
        public bool IsPersistent { get; set; }
        public object Data { get; set; }
        public bool IsRealized { get; set; }
        public View RealizedView { get; set; }
    }

}
