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
using Tizen.Multimedia;
using Tizen.TV.Multimedia;
using Tizen.TV.UIControls.Forms.Renderer;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Platform.Tizen;
using MPlayer = Tizen.Multimedia.Player;
using TVPlayer  = Tizen.TV.Multimedia.Player;

[assembly: Xamarin.Forms.Dependency(typeof(MediaPlayerImpl))]
namespace Tizen.TV.UIControls.Forms.Renderer
{
    public class MediaPlayerImpl : IPlatformMediaPlayer
    {
        MPlayer _player;
        bool _cancelToStart;
        DisplayAspectMode _aspectMode = DisplayAspectMode.AspectFit;
        Task _taskPrepare;
        TaskCompletionSource<bool> _tcsForStreamInfo;
        IVideoOutput _videoOutput;
        MediaSource _source;
        DRMManager _drmManager;

        public MediaPlayerImpl()
        {
            _player = CreateMediaPlayer();
            _player.PlaybackCompleted += OnPlaybackCompleted;
            _player.BufferingProgressChanged += OnBufferingProgressChanged;
        }

        protected virtual MPlayer CreateMediaPlayer()
        {
            return new TVPlayer();
        }

        public DRMManager DRMManager
        {
            get => _drmManager;
            set
            {
                if (_player is TVPlayer tvPlayer)
                {
                    _drmManager = value;
                    if (value != null)
                    {
                        tvPlayer.SetDrm(value);
                    }
                }
                else
                {
                    Log.Debug(UIControls.Tag, "DRMManager is avaialbe only on TVPlayer.");
                }
            }
        }

        public MPlayer NativePlayer => _player;
        public bool UsesEmbeddingControls { get; set; }
        public bool AutoPlay { get; set; }

        public bool AutoStop { get; set; }
        public bool IsLooping
        {
            get => _player.IsLooping;
            set => _player.IsLooping = (bool)value;
        }
        public double Volume
        {
            get => _player.Volume;
            set => _player.Volume = (float)value;
        }

        public int Duration => _player.StreamInfo.GetDuration();

        public bool IsMuted
        {
            get => _player.Muted;
            set => _player.Muted = value;
        }

        public int Position
        {
            get
            {
                if (_player.State == PlayerState.Idle || _player.State == PlayerState.Preparing)
                    return 0;
                return _player.GetPlayPosition();
            }
        }

        public DisplayAspectMode AspectMode
        {
            get { return _aspectMode; }
            set
            {
                _aspectMode = value;
                if (IsOverlayMode)
                {
                    ApplyOverlayArea();
                }
                else
                {
                    ApplyAspectMode();
                }
            }
        }

        bool HasSource => _source != null;

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

        Task TaskPrepare
        {
            get => _taskPrepare ?? Task.CompletedTask;
            set => _taskPrepare = value;
        }

        bool IsOverlayMode => OverlayOutput != null;

        public event EventHandler UpdateStreamInfo;
        public event EventHandler PlaybackCompleted;
        public event EventHandler PlaybackStarted;
        public event EventHandler<BufferingProgressUpdatedEventArgs> BufferingProgressUpdated;
        public event EventHandler PlaybackStopped;
        public event EventHandler PlaybackPaused;

        public async Task<bool> Start()
        {
           
            _cancelToStart = false;
            if (!HasSource)
                return false;

            if (_player.State == PlayerState.Idle)
            {
                await Prepare();
            }

            if (_cancelToStart)
                return false;

            try
            {
                _player.Start();
            }
            catch (Exception e)
            {
                 return false;
            }
            PlaybackStarted?.Invoke(this, EventArgs.Empty);
            return true;
        }

        public void Pause()
        {
            Log.Debug(UIControls.Tag, "Pause");

            try
            {
                _player.Pause();
                PlaybackPaused.Invoke(this, EventArgs.Empty);
            }
            catch (Exception e)
            {
                Log.Error(UIControls.Tag, $"Error on Pause : {e.Message}");
            }
        }

        public void Stop()
        {
            Log.Debug(UIControls.Tag, "Stop");

            _cancelToStart = true;
            var unusedTask = ChangeToIdleState();
            PlaybackStopped.Invoke(this, EventArgs.Empty);
            if (DRMManager != null)
            {
                DRMManager.Close();
                DRMManager.Dispose();
                DRMManager = null;
            }
        }

        public void SetDisplay(IVideoOutput output)
        {
            VideoOutput = output;
        }

        public async Task<int> Seek(int ms)
        {
            try
            {
                await _player.SetPlayPositionAsync(ms, false);
            }
            catch (Exception e)
            {
                Log.Error(UIControls.Tag, $"Fail to seek : {e.Message}");
            }
            return Position;
        }

        public void SetSource(MediaSource source)
        {
            _source = source;
        }

        public async Task<Stream> GetAlbumArts()
        {
            if (_player.State == PlayerState.Idle)
            {
                if (_tcsForStreamInfo == null || _tcsForStreamInfo.Task.IsCompleted)
                {
                    _tcsForStreamInfo = new TaskCompletionSource<bool>();
                }
                await _tcsForStreamInfo.Task;
            }
            await TaskPrepare;

            var imageData = _player.StreamInfo.GetAlbumArt();
            if (imageData == null)
                return null;
            return new MemoryStream(imageData);
        }

        public async Task<IDictionary<string, string>> GetMetadata()
        {
            if (_player.State == PlayerState.Idle)
            {
                if (_tcsForStreamInfo == null || _tcsForStreamInfo.Task.IsCompleted)
                {
                    _tcsForStreamInfo = new TaskCompletionSource<bool>();
                }
                await _tcsForStreamInfo.Task;
            }
            await TaskPrepare;

            Dictionary<string, string> metadata = new Dictionary<string, string>
            {
                [nameof(StreamMetadataKey.Album)] = _player.StreamInfo.GetMetadata(StreamMetadataKey.Album),
                [nameof(StreamMetadataKey.Artist)] = _player.StreamInfo.GetMetadata(StreamMetadataKey.Artist),
                [nameof(StreamMetadataKey.Author)] = _player.StreamInfo.GetMetadata(StreamMetadataKey.Author),
                [nameof(StreamMetadataKey.Genre)] = _player.StreamInfo.GetMetadata(StreamMetadataKey.Genre),
                [nameof(StreamMetadataKey.Title)] = _player.StreamInfo.GetMetadata(StreamMetadataKey.Title),
                [nameof(StreamMetadataKey.Year)] = _player.StreamInfo.GetMetadata(StreamMetadataKey.Year)
            };
            return metadata;
        }
        void ApplyDisplay()
        {
            if (VideoOutput == null)
            {
                _player.Display = null;
            }
            else if (!IsOverlayMode)
            {
                var renderer = Platform.GetRenderer(TargetView);
                if (renderer is IMediaViewProvider provider && provider.GetMediaView() != null)
                {
                    try
                    {
                        Display display = new Display(provider.GetMediaView());
                        _player.Display = display;
                        _player.DisplaySettings.Mode = _aspectMode.ToMultimeida();
                    }
                    catch
                    {
                        Log.Error(UIControls.Tag, "Error on MediaView");
                    }
                }
            }
            else
            {
                Display display = new Display(UIControls.MainWindowProvider());
                _player.Display = display;
                OverlayOutput.AreaUpdated += OnOverlayAreaUpdated;
                ApplyOverlayArea();
            }
        }

        async void ApplyOverlayArea()
        {
            if (_player.State == PlayerState.Preparing)
            {
                await TaskPrepare;
            }

            try
            {
                if (OverlayOutput.OverlayArea.IsEmpty)
                {
                    _player.DisplaySettings.Mode = AspectMode.ToMultimeida();
                }
                else
                {
                    _player.DisplaySettings.Mode = PlayerDisplayMode.Roi;
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
                    _player.DisplaySettings.SetRoi(bound.ToMultimedia());
                }
            }
            catch (Exception e)
            {
                Log.Error(UIControls.Tag, $"Error on Update Overlayarea : {e.Message}");
            }
        }

        async Task ApplySource()
        {
            if (_source == null)
            {
                return;
            }
            IMediaSourceHandler handler = Registrar.Registered.GetHandlerForObject<IMediaSourceHandler>(_source);
            await handler.SetSource(this, _source);
        }

        async void OnTargetViewPropertyChanged(object sender, global::System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Renderer")
            {
                if (Platform.GetRenderer(sender as BindableObject) != null && HasSource && AutoPlay)
                {
                    await Start();
                }
                else if (Platform.GetRenderer(sender as BindableObject) == null && AutoStop)
                {
                    Stop();
                }
            }
        }

        void OnOverlayAreaUpdated(object sender, EventArgs e)
        {
            ApplyOverlayArea();
        }

        async Task Prepare()
        {
            TaskCompletionSource<bool> tcs = new TaskCompletionSource<bool>();
            var prevTask = TaskPrepare;
            TaskPrepare = tcs.Task;
            await prevTask;

            if (_player.State == PlayerState.Ready)
                return;

            ApplyDisplay();
            await ApplySource();

            try {
                await _player.PrepareAsync();
                UpdateStreamInfo?.Invoke(this, EventArgs.Empty);
                _tcsForStreamInfo?.TrySetResult(true);
            }
            catch (Exception e)
            {
                Log.Error(UIControls.Tag, $"Error on prepare : {e.Message}");
            }
            tcs.SetResult(true);
        }

        async void ApplyAspectMode()
        {
            if (_player.State == PlayerState.Preparing)
            {
                await TaskPrepare;
            }
            _player.DisplaySettings.Mode = AspectMode.ToMultimeida();
        }

        void OnBufferingProgressChanged(object sender, BufferingProgressChangedEventArgs e)
        {
            BufferingProgressUpdated?.Invoke(this, new BufferingProgressUpdatedEventArgs { Progress = e.Percent / 100.0 });
        }

        void OnPlaybackCompleted(object sender, EventArgs e)
        {
            PlaybackCompleted?.Invoke(this, EventArgs.Empty);
        }

        async Task ChangeToIdleState()
        {
            switch (_player.State)
            {
                case PlayerState.Playing:
                case PlayerState.Paused:
                    _player.Stop();
                    _player.Unprepare();
                    break;
                case PlayerState.Ready:
                    _player.Unprepare();
                    break;
                case PlayerState.Preparing:
                    await TaskPrepare;
                    _player.Unprepare();
                    break;
            }
        }
    }

    public static class MultimediaConvertExtensions
    {
        public static Tizen.Multimedia.Rectangle ToMultimedia(this ElmSharp.Rect rect)
        {
            return new Tizen.Multimedia.Rectangle(rect.X, rect.Y, rect.Width, rect.Height);
        }

        public static PlayerDisplayMode ToMultimeida(this DisplayAspectMode mode)
        {
            PlayerDisplayMode ret = PlayerDisplayMode.LetterBox;
            switch (mode)
            {
                case DisplayAspectMode.AspectFill:
                    ret = PlayerDisplayMode.CroppedFull;
                    break;
                case DisplayAspectMode.AspectFit:
                    ret = PlayerDisplayMode.LetterBox;
                    break;
                case DisplayAspectMode.Fill:
                    ret = PlayerDisplayMode.FullScreen;
                    break;
                case DisplayAspectMode.OrignalSize:
                    ret = PlayerDisplayMode.OriginalOrFull;
                    break;
            }
            return ret;
        }
    }
}
