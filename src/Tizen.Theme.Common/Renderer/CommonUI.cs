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

using System;
using ElmSharp;
using Microsoft.Maui;
using Tizen.Applications;
//using Xamarin.Forms.Platform.Tizen;
using Microsoft.Maui.Platform;
using System.Linq;

namespace Tizen.Theme.Common
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

        ///// <summary>
        ///// Default Constructor
        ///// </summary>
        ///// <param name="application"></param>
        //public InitOptions() : this () { }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="application"></param>
        /// <param name="mainWindowProvider"></param>
        public InitOptions()
        {
            Context = MauiApplication.Current;
            MainWindowProvider = () => { return ((IApplication)Microsoft.Maui.Controls.Application.Current)?.Windows.FirstOrDefault<IWindow>() as Window; };
        }
    }

    public static class CommonUI
    {
        public static readonly string Tag = "Tizen.Theme.Common";

        public static bool IsInitialized { get; private set; }

        public static Func<Window> MainWindowProvider { get; set; }

        public static CoreApplication Context { get; private set; }

        /// <summary>
        ///  Used for registration with dependency service
        /// </summary>
        public static void Init(CoreApplication context)
        {
            if (IsInitialized) return;
            if (context == null)
            {
                throw new InvalidOperationException($"{nameof(context)} could not be null.");
            }

            Context = context;
            if (context is MauiApplication mauiApplication)
            {
                MainWindowProvider = () => { return ((IApplication)Microsoft.Maui.Controls.Application.Current)?.Windows.FirstOrDefault<IWindow>() as Window; };
            }
            IsInitialized = true;
        }

        /// <summary>
        /// Init with options
        /// </summary>
        /// <param name="options"></param>
        public static void Init(InitOptions options)
        {
            Init(options.Context);
            if (options.MainWindowProvider != null)
            {
                MainWindowProvider = options.MainWindowProvider;
            }
        }

        /// <summary>
        /// Adds the common theme overlay
        /// </summary>
        public static void AddCommonThemeOverlay()
        {
            if (!IsInitialized)
            {
                throw new InvalidOperationException($"{nameof(AddCommonThemeOverlay)} must be called after {nameof(Init)}");
            };

            var resPath = Context.DirectoryInfo?.Resource;
            ThemeLoader.Initialize(resPath);
        }
    }
}
