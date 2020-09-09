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
            Tizen.TV.UIControls.Forms.UIControls.MainWindowProvider = () => MainWindow;
            LoadApplication(new App());
        }

        static void Main(string[] args)
        {
            var app = new Program();
            FFImageLoading.Forms.Platform.CachedImageRenderer.Init(app);
            UIControls.Init();
            Forms.Init(app);
            app.Run(args);
        }
    }
}
