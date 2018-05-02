using ElmSharp;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Tizen;

using NPage = Xamarin.Forms.Platform.Tizen.Native.Page;

namespace Tizen.TV.UIControls.Forms.Impl
{
    class OverlayPageRenderer : PageRenderer
    {
        NPage Control => NativeView as NPage;
        OverlayPage OverlayPage => Element as OverlayPage;

        public OverlayPageRenderer()
        {
        }
    }
}
