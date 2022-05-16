using Microsoft.Maui;
using Microsoft.Maui.Hosting;
using Tizen.Theme.Common;
using Tizen.TV.UIControls.Forms;
using InitOptions = Tizen.TV.UIControls.Forms.InitOptions;

namespace Sample
{
    internal class Program :MauiApplication
    {
        protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();

        static void Main(string[] args)
        {
            var app = new Program();

            UIControls.Init(new InitOptions());
            Tizen.Theme.Common.CommonUI.Init(app);

            app.Run(args);
        }
    }
}
