using Xamarin.Forms.Platform.Tizen;
using MMView = Tizen.Multimedia.MediaView;
using Xamarin.Forms.Platform.Tizen.Native;

namespace Tizen.TV.UIControls.Forms.Renderer
{
    public class MediaViewRenderer : ViewRenderer<MediaView, LayoutCanvas>, IMediaViewProvider
    {
        MMView _mediaView;
        protected override void OnElementChanged(ElementChangedEventArgs<MediaView> e)
        {
            if (Control == null)
            {
                _mediaView = new MMView(Xamarin.Forms.Platform.Tizen.Forms.NativeParent);
                SetNativeControl(new LayoutCanvas(Xamarin.Forms.Platform.Tizen.Forms.NativeParent));
                Control.LayoutUpdated += (s, evt) => OnLayout();
                Control.Children.Add(_mediaView);
                Control.AllowFocus(true);
            }
            base.OnElementChanged(e);
        }

        MMView IMediaViewProvider.GetMediaView()
        {
            return _mediaView;
        }

        void OnLayout()
        {
            _mediaView.Geometry = Control.Geometry;
        }
    }
}
