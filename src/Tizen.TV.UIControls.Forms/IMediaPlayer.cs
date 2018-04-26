using System;
using System.Threading.Tasks;

namespace Tizen.TV.UIControls.Forms
{
    public enum DisplayAspectMode
    {
        Fill,
        AspectFit,
        AspectFill,
        OrignalSize
    }

    public class BufferingProgressUpdatedEventArgs : EventArgs
    {
        public double Progress { get; set; }
    }

    public interface IMediaPlayer
    {
        bool UsesEmbeddingControls { get; set; }
        bool AutoPlay { get; set; }
        bool AutoStop { get; set; }
        double Volume { get; set; }
        bool IsMuted { get; set; }
        int Position { get; }

        int Duration { get; }

        DisplayAspectMode AspectMode { get; set; }

        event EventHandler PlaybackCompleted;
        event EventHandler PlaybackStarted;
        event EventHandler UpdateStreamInfo;
        event EventHandler<BufferingProgressUpdatedEventArgs> BufferingProgressUpdated;

        void SetDisplay(IVideoOutput output);

        void SetSource(MediaSource source);

        Task<bool> Start();
        void Stop();
        void Pause();
        Task<int> Seek(int ms);
    }
}
