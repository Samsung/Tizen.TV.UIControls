using ElmSharp;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using Xamarin.Forms.Platform.Tizen;
using Xamarin.Forms.Platform.Tizen.Native;

namespace Tizen.TV.UIControls.Forms
{
    class RecycleItemsViewContentRenderer : ViewRenderer<RecycleItemsView.ContentLayout, LayoutCanvas>
    {
        IRecycleItemsViewController ViewController => Element as IRecycleItemsViewController;

        public RecycleItemsViewContentRenderer()
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<RecycleItemsView.ContentLayout> e)
        {
            if (null == Control)
            {
                SetNativeControl(new LayoutCanvas(Xamarin.Forms.Platform.Tizen.Forms.NativeParent));
                Control.AllowFocus(true);
                Control.KeyDown += OnKeyDown;
            }

            base.OnElementChanged(e);
        }

        void OnKeyDown(object sender, EvasKeyEventArgs e)
        {
            if (e.KeyName == "Left" || e.KeyName == "Right" || e.KeyName == "Up" || e.KeyName == "Down" || e.KeyName == "Return")
            {
                if (ViewController?.SendKeyDown(e.KeyName) ?? false)
                {
                    e.Flags = EvasEventFlag.OnHold;
                }
            }
        }
    }

    public class LayoutCanvas : ElmSharp.Layout, IContainable<EvasObject>
    {
        /// <summary>
        /// The list of Views.
        /// </summary>
        readonly ObservableCollection<EvasObject> _children = new ObservableCollection<EvasObject>();
        Xamarin.Forms.Platform.Tizen.Native.Box _box;


        /// <summary>
        /// Initializes a new instance of the <see cref="Xamarin.Forms.Platform.Tizen.Native.Canvas"/> class.
        /// </summary>
        /// <remarks>Canvas doesn't support replacing its children, this will be ignored.</remarks>
        /// <param name="parent">Parent of this instance.</param>
        public LayoutCanvas(EvasObject parent) : base(parent)
        {
            SetTheme("layout", "background", "default");
            _box = new Xamarin.Forms.Platform.Tizen.Native.Box(parent);
            SetContent(_box);

            _children.CollectionChanged += (o, e) =>
            {
                if (e.Action == NotifyCollectionChangedAction.Add)
                {
                    foreach (var v in e.NewItems)
                    {
                        var view = v as EvasObject;
                        if (null != view)
                        {
                            OnAdd(view);
                        }
                    }
                }
                else if (e.Action == NotifyCollectionChangedAction.Remove)
                {
                    foreach (var v in e.OldItems)
                    {
                        if (v is EvasObject view)
                        {
                            OnRemove(view);
                        }
                    }
                }
                else if (e.Action == NotifyCollectionChangedAction.Reset)
                {
                    OnRemoveAll();
                }
            };
        }

        /// <summary>
        /// Gets list of native elements that are placed in the canvas.
        /// </summary>
        public new IList<EvasObject> Children
        {
            get
            {
                return _children;
            }
        }

        /// <summary>
        /// Provides destruction for native element and contained elements.
        /// </summary>
        protected override void OnUnrealize()
        {
            foreach (var child in _children)
            {
                child.Unrealize();
            }

            base.OnUnrealize();
        }

        /// <summary>
        /// Adds a new child to a container.
        /// </summary>
        /// <param name="view">Native element which will be added</param>
        void OnAdd(EvasObject view)
        {
            _box.PackEnd(view);
        }

        /// <summary>
        /// Removes a child from a container.
        /// </summary>
        /// <param name="view">Child element to be removed from canvas</param>
        void OnRemove(EvasObject view)
        {
            _box.UnPack(view);
        }

        /// <summary>
        /// Removes all children from a canvas.
        /// </summary>
        void OnRemoveAll()
        {
            _box.UnPackAll();
        }

    }

    class RecycleItemsViewRenderer : LayoutRenderer
    {
        IRecycleItemsViewController ViewController => Element as IRecycleItemsViewController;
        public RecycleItemsViewRenderer()
        {
            RegisterPropertyHandler("FocusedView", UpdateFocusedView);
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Layout> e)
        {
            base.OnElementChanged(e);
            Control.AllowFocus(true);
        }

        void UpdateFocusedView(bool init)
        {
            if (init)
                return;

            Platform.GetRenderer(ViewController.FocusedView)?.NativeView?.RaiseTop();
        }
    }


    class PropagatableButtonRenderer : ButtonRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Button> e)
        {
            base.OnElementChanged(e);
            Control.PropagateEvents = true;
        }
    }


}
