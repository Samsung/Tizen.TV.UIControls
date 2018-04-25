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

    public interface IMediaPlayer
    {
        bool UsesEmbeddingControls { get; set; }
        bool AutoPlay { get; set; }
        bool AutoStop { get; set; }
        double Volume { get; set; }
        int Position { get; }

        int Duration { get; }

        DisplayAspectMode AspectMode { get; set; }

        event EventHandler PlaybackCompleted;
        event EventHandler UpdateStreamInfo;

        void SetDisplay(IVideoOutput output);

        void SetSource(MediaSource source);

        Task<bool> Start();
        void Stop();
        void Pause();
        Task<int> Seek(int ms);
    }
}
