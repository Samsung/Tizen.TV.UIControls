using System;
using Xamarin.Forms.Platform.Tizen;
using ElmSharp;
using EColor = ElmSharp.Color;
using System.Runtime.InteropServices;

namespace Tizen.TV.UIControls.Forms.Impl
{
    class OverlayViewRenderer : ViewRenderer<OverlayMediaView, Box>
    {
        EvasObject _embeddingControls;
        protected override void OnElementChanged(ElementChangedEventArgs<OverlayMediaView> e)
        {
            if (Control == null)
            {
                SetNativeControl(new Box(Xamarin.Forms.Platform.Tizen.Forms.NativeParent));
                Control.Color = EColor.FromRgba(0, 0, 0, 0);
                Control.SetLayoutCallback(OnLayout);
                MakeTransparent();

            }
            base.OnElementChanged(e);
        }

        public void SetEmbeddingControls(EvasObject layout)
        {
            if (_embeddingControls != null)
            {
                Control.UnPack(_embeddingControls);
            }

            _embeddingControls = layout;

            if (layout != null)
            {
                Control.PackEnd(layout);
            }
        }

        void OnLayout()
        {
            if (_embeddingControls != null)
            {
                _embeddingControls.Geometry = Control.Geometry;
            }
        }

        void MakeTransparent()
        {
            evas_object_render_op_set(Control.RealHandle, 2);
            //var propertyInfo = Control.GetType().GetProperty("RenderOperation");
            //propertyInfo?.SetValue(Control, 2);
        }
        [DllImport("libevas.so.1")]
        internal static extern void evas_object_render_op_set(IntPtr obj, int op);
    }
}
