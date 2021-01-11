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
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Tizen;
using TVForms = Tizen.TV.UIControls.Forms;
using Tizen.TV.UIControls.Forms.Renderer;
using Tizen.TV.Multimedia;
using TM = Tizen.TV.Multimedia;

[assembly: Xamarin.Forms.Dependency(typeof(TVESPlayerImpl))]
namespace Tizen.TV.UIControls.Forms.Renderer
{
    class TVESPlayerImpl : ITVESPlayer, IDisposable
    {
        ESPlayer esPlayer;
        bool _cancelToStart;
        IVideoOutput _videoOutput;
        TaskCompletionSource<bool> _tcsSubmit;
        bool _isMuted = false;
        bool _isStarted = false;
        DisplayAspectMode _aspectMode = DisplayAspectMode.AspectFit;

        public TVESPlayerImpl()
        {
            esPlayer = new ESPlayer();
            esPlayer.EOSEmitted += OnEOSEmitted;
            esPlayer.ErrorOccurred += OnErrorOccurred;
            esPlayer.BufferStatusChanged += OnBufferStatusChanged;
            esPlayer.ResourceConflicted += OnResourceConflicted;
            esPlayer.Open();

            //The Tizen TV emulator is based on the x86 architecture. Using trust zone (DRM'ed content playback) is not supported by the emulator.
            if (RuntimeInformation.ProcessArchitecture != Architecture.X86)
            {
                esPlayer.SetTrustZoneUse(true);
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public async Task<bool> Start()
        {
            _cancelToStart = false;
            if (esPlayer.GetState() == ESPlayerState.Idle)
            {
                if (_tcsSubmit == null || _tcsSubmit.Task.IsCompleted)
                {
                    _tcsSubmit = new TaskCompletionSource<bool>();
                }
                ApplyDisplay();
                await esPlayer.PrepareAsync(OnReadyToPrepare);
            }

            if (_cancelToStart)
                return false;

            if (esPlayer.GetState() == ESPlayerState.Ready)
            {
                await _tcsSubmit.Task;
                try
                {
                    esPlayer.Start();
                    PlaybackStarted?.Invoke(this, EventArgs.Empty);
                }
                catch (Exception e)
                {
                    Log.Error(UIControls.Tag, $"Error On Start : {e.Message}");
                }
            }
            else if (esPlayer.GetState() == ESPlayerState.Paused)
            {
                esPlayer.Resume();
            }
            else 
            {
                return false;
            }
            _isStarted = true;
            return true;
        }

        public void Pause()
        {
            Log.Debug(UIControls.Tag, "Pause");
            try
            {
                esPlayer.Pause();
                PlaybackPaused.Invoke(this, EventArgs.Empty);
            }
            catch (Exception e)
            {
                Log.Error(UIControls.Tag, $"Error on Pause : {e.Message}");
            }
        }

        public void Stop()
        {
            _cancelToStart = true;
            _isStarted = false;
            ChangeToIdleState();
            PlaybackStopped.Invoke(this, EventArgs.Empty);
        }

        public SubmitStatus SubmitPacket(ESPacket packet)
        {
            _tcsSubmit?.TrySetResult(true);
            return esPlayer.SubmitPacket(packet.ToESPacket()).ToSubmitStatus();
        }

        public SubmitStatus SubmitEosPacket(ESPlayerStreamType type)
        {
            var status = esPlayer.SubmitEosPacket(type.ToStreamType()).ToSubmitStatus();
            return status;
        }

        public SubmitStatus SubmitPacket(ESHandlePacket packet)
        {
            _tcsSubmit?.TrySetResult(true);
            return esPlayer.SubmitPacket(packet.ToESHandlePacket()).ToSubmitStatus();
        }

        public async Task<int> Seek(int ms)
        {
            TimeSpan time = TimeSpan.FromMilliseconds(ms);
            await esPlayer.SeekAsync(time, OnSeekReady);
            return ms;
        }

        public void SetDisplay(IVideoOutput output)
        {
            VideoOutput = output;
        }

        public void SetSource(TVForms.MediaSource source)
        {
        }

        public event EventHandler PlaybackCompleted;
        public event EventHandler PlaybackStarted;
        public event EventHandler PlaybackPaused;
        public event EventHandler PlaybackStopped;
        public event EventHandler UpdateStreamInfo;
        public event EventHandler<BufferingProgressUpdatedEventArgs> BufferingProgressUpdated;

        public event EventHandler EOSEmitted;
        public event EventHandler ResourceConflicted;
        public event EventHandler<ErrorOccurredEventArgs> ErrorOccurred;
        public event EventHandler<StreamReadyEventArgs> StreamReady;
        public event EventHandler<SeekReadyEventArgs> SeekReady;
        public event EventHandler<BufferStatusEventArgs> BufferStatusChanged;

        public bool UsesEmbeddingControls { get; set; }

        public bool AutoPlay { get; set; }

        public bool AutoStop { get; set; }

        public double Volume { get; set; }

        public bool IsMuted 
        {
            get
            {
                return _isMuted;
            }
            set
            {
                _isMuted = value;
                esPlayer.SetAudioMute(_isMuted);
            }
            
        }

        public int Position 
        {
            get
            {
                if (!_isStarted)
                    return 0;

                TimeSpan time;
                esPlayer.GetPlayingTime(out time);
                return (int)time.TotalMilliseconds;
            }
        }

        public int Duration 
        {
            get
            {
                return -1;
            }
        }

        public DisplayAspectMode AspectMode 
        { 
            get
            { 
                return _aspectMode; 
            }
            set
            {
                _aspectMode = value;
                esPlayer.SetDisplayMode(_aspectMode.ToTVDisplayMode());
            }
        }

        public async Task<Stream> GetAlbumArts()
        {
            return null;
        }

        public async Task<IDictionary<string, string>> GetMetadata()
        {
            return null;
        }

        public void SetStreamInfomation(VideoStreamInfo info)
        {
            esPlayer.SetStream(info.ToVideoStreamInfo());
            UpdateStreamInfo?.Invoke(this, EventArgs.Empty);
        }

        public void SetStreamInfomation(AudioStreamInfo info)
        {
            esPlayer.SetStream(info.ToAudioStreamInfo());
            UpdateStreamInfo?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (IsDisposed)
            {
                return;
            }

            IsDisposed = true;
            if (esPlayer != null)
            {
                esPlayer.EOSEmitted -= OnEOSEmitted;
                esPlayer.ErrorOccurred -= OnErrorOccurred;
                esPlayer.BufferStatusChanged -= OnBufferStatusChanged;
                esPlayer.ResourceConflicted -= OnResourceConflicted;
                esPlayer.Close();
                esPlayer = null;
            }
        }

        protected bool IsDisposed = false;

        void OnResourceConflicted(object sender, ResourceConflictEventArgs e)
        {
            ResourceConflicted?.Invoke(sender, EventArgs.Empty);
        }

        void OnBufferStatusChanged(object sender, TM.BufferStatusEventArgs e)
        {
            BufferStatusChanged?.Invoke(sender, new BufferStatusEventArgs(e.StreamType.ToStreamType(), e.BufferStatus.ToBufferStatus()));
        }

        void OnErrorOccurred(object sender, Multimedia.ErrorEventArgs e)
        {
            ErrorOccurred?.Invoke(sender, new ErrorOccurredEventArgs(e.ErrorType.ToErrorType()));
        }

        void OnEOSEmitted(object sender, EOSEventArgs e)
        {
            EOSEmitted?.Invoke(sender, EventArgs.Empty);
        }

        void ChangeToIdleState()
        {
            switch (esPlayer.GetState())
            {
                case ESPlayerState.Ready:
                case ESPlayerState.Playing:
                case ESPlayerState.Paused:
                    esPlayer.Stop();
                    break;
            }
        }

        void OnReadyToPrepare(Tizen.TV.Multimedia.StreamType streamType)
        {
            switch (streamType)
            {
                case Tizen.TV.Multimedia.StreamType.Audio:
                    StreamReady?.Invoke(this, new StreamReadyEventArgs(ESPlayerStreamType.Audio));
                    break;
                case Tizen.TV.Multimedia.StreamType.Video:
                    StreamReady?.Invoke(this, new StreamReadyEventArgs(ESPlayerStreamType.Video));
                    break;
            }
        }

        void OnSeekReady(TM.StreamType streamType, TimeSpan time)
        {
            switch (streamType)
            {
                case TM.StreamType.Audio:
                    SeekReady?.Invoke(this, new SeekReadyEventArgs(ESPlayerStreamType.Audio, time));
                    break;
                case TM.StreamType.Video:
                    SeekReady?.Invoke(this, new SeekReadyEventArgs(ESPlayerStreamType.Video, time));
                    break;
            }
        }

        IVideoOutput VideoOutput
        {
            get { return _videoOutput; }
            set
            {
                if (TargetView != null)
                    TargetView.PropertyChanged -= OnTargetViewPropertyChanged;
                if (OverlayOutput != null)
                    OverlayOutput.AreaUpdated -= OnOverlayAreaUpdated;

                _videoOutput = value;

                if (TargetView != null)
                    TargetView.PropertyChanged += OnTargetViewPropertyChanged;
                if (OverlayOutput != null)
                    OverlayOutput.AreaUpdated += OnOverlayAreaUpdated;
            }
        }

        VisualElement TargetView => VideoOutput?.MediaView;

        IOverlayOutput OverlayOutput => TargetView as IOverlayOutput;

        void ApplyDisplay()
        {
            if (VideoOutput == null)
            {
                esPlayer.SetDisplay(null);
            }
            else
            {
                esPlayer.SetDisplay(TVForms.UIControls.MainWindowProvider());
                OverlayOutput.AreaUpdated += OnOverlayAreaUpdated;
                ApplyOverlayArea();
            }
        }

        void OnOverlayAreaUpdated(object sender, EventArgs e)
        {
            ApplyOverlayArea();
        }

        async void OnTargetViewPropertyChanged(object sender, global::System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Renderer")
            {
                if (Platform.GetRenderer(sender as BindableObject) != null && AutoPlay)
                {
                    await Start();
                }
                else if (Platform.GetRenderer(sender as BindableObject) == null && AutoStop)
                {
                    Stop();
                }
            }
        }

        async void ApplyOverlayArea()
        {
            try
            {
                if (OverlayOutput.OverlayArea.IsEmpty)
                {
                    esPlayer.SetDisplayMode(AspectMode.ToTVDisplayMode());
                }
                else
                {
                    var bound = OverlayOutput.OverlayArea.ToPixel();
                    var renderer = Platform.GetRenderer(TargetView);
                    if (renderer is OverlayViewRenderer)
                    {
                        var parentArea = renderer.NativeView.Geometry;
                        if (parentArea.Width == 0 || parentArea.Height == 0)
                        {
                            await Task.Delay(1);
                            parentArea = renderer.NativeView.Geometry;
                        }
                        bound = parentArea;
                    }
                    var roiBound = bound.ToMultimedia();
                    esPlayer.SetDisplayRoi(roiBound.X, roiBound.Y, roiBound.Width, roiBound.Height);
                }
            }
            catch (Exception e)
            {
                Log.Error(UIControls.Tag, $"Error on Update Overlayarea : {e.Message}");
            }
        }
    }
}