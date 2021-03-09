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
using System.ComponentModel;
using Xamarin.Forms;

namespace Tizen.TV.UIControls.Forms
{
    /// <summary>
    /// Enumeration that specifies the video ouput.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Obsolete("This class is obsolete as of 1.1.0. Please use IPlatformMediaPlayer from Tizen.Theme.Common instead.")]
    public enum VideoOuputType
    {
        /// <summary>
        /// An overlay type.
        /// </summary>
        Overlay,
        /// <summary>
        /// A buffer type.
        /// </summary>
        Buffer,
    }

    /// <summary>
    /// Interface indicating the ouput type of the media.
    /// </summary>
    public interface IVideoOutput
    {
        VisualElement MediaView { get; }
        View Controller { get; set; }
        VideoOuputType OuputType { get; }
    }

    /// <summary>
    /// Interface for defining the overlay type of output.
    /// </summary>
    public interface IOverlayOutput : IVideoOutput
    {
        Rectangle OverlayArea { get; }
        event EventHandler AreaUpdated;
    }
}
