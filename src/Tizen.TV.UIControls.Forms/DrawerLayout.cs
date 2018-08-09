using System;
using Xamarin.Forms;

namespace Tizen.TV.UIControls.Forms
{
    /// <summary>
    /// Enumeration that specifies the DrawerMode
    /// </summary>
    public enum DrawerMode
    {
        /// <summary>
        /// Content view is resized to open drawer view
        /// </summary>
        Resize,

        /// <summary>
        /// Content view is moved to side
        /// </summary>
        Panning,

        /// <summary>
        /// Drawer view is overlapped above content view
        /// </summary>
        Overlap
    }

    /// <summary>
    /// Enumeration that specifies the DrawerPosition
    /// </summary>
    public enum DrawerPosition
    {
        /// <summary>
        /// Except when in RTL mode, default is left.
        /// </summary>
        Default,

        /// <summary>
        /// Drawer on left side
        /// </summary>
        Left,

        /// <summary>
        /// Drawer on right side
        /// </summary>
        Right,
    }

    /// <summary>
    /// `DrawerLayout` is a kind of `Layout` that acts like a `MasterDetailPage`.
    /// </summary>
    public class DrawerLayout : Layout<View>
    {
        /// <summary>
        /// Identifies the IsOpen bindable property.
        /// </summary>
        public static readonly BindableProperty IsOpenProperty = BindableProperty.Create(nameof(IsOpen), typeof(bool), typeof(DrawerLayout), true, propertyChanged: (b, o, n) => ((DrawerLayout)b).UpdateDrawerState((bool)n));

        /// <summary>
        /// Identifies the DrawerPosition bindable property.
        /// </summary>
        public static readonly BindableProperty DrawerPositionProperty = BindableProperty.Create(nameof(DrawerPosition), typeof(DrawerPosition), typeof(DrawerLayout), DrawerPosition.Default, propertyChanged: (b, o, n) => ((DrawerLayout)b).InvalidateLayout());

        /// <summary>
        /// Identifies the ClosedWidth bindable property.
        /// </summary>
        public static readonly BindableProperty ClosedWidthProperty = BindableProperty.Create(nameof(DrawerClosedWidth), typeof(double), typeof(DrawerLayout), 0d, propertyChanged: (b, o, n) => ((DrawerLayout)b).InvalidateLayout());

        /// <summary>
        /// Identifies the DrawerWidth bindable property.
        /// </summary>
        public static readonly BindableProperty DrawerWidthProperty = BindableProperty.Create(nameof(DrawerWidth), typeof(double), typeof(DrawerLayout), 0d, propertyChanged: (b, o, n) => ((DrawerLayout)b).InvalidateLayout());

        /// <summary>
        /// Identifies the DrawerMode bindable property.
        /// </summary>
        public static readonly BindableProperty DrawerModeProperty = BindableProperty.Create(nameof(DrawerMode), typeof(DrawerMode), typeof(DrawerLayout), DrawerMode.Resize, propertyChanged: (b, o, n) => ((DrawerLayout)b).InvalidateLayout());

        /// <summary>
        /// Identifies the Drawer bindable property.
        /// </summary>
        public static readonly BindableProperty DrawerProperty = BindableProperty.Create(nameof(Drawer), typeof(View), typeof(DrawerLayout), default(View), propertyChanged: (b, o, n) => ((DrawerLayout)b).InvalidateLayout());

        /// <summary>
        /// Identifies the Content bindable property.
        /// </summary>
        public static readonly BindableProperty ContentProperty = BindableProperty.Create(nameof(Content), typeof(View), typeof(DrawerLayout), default(View), propertyChanged: (b, o, n) => ((DrawerLayout)b).InvalidateLayout());

        static readonly BindablePropertyKey OpenRatioPropertyKey = BindableProperty.CreateReadOnly(nameof(OpenRatio), typeof(double), typeof(DrawerLayout), 1.0);

        /// <summary>
        /// Identifies the Content bindable property.
        /// </summary>
        public static readonly BindableProperty OpenRatioProperty = OpenRatioPropertyKey.BindableProperty;

        /// <summary>
        /// Creates and initializes a new instance of the DrawerLayout class.
        /// </summary>
        public DrawerLayout()
        {
            DrawerHolder = new ContentView();
            ContentHolder = new ContentView();
            Children.Add(ContentHolder);
            Children.Add(DrawerHolder);

            ContentHolder.SetBinding(ContentView.ContentProperty, new Binding("Content", source: this));
            DrawerHolder.SetBinding(ContentView.ContentProperty, new Binding("Drawer", source: this));
        }

        /// <summary>
        /// Gets or sets a value that represents position of drawer
        /// </summary>
        public DrawerPosition DrawerPosition
        {
            get
            {
                return (DrawerPosition)GetValue(DrawerPositionProperty);
            }
            set
            {
                SetValue(DrawerPositionProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets a value that represents state of drawer open
        /// </summary>
        public bool IsOpen
        {
            get
            {
                return (bool)GetValue(IsOpenProperty);
            }
            set
            {
                SetValue(IsOpenProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets a value that represents the width of closed drawer.
        /// </summary>
        public double DrawerClosedWidth
        {
            get
            {
                return (double)GetValue(ClosedWidthProperty);
            }
            set
            {
                SetValue(ClosedWidthProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets a value that represents the width of drawer.
        /// </summary>
        public double DrawerWidth
        {
            get
            {
                return (double)GetValue(DrawerWidthProperty);
            }
            set
            {
                SetValue(DrawerWidthProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets a value that represents the drawer mode
        /// </summary>
        public DrawerMode DrawerMode
        {
            get
            {
                return (DrawerMode)GetValue(DrawerModeProperty);
            }
            set
            {
                SetValue(DrawerModeProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets a view to place content area
        /// </summary>
        public View Content
        {
            get
            {
                return (View)GetValue(ContentProperty);
            }
            set
            {
                SetValue(ContentProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets a view to place drawer area
        /// </summary>
        public View Drawer
        {
            get
            {
                return (View)GetValue(DrawerProperty);
            }
            set
            {
                SetValue(DrawerProperty, value);
            }
        }

        /// <summary>
        /// Gets a value that represents persent of drawer open ratio
        /// 0(closed) ~ opened(1.0)
        /// </summary>
        public double OpenRatio
        {
            get { return (double)GetValue(OpenRatioProperty); }
            private set { SetValue(OpenRatioPropertyKey, value); }
        }

        /// <summary>
        /// Event that is raised when drawer was opened.
        /// </summary>
        public event EventHandler DrawerOpened;

        /// <summary>
        /// Event that is raised when drawer was closed.
        /// </summary>
        public event EventHandler DrawerClosed;

        ContentView DrawerHolder { get; }
        ContentView ContentHolder { get; }

        protected override SizeRequest OnMeasure(double widthConstraint, double heightConstraint)
        {
            var contentMeasure = ContentHolder.Measure(widthConstraint, heightConstraint, MeasureFlags.IncludeMargins);
            var drawerMeasure = DrawerHolder.Measure(widthConstraint, heightConstraint, MeasureFlags.IncludeMargins);

            return new SizeRequest(new Size
            {
                Width = contentMeasure.Request.Width + DrawerClosedWidth,
                Height = Math.Max(contentMeasure.Request.Height, drawerMeasure.Request.Height)
            },
            new Size
            {
                Width = contentMeasure.Minimum.Width + DrawerClosedWidth,
                Height = Math.Max(contentMeasure.Minimum.Height, drawerMeasure.Minimum.Height)
            });
        }

        protected override void LayoutChildren(double x, double y, double width, double height)
        {
            var isRTL = (DrawerPosition == DrawerPosition.Right) || 
                        (DrawerPosition == DrawerPosition.Default &&
                            (this as IVisualElementController).EffectiveFlowDirection.IsRightToLeft());

            var drawerMeasure = DrawerHolder.Measure(width, height);

            double drawerWidth = DrawerWidth != 0 ? DrawerWidth : drawerMeasure.Request.Width;
            double drawerWidthOutBound = (drawerWidth - DrawerClosedWidth) * (1 - OpenRatio);
            double drawerWidthInBound = drawerWidth - drawerWidthOutBound;

            double startX = isRTL ? (x + width - (drawerWidthInBound)) : (x - drawerWidthOutBound);

            LayoutChildIntoBoundingRegion(DrawerHolder, new Rectangle(startX, y, drawerWidth, height));

            double contentWidth = DrawerMode == DrawerMode.Resize ? width - drawerWidthInBound : width;
            double contentStartX = isRTL || DrawerMode == DrawerMode.Overlap ? x : (x + drawerWidthInBound);

            LayoutChildIntoBoundingRegion(ContentHolder, new Rectangle(contentStartX, y, contentWidth, height));
        }

        void UpdateDrawerState(bool isOpen)
        {
            double endState = isOpen ? 1 : 0;
            var animation = new Animation((r) =>
            {
                OpenRatio = r;
                InvalidateLayout();
            }, OpenRatio, endState, Easing.SinIn);
            animation.Commit(this, "DrawerOpen", length: (uint)(250 * Math.Abs(endState - OpenRatio)), finished:(f,aborted) =>
            {
                if (!aborted)
                {
                    if (isOpen)
                    {
                        DrawerOpened?.Invoke(this, EventArgs.Empty);
                    }
                    else
                    {
                        DrawerClosed?.Invoke(this, EventArgs.Empty);
                    }
                }
            });
        }
    }
}
