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
using Xamarin.Forms;

namespace Tizen.TV.UIControls.Forms
{
    /// <summary>
    /// TVESMediaPlayer provieds the essential components to play the element stream.
    /// </summary>
    public class TVESPlayer : MediaPlayer
    {
        ITVESPlayer _esImpl;

        ~TVESPlayer()
        {
            (_esImpl as IDisposable)?.Dispose();
        }

        protected override IPlatformMediaPlayer CreateMediaPlayerImpl()
        {
            _esImpl = DependencyService.Get<ITVESPlayer>(fetchTarget: DependencyFetchTarget.NewInstance) ;
            _esImpl.ResourceConflicted += SendResourceConflicted;
            _esImpl.BufferStatusChanged += SendBufferStatusChanged;
            _esImpl.EOSEmitted += SendEOSEmitted;
            _esImpl.ErrorOccurred += SendErrorOccurred;
            _esImpl.StreamReady += SendStreamReady;
            _esImpl.SeekReady += SendSeekReady;
            return _esImpl as IPlatformMediaPlayer;
        }

        /// <summary>
        /// Stream information for es contents of audio stream
        /// </summary>
        public void SetStreamInformation(AudioStreamInfo info)
        {
            _esImpl.SetStreamInfomation(info);

        }

        /// <summary>
        /// Stream information for es contents of video stream
        /// </summary>
        public void SetStreamInformation(VideoStreamInfo info)
        {
            _esImpl.SetStreamInfomation(info);

        }

        /// <summary>
        /// Generates eos(end of stream) packet explicitly and pushes it to ESPlayer.
        /// </summary>
        public SubmitStatus SubmitEosPacket(ESPlayerStreamType type)
        {
            return _esImpl.SubmitEosPacket(type);
        }

        /// <summary>
        /// Pushes es packet to native pipelines.
        /// </summary>
        public SubmitStatus SubmitPacket(ESPacket packet)
        {
            return _esImpl.SubmitPacket(packet);
        }

        /// <summary>
        /// Pushes es packet which includes handle of data inside the trust zone to ESPlayer.
        /// </summary>
        public SubmitStatus SubmitPacket(ESHandlePacket packet)
        {
            return _esImpl.SubmitPacket(packet);
        }

        /// <summary>
        /// Emit when es playback reaches end-of-stream.
        /// </summary>
        public event EventHandler EOSEmitted;

        /// <summary>
        /// Emit when resource conflicted event is emitted.
        /// </summary>
        public event EventHandler ResourceConflicted;

        /// <summary>
        /// Emit when esplayer encounters some error during preparing for esplayer pipeline or playback es packet.
          /// </summary>
        public event EventHandler<ErrorOccurredEventArgs> ErrorOccurred;

        /// <summary>
        /// Called when the ESPlayer is ready to accept the first es packet of the point to be played for each stream during preparing player.
        /// </summary>
        public event EventHandler<StreamReadyEventArgs> StreamReady;

        /// <summary>
        /// Called when the ESPlayer is ready to accept the first es packet of the point to be moved for each stream during seeking player.
        /// </summary>
        public event EventHandler<SeekReadyEventArgs> SeekReady;

        /// <summary>
        /// Emit when es packet queue is empty or full.
        /// </summary>
        public event EventHandler<BufferStatusEventArgs> BufferStatusChanged;

        void SendSeekReady(object sender, SeekReadyEventArgs e)
        {
            SeekReady?.Invoke(sender, e);
        }

        void SendStreamReady(object sender, StreamReadyEventArgs e)
        {
            StreamReady?.Invoke(sender, e);
        }

        void SendErrorOccurred(object sender, ErrorOccurredEventArgs e)
        {
            ErrorOccurred?.Invoke(sender, e);
        }

        void SendEOSEmitted(object sender, EventArgs e)
        {
            EOSEmitted?.Invoke(sender, e);
        }

        void SendBufferStatusChanged(object sender, BufferStatusEventArgs e)
        {
            BufferStatusChanged?.Invoke(sender, e);
        }

        void SendResourceConflicted(object sender, EventArgs e)
        {
            ResourceConflicted?.Invoke(sender, e);
        }
    }
}