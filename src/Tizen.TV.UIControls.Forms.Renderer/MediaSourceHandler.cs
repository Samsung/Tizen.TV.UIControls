using System.Threading.Tasks;
using Tizen.Multimedia;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Tizen;

namespace Tizen.TV.UIControls.Forms.Renderer
{

    public interface IMediaSourceHandler : IRegisterable
    {
        Task<bool> SetSource(Player player, MediaSource imageSource);
    }

    public sealed class UriMediaSourceHandler : IMediaSourceHandler
    {
        public Task<bool> SetSource(Player player, MediaSource source)
        {
            if (source is UriMediaSource uriSource)
            {
                Log.Info(UIControls.Tag, $"Set UriMediaSource");
                var uri = uriSource.Uri;
                player.SetSource(new MediaUriSource(uri.IsFile ? uri.LocalPath : uri.AbsoluteUri));
            }
            return Task.FromResult<bool>(true);
        }
    }

    public sealed class FileMediaSourceHandler : IMediaSourceHandler
    {
        public Task<bool> SetSource(Player player, MediaSource source)
        {
            if (source is FileMediaSource fileSource)
            {
                Log.Info(UIControls.Tag, $"Set FileMediaSource");
                player.SetSource(new MediaUriSource(ResourcePath.GetPath(fileSource.File)));
            }
            return Task.FromResult<bool>(true);
        }
    }
}
