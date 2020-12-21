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

namespace Tizen.TV.UIControls.Forms
{
    /// <summary>
    /// Arguments for the event that is raised when buffer status is changed.
    /// </summary>
    public class BufferStatusEventArgs : EventArgs
    {
        public BufferStatusEventArgs(ESPlayerStreamType type, BufferStatus status)
        {
            StreamType = type;
            BufferStatus = status;
        }
        /// <summary>
        /// The stream type of the emitted event.
        /// </summary>
        public ESPlayerStreamType StreamType { get; internal set; }
        /// <summary>
        /// The buffer status of ESPlayer
        /// </summary>
        public BufferStatus BufferStatus { get; internal set; }
    }

    /// <summary>
    /// Arguments for the event that is raised when error is cccurred
    /// </summary>
    public class ErrorOccurredEventArgs : EventArgs
    {
        public ErrorOccurredEventArgs(ESPlayerErrorType type)
        {
            ErrorType = type;
        }
        /// <summary>
        /// The type of error from ESPlayer
        /// </summary>
        public ESPlayerErrorType ErrorType { get; internal set; }
    }


    /// <summary>
    /// Arguments for providing stream infomation 
    /// </summary>
    public class StreamReadyEventArgs : EventArgs
    {
        public StreamReadyEventArgs(ESPlayerStreamType type)
        {
            Type = type;
        }

        /// <summary>
        /// The type of stream for ESPlayer
        /// </summary>
        public ESPlayerStreamType Type { get; set; }
    }

    /// <summary>
    /// Arguments for the event that is raised when the TVESPlayer is ready to accept the first es packet of the point to be moved for each stream during seeking player.
    /// </summary>
    public class SeekReadyEventArgs : EventArgs
    {
        public SeekReadyEventArgs(ESPlayerStreamType type, TimeSpan time)
        {
            Type = type;
            Time = time;
        }

        /// <summary>
        /// The time for seeking of TVESPlayer
        /// </summary>
        public TimeSpan Time { get; set; }
        /// <summary>
        /// The type of stream for TVESPlayer
        /// </summary>
        public ESPlayerStreamType Type { get; set; }
    }
}