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
using Microsoft.Maui.Controls.Compatibility;
using System;
using System.Linq;
using Tizen.Applications;
using Tizen.Theme.Common.Renderer;
using Tizen.TV.UIControls.Forms;
using Tizen.TV.UIControls.Forms.Renderer;
using Window = ElmSharp.Window;

[assembly: ExportRenderer(typeof(AnimatedNavigationPage), typeof(AnimatedNavigationPageRenderer))]
[assembly: ExportRenderer(typeof(ContentButton), typeof(ContentButtonRenderer))]
//[assembly: ExportRenderer(typeof(GridView), typeof(GridViewRenderer))]
[assembly: ExportRenderer(typeof(MediaView), typeof(MediaViewRenderer))]
[assembly: ExportRenderer(typeof(OverlayPage), typeof(OverlayPageRenderer))]
[assembly: ExportRenderer(typeof(OverlayMediaView), typeof(OverlayViewRenderer))]

//[assembly: ExportHandler(typeof(UriMediaSource), typeof(UriMediaSourceHandler))]
//[assembly: ExportHandler(typeof(FileMediaSource), typeof(FileMediaSourceHandler))]
[assembly: Dependency(typeof(ContentPopupRenderer))]

namespace Tizen.TV.UIControls.Forms
{
    /// <summary>
    /// Initialization Option
    /// </summary>
    public class InitOptions
    {
        /// <summary>
        /// Gets or sets the context for CoreApplication
        /// </summary>
        public CoreApplication Context { get; set; }

        /// <summary>
        /// Gets or sets the main window provider
        /// </summary>
        public Func<Window> MainWindowProvider { get; set; }

        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="application"></param>
        //public InitOptions(FormsApplication application) : this (application, () => application.MainWindow) { }


        ///// <summary>
        ///// Default Constructor
        ///// </summary>
        ///// <param name="application"></param>
        //public InitOptions(MauiApplication application) : this(application, () => application.MainWindow) { }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="application"></param>
        /// <param name="mainWindowProvider"></param>
        public InitOptions()
        {
            Context = MauiApplication.Current;
            MainWindowProvider = () => {
                return (Context as MauiApplication).Application.Windows.FirstOrDefault<IWindow>().Handler.PlatformView as Window;
            };
        }
    }

    public static class UIControls
    {
        public static readonly string Tag = "TV.UIControls";

        public static bool IsInitialized { get; private set; }

        public static Func<Window> MainWindowProvider { get; set; }

        /// <summary>
        ///  Used for registration with dependency service
        /// </summary>
        public static void Init()
        {
            if (IsInitialized) return;
            IsInitialized = true;
        }

        /// <summary>
        /// Init with options
        /// </summary>
        /// <param name="options"></param>
        public static void Init(InitOptions options)
        {
            var resPath = options.Context?.DirectoryInfo?.Resource;
            if (!string.IsNullOrEmpty(resPath))
            {
                TVThemeLoader.Initialize(resPath);
            }

            MainWindowProvider = options.MainWindowProvider;
            Init();
        }
    }
}
