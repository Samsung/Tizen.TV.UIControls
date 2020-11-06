using Xamarin.Forms;
using Xamarin.Forms.Platform.Tizen;
using Tizen.TV.UIControls.Forms;

namespace TMDb
{
    class Program : FormsApplication
    {
        protected override void OnCreate()
        {
            base.OnCreate();
            LoadApplication(new App());
        }

        static void Main(string[] args)
        {
            var app = new Program();
            var option = new InitializationOptions(app)
            {
                DisplayResolutionUnit = DisplayResolutionUnit.Pixel()
            };
            Forms.Init(option);
            // UIControls.Init() should be called after Forms.Init()
            UIControls.Init(new InitOptions(app));
            FFImageLoading.Forms.Platform.CachedImageRenderer.Init(app);
            app.Run(args);
        }
    }
}
