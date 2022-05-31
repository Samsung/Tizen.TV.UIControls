/*
 * Copyright (c) 2018 Samsung Electronics Co., Ltd All Rights Reserved
 *
 * Licensed under the Apache License, Version 2.0 (the License);
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an AS IS BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Maui;
using Microsoft.Maui.Controls.Compatibility;
using Microsoft.Maui.Controls.Compatibility.Hosting;
using Microsoft.Maui.Hosting;
using Microsoft.Maui.LifecycleEvents;
using Tizen.Theme.Common;
using Tizen.Theme.Common.Renderer;

[assembly: ExportRenderer(typeof(AnimatedNavigationPage), typeof(AnimatedNavigationPageRenderer))]
namespace Tizen.Theme.Common
{
    public static class CommonUIExtensions
    {
        public static MauiAppBuilder UseTizenThemeCommonUI(this MauiAppBuilder builder)
        {
            builder
                .UseMauiCompatibility()
                .ConfigureMauiHandlers(handlers =>
                {
                    handlers.AddCompatibilityRenderer(typeof(ShadowFrame), typeof(ShadowFrameRenderer));
                    handlers.AddCompatibilityRenderer(typeof(ContentButton), typeof(ContentButtonRenderer));

                    handlers.AddCompatibilityRenderer(typeof(GridView), typeof(GridViewRenderer));
                    handlers.AddCompatibilityRenderer(typeof(MediaView), typeof(MediaViewRenderer));
                    handlers.AddCompatibilityRenderer(typeof(UriMediaSource), typeof(UriMediaSourceHandler));
                    handlers.AddCompatibilityRenderer(typeof(FileMediaSource), typeof(FileMediaSourceHandler));

                    handlers.AddCompatibilityRenderer(typeof(OverlayPage), typeof(OverlayPageRenderer));
                    handlers.AddCompatibilityRenderer(typeof(OverlayMediaView), typeof(OverlayViewRenderer));

                    //TODO
                    //handlers.AddCompatibilityRenderer(typeof(AnimatedNavigationPage), typeof(AnimatedNavigationPageRenderer));

                })
                .ConfigureLifecycleEvents(events =>
                {
                    events.AddTizen(tizen => tizen
                    .OnPreCreate((a) =>
                    {
                        var option = new InitOptions(MauiApplication.Current);
                        CommonUI.Init(option);
                    }));
                });

            var services = builder.Services;
            services.AddTransient<IContentPopupRenderer, ContentPopupRenderer>();
            services.AddTransient<IPlatformMediaPlayer, MediaPlayerImpl>();

            return builder;
        }
    }
}
