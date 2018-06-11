using System;
using Xamarin.Forms.Platform.Tizen;
using ElmSharp;
using EColor = ElmSharp.Color;
using System.Runtime.InteropServices;
using Xamarin.Forms.Platform.Tizen.Native;

namespace Tizen.TV.UIControls.Forms.Renderer
{
    class OverlayViewRenderer : ViewRenderer<OverlayMediaView, LayoutCanvas>
    {
        EvasObject _overlayHolder;
        protected override void OnElementChanged(ElementChangedEventArgs<OverlayMediaView> e)
        {
            if (Control == null)
            {
                SetNativeControl(new LayoutCanvas(Xamarin.Forms.Platform.Tizen.Forms.NativeParent));
                Control.LayoutUpdated += (s, evt) => OnLayout();
                _overlayHolder = new LayoutCanvas(Xamarin.Forms.Platform.Tizen.Forms.NativeParent)
                {
                    Color = EColor.Transparent
                };
                Control.Children.Add(_overlayHolder);
                Control.AllowFocus(true);
                MakeTransparent();
            }
            base.OnElementChanged(e);
        }

        void OnLayout()
        {
            _overlayHolder.Geometry = Control.Geometry;
        }

        void MakeTransparent()
        {
            try
            {
                evas_object_render_op_set(_overlayHolder.RealHandle, 2);
            }
            catch (Exception e)
            {
                Log.Error(UIControls.Tag, $"Error MakeTransparent : {e.Message}");
            }
        }
        [DllImport("libevas.so.1")]
        internal static extern void evas_object_render_op_set(IntPtr obj, int op);
    }
}
