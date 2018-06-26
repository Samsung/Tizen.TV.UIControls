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
using System.IO;
using System.Threading.Tasks;

namespace Tizen.TV.UIControls.Forms
{
    /// <summary>
    /// Enumerates values that define how a media content is displayed.
    /// </summary>
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

    public class BufferingProgressUpdatedEventArgs : EventArgs
    {
        public double Progress { get; set; }
    }

    public interface IPlatformMediaPlayer
    {
        bool UsesEmbeddingControls { get; set; }
        bool AutoPlay { get; set; }
        bool AutoStop { get; set; }
        double Volume { get; set; }
        bool IsMuted { get; set; }
        int Position { get; }

        int Duration { get; }

        DisplayAspectMode AspectMode { get; set; }

        event EventHandler PlaybackCompleted;
        event EventHandler PlaybackStarted;
        event EventHandler PlaybackPaused;
        event EventHandler PlaybackStopped;
        event EventHandler UpdateStreamInfo;
        event EventHandler<BufferingProgressUpdatedEventArgs> BufferingProgressUpdated;

        void SetDisplay(IVideoOutput output);

        void SetSource(MediaSource source);

        Task<bool> Start();
        void Stop();
        void Pause();
        Task<int> Seek(int ms);
        Task<Stream> GetAlbumArts();

        Task<IDictionary<string, string>> GetMetadata();
    }
}
