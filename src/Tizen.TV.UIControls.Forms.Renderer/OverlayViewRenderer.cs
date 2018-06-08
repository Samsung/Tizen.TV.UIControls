using System;
using Xamarin.Forms.Platform.Tizen;
using ElmSharp;
using EColor = ElmSharp.Color;
using System.Runtime.InteropServices;
using Xamarin.Forms.Platform.Tizen.Native;

namespace Tizen.TV.UIControls.Forms.Renderer
{
    class OverlayViewRenderer : ViewRenderer<OverlayMediaView, Canvas>
    {
        EvasObject _overlayHolder;
        protected override void OnElementChanged(ElementChangedEventArgs<OverlayMediaView> e)
        {
            if (Control == null)
            {
                SetNativeControl(new Canvas(Xamarin.Forms.Platform.Tizen.Forms.NativeParent));
                Control.SetLayoutCallback(OnLayout);
                _overlayHolder = new Rectangle(Xamarin.Forms.Platform.Tizen.Forms.NativeParent)
                {
                    Color = EColor.Transparent
                };
                Control.PackEnd(_overlayHolder);
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
