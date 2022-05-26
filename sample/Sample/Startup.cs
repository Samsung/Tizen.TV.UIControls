using Microsoft.Maui.Controls.Compatibility.Hosting;
using Microsoft.Maui.LifecycleEvents;
using Tizen.Theme.Common.Renderer;
using Tizen.TV.UIControls.Forms;
using Tizen.TV.UIControls.Forms.Renderer;
using ShadowFramme = Tizen.Theme.Common.ShadowFrame;

namespace Sample
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();

            var services = builder.Services;
            //services.AddSingleton<TIPlatformMediaPlayer, MediaPlayerImpl>();

            builder
                .UseMauiApp<App>()
                .UseMauiCompatibility()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                })
                .ConfigureLifecycleEvents(life =>
                {
                    //DependencyService.Register<IPlatformMediaPlayer, MediaPlayerImpl>();
                })
                .ConfigureMauiHandlers(handlers =>
                {
                    //handlers.AddCompatibilityRenderer(typeof(TGridView), typeof(GridViewRenderer));
                    //handlers.AddCompatibilityRenderer(typeof(Entry), typeof(TVEntryRenderer));
                    handlers.AddCompatibilityRenderer(typeof(ShadowFramme), typeof(ShadowFrameRenderer));
                    
                })
                .ConfigureEssentials()
                .ConfigureEffects(builder =>
                {
                    builder.Add<FocusUpEffect, PlatformFocusUpEffect>();
                    builder.Add<FocusDownEffect, PlatformFocusDownEffect>();
                    builder.Add<FocusLeftEffect, PlatformFocusLeftEffect>();
                    builder.Add<FocusRightEffect, PlatformFocusRightEffect>();

                    builder.Add<InputEvents.RemoteKeyEventEffect, PlatformRemoteKeyEventEffect>();
                    builder.Add<InputEvents.AccessKeyEffect, PlatformAccessKeyEffect>();
                });


            return builder.Build();
        }
    }
}