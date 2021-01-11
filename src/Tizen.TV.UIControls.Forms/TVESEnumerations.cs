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

namespace Tizen.TV.UIControls.Forms
{
    /// <summary>
    /// Enumerator for stream type of es stream
    /// </summary>
    public enum ESPlayerStreamType
    {
        /// <summary>
        /// Audio
        /// </summary>
        Audio,
        /// <summary>
        /// Video
        /// </summary>
        Video
    }

    /// <summary>
    /// Enumerator for buffer status whether empty or full.
    /// </summary>
    public enum BufferStatus
    {
        /// <summary>
        /// Status of buffer queue in TVESPlayer is underrun.
        /// When status is Underrun, application should push es packet sufficiently.
        /// </summary>
        Underrun,
        /// <summary>
        /// Status of buffer queue in TVESPlayer is overrun.
        /// When status is Overrun, application should stop pushing es packet.
        /// </summary>
        Overrun
    }

    /// <summary>
    /// Enumerator for es packet submit status
    /// </summary>
    public enum SubmitStatus
    {
        /// <summary>
        /// Not prepared to get packet
        /// </summary>
        NotPrepared,
        /// <summary>
        /// Invalid packet
        /// </summary>
        InvalidPacket,
        /// <summary>
        /// Out of memory on device
        /// </summary>
        OutOfMemory,
        /// <summary>
        /// Buffer already full
        /// </summary>
        Full,
        /// <summary>
        /// Submit succeeded
        /// </summary>
        Success 
    }

    /// <summary>
    /// Enumerator for video mime type
    /// </summary>
    public enum VideoMimeType
    {
        /// <summary>
        /// H.263
        /// </summary>
        H263,
        /// <summary>
        /// H.254
        /// </summary>
        H264,
        /// <summary>
        /// HEVC
        /// </summary>
        Hevc,
        /// <summary>
        /// MPEG-1
        /// </summary>
        Mpeg1,
        /// <summary>
        /// MPEG-2
        /// </summary>
        Mpeg2,
        /// <summary>
        /// MPEG-4
        /// </summary>
        Mpeg4,
        /// <summary>
        /// VP8
        /// </summary>
        Vp8,
        /// <summary>
        /// VP9
        /// </summary>
        Vp9,
        /// <summary>
        /// WMV3
        /// </summary>
        Wmv3
    }

    /// <summary>
    /// Enumerator for audio mime type
    /// </summary>
    public enum AudioMimeType
    {
        /// <summary>
        /// Unknown
        /// </summary>
        Unknown,
        /// <summary>
        /// AAC
        /// </summary>
        Aac,
        /// <summary>
        /// MP2
        /// </summary>
        Mp2,
        /// <summary>
        /// MP3
        /// </summary>
        Mp3,
        /// <summary>
        /// AC3
        /// </summary>
        Ac3,
        /// <summary>
        /// EAC3
        /// </summary>
        Eac3,
        /// <summary>
        /// VORBIS
        /// </summary>
        Vorbis,
        /// <summary>
        /// OPUS
        /// </summary>
        Opus,
        /// <summary>
        /// PCM_S16LE
        /// </summary>
        PcmS16le,
        /// <summary>
        /// PCM_S16BE
        /// </summary>
        PcmS16be,
        /// <summary>
        /// PCM_U16LE
        /// </summary>
        PcmU16le,
        /// <summary>
        /// PCM_U16BE
        /// </summary>
        PcmU16be,
        /// <summary>
        /// PCM_S24LE
        /// </summary>
        PcmS24le,
        /// <summary>
        /// PCM_S24BE
        /// </summary>
        PcmS24be,
        /// <summary>
        /// PCM_U24LE
        /// </summary>
        PcmU24le,
        /// <summary>
        /// PCM_U24BE
        /// </summary>
        PcmU24be,
        /// <summary>
        /// PCM_S32LE
        /// </summary>
        PcmS32le,
        /// <summary>
        /// PCM_S32BE
        /// </summary>
        PcmS32be,
        /// <summary>
        /// PCM_U32LE
        /// </summary>
        PcmU32le,
        /// <summary>
        /// PCM_U32BE
        /// </summary>
        PcmU32be
    }

    /// <summary>
    /// Enumerator for error type from TVESPlayer
    /// </summary>
    public enum ESPlayerErrorType
    {
        /// <summary>
        /// Seek operation failure
        /// </summary>
        SeekFailed,
        /// <summary>
        /// Invalid esplayer state
        /// </summary>
        InvalidState,
        /// <summary>
        /// File format not supported
        /// </summary>
        NotSupportedFile,
        /// <summary>
        /// Streaming connection failed
        /// </summary>
        ConnectionFailed,
        /// <summary>
        /// Expired license
        /// </summary>
        DRMExpired,
        /// <summary>
        /// No license
        /// </summary>
        DRMNoLicense,
        /// <summary>
        /// License for future use
        /// </summary>
        DRMFutureUse,
        /// <summary>
        /// Format not permitted
        /// </summary>
        NotPermitted,
        /// <summary>
        /// Resource limit
        /// </summary>
        ResourceLimit,
        /// <summary>
        /// Not supported audio codec but video can be played
        /// </summary>
        NotSupportedAudioCodec,
        /// <summary>
        /// Not supported video codec but audio can be played
        /// </summary>
        NotSupportedVideoCodec,
        /// <summary>
        /// DRM decryption failed
        /// </summary>
        DRMDecryptionFailed,
        /// <summary>
        /// Format not supported
        /// </summary>
        NotSupportedFormat,
        /// <summary>
        /// Unknown error
        /// </summary>
        Unknown,
        /// <summary>
        /// No buffer space available
        /// </summary>
        BufferSpace,
        /// <summary>
        /// Invalid operation
        /// </summary>
        InvalidOperator,
        /// <summary>
        /// Invalid parameter
        /// </summary>
        InvalidParameter,
        /// <summary>
        /// Permission denied
        /// </summary>
        PermissionDenied,
        /// <summary>
        /// Out of memory
        /// </summary>
        OutOfMemory,
        /// <summary>
        /// Successful
        /// </summary>
        None
    }
}