using Xamarin.Forms.Platform.Tizen;
using MMView = Tizen.Multimedia.MediaView;
namespace Tizen.TV.UIControls.Forms.Impl
{
    public class MediaViewRenderer : ViewRenderer<MediaView, MMView>
    {
        protected override void OnElementChanged(ElementChangedEventArgs<MediaView> e)
        {
            if (Control == null)
            {
                SetNativeControl(new MMView(Xamarin.Forms.Platform.Tizen.Forms.NativeParent));
            }
            base.OnElementChanged(e);
        }
    }
}
