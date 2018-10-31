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

using System.Threading.Tasks;
using Tizen.Multimedia;
using Tizen.TV.UIControls.Forms;
using Tizen.TV.UIControls.Forms.Renderer;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Tizen;

[assembly: ExportHandler(typeof(UriMediaSource), typeof(UriMediaSourceHandler))]
[assembly: ExportHandler(typeof(FileMediaSource), typeof(FileMediaSourceHandler))]
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
