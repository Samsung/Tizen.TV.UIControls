using System;

namespace Sample
{
    class Program : global::Xamarin.Forms.Platform.Tizen.FormsApplication
    {
        protected override void OnCreate()
        {
            base.OnCreate();
            LoadApplication(new App());
        }

        static void Main(string[] args)
        {
            var app = new Program();
            try
            {
                Tizen.TV.UIControls.Forms.Renderer.UIControls.PreInit();
                global::Xamarin.Forms.Platform.Tizen.Forms.Init(app);
                Tizen.TV.UIControls.Forms.Renderer.UIControls.PostInit();

                Tizen.TV.UIControls.Forms.Renderer.UIControls.MainWindowProvider = () => app.MainWindow;
                app.Run(args);
            }
            catch (Exception e)
            {
                System.Console.WriteLine($"Exception : {e.Message}");
            }
        }
    }
}
