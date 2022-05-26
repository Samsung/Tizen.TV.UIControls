using Microsoft.Maui.Controls.Compatibility.Hosting;
using Microsoft.Maui.LifecycleEvents;
using Tizen.Theme.Common.Renderer;
using Tizen.TV.UIControls.Forms;
using static Tizen.TV.UIControls.Forms.InputEvents;
//using Tizen.TV.UIControls.Forms.Renderer;
using TIPlatformMediaPlayer = Tizen.Theme.Common.IPlatformMediaPlayer;
using TGridView = Tizen.Theme.Common.GridView;
using TShadowFramme = Tizen.Theme.Common.ShadowFrame;
using Tizen.Theme.Common;

namespace Sample
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();

            var services = builder.Services;
            //services.AddSingleton<TIPlatformMediaPlayer, MediaPlayerImpl>();
            //services.AddTransient<IWindow, Window>();

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
                    //handlers.AddCompatibilityRenderer(typeof(CustomButton), typeof(CustomButtonRenderer));
                    handlers.AddCompatibilityRenderer(typeof(CustomButton), typeof(Microsoft.Maui.Controls.Compatibility.Platform.Tizen.ButtonRenderer));
                    handlers.AddCompatibilityRenderer(typeof(TShadowFramme), typeof(ShadowFrameRenderer));
                })
                .ConfigureEssentials()
                .ConfigureEffects(builder =>
                {
                    builder.Add<PlatformFocusUpEffect, Tizen.TV.UIControls.Forms.Renderer.PlatformFocusUpEffect>();
                    builder.Add<PlatformFocusDownEffect, Tizen.TV.UIControls.Forms.Renderer.PlatformFocusDownEffect>();
                    builder.Add<PlatformFocusLeftEffect, Tizen.TV.UIControls.Forms.Renderer.PlatformFocusLeftEffect>();
                    builder.Add<PlatformFocusRightEffect, Tizen.TV.UIControls.Forms.Renderer.PlatformFocusRightEffect>();

                    builder.Add<RemoteKeyEventEffect, Tizen.TV.UIControls.Forms.Renderer.RemoteKeyEventEffect>();
                    builder.Add<AccessKeyEffect, Tizen.TV.UIControls.Forms.Renderer.AccessKeyEffect>();
                });


            return builder.Build();
        }
    }
}