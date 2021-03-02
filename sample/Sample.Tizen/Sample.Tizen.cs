using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Tizen;
using Tizen.TV.UIControls.Forms;
using Tizen.Theme.Common;
using InitOptions = Tizen.TV.UIControls.Forms.InitOptions;

namespace Sample
{
    class Program : FormsApplication
    {
        protected override void OnPreCreate()
        {
            MainWindow = new ElmSharp.Window("forms")
            {
                Alpha = false,
            };
        }
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
                var option = new InitializationOptions(app)
                {
                    //Using DP without device scaling mode
                    DisplayResolutionUnit = DisplayResolutionUnit.DP()
                };
                Forms.Init(option);

                // UIControls.Init() should be called after Forms.Init()
                UIControls.Init(new InitOptions(app));
                CommonUI.Init(app);
                if (Device.Idiom != TargetIdiom.TV)
                {
                    CommonUI.AddCommonThemeOverlay();
                }
                app.Run(args);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Exception : {e.Message}");
            }
        }
    }
}
