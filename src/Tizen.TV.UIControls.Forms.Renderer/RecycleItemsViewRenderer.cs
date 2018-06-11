using ElmSharp;
using Xamarin.Forms.Platform.Tizen;

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
