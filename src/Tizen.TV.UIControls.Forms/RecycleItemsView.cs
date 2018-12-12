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
        public static readonly BindableProperty ItemWidthProperty = BindableProperty.Create(nameof(ItemWidth), typeof(double), typeof(RecycleItemsView), 0d, propertyChanged: (b, o, n) => ((RecycleItemsView)b).UpdateItemSize());
        /// <summary>
        /// Backing store for the ItemHeight property.
        /// </summary>
        public static readonly BindableProperty ItemHeightProperty = BindableProperty.Create(nameof(ItemHeight), typeof(double), typeof(RecycleItemsView), 0d, propertyChanged: (b, o, n) => ((RecycleItemsView)b).UpdateItemSize());
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
        /// <summary>
        /// Backing store for the AllowFocusHeader property.
        /// </summary>
        public static readonly BindableProperty AllowFocusHeaderProperty = BindableProperty.Create(nameof(AllowFocusHeader), typeof(bool), typeof(RecycleItemsView), true);
        /// <summary>
        /// Backing store for the AllowFocusFooter property.
        /// </summary>
        public static readonly BindableProperty AllowFocusFooterProperty = BindableProperty.Create(nameof(AllowFocusFooter), typeof(bool), typeof(RecycleItemsView), true);

        static readonly BindableProperty HeaderElementProperty = BindableProperty.Create(nameof(HeaderElement), typeof(View), typeof(RecycleItemsView), null, propertyChanging: (b, o, n) => ((RecycleItemsView)b).OnHeaderElementChanging(), propertyChanged: (b, o, n) => ((RecycleItemsView)b).OnHeaderElementChanged());
        static readonly BindableProperty FooterElementProperty = BindableProperty.Create(nameof(FooterElement), typeof(View), typeof(RecycleItemsView), null, propertyChanging: (b, o, n) => ((RecycleItemsView)b).OnFooterElementChanging(), propertyChanged: (b, o, n) => ((RecycleItemsView)b).OnFooterElementChanged());


        static readonly BindablePropertyKey FocusedViewPropertyKey = BindableProperty.CreateReadOnly("FocusedView", typeof(View), typeof(RecycleItemsView), null);
        static readonly BindableProperty FocusedViewProperty = FocusedViewPropertyKey.BindableProperty;

        static readonly int HeaderIndex = -1;
        static readonly int FooterIndex = -2;
        static readonly int InvalidIndex = -3;

        IList<ItemContext> _itemContexts = new List<ItemContext>();
        ContentLayout _contentLayout;
        LinkedList<View> _recycleViews = new LinkedList<View>();
        Dictionary<View, ItemContext> _viewToItemTable = new Dictionary<View, ItemContext>();
        Rectangle _lastViewPort;
        int _lastStart = -1;
        int _lastEnd = -1;
        int _focusedItemIndex = InvalidIndex;
        bool _itemSizeIsDirty;
        bool _requestLayoutItems;

        SortedSet<ItemContext> _realizedItems;

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
            set
            {
                SetValue(SelectedItemProperty, value);
                ItemSelected?.Invoke(this, new SelectedItemChangedEventArgs(SelectedItem));
            }
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
        /// Gets or sets the focusable on header
        /// </summary>
        public bool AllowFocusHeader
        {
            get { return (bool)GetValue(AllowFocusHeaderProperty); }
            set { SetValue(AllowFocusHeaderProperty, value); }
        }

        /// <summary>
        /// Gets or sets the focusable on footer
        /// </summary>
        public bool AllowFocusFooter
        {
            get { return (bool)GetValue(AllowFocusFooterProperty); }
            set { SetValue(AllowFocusFooterProperty, value); }
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
            get { return Math.Max(100, ItemSize * RedundantItemCount); }
        }

        Rectangle ViewPortWithMargin
        {
            get
            {
                var viewport = ViewPort;
                if (IsHorizontal)
                {
                    viewport.X = Math.Max(viewport.X - MaringForViewPort - HeaderSize, 0);
                    viewport.Width += (MaringForViewPort * 2);
                }
                else
                {
                    viewport.Y = Math.Max(viewport.Y - MaringForViewPort - HeaderSize, 0);
                    viewport.Height += (MaringForViewPort * 2);
                }
                return viewport;
            }
        }

        double ViewPortStartPoint => IsHorizontal ? ViewPortWithMargin.X : ViewPortWithMargin.Y;

        double ViewPortEndPoint => ViewPortStartPoint + ViewPortSize;

        double ViewPortSize => IsHorizontal ? ViewPortWithMargin.Width : ViewPortWithMargin.Height;

        double HeaderSize { get; set; }
        double HeaderSizeWithSpacing => HeaderSize == 0 ? 0 : HeaderSize + Spacing;
        double FooterSize { get; set; }
        double FooterSizeWithSpacing => FooterSize == 0 ? 0 : FooterSize + Spacing;

        double ItemSize => IsHorizontal ? ItemWidth + Spacing : ItemHeight + Spacing;

        double ColumnSize
        {
            get
            {
                if (IsHorizontal)
                {
                    if (ItemHeight > 0)
                    {
                        return ItemHeight + Spacing;
                    }
                    else if (AllocatedHeight > 0)
                    {
                        return (AllocatedHeight - ContentMargin.VerticalThickness + Spacing) / ColumnCount;
                    }
                    else
                    {
                        return 100;
                    }
                }
                else
                {
                    if (ItemWidth > 0)
                    {
                        return ItemWidth + Spacing;
                    }
                    else if (AllocatedWidth > 0)
                    {
                        return (AllocatedWidth - ContentMargin.HorizontalThickness + Spacing) / ColumnCount;
                    }
                    else
                    {
                        return 100;
                    }
                }
            }
        }

        int ItemsCount => _itemContexts.Count;

        bool IsHorizontal => Orientation == RecycleItemsViewOrientation.Horizontal;

        int RowsCount => (int)Math.Ceiling(ItemsCount / (double)ColumnCount);

        bool FirstIsVisible => ViewPort.Contains(_itemContexts.FirstOrDefault()?.RealizedView?.Bounds ?? new Rectangle(-1, -1, -1, -1));
        bool LastIsVisible => ViewPort.Contains(_itemContexts.LastOrDefault()?.RealizedView?.Bounds ?? new Rectangle(-1, -1, -1, -1));

        bool HasFooter => FooterElement != null;
        bool HasHeader => HeaderElement != null;

        bool HasFocusOnHeader => HasHeader && FocusedItemIndex == HeaderIndex;
        bool HasFocusOnFooter => HasFooter && FocusedItemIndex == FooterIndex;

        ItemContext HeaderContext { get; set; }
        ItemContext FooterContext { get; set; }

        int FocusedItemIndex
        {
            get => _focusedItemIndex;
            set
            {
                if (_focusedItemIndex == value)
                    return;
                _focusedItemIndex = value;
                UpdateFocus(_focusedItemIndex);
            }
        }

        double AllocatedHeight { get; set; }
        double AllocatedWidth { get; set; }

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
            if (item == Header)
            {
                return ScrollView.ScrollToAsync(HeaderElement, position, animation);
            }
            else if (item == Footer)
            {
                return ScrollView.ScrollToAsync(FooterElement, position, animation);
            }
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
                    return false || !FirstIsVisible;

                if (HasFocusOnFooter)
                {
                    FocusedItemIndex = ItemsCount - 1;
                    return true;
                }
                // On top of rows
                if (rows == 0)
                {
                    if (HasHeader && AllowFocusHeader)
                    {
                        FocusedItemIndex = HeaderIndex;
                        return true;
                    }
                    else
                    {
                        return false || !FirstIsVisible; // focus out to another view
                    }
                }

                FocusedItemIndex = Math.Max(0, PositionToIndex(rows - 1, cols));
                return true;
            }
            else if (keyname == nextRows)
            {
                if (HasFocusOnFooter)
                    return false || !LastIsVisible;

                if (rows == RowsCount - 1)
                {
                    if (HasFooter && AllowFocusFooter)
                    {
                        FocusedItemIndex = FooterIndex;
                        return true;
                    }
                    else
                    {
                        return false || !LastIsVisible; // focus out to another view
                    }
                }

                FocusedItemIndex = Math.Min(PositionToIndex(rows + 1, cols), _itemContexts.Count - 1);
                return true;
            }
            else if (keyname == nextCols)
            {
                if (HasFocusOnHeader || HasFocusOnFooter)
                    return false;

                if (cols == ColumnCount - 1)
                    return false;

                FocusedItemIndex = Math.Min(PositionToIndex(rows, cols + 1), _itemContexts.Count - 1);
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
                }
                else
                {
                    // no focus anywhere, so set default on first item
                    FocusedItemIndex = 0;
                }
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
            _realizedItems = new SortedSet<ItemContext>(new ItemComparer(_itemContexts));
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

        protected override void OnSizeAllocated(double width, double height)
        {
            _itemSizeIsDirty = true;
            AllocatedWidth = width;
            AllocatedHeight = height;
            base.OnSizeAllocated(width, height);
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
            RealizeItem(_itemContexts[index], bounds);
        }

        void RealizeItem(ItemContext item, Rectangle bounds)
        {
            if (item.IsRealized)
            {
                if (_itemSizeIsDirty)
                {
                    AbsoluteLayout.SetLayoutBounds(item.RealizedView, bounds);
                }
                else if (AbsoluteLayout.GetLayoutBounds(item.RealizedView) != bounds)
                {
                    item.RealizedView.LayoutTo(bounds, 150, Easing.Linear).ConfigureAwait(true).GetAwaiter().OnCompleted(() =>
                    {
                        if (item.IsRealized)
                        {
                            AbsoluteLayout.SetLayoutBounds(item.RealizedView, bounds);
                        }
                    });
                }
                return;
            }
            if (!item.IsPersistent)
            {
                _realizedItems.Add(item);
            }

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
            UnrealizeItem(_itemContexts[index]);
        }

        void UnrealizeItem(ItemContext item)
        {
            if (!item.IsRealized || item.IsPersistent)
                return;

            _realizedItems.Add(item);


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
            {
                return new Rectangle(HeaderSizeWithSpacing + rowIndex * ItemSize, colIndex * ColumnSize, ItemWidth, ColumnSize - Spacing);
            }
            else
            {
                return new Rectangle(colIndex * ColumnSize, HeaderSizeWithSpacing + rowIndex * ItemSize, ColumnSize - Spacing, ItemHeight);
            }
        }

        void LayoutInvalidate()
        {
            _lastViewPort = new Rectangle(0, 0, -1, -1);
            _lastEnd = _lastStart = -1;

            foreach (var item in _itemContexts)
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

            int startIndex = (int)(ViewPortStartPoint / ItemSize) * ColumnCount;
            int endIndex = (int)Math.Ceiling(ViewPortEndPoint / ItemSize) * ColumnCount;

            startIndex = Math.Min(ItemsCount - 1, startIndex);
            endIndex = Math.Min(ItemsCount - 1, endIndex);

            foreach (var realized in _realizedItems.ToList())
            {
                if (_realizedItems.Comparer.Compare(realized, _itemContexts[startIndex]) < 0)
                {
                    UnrealizeItem(realized);
                }
                if (_realizedItems.Comparer.Compare(realized, _itemContexts[endIndex]) > 0)
                {
                    UnrealizeItem(realized);
                }
            }

            for (int i = startIndex; i <= endIndex; i++)
            {
                RealizeItem(i);
            }
            _lastStart = startIndex;
            _lastEnd = endIndex;
            ClearRecycleViews();
            _itemSizeIsDirty = false;
        }

        bool ValidateItemTemplate(DataTemplate template)
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

        ItemContext GetItemContext(object data)
        {
            if (data == Header) return HeaderContext;
            if (data == Footer) return FooterContext;
            return _itemContexts.FirstOrDefault(d => d.Data == data);
        }

        int GetItemIndex(object data)
        {
            if (data == Header) return HeaderIndex;
            if (data == Footer) return FooterIndex;
            for (int i = 0; i < _itemContexts.Count; i++)
            {
                if (_itemContexts[i].Data == data)
                {
                    return i;
                }
            }
            return InvalidIndex;
        }

        Point GetScrollPositionForItem(int index, ScrollToPosition pos)
        {
            if (index < 0 && ItemsCount >= index)
            {
                return new Point(0, 0);
            }

            ScrollToPosition position = pos;
            Rectangle bound = CalculateCellBounds(index);

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
            else if (position == ScrollToPosition.Center)
            {
                y = y - Height / 2 + bound.Height / 2;
                x = x - Width / 2 + bound.Width / 2;
            }
            else if (position == ScrollToPosition.End)
            {
                y = y - Height + bound.Height;
                x = x - Width + bound.Width;
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
                if (FocusedItemIndex == InvalidIndex ||
                    (FocusedItemIndex == HeaderIndex && (!AllowFocusHeader || !HasHeader)) ||
                    (FocusedItemIndex == FooterIndex && (!AllowFocusFooter || !HasFooter)))
                {
                    FocusedItemIndex = 0;
                }
                else if (FocusedItemIndex >= ItemsCount)
                {
                    FocusedItemIndex = ItemsCount - 1;
                }

                // To update scrolled area
                Device.BeginInvokeOnMainThread(() =>
                {
                    var index = _focusedItemIndex;
                    _focusedItemIndex = InvalidIndex;
                    // To trigger UpdateFocus
                    FocusedItemIndex = index;
                });
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
            ItemContext context = null;
            if (focusedIndex == HeaderIndex && HasHeader && AllowFocusHeader)
            {
                context = HeaderContext;
            }
            else if (focusedIndex == FooterIndex && HasFooter && AllowFocusFooter)
            {
                context = FooterContext;
            }
            else if (focusedIndex < 0 || focusedIndex >= ItemsCount)
            {
                return;
            }
            else
            {
                context = _itemContexts[focusedIndex];
            }

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
                foreach (var item in e.NewItems)
                {
                    _itemContexts.Insert(idx++, new ItemContext { Data = item });
                }
            }
            else if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                int idx = e.OldStartingIndex;
                foreach (var item in e.OldItems)
                {
                    UnrealizeItem(_itemContexts[idx]);
                    _itemContexts.RemoveAt(idx);
                }
            }
            else if (e.Action == NotifyCollectionChangedAction.Move)
            {
                var v = _itemContexts[e.OldStartingIndex];
                _itemContexts.RemoveAt(e.OldStartingIndex);
                _itemContexts.Insert(e.NewStartingIndex, v);
            }
            else if (e.Action == NotifyCollectionChangedAction.Replace)
            {
                var old = _itemContexts[e.NewStartingIndex];
                UnrealizeItem(old);
                _itemContexts[e.NewStartingIndex] = new ItemContext { Data = e.NewItems[0] };
            }
            else if (e.Action == NotifyCollectionChangedAction.Reset)
            {
                ClearItemContext();
            }
            RequestLayoutItems();
        }

        void RequestLayoutItems()
        {
            if (!_requestLayoutItems)
            {
                _requestLayoutItems = true;
                Device.BeginInvokeOnMainThread(() =>
                {
                    _requestLayoutItems = false;
                    _lastViewPort = new Rectangle(0, 0, -1, -1);
                    LayoutItems();
                });
            }
        }

        void UpdateContentMargin()
        {
            _contentLayout.Padding = ContentMargin;
        }

        void UpdateSelectedItem()
        {
            FocusedItem = SelectedItem;
        }

        void UpdateItemTemplate()
        {
            LayoutInvalidate();
        }

        void UpdateItemsSource()
        {
            ClearItemContext();

            if (ItemsSource != null)
            {
                foreach (var item in ItemsSource)
                {
                    _itemContexts.Add(new ItemContext
                    {
                        Data = item
                    });
                }

                if (ItemsSource is INotifyCollectionChanged collection)
                {
                    collection.CollectionChanged += OnCollectionChanged;
                }
            }
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
                HeaderContext = null;
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
                HeaderContext = item;
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
                FooterContext = null;
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
                FooterContext = item;
                _viewToItemTable[FooterElement] = item;
            }
            else
            {
                FooterSize = 0.0;
            }
        }

        int PositionToIndex(int row, int cols)
        {
            return row * ColumnCount + cols;
        }

        Point IndexToPosition(int index)
        {
            return new Point(index / ColumnCount, index % ColumnCount);
        }

        void ClearItemContext()
        {
            _focusedItemIndex = InvalidIndex;
            foreach (var item in _itemContexts)
            {
                if (item.IsRealized && !item.IsPersistent)
                {
                    UnrealizeItem(item);
                }
            }
            ClearRecycleViews();
            _itemContexts.Clear();
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

    class ItemComparer : IComparer<ItemContext>
    {
        IList<ItemContext> _container;
        public ItemComparer(IList<ItemContext> container)
        {
            _container = container;
        }
        public int Compare(ItemContext x, ItemContext y)
        {
            return _container.IndexOf(x) - _container.IndexOf(y);
        }
    }



}
