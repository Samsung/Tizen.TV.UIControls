using System;
using ElmSharp;
using Xamarin.Forms.Internals;

namespace Tizen.TV.UIControls.Forms.Renderer
{
    public static class UIControls
    {
        public static readonly string Tag = "TV.UIControls";
        public static Func<Window> MainWindowProvider { get; set; }

        public static void PreInit()
        {
            bool donotrun = false;
            if (donotrun)
            {
                var a = new MediaViewRenderer();
                var b = new OverlayPageRenderer();
                var c = new OverlayViewRenderer();
            }
        }

        public static void PostInit()
        {
            Registrar.RegisterAll(new Type[]
            {
                typeof(ExportMediaSourceHandlerAttribute),
            });
        }
    }
}
