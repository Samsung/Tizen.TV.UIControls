using Microsoft.Maui.Controls.Compatibility.Hosting;
using Microsoft.Maui.Controls.Hosting;
using Microsoft.Maui.Hosting;
using Sample.Platforms.Tizen;
using Tizen.Theme.Common;
using Tizen.TV.UIControls.Forms;

namespace Sample
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();

            builder
                .UseMauiApp<App>()
                .UseTizenTVUIControls()
                .ConfigureMauiHandlers(handlers =>
                {
                    // This is a workaround for wrong ListView behavior
                    handlers.AddHandler<ListView, TVListViewRenderer>();
                })
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            return builder.Build();
        }
    }
}