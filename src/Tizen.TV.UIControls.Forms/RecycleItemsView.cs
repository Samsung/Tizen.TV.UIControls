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
    public enum RecycleItemsViewOrientation
    {
        Horizontal,
        Vertical,
    }

    public interface IRecycleItemsViewController
    {
        bool SendKeyDown(string keyname);
        View FocusedView { get; }
        Rectangle ScrollBounds { get; }
    }

    public class RecycleItemsView : ContentView, IRecycleItemsViewController
    {
        public static readonly BindableProperty ItemWidthProperty = BindableProperty.Create(nameof(ItemWidth), typeof(double), typeof(RecycleItemsView), 100d, propertyChanged: (b, o, n) => ((RecycleItemsView)b).UpdateItemSize());
        public static readonly BindableProperty ItemHeightProperty = BindableProperty.Create(nameof(ItemHeight), typeof(double), typeof(RecycleItemsView), 100d, propertyChanged: (b, o, n) => ((RecycleItemsView)b).UpdateItemSize());
        public static readonly BindableProperty OrientationProperty = BindableProperty.Create(nameof(Orientation), typeof(RecycleItemsViewOrientation), typeof(RecycleItemsView), RecycleItemsViewOrientation.Horizontal, propertyChanged: (b, o, n) => ((RecycleItemsView)b).UpdateOrientation());
        public static readonly BindableProperty SpacingProperty = BindableProperty.Create(nameof(Spacing), typeof(double), typeof(RecycleItemsView), 6d, propertyChanged: (b, o, n) => ((RecycleItemsView)b).UpdateSpacing());
        public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create(nameof(ItemsSource), typeof(IEnumerable), typeof(RecycleItemsView), null, propertyChanged: (b, o, n) => ((RecycleItemsView)b).UpdateItemsSource());
        public static readonly BindableProperty ItemTemplateProperty = BindableProperty.Create(nameof(ItemTemplate), typeof(DataTemplate), typeof(RecycleItemsView), null, validateValue: (b, v) => ((RecycleItemsView)b).ValidateItemTemplate((DataTemplate)v), propertyChanged: (b, o, n) => ((RecycleItemsView)b).UpdateItemTemplate());
        public static readonly BindableProperty FocusedItemProperty = BindableProperty.Create(nameof(FocusedItem), typeof(object), typeof(RecycleItemsView), null, BindingMode.OneWayToSource, propertyChanging: (b, o, n) => ((RecycleItemsView)b).OnFocusedItemChanging(), propertyChanged: (b, o, n) => ((RecycleItemsView)b).OnFocusedItemChanged());
        public static readonly BindableProperty SelectedItemProperty = BindableProperty.Create(nameof(SelectedItem), typeof(object), typeof(RecycleItemsView), null, BindingMode.OneWayToSource, propertyChanged: (b, o, n) => ((RecycleItemsView)b).UpdateSelectedItem());
        public static readonly BindableProperty ContentMarginProperty = BindableProperty.Create(nameof(ContentMargin), typeof(Thickness), typeof(RecycleItemsView), default(Thickness), propertyChanged: (b, o, n) => ((RecycleItemsView)b).UpdateContentMargin());
        public static readonly BindableProperty ScrollPolicyProperty = BindableProperty.Create(nameof(ScrollPolicy), typeof(ScrollToPosition), typeof(RecycleItemsView), ScrollToPosition.Center);
        public static readonly BindableProperty ScrollBarVisibilityProperty = BindableProperty.Create(nameof(ScrollBarVisibility), typeof(ScrollBarVisibility), typeof(RecycleItemsView), ScrollBarVisibility.Default, propertyChanged: (b, o, n) => ((RecycleItemsView)b).UpdateScrollBarVisibility());

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

        public RecycleItemsView()
        {
            InitializeComponent();
        }

        public double ItemWidth
        {
            get { return (double)GetValue(ItemWidthProperty); }
            set { SetValue(ItemWidthProperty, value); }
        }

        public double ItemHeight
        {
            get { return (double)GetValue(ItemHeightProperty); }
            set { SetValue(ItemHeightProperty, value); }
        }

        public RecycleItemsViewOrientation Orientation
        {
            get { return (RecycleItemsViewOrientation)GetValue(OrientationProperty); }
            set { SetValue(OrientationProperty, value); }
        }

        public double Spacing
        {
            get { return (double)GetValue(SpacingProperty); }
            set { SetValue(SpacingProperty, value); }
        }

        public IEnumerable ItemsSource
        {
            get { return (IEnumerable)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        public DataTemplate ItemTemplate
        {
            get { return (DataTemplate)GetValue(ItemTemplateProperty); }
            set { SetValue(ItemTemplateProperty, value); }
        }

        public object FocusedItem
        {
            get { return GetValue(FocusedItemProperty); }
            set { SetValue(FocusedItemProperty, value); }
        }

        public object SelectedItem
        {
            get { return GetValue(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); }
        }

        public Thickness ContentMargin
        {
            get { return (Thickness)GetValue(ContentMarginProperty); }
            set { SetValue(ContentMarginProperty, value); }
        }

        Size ContentSize
        {
            get
            {
                if (IsHorizontal)
                    return new Size((ItemWidth + Spacing) * ItemsCount, ItemHeight);
                else
                    return new Size(ItemWidth, (ItemHeight + Spacing) * ItemsCount);
            }
        }

        public ScrollToPosition ScrollPolicy
        {
            get { return (ScrollToPosition)GetValue(ScrollPolicyProperty); }
            set { SetValue(ScrollPolicyProperty, value); }
        }

        public ScrollBarVisibility ScrollBarVisibility
        {
            get { return (ScrollBarVisibility)GetValue(ScrollBarVisibilityProperty); }
            set { SetValue(ScrollBarVisibilityProperty, value); }
        }

        protected virtual int RedundantItemCount
        {
            get { return 5; }
        }

        protected ScrollView ScrollView
        {
            get;
            private set;
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

        double ItemSize => IsHorizontal ? ItemWidth + Spacing : ItemHeight + Spacing;

        int ItemsCount => _itemsContext.Count;
        bool IsHorizontal => Orientation == RecycleItemsViewOrientation.Horizontal;


        public event EventHandler<SelectedItemChangedEventArgs> ItemSelected;
        public event EventHandler<ItemRealizedEventArgs> ItemRealized;
        public event EventHandler<ItemUnrealizedEventArgs> ItemUnrealized;

        public Task ScrollToAsync(object item, ScrollToPosition position, bool animation)
        {
            var point = GetScrollPositionForItem(GetItemIndex(item), ScrollPolicy);
            return ScrollView.ScrollToAsync(point.X, point.Y, animation);
        }

        public Task ScrollToAsync(double x, double y, bool animation)
        {
            return ScrollView.ScrollToAsync(x, y, animation);
        }

        bool IRecycleItemsViewController.SendKeyDown(string keyname)
        {
            if (ItemsCount == 0)
                return false;

            string toPrev = IsHorizontal ? "Left" : "Up";
            string toNext = IsHorizontal ? "Right" : "Down";

            if (keyname == toPrev)
            {
                if (_focusedItemIndex == 0)
                {
                    return false;
                }
                else
                {
                    _focusedItemIndex = Math.Max(0, _focusedItemIndex - 1);
                }
                FocusedItem = _itemsContext[_focusedItemIndex].Data;
                return true;
            }
            else if (keyname == toNext)
            {
                if (_focusedItemIndex == ItemsCount - 1)
                {
                    return false;
                }
                _focusedItemIndex = Math.Min(_focusedItemIndex + 1, ItemsCount - 1);
                FocusedItem = _itemsContext[_focusedItemIndex].Data;
                return true;
            }
            else if (keyname == "Return")
            {
                if (FocusedItem != null)
                {
                    SelectedItem = FocusedItem;
                    return true;
                }
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

        protected virtual ScrollView CreateScrollView()
        {
            return new ScrollView();
        }

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

        bool ShouldReArrange()
        {
            if (_lastViewPort.Size != ViewPort.Size)
                return true;

            var diff = IsHorizontal ? Math.Abs(_lastViewPort.X - ViewPort.X) : Math.Abs(_lastViewPort.Y - ViewPort.Y);

            if (diff > (MaringForViewPort / 2))
                return true;

            return false;
        }

        void RealizeItem(ItemContext item, Rectangle bounds)
        {
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
                _contentLayout.Children.Add(view, bounds, IsHorizontal ? AbsoluteLayoutFlags.YProportional : AbsoluteLayoutFlags.XProportional);
            }
            _viewToItemTable[view] = item;

            view.BindingContext = item.Data;
            item.RealizedView = view;

            if (item.Data == FocusedItem)
            {
                OnItemFocused(item.Data, view, true);
            }
            ItemRealized?.Invoke(this, new ItemRealizedEventArgs { Item = item.Data, RealizedView = view });
        }

        void UnrealizeItem(ItemContext item)
        {
            item.IsRealized = false;
            var view = item.RealizedView;
            item.RealizedView = null;
            if (view != null)
            {
                if (item.Data == FocusedItem)
                {
                    OnItemFocused(item.Data, view, false);
                }
                _recycleViews.AddLast(view);
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
            if (IsHorizontal)
                return new Rectangle(index * ItemSize, 0.5, ItemWidth, ItemHeight);
            else
                return new Rectangle(0.5, index * ItemSize, ItemWidth, ItemHeight);
        }

        void LayoutInvalidate()
        {
            _lastViewPort = new Rectangle(0, 0, -1, -1);
            if (_lastStart == -1 || _lastEnd == -1)
                return;

            for (int i = _lastStart; i <= _lastEnd; i++)
            {
                _itemsContext[i].IsRealized = false;
                _itemsContext[i].RealizedView = null;
            }
            _contentLayout.Children.Clear();
            _viewToItemTable.Clear();
            _lastEnd = -1;
            _lastStart = -1;

            LayoutItems();
        }

        void LayoutItems()
        {
            if (ViewPort.Height <= 0 || ViewPort.Width <= 0 || ItemsCount <= 0)
            {
                return;
            }
            if (!ShouldReArrange())
                return;

            _lastViewPort = ViewPort;

            int startIndex = (int)(ViewPortStartPoint / ItemSize);
            int endIndex = startIndex + (int)Math.Ceiling(ViewPortSize / ItemSize);

            startIndex = Math.Min(ItemsCount - 1, startIndex);
            endIndex = Math.Min(ItemsCount - 1, endIndex);

            if (_lastStart != -1 && _lastEnd != -1)
            {
                if (_lastStart < startIndex)
                {
                    for (int i = _lastStart; i <= Math.Min(Math.Max(startIndex - 1, 0), _lastEnd); i++)
                    {
                        if (_itemsContext[i].IsRealized)
                        {
                            UnrealizeItem(_itemsContext[i]);
                        }
                    }
                }

                if (_lastEnd > endIndex)
                {
                    for (int i = endIndex + 1; i <= _lastEnd; i++)
                    {
                        if (_itemsContext[i].IsRealized)
                        {
                            UnrealizeItem(_itemsContext[i]);
                        }
                    }
                }
            }

            for (int i = startIndex; i <= endIndex; i++)
            {
                if (!_itemsContext[i].IsRealized)
                {
                    RealizeItem(_itemsContext[i], CalculateCellBounds(i));
                }
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
            return _itemsContext.First(d => d.Data == data);
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
            var bound = CalculateCellBounds(index);
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
                FocusedItem = _viewToItemTable[(View)sender].Data;
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
                if (_focusedItemIndex == -1)
                    _focusedItemIndex = 0;

                if (_focusedItemIndex >= _itemsContext.Count)
                    _focusedItemIndex = _itemsContext.Count - 1;

                FocusedItem = _itemsContext[_focusedItemIndex].Data;
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
            if (FocusedItem != null && GetItemContext(FocusedItem).RealizedView != null)
            {
                OnItemFocused(FocusedItem, GetItemContext(FocusedItem).RealizedView, false);
            }
        }

        async void OnFocusedItemChanged()
        {
            // To update scroll area
            await Task.Delay(1);

            if (FocusedItem != null)
            {
                var context = GetItemContext(FocusedItem);
                _focusedItemIndex = _itemsContext.IndexOf(context);

                if (!context.IsRealized)
                {
                    var point = GetScrollPositionForItem(_focusedItemIndex, ScrollPolicy);
                    await ScrollView.ScrollToAsync(point.X, point.Y, true);
                }

                var view = context.RealizedView;
                if (view != null)
                {
                    view.Focus();
                    OnItemFocused(FocusedItem, view, true);
                    SetValue(FocusedViewPropertyKey, view);
                    await ScrollView.ScrollToAsync(view, ScrollPolicy, true);
                }
            }
        }

        void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                int idx = e.NewStartingIndex;
                foreach (var item in e.NewItems)
                {
                    _itemsContext.Insert(idx++, new ItemContext { Data = item });
                }
            }
            else if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                int idx = e.OldStartingIndex;
                foreach (var item in e.OldItems)
                {
                    _itemsContext.RemoveAt(idx--);
                }
            }
            else if (e.Action == NotifyCollectionChangedAction.Move)
            {
                var v = _itemsContext[e.OldStartingIndex];
                _itemsContext.RemoveAt(e.OldStartingIndex);
                _itemsContext.Insert(e.NewStartingIndex, v);
            }
            else if (e.Action == NotifyCollectionChangedAction.Replace)
            {
                _itemsContext[e.NewStartingIndex] = new ItemContext { Data = e.NewItems[0] };
            }
            else if (e.Action == NotifyCollectionChangedAction.Reset)
            {
                _itemsContext.Clear();
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
            _itemsContext.Clear();

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

    public class ItemRealizedEventArgs : EventArgs
    {
        public object Item { get; set; }
        public View RealizedView { get; set; }
    }

    public class ItemUnrealizedEventArgs : EventArgs
    {
        public object Item { get; set; }
        public View UnrealizedView { get; set; }
    }

    class ItemContext
    {
        public object Data { get; set; }
        public bool IsRealized { get; set; }
        public View RealizedView { get; set; }
    }

}
