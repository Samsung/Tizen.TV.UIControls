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
        public static readonly BindableProperty AspectModeProperty = BindableProperty.Create(nameof(AspectMode), typeof(DisplayAspectMode), typeof(MediaPlayer), DisplayAspectMode.AspectFit, propertyChanged: (b, o, n) => ((MediaPlayer)b).OnAspectModeChanged());
        public static readonly BindableProperty AutoPlayProperty = BindableProperty.Create("AutoPlay", typeof(bool), typeof(MediaPlayer), false, propertyChanged: (b, o, n) => ((MediaPlayer)b).UpdateAutoPlay());
        public static readonly BindableProperty AutoStopProperty = BindableProperty.Create("AutoStop", typeof(bool), typeof(MediaPlayer), true, propertyChanged: (b, o, n) => ((MediaPlayer)b).UpdateAutoStop());
        internal static readonly BindablePropertyKey DurationPropertyKey = BindableProperty.CreateReadOnly("Duration", typeof(int), typeof(MediaPlayer), 0);
        public static readonly BindableProperty DurationProperty = DurationPropertyKey.BindableProperty;


        IMediaPlayer _impl;
        public MediaPlayer()
        {
            _impl = DependencyService.Get<IMediaPlayer>(fetchTarget: DependencyFetchTarget.NewInstance);
            _impl.UpdateStreamInfo += OnUpdateStreamInfo; ;
            _impl.PlaybackCompleted += SendPlayCompleted;
            _impl.UsesEmbeddingControls = true;
            _impl.Volume = 1d;
            _impl.AspectMode = DisplayAspectMode.AspectFit;
            _impl.AutoPlay = false;
            _impl.AutoStop = true;
        }

        [TypeConverter(typeof(ImageSourceConverter))]
        public MediaSource Source
        {
            get { return (MediaSource)GetValue(SourceProperty); }
            set { SetValue(SourceProperty, value); }
        }

        public DisplayAspectMode AspectMode
        {
            get { return (DisplayAspectMode)GetValue(AspectModeProperty); }
            set { SetValue(AspectModeProperty, value); }
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

        public event EventHandler PlaybackCompleted;

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

        void OnUpdateStreamInfo(object sender, EventArgs e)
        {
            Duration = _impl.Duration;
        }

        void SendPlayCompleted(object sender, EventArgs e)
        {
            PlaybackCompleted?.Invoke(this, EventArgs.Empty);
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
