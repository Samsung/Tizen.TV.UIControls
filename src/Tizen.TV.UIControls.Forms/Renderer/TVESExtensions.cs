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

using Tizen.TV.Multimedia;
using TM = Tizen.TV.Multimedia;

namespace Tizen.TV.UIControls.Forms
{
    public static class TVESConvertExtensions
    {
        public static ESPlayerStreamType ToStreamType(this TM.StreamType type)
        {
            ESPlayerStreamType ret = ESPlayerStreamType.Audio;
            switch (type)
            {
                case TM.StreamType.Video:
                    ret = ESPlayerStreamType.Video;
                    break;
            }
            return ret;
        }

        public static TM.StreamType ToStreamType(this ESPlayerStreamType type)
        {
            TM.StreamType ret = TM.StreamType.Audio;
            switch (type)
            {
                case ESPlayerStreamType.Video:
                    ret = TM.StreamType.Video;
                    break;
            }
            return ret;
        }

        public static BufferStatus ToBufferStatus(this TM.BufferStatus type)
        {
            BufferStatus ret = BufferStatus.Underrun;
            switch (type)
            {
                case TM.BufferStatus.Overrun:
                    ret = BufferStatus.Overrun;
                    break;
            }
            return ret;
        }

        public static TM.ESPacket ToESPacket(this ESPacket packet)
        {
            return new TM.ESPacket()
            {
                type = packet.StreamType.ToStreamType(),
                duration = packet.Duration,
                buffer = packet.Buffer,
                pts = packet.PresentationTimeStamp,
            };
        }

        public static TM.ESHandlePacket ToESHandlePacket(this ESHandlePacket packet)
        {
            return new TM.ESHandlePacket()
            {
                type = packet.StreamType.ToStreamType(),
                handle = packet.Handle,
                handleSize = packet.HandleSize,
                pts = packet.PresentationTimeStamp,
                duration = packet.Duration,
            };
        }

        public static TM.VideoStreamInfo ToVideoStreamInfo(this VideoStreamInfo info)
        {
            return new TM.VideoStreamInfo()
            {
                codecData = info.CodecData,
                mimeType = info.MimeType.ToMimeType(),
                num = info.FrameRate.Numerator,
                den = info.FrameRate.Denominator,
                height = info.Height,
                maxHeight = info.MaxHeight,
                width = info.Width,
                maxWidth = info.MaxWidth,
            };
        }

        public static TM.AudioStreamInfo ToAudioStreamInfo(this AudioStreamInfo info)
        {
            return new TM.AudioStreamInfo()
            {
                mimeType = info.MimeType.ToMimeType(),
                codecData = info.CodecData,
                channels = info.Channels,
                bitrate = info.Bitrate,
                sampleRate = info.SampleRate,
            };
        }

        public static ESPlayerErrorType ToErrorType(this TM.ErrorType type)
        {
            ESPlayerErrorType ret = ESPlayerErrorType.None;
            switch (type)
            {
                case TM.ErrorType.SeekFailed:
                    ret = ESPlayerErrorType.SeekFailed;
                    break;
                case TM.ErrorType.InvalidState:
                    ret = ESPlayerErrorType.InvalidState;
                    break;
                case TM.ErrorType.NotSupportedFile:
                    ret = ESPlayerErrorType.NotSupportedFile;
                    break;
                case TM.ErrorType.ConnectionFailed:
                    ret = ESPlayerErrorType.ConnectionFailed;
                    break;
                case TM.ErrorType.DRMExpired:
                    ret = ESPlayerErrorType.DRMExpired;
                    break;
                case TM.ErrorType.DRMNoLicense:
                    ret = ESPlayerErrorType.DRMNoLicense;
                    break;
                case TM.ErrorType.DRMFutureUse:
                    ret = ESPlayerErrorType.DRMFutureUse;
                    break;
                case TM.ErrorType.NotPermitted:
                    ret = ESPlayerErrorType.NotPermitted;
                    break;
                case TM.ErrorType.ResourceLimit:
                    ret = ESPlayerErrorType.ResourceLimit;
                    break;
                case TM.ErrorType.NotSupportedAudioCodec:
                    ret = ESPlayerErrorType.NotSupportedAudioCodec;
                    break;
                case TM.ErrorType.NotSupportedVideoCodec:
                    ret = ESPlayerErrorType.NotSupportedVideoCodec;
                    break;
                case TM.ErrorType.DRMDecryptionFailed:
                    ret = ESPlayerErrorType.DRMDecryptionFailed;
                    break;
                case TM.ErrorType.NotSupportedFormat:
                    ret = ESPlayerErrorType.NotSupportedFormat;
                    break;
                case TM.ErrorType.Unknown:
                    ret = ESPlayerErrorType.Unknown;
                    break;
                case TM.ErrorType.BufferSpace:
                    ret = ESPlayerErrorType.BufferSpace;
                    break;
                case TM.ErrorType.InvalidOperator:
                    ret = ESPlayerErrorType.InvalidOperator;
                    break;
                case TM.ErrorType.InvalidParameter:
                    ret = ESPlayerErrorType.InvalidParameter;
                    break;
                case TM.ErrorType.PermissionDenied:
                    ret = ESPlayerErrorType.PermissionDenied;
                    break;
                case TM.ErrorType.OutOfMemory:
                    ret = ESPlayerErrorType.OutOfMemory;
                    break;
            }
            return ret;
        }

        public static DisplayMode ToTVDisplayMode(this DisplayAspectMode mode)
        {
            DisplayMode ret = DisplayMode.LetterBox;
            switch (mode)
            {
                case DisplayAspectMode.AspectFill:
                    ret = DisplayMode.CroppedFull;
                    break;
                case DisplayAspectMode.AspectFit:
                    ret = DisplayMode.LetterBox;
                    break;
                case DisplayAspectMode.Fill:
                    ret = DisplayMode.FullScreen;
                    break;
                case DisplayAspectMode.OrignalSize:
                    ret = DisplayMode.OriginSize;
                    break;
            }
            return ret;
        }

        public static SubmitStatus ToSubmitStatus(this TM.SubmitStatus mode)
        {
            SubmitStatus ret = SubmitStatus.Success;
            switch (mode)
            {
                case TM.SubmitStatus.Success:
                    ret = SubmitStatus.Success;
                    break;
                case TM.SubmitStatus.Full:
                    ret = SubmitStatus.Full;
                    break;
                case TM.SubmitStatus.InvalidPacket:
                    ret = SubmitStatus.InvalidPacket;
                    break;
                case TM.SubmitStatus.NotPrepared:
                    ret = SubmitStatus.NotPrepared;
                    break;
                case TM.SubmitStatus.OutOfMemory:
                    ret = SubmitStatus.OutOfMemory;
                    break;
            }
            return ret;
        }

        public static TM.AudioMimeType ToMimeType(this AudioMimeType mode)
        {
            TM.AudioMimeType ret = TM.AudioMimeType.Unknown;
            switch (mode)
            {
                case AudioMimeType.Aac:
                    ret = TM.AudioMimeType.Aac;
                    break;
                case AudioMimeType.Mp2:
                    ret = TM.AudioMimeType.Mp2;
                    break;
                case AudioMimeType.Mp3:
                    ret = TM.AudioMimeType.Mp3;
                    break;
                case AudioMimeType.Ac3:
                    ret = TM.AudioMimeType.Ac3;
                    break;
                case AudioMimeType.Eac3:
                    ret = TM.AudioMimeType.Eac3;
                    break;
                case AudioMimeType.Vorbis:
                    ret = TM.AudioMimeType.Vorbis;
                    break;
                case AudioMimeType.Opus:
                    ret = TM.AudioMimeType.Opus;
                    break;
                case AudioMimeType.PcmS16le:
                    ret = TM.AudioMimeType.PcmS16le;
                    break;
                case AudioMimeType.PcmS16be:
                    ret = TM.AudioMimeType.PcmS16be;
                    break;
                case AudioMimeType.PcmU16le:
                    ret = TM.AudioMimeType.PcmU16le;
                    break;
                case AudioMimeType.PcmU16be:
                    ret = TM.AudioMimeType.PcmU16be;
                    break;
                case AudioMimeType.PcmS24le:
                    ret = TM.AudioMimeType.PcmS24le;
                    break;
                case AudioMimeType.PcmS24be:
                    ret = TM.AudioMimeType.PcmS24be;
                    break;
                case AudioMimeType.PcmU24le:
                    ret = TM.AudioMimeType.PcmU24le;
                    break;
                case AudioMimeType.PcmU24be:
                    ret = TM.AudioMimeType.PcmU24be;
                    break;
                case AudioMimeType.PcmS32le:
                    ret = TM.AudioMimeType.PcmS32le;
                    break;
                case AudioMimeType.PcmS32be:
                    ret = TM.AudioMimeType.PcmS32be;
                    break;
                case AudioMimeType.PcmU32le:
                    ret = TM.AudioMimeType.PcmU32le;
                    break;
                case AudioMimeType.PcmU32be:
                    ret = TM.AudioMimeType.PcmU32be;
                    break;
            }
            return ret;
        }

        public static TM.VideoMimeType ToMimeType(this VideoMimeType mode)
        {
            TM.VideoMimeType ret = TM.VideoMimeType.H264;
            switch (mode)
            {
                case VideoMimeType.H263:
                    ret = TM.VideoMimeType.H263;
                    break;
                case VideoMimeType.H264:
                    ret = TM.VideoMimeType.H264;
                    break;
                case VideoMimeType.Hevc:
                    ret = TM.VideoMimeType.Hevc;
                    break;
                case VideoMimeType.Mpeg1:
                    ret = TM.VideoMimeType.Mpeg1;
                    break;
                case VideoMimeType.Mpeg2:
                    ret = TM.VideoMimeType.Mpeg2;
                    break;
                case VideoMimeType.Mpeg4:
                    ret = TM.VideoMimeType.Mpeg4;
                    break;
                case VideoMimeType.Vp8:
                    ret = TM.VideoMimeType.Vp8;
                    break;
                case VideoMimeType.Vp9:
                    ret = TM.VideoMimeType.Vp9;
                    break;
                case VideoMimeType.Wmv3:
                    ret = TM.VideoMimeType.Wmv3;
                    break;
            }
            return ret;
        }
    }
}