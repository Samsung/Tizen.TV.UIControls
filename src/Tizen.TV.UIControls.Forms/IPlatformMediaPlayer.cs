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
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Threading.Tasks;

namespace Tizen.TV.UIControls.Forms
{
    /// <summary>
    /// Enumerates values that define how a media content is displayed.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Obsolete("This class is obsolete as of 1.1.0. Please use DisplayAspectMode from Tizen.Theme.Common instead.")]
    public enum DisplayAspectMode
    {
        /// <summary>
        /// Scale the media content to so it exactly fills the view.
        /// </summary>
        Fill,
        /// <summary>
        /// Scale the media content to fit the view.
        /// </summary>
        AspectFit,
        /// <summary>
        /// Scale the media content to fill the view.
        /// </summary>
        AspectFill,
        /// <summary>
        /// The original size of the media content.
        /// </summary>
        OrignalSize
    }

    /// <summary>
    /// Internal use only. Contains arguments for the event that is raised when the buffering progress is updated.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Obsolete("This class is obsolete as of 1.1.0. Please use BufferingProgressUpdatedEventArgs from Tizen.Theme.Common instead.")]
    public class BufferingProgressUpdatedEventArgs : Theme.Common.BufferingProgressUpdatedEventArgs { }

    /// <summary>
    /// For internal use by platform renderers.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Obsolete("This class is obsolete as of 1.1.0. Please use IPlatformMediaPlayer from Tizen.Theme.Common instead.")]
    public interface IPlatformMediaPlayer : Theme.Common.IPlatformMediaPlayer { }
}
