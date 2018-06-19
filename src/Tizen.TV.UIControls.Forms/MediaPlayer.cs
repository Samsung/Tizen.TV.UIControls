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
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace Tizen.TV.UIControls.Forms
{
    public class MediaPlayer : Element
    {
        public static readonly BindableProperty SourceProperty = BindableProperty.Create(nameof(Source), typeof(MediaSource), typeof(MediaPlayer), default(MediaSource), propertyChanging: OnSourceChanging, propertyChanged: OnSourceChanged);
        public static readonly BindableProperty VideoOutputProperty = BindableProperty.Create(nameof(VideoOutput), typeof(IVideoOutput), typeof(MediaPlayer), null, propertyChanging: null, propertyChanged: (b, o, n) => ((MediaPlayer)b).OnVideoOutputChanged());
        public static readonly BindableProperty UsesEmbeddingControlsProperty = BindableProperty.Create(nameof(UsesEmbeddingControls), typeof(bool), typeof(MediaPlayer), true, propertyChanged: (b, o, n) => ((MediaPlayer)b).OnUsesEmbeddingControlsChanged());
        public static readonly BindableProperty VolumeProperty = BindableProperty.Create(nameof(Volume), typeof(double), typeof(MediaPlayer), 1d, coerceValue: (bindable, value) => ((double)value).Clamp(0, 1), propertyChanged: (b, o, n)=> ((MediaPlayer)b).OnVolumeChanged());
        public static readonly BindableProperty IsMutedProperty = BindableProperty.Create(nameof(IsMuted), typeof(bool), typeof(MediaPlayer), false, propertyChanged: (b, o, n) => ((MediaPlayer)b).UpdateIsMuted());
        public static readonly BindableProperty AspectModeProperty = BindableProperty.Create(nameof(AspectMode), typeof(DisplayAspectMode), typeof(MediaPlayer), DisplayAspectMode.AspectFit, propertyChanged: (b, o, n) => ((MediaPlayer)b).OnAspectModeChanged());
        public static readonly BindableProperty AutoPlayProperty = BindableProperty.Create(nameof(AutoPlay), typeof(bool), typeof(MediaPlayer), false, propertyChanged: (b, o, n) => ((MediaPlayer)b).UpdateAutoPlay());
        public static readonly BindableProperty AutoStopProperty = BindableProperty.Create(nameof(AutoStop), typeof(bool), typeof(MediaPlayer), true, propertyChanged: (b, o, n) => ((MediaPlayer)b).UpdateAutoStop());
        static readonly BindablePropertyKey DurationPropertyKey = BindableProperty.CreateReadOnly(nameof(Duration), typeof(int), typeof(MediaPlayer), 0);
        public static readonly BindableProperty DurationProperty = DurationPropertyKey.BindableProperty;
        static readonly BindablePropertyKey BufferingProgressPropertyKey = BindableProperty.CreateReadOnly(nameof(BufferingProgress), typeof(double), typeof(MediaPlayer), 0d);
        public static readonly BindableProperty BufferingProgressProperty = BufferingProgressPropertyKey.BindableProperty;
        static readonly BindablePropertyKey PositionPropertyKey = BindableProperty.CreateReadOnly(nameof(Position), typeof(int), typeof(MediaPlayer), 0);
        public static readonly BindableProperty PositionProperty = PositionPropertyKey.BindableProperty;
        static readonly BindablePropertyKey StatePropertyKey = BindableProperty.CreateReadOnly(nameof(State), typeof(PlaybackState), typeof(MediaPlayer), PlaybackState.Stopped);
        public static readonly BindableProperty StateProperty = StatePropertyKey.BindableProperty;
        public static readonly BindableProperty PositionUpdateIntervalProperty = BindableProperty.Create(nameof(PositionUpdateInterval), typeof(int), typeof(MediaPlayer), 500);
        static readonly BindablePropertyKey IsBufferingPropertyKey = BindableProperty.CreateReadOnly(nameof(IsBuffering), typeof(bool), typeof(MediaPlayer), false);
        public static readonly BindableProperty IsBufferingProperty = IsBufferingPropertyKey.BindableProperty;

        IPlatformMediaPlayer _impl;
        bool _isPlaying;
        bool _controlsAlwaysVisible;
        CancellationTokenSource _hideTimerCTS = new CancellationTokenSource();
        Lazy<View> _controls;

        public MediaPlayer()
        {
            _impl = DependencyService.Get<IPlatformMediaPlayer>(fetchTarget: DependencyFetchTarget.NewInstance);
            _impl.UpdateStreamInfo += OnUpdateStreamInfo;
            _impl.PlaybackCompleted += SendPlaybackCompleted;
            _impl.PlaybackStarted += SendPlaybackStarted;
            _impl.PlaybackPaused += SendPlaybackPaused;
            _impl.PlaybackStopped += SendPlaybackStopped;
            _impl.BufferingProgressUpdated += OnUpdateBufferingProgress;
            _impl.UsesEmbeddingControls = true;
            _impl.Volume = 1d;
            _impl.AspectMode = DisplayAspectMode.AspectFit;
            _impl.AutoPlay = false;
            _impl.AutoStop = true;

            _controlsAlwaysVisible = false;
            _controls = new Lazy<View>(() =>
            {
                return new EmbeddingControls()
                {
                    BindingContext = this
                };
            });
        }

        public DisplayAspectMode AspectMode
        {
            get { return (DisplayAspectMode)GetValue(AspectModeProperty); }
            set { SetValue(AspectModeProperty, value); }
        }

        public bool AutoPlay
        {
            get
            {
                return (bool)GetValue(AutoPlayProperty);
            }
            set
            {
                SetValue(AutoPlayProperty, value);
            }
        }

        public bool AutoStop
        {
            get
            {
                return (bool)GetValue(AutoStopProperty);
            }
            set
            {
                SetValue(AutoStopProperty, value);
            }
        }

        public double BufferingProgress
        {
            get
            {
                return (double)GetValue(BufferingProgressProperty);
            }
            private set
            {
                SetValue(BufferingProgressPropertyKey, value);
            }
        }

        public int Duration
        {
            get
            {
                return (int)GetValue(DurationProperty);
            }
            private set
            {
                SetValue(DurationPropertyKey, value);
            }
        }

        [Xamarin.Forms.TypeConverter(typeof(ImageSourceConverter))]
        public MediaSource Source
        {
            get { return (MediaSource)GetValue(SourceProperty); }
            set { SetValue(SourceProperty, value); }
        }

        public IVideoOutput VideoOutput
        {
            get { return (IVideoOutput)GetValue(VideoOutputProperty); }
            set { SetValue(VideoOutputProperty, value); }
        }

        public double Volume
        {
            get { return (double)GetValue(VolumeProperty); }
            set { SetValue(VolumeProperty, value); }
        }

        public bool IsMuted
        {
            get { return (bool)GetValue(IsMutedProperty); }
            set { SetValue(IsMutedProperty, value); }
        }

        public int PositionUpdateInterval
        {
            get { return (int)GetValue(PositionUpdateIntervalProperty); }
            set { SetValue(PositionUpdateIntervalProperty, value); }
        }

        public bool UsesEmbeddingControls
        {
            get
            {
                return (bool)GetValue(UsesEmbeddingControlsProperty);
            }
            set
            {
                SetValue(UsesEmbeddingControlsProperty, value);
                _impl.UsesEmbeddingControls = value;
            }
        }

        public int Position
        {
            get
            {
                return _impl.Position;
            }
            private set
            {
                SetValue(PositionPropertyKey, value);
                OnPropertyChanged(nameof(Progress));
            }
        }

        public PlaybackState State
        {
            get
            {
                return (PlaybackState)GetValue(StateProperty);
            }
            private set
            {
                SetValue(StatePropertyKey, value);
            }
        }

        public bool IsBuffering
        {
            get
            {
                return (bool)GetValue(IsBufferingProperty);
            }
            private set
            {
                SetValue(IsBufferingPropertyKey, value);
            }
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public double Progress
        {
            get
            {
                return Position / (double)Math.Max(Position, Duration);
            }
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public Command StartCommand => new Command(() =>
        {
            if (State == PlaybackState.Playing)
            {
                Pause();
            }
            else
            {
                Start();
            }
        });

        [EditorBrowsable(EditorBrowsableState.Never)]
        public Command FastForwardCommand => new Command(() =>
        {
            if (State != PlaybackState.Stopped)
            {
                Seek(Math.Min(Position + 5000, Duration));
            }
        }, () => State != PlaybackState.Stopped);

        [EditorBrowsable(EditorBrowsableState.Never)]
        public Command RewindCommand => new Command(() =>
        {
            if (State != PlaybackState.Stopped)
            {
                Seek(Math.Max(Position - 5000, 0));
            }
        }, () => State != PlaybackState.Stopped);

        public event EventHandler PlaybackCompleted;
        public event EventHandler PlaybackStarted;
        public event EventHandler PlaybackPaused;
        public event EventHandler PlaybackStopped;
        public event EventHandler BufferingStarted;
        public event EventHandler BufferingCompleted;

        public void Pause()
        {
            _impl.Pause();
        }

        public Task<int> Seek(int ms)
        {
            ShowController();
            return _impl.Seek(ms).ContinueWith((t) => Position = _impl.Position);
        }

        public Task<bool> Start()
        {
            return _impl.Start();
        }

        public void Stop()
        {
            _impl.Stop();
        }

        public Task<Stream> GetAlbumArts()
        {
            return _impl.GetAlbumArts();
        }

        public Task<IDictionary<string, string>> GetMetadata()
        {
            return _impl.GetMetadata();
        }

        void UpdateAutoPlay()
        {
            _impl.AutoPlay = AutoPlay;
        }

        void UpdateAutoStop()
        {
            _impl.AutoStop = AutoStop;
        }

        void UpdateIsMuted()
        {
            _impl.IsMuted = IsMuted;
        }

        void OnUpdateStreamInfo(object sender, EventArgs e)
        {
            Duration = _impl.Duration;
        }

        void SendPlaybackCompleted(object sender, EventArgs e)
        {
            PlaybackCompleted?.Invoke(this, EventArgs.Empty);
        }

        void SendPlaybackStarted(object sender, EventArgs e)
        {
            _isPlaying = true;
            State = PlaybackState.Playing;
            StartPostionPollingTimer();
            PlaybackStarted?.Invoke(this, EventArgs.Empty);
            _controlsAlwaysVisible = false;
            ShowController();
        }

        void SendPlaybackPaused(object sender, EventArgs e)
        {
            _isPlaying = false;
            State = PlaybackState.Paused;
            PlaybackPaused?.Invoke(this, EventArgs.Empty);
            _controlsAlwaysVisible = true;
            ShowController();
        }


        void SendPlaybackStopped(object sender, EventArgs e)
        {
            _isPlaying = false;
            State = PlaybackState.Stopped;
            PlaybackStopped?.Invoke(this, EventArgs.Empty);
            _controlsAlwaysVisible = true;
            ShowController();
        }


        void StartPostionPollingTimer()
        {
            Device.StartTimer(TimeSpan.FromMilliseconds(PositionUpdateInterval), () =>
            {
                Position = _impl.Position;
                return _isPlaying;
            });
        }

        void OnSourceChanged(object sender, EventArgs e)
        {
            _impl.SetSource(Source);
        }

        void OnSourceChanging(MediaSource oldValue, MediaSource newValue)
        {
            if (oldValue != null)
                oldValue.SourceChanged -= OnSourceChanged;

            if (newValue != null)
                newValue.SourceChanged += OnSourceChanged;
        }

        void OnVideoOutputChanged()
        {
            if (VideoOutput != null)
            {
                if (UsesEmbeddingControls)
                {
                    VideoOutput.Controller = _controls.Value;
                }
                VideoOutput.MediaView.Focused += OnVideoOutputFocused;
                InputEvents.GetEventHandlers(VideoOutput.MediaView).Add(new RemoteKeyHandler(OnVideoOutputKeyEvent));
                if (VideoOutput.MediaView is View outputView)
                {
                    TapGestureRecognizer tapGesture = new TapGestureRecognizer();
                    tapGesture.Tapped += OnOutputTapped;
                    outputView.GestureRecognizers.Add(tapGesture);
                }
            }
            _impl.SetDisplay(VideoOutput);
        }

        void OnOutputTapped(object sender, EventArgs e)
        {
            if (!UsesEmbeddingControls)
                return;
            if (!_controls.Value.IsVisible)
            {
                ShowController();
            }
        }

        async void OnUsesEmbeddingControlsChanged()
        {
            if (UsesEmbeddingControls)
            {
                if (VideoOutput != null)
                {
                    VideoOutput.Controller = _controls.Value;
                    ShowController();
                }
            }
            else
            {
                if (VideoOutput != null)
                {
                    HideController(0);
                    await Task.Delay(200);
                    VideoOutput.Controller = null;
                }
            }
        }

        void OnVideoOutputKeyEvent(RemoteControlKeyEventArgs args)
        {
            if (args.KeyType == RemoteControlKeyTypes.KeyDown)
                return;
            if (!UsesEmbeddingControls)
                return;

            if (!_controls.Value.IsVisible)
            {
                ShowController();
                return;
            }

            if (args.KeyName == RemoteControlKeyNames.Left)
            {
                if (RewindCommand.CanExecute(null))
                {
                    args.Handled = true;
                    RewindCommand.Execute(null);
                }
            }
            else if (args.KeyName == RemoteControlKeyNames.Right)
            {
                if (FastForwardCommand.CanExecute(null))
                {
                    args.Handled = true;
                    FastForwardCommand.Execute(null);
                }
            }
            else if (args.KeyName == RemoteControlKeyNames.Return)
            {
                if (StartCommand.CanExecute(null))
                {
                    args.Handled = true;
                    StartCommand.Execute(null);
                }
            }
        }

        void OnVideoOutputFocused(object sender, FocusEventArgs e)
        {
            if (UsesEmbeddingControls)
            {
                ShowController();
            }
        }

        void OnVolumeChanged()
        {
            _impl.Volume = Volume;
        }

        void OnAspectModeChanged()
        {
            _impl.AspectMode = AspectMode;
        }

        void OnUpdateBufferingProgress(object sender, BufferingProgressUpdatedEventArgs e)
        {
            if (!IsBuffering && e.Progress >= 0)
            {
                IsBuffering = true;
                BufferingStarted?.Invoke(this, EventArgs.Empty);
            }
            else if (IsBuffering && e.Progress == 1.0)
            {
                IsBuffering = false;
                BufferingCompleted?.Invoke(this, EventArgs.Empty);
            }
            BufferingProgress = e.Progress;
        }

        async void HideController(int after)
        {
            if (!_controls.IsValueCreated)
                return;

            _hideTimerCTS?.Cancel();
            _hideTimerCTS = new CancellationTokenSource();
            try
            {
                await Task.Delay(after, _hideTimerCTS.Token);

                if (!_controlsAlwaysVisible)
                {
                    await _controls.Value.FadeTo(0, 200);
                    _controls.Value.IsVisible = false;
                }
            }
            catch (Exception)
            {
            }
        }
        
        void ShowController()
        {
            if (_controls.IsValueCreated)
            {
                _controls.Value.IsVisible = true;
                _controls.Value.FadeTo(1.0, 200);
                HideController(5000);
            }
        }


        static void OnSourceChanging(BindableObject bindable, object oldValue, object newValue)
        {
            (bindable as MediaPlayer)?.OnSourceChanging(oldValue as MediaSource, newValue as MediaSource);
        }
        static void OnSourceChanged(BindableObject bindable, object oldValue, object newValue)
        {
            (bindable as MediaPlayer)?.OnSourceChanged(bindable, EventArgs.Empty);
        }
    }
}
