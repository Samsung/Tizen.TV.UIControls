using Xamarin.Forms.Platform.Tizen;
using MMView = Tizen.Multimedia.MediaView;
using Xamarin.Forms.Platform.Tizen.Native;

namespace Tizen.TV.UIControls.Forms.Renderer
{
    public class MediaViewRenderer : ViewRenderer<MediaView, Canvas>, IMediaViewProvider
    {
        MMView _mediaView;
        protected override void OnElementChanged(ElementChangedEventArgs<MediaView> e)
        {
            if (Control == null)
            {
                _mediaView = new MMView(Xamarin.Forms.Platform.Tizen.Forms.NativeParent);
                SetNativeControl(new Canvas(Xamarin.Forms.Platform.Tizen.Forms.NativeParent));
                Control.SetLayoutCallback(OnLayout);
                Control.PackEnd(_mediaView);
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
