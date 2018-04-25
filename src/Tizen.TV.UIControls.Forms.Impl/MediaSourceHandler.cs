using System;
using System.Threading.Tasks;
using Tizen.Multimedia;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Tizen;

namespace Tizen.TV.UIControls.Forms.Impl
{

    public interface IMediaSourceHandler : IRegisterable
    {
        Task<bool> SetSource(Player player, MediaSource imageSource);
    }

    public sealed class UriMediaSourceHandler : IMediaSourceHandler
    {
        public Task<bool> SetSource(Player player, MediaSource source)
        {
            Console.WriteLine("Set source by UriSourceHandler");
            if (source is UriMediaSource uriSource)
            {
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
            Console.WriteLine("Set source by FileMediaSourceHandler");
            if (source is FileMediaSource fileSource)
            {
                Console.WriteLine("Set source by FileMediaSourceHandler2 : " + ResourcePath.GetPath(fileSource.File));
                player.SetSource(new MediaUriSource(ResourcePath.GetPath(fileSource.File)));
                Console.WriteLine("Set source by FileMediaSourceHandler2 end : " + ResourcePath.GetPath(fileSource.File));
            }
            return Task.FromResult<bool>(true);
        }
    }
}
