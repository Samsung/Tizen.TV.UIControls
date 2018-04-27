using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Tizen;

using Tizen.Multimedia;
using Xamarin.Forms.Internals;
using MMView = Tizen.Multimedia.MediaView;

namespace Tizen.TV.UIControls.Forms.Impl
{

    class MMPlayer : Player
    {
    }

    public class MediaPlayerImpl : IMediaPlayer
    {
        Player _player;

        bool _usesEmbeddingControls = true;
        bool _cancelToStart = false;
        DisplayAspectMode _aspectMode = DisplayAspectMode.AspectFit;

        Task _taskPrepare = null;

        IVideoOutput _videoOutput;

        MediaSource _source;

        public MediaPlayerImpl()
        {
            _player = new MMPlayer();
            _player.PlaybackCompleted += OnPlaybackCompleted;
            _player.BufferingProgressChanged += OnBufferingProgressChanged;

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

        void OnPlaybackCompleted(object sender, EventArgs e)
        {
            Console.WriteLine("OnPlaybackCompleted state : {0}", _player.State);
            PlaybackCompleted?.Invoke(this, EventArgs.Empty);
            Pause();
            var unused = Seek(0);
        }

        public bool UsesEmbeddingControls
        {
            get => _usesEmbeddingControls;
            set
            {
                _usesEmbeddingControls = value;
            }
        }

        public bool AutoPlay { get; set; }
        public bool AutoStop { get; set; }

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
                Console.WriteLine("Update Aspect mode to {0}", value);
                _aspectMode = value;

                if (IsOverlayMode)
                {
                    Console.WriteLine("UpdateOverlayArea 2");
                    UpdateOverlayArea();
                }
                else
                {
                    _player.DisplaySettings.Mode = value.ToMultimeida();
                }
            }
        }

        bool IsOverlayMode => OverlayOutput != null;

        public event EventHandler UpdateStreamInfo;
        public event EventHandler PlaybackCompleted;
        public event EventHandler PlaybackStarted;
        public event EventHandler<BufferingProgressUpdatedEventArgs> BufferingProgressUpdated;
        public event EventHandler PlaybackStopped;
        public event EventHandler PlaybackPaused;

        public void Pause()
        {
            try
            {
                _player.Pause();
                PlaybackPaused.Invoke(this, EventArgs.Empty);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error on Pause {0}", e.Message);
            }
            
        }

        public async Task<int> Seek(int ms)
        {
            try
            {
                Console.WriteLine("before Seek state : {0}", _player.State);
                await _player.SetPlayPositionAsync(ms, false);
                Console.WriteLine("after Seek state : {0}", _player.State);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Fail to seek {e.Message}");
            }
            return Position;
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
                if (renderer?.NativeView is MMView mediaView)
                {
                    Display display = new Display(mediaView);
                    _player.Display = display;
                    _player.DisplaySettings.Mode = _aspectMode.ToMultimeida();
                }
            }
            else
            {
                Console.WriteLine("apply display with window");
                Display display = new Display(UIControls.MainWindowProvider());
                _player.Display = display;
                OverlayOutput.AreaUpdated += OnOverlayAreaUpdated;
                Console.WriteLine("UpdateOverlayArea 3");
                UpdateOverlayArea();
            }

        }
        public void SetDisplay(IVideoOutput output)
        {
            VideoOutput = output;
        }


        async Task ApplySource()
        {
            Console.WriteLine("---- ApplySource - start");
            if (_source == null)
            {
                return;
            }
            IMediaSourceHandler handler = Registrar.Registered.GetHandlerForObject<IMediaSourceHandler>(_source);
            await handler.SetSource(_player, _source);
            Console.WriteLine("---- ApplySource - End");
        }


        public void SetSource(MediaSource source)
        {
            _source = source;
        }


        async void OnTargetViewPropertyChanged(object sender, global::System.ComponentModel.PropertyChangedEventArgs e)
        {
            Console.WriteLine("Target View property changed {0}", e.PropertyName);
            if (e.PropertyName == "Renderer")
            {
                Console.WriteLine("Render was added HasSource {0}", HasSource);
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
            Console.WriteLine("UpdateOverlayArea 1 state : {0}", _player.State);
            UpdateOverlayArea();
        }

        async void UpdateOverlayArea()
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
                    _player.DisplaySettings.SetRoi(OverlayOutput.OverlayArea.ToPixel().ToMultimedia());
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error : {e.Message}");
            }
        }


        public async Task<bool> Start()
        {
            _cancelToStart = false;
            if (!HasSource)
                return false;

            Console.WriteLine("Start1 State : {0}", _player.State);

            if (_player.State == PlayerState.Idle)
            {
                await Prepare();
            }

            Console.WriteLine("Start3 State : {0}", _player.State);

            if (_cancelToStart)
                return false;

            try
            {
                _player.Start();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error on start {0}", e.Message);
                return false;

            }
            PlaybackStarted?.Invoke(this, EventArgs.Empty);
            return true;
        }

        public void Stop()
        {
            Console.WriteLine("Stop");
            _cancelToStart = true;
            PlaybackStopped.Invoke(this, EventArgs.Empty);
            var unusedTask = ChangeToIdleState();
        }

        async Task Prepare()
        {
            TaskCompletionSource<bool> tcs = new TaskCompletionSource<bool>();
            var prevTask = TaskPrepare;
            TaskPrepare = tcs.Task;
            await prevTask;

            Console.WriteLine("Prepare1 : state : {0}", _player.State);

            if (_player.State == PlayerState.Ready)
                return;

            ApplyDisplay();
            await ApplySource();

            Console.WriteLine("Prepare2 : state : {0}", _player.State);
            try {
                await _player.PrepareAsync();
                UpdateStreamInfo?.Invoke(this, EventArgs.Empty);
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception on prepare : {0}", e.Message);
            }
            tcs.SetResult(true);
        }

        void OnBufferingProgressChanged(object sender, BufferingProgressChangedEventArgs e)
        {
            Console.WriteLine("Progress {0}%", e.Percent);
            Console.WriteLine("Progress2 {0}%", e.Percent / 100.0);
            BufferingProgressUpdated?.Invoke(this, new BufferingProgressUpdatedEventArgs { Progress = e.Percent / 100.0 });
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
        public static Multimedia.Rectangle ToMultimedia(this ElmSharp.Rect rect)
        {
            return new Multimedia.Rectangle(rect.X, rect.Y, rect.Width, rect.Height);
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
