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
    public interface ITVESPlayer : IPlatformMediaPlayer
    {
        void SetStreamInfomation(VideoStreamInfo info);

        void SetStreamInfomation(AudioStreamInfo info);

        SubmitStatus SubmitPacket(ESPacket packet);

        SubmitStatus SubmitPacket(ESHandlePacket packet);
        
        SubmitStatus SubmitEosPacket(ESPlayerStreamType type);

        event EventHandler EOSEmitted;

        event EventHandler ResourceConflicted;

        event EventHandler<ErrorOccurredEventArgs> ErrorOccurred;

        event EventHandler<StreamReadyEventArgs> StreamReady;

        event EventHandler<SeekReadyEventArgs> SeekReady;

        event EventHandler<BufferStatusEventArgs> BufferStatusChanged;
    }

    /// <summary>
    ///  Represents one of es packet which is demuxed from application, contains stream type, es packet buffer, pts and duration.
    /// </summary>
    public class ESPacket
    {
        /// <summary>
        /// Stream type of ESPacket
        /// </summary>
        public ESPlayerStreamType StreamType { get; set; }
        /// <summary>
        /// Buffer containing demuxed es packet
        /// </summary>
        public byte[] Buffer { get; set; }
        /// <summary>
        /// PTS(Presentation Time Stamp) of es packet
        /// </summary>
        public ulong PresentationTimeStamp { get; set; }
        /// <summary>
        /// Duration of es packet
        /// </summary>
        public ulong Duration { get; set; }
    }

    /// <summary>
    /// Represents one of es packet which includes handle of data inside the trust zone,
    /// contains stream type, handle for encrypted es data packet, handle size, pts and duration.
    /// </summary>
    public class ESHandlePacket
    {
        /// <summary>
        /// Stream type of ESPacket
        /// </summary>
        public ESPlayerStreamType StreamType { get; set; }
        /// <summary>
        /// Handle for encrypted es packet inside the trust zone
        /// </summary>
        public uint Handle { get; set; }
        /// <summary>
        /// Handle size for the handle of ESHandlePacket
        /// </summary>
        public uint HandleSize { get; set; }
        /// <summary>
        /// PTS(Presentation Time Stamp) of es packet
        /// </summary>
        public ulong PresentationTimeStamp { get; set; }
        /// <summary>
        /// DUration of es packet
        /// </summary>
        public ulong Duration { get; set; }
    }


    /// <summary>
    /// Represents numerator and denominator of frame rate for video stream
    /// </summary>
    public struct ESFrameRate
    {
        /// <summary>
        /// Numerator of framerate for the associated video stream
        /// </summary>
        public int Numerator { get; set; }
        /// <summary>
        /// Denominator of framerate for the associated video stream
        /// </summary>
        public int Denominator { get; set; }
    }

    /// <summary>
    /// Represents stream information for video stream which is demuxed from application,
    /// contains codec data, mime type, width, height, max width, max hgeight and framerate(Numerator, Denominator).
    /// </summary>
    public struct VideoStreamInfo
    {
        /// <summary>
        /// Codec data for the associated video stream
        /// </summary>
        public byte[] CodecData { get; set; }
        /// <summary>
        /// Mime type for the associated video stream
        /// </summary>
        public VideoMimeType MimeType { get; set; }
        /// <summary>
        /// width for the associated video stream
        /// </summary>
        public int Width { get; set; }
        /// <summary>
        /// Height for the associated video stream
        /// </summary>
        public int Height { get; set; }
        /// <summary>
        /// Max width for the associated video stream
        /// </summary>
        public int MaxWidth { get; set; }
        /// <summary>
        /// Max height for the associated video stream
        /// </summary>
        public int MaxHeight { get; set; }
        /// <summary>
        /// Framerate for the associated video stream
        /// </summary>
        public ESFrameRate FrameRate { get; set; }
    }

    public struct AudioStreamInfo
    {
        /// <summary>
        /// Codec data for the associated audio stream
        /// </summary>
        public byte[] CodecData { get; set; }
        /// <summary>
        /// Mime type for the associated audio stream
        /// </summary>
        public AudioMimeType MimeType { get; set; }
        /// <summary>
        /// Bitrate for the associated audio stream
        /// </summary>
        public int Bitrate { get; set; }
        /// <summary>
        /// Channels for the associated audio stream
        /// </summary>
        public int Channels { get; set; }
        /// <summary>
        /// Sample rate for the associated audio stream
        /// </summary>
        public int SampleRate { get; set; }
    }
}