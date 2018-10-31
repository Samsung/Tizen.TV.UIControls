using System;

namespace TMDb
{
    class Program : global::Xamarin.Forms.Platform.Tizen.FormsApplication
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
            Tizen.TV.UIControls.Forms.UIControls.Init();
            global::Xamarin.Forms.Platform.Tizen.Forms.Init(app);
            app.Run(args);
        }
    }
}
