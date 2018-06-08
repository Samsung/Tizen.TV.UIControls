using Tizen.TV.UIControls.Forms;
using Tizen.TV.UIControls.Forms.Renderer;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Tizen;

[assembly: ExportRenderer(typeof(OverlayPage), typeof(OverlayPageRenderer))]
[assembly: ExportRenderer(typeof(OverlayMediaView), typeof(OverlayViewRenderer))]
[assembly: ExportRenderer(typeof(MediaView), typeof(MediaViewRenderer))]
[assembly: ExportMediaSourceHandler(typeof(UriMediaSource), typeof(UriMediaSourceHandler))]
[assembly: ExportMediaSourceHandler(typeof(FileMediaSource), typeof(FileMediaSourceHandler))]

[assembly: Dependency(typeof(MediaPlayerImpl))]