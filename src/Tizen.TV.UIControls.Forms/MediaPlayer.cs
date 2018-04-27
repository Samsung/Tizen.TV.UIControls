using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace Tizen.TV.UIControls.Forms
{
    public class MediaPlayer : Element
    {
        public static readonly BindableProperty SourceProperty = BindableProperty.Create("Source", typeof(MediaSource), typeof(MediaPlayer), default(MediaSource), propertyChanging: OnSourceChanging, propertyChanged: OnSourceChanged);
        public static readonly BindableProperty VideoOutputProperty = BindableProperty.Create("VideoOutput", typeof(IVideoOutput), typeof(MediaPlayer), null, propertyChanging: null, propertyChanged: (b, o, n) => ((MediaPlayer)b).OnVideoOutputChanged());
        public static readonly BindableProperty UsesEmbeddingControlsProperty = BindableProperty.Create("UsesEmbeddingControls", typeof(bool), typeof(MediaPlayer), true);
        public static readonly BindableProperty VolumeProperty = BindableProperty.Create("Volume", typeof(double), typeof(MediaPlayer), 1d, coerceValue: (bindable, value) => ((double)value).Clamp(0, 1), propertyChanged: (b, o, n)=> ((MediaPlayer)b).OnVolumeChanged());
        public static readonly BindableProperty IsMutedProperty = BindableProperty.Create("IsMuted", typeof(bool), typeof(MediaPlayer), false, propertyChanged: (b, o, n) => ((MediaPlayer)b).UpdateIsMuted());
        public static readonly BindableProperty AspectModeProperty = BindableProperty.Create(nameof(AspectMode), typeof(DisplayAspectMode), typeof(MediaPlayer), DisplayAspectMode.AspectFit, propertyChanged: (b, o, n) => ((MediaPlayer)b).OnAspectModeChanged());
        public static readonly BindableProperty AutoPlayProperty = BindableProperty.Create("AutoPlay", typeof(bool), typeof(MediaPlayer), false, propertyChanged: (b, o, n) => ((MediaPlayer)b).UpdateAutoPlay());
        public static readonly BindableProperty AutoStopProperty = BindableProperty.Create("AutoStop", typeof(bool), typeof(MediaPlayer), true, propertyChanged: (b, o, n) => ((MediaPlayer)b).UpdateAutoStop());
        static readonly BindablePropertyKey DurationPropertyKey = BindableProperty.CreateReadOnly("Duration", typeof(int), typeof(MediaPlayer), 0);
        public static readonly BindableProperty DurationProperty = DurationPropertyKey.BindableProperty;
        static readonly BindablePropertyKey BufferingProgressPropertyKey = BindableProperty.CreateReadOnly("BufferingProgress", typeof(double), typeof(MediaPlayer), 0d);
        public static readonly BindableProperty BufferingProgressProperty = BufferingProgressPropertyKey.BindableProperty;
        static readonly BindablePropertyKey PositionPropertyKey = BindableProperty.CreateReadOnly("Position", typeof(int), typeof(MediaPlayer), 0);
        public static readonly BindableProperty PositionProperty = PositionPropertyKey.BindableProperty;
        static readonly BindablePropertyKey StatePropertyKey = BindableProperty.CreateReadOnly("State", typeof(PlaybackState), typeof(MediaPlayer), PlaybackState.Stopped, propertyChanged: (b, o, n)=> { Console.WriteLine($"State :{n}"); });
        public static readonly BindableProperty StateProperty = StatePropertyKey.BindableProperty;
        public static readonly BindableProperty PositionUpdateIntervalProperty = BindableProperty.Create("PositionUpdateInterval", typeof(int), typeof(MediaPlayer), 500);
        static readonly BindablePropertyKey IsBufferingPropertyKey = BindableProperty.CreateReadOnly("IsBuffering", typeof(bool), typeof(MediaPlayer), false);
        public static readonly BindableProperty IsBufferingProperty = IsBufferingPropertyKey.BindableProperty;


        IMediaPlayer _impl;
        bool _isPlaying;


        public MediaPlayer()
        {
            _impl = DependencyService.Get<IMediaPlayer>(fetchTarget: DependencyFetchTarget.NewInstance);
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

        [TypeConverter(typeof(ImageSourceConverter))]
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
                return Position = _impl.Position;
            }
            private set
            {
                SetValue(PositionPropertyKey, value);
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
            return _impl.Seek(ms);
        }

        public Task<bool> Start()
        {
            return _impl.Start();
        }

        public void Stop()
        {
            _impl.Stop();
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
        }

        void SendPlaybackPaused(object sender, EventArgs e)
        {
            State = PlaybackState.Paused;
            _isPlaying = false;
            PlaybackPaused?.Invoke(this, EventArgs.Empty);
        }


        void SendPlaybackStopped(object sender, EventArgs e)
        {
            _isPlaying = false;
            State = PlaybackState.Stopped;
            PlaybackStopped?.Invoke(this, EventArgs.Empty);
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
            _impl.SetDisplay(VideoOutput);
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
