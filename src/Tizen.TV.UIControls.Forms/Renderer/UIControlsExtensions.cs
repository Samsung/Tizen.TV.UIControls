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

using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Compatibility.Hosting;
using Microsoft.Maui.Controls.Hosting;
using Microsoft.Maui.Hosting;
using Microsoft.Maui.LifecycleEvents;
using Microsoft.Extensions.DependencyInjection;
using Tizen.Theme.Common;
using Tizen.Theme.Common.Renderer;
using Tizen.TV.UIControls.Forms.Renderer;


[assembly: ResolutionGroupName("TizenTVUIControl")]
namespace Tizen.TV.UIControls.Forms
{
    public static class UIControlsExtensions
    {
        public static MauiAppBuilder UseTizenTVUIControls(this MauiAppBuilder builder)
        {
            builder
                .UseTizenThemeCommonUI()
                .ConfigureMauiHandlers(handlers =>
                {
                    //handlers.AddHandler<Entry, TVEntryHandler>();
                    //handlers.AddHandler<Editor, TVEditorHandler>();
                    handlers.AddHandler<ContentButton, ContentButtonHandler>();

                    //TODO
                    //handlers.AddCompatibilityRenderer(typeof(GridView), typeof(GridViewRenderer));
                    //handlers.AddCompatibilityRenderer(typeof(MediaView), typeof(MediaViewRenderer));
                    handlers.AddCompatibilityRenderer(typeof(UriMediaSource), typeof(UriMediaSourceHandler));
                    handlers.AddCompatibilityRenderer(typeof(FileMediaSource), typeof(FileMediaSourceHandler));
                    //handlers.AddCompatibilityRenderer(typeof(RecycleItemsView), typeof(RecycleItemsViewContentRenderer));
                    //handlers.AddCompatibilityRenderer(typeof(Button), typeof(PropagatableButtonRenderer));
                    //handlers.AddCompatibilityRenderer(typeof(RecycleItemsView), typeof(RecycleItemsViewRenderer));
                    //handlers.AddCompatibilityRenderer(typeof(RecycleItemsView.ContentLayout), typeof(RecycleItemsViewContentRenderer));

                    //TODO
                    //handlers.AddCompatibilityRenderer(typeof(AnimatedNavigationPage), typeof(AnimatedNavigationPageRenderer));
                })
                .ConfigureEffects(builder =>
                {
                    //builder.Add<FocusUpEffect, PlatformFocusUpEffect>();
                    //builder.Add<FocusDownEffect, PlatformFocusDownEffect>();
                    //builder.Add<FocusLeftEffect, PlatformFocusLeftEffect>();
                    //builder.Add<FocusRightEffect, PlatformFocusRightEffect>();

                    //builder.Add<InputEvents.RemoteKeyEventEffect, PlatformRemoteKeyEventEffect>();
                    builder.Add<InputEvents.AccessKeyEffect, PlatformAccessKeyEffect>();
                })
                .ConfigureLifecycleEvents(events =>
                {
                    events.AddTizen(tizen => tizen
                    .OnPreCreate((a) =>
                    {
                        var option = new InitOptions(MauiApplication.Current);
                        UIControls.Init(option);
                    }));
                });

            var services = builder.Services;
            //services.AddTransient<ContentPopupRenderer, ContentPopupRenderer>();
            //services.AddTransient<Tizen.Theme.Common.IPlatformMediaPlayer, TVMediaPlayerImpl>();

            return builder;
        }
    }
}
