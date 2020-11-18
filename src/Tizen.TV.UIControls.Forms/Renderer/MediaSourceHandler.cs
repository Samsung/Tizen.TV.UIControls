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
using Tizen.TV.UIControls.Forms.Renderer;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Tizen;
using DRMMediaSource = Tizen.TV.UIControls.Forms.DRMMediaSource;
using TUriMediaSource = Tizen.TV.UIControls.Forms.UriMediaSource;
using TFileMediaSource = Tizen.TV.UIControls.Forms.FileMediaSource;
using System.Collections.Generic;

[assembly: ExportHandler(typeof(DRMMediaSource), typeof(DRMMediaSourceHandler))]
[assembly: ExportHandler(typeof(TUriMediaSource), typeof(UriMediaSourceHandler))]
[assembly: ExportHandler(typeof(TFileMediaSource), typeof(FileMediaSourceHandler))]
namespace Tizen.TV.UIControls.Forms.Renderer
{

    public interface IMediaSourceHandler : IRegisterable
    {
        Task<bool> SetSource(MediaPlayerImpl player, MediaSource imageSource);
    }

    public sealed class UriMediaSourceHandler : IMediaSourceHandler
    {
        public Task<bool> SetSource(MediaPlayerImpl player, MediaSource source)
        {
            if (source is UriMediaSource uriSource)
            {
                Log.Info(UIControls.Tag, $"Set UriMediaSource");
                var uri = uriSource.Uri;
                player.NativePlayer.SetSource(new MediaUriSource(uri.IsFile ? uri.LocalPath : uri.AbsoluteUri));
            }
            return Task.FromResult<bool>(true);
        }
    }

    public sealed class FileMediaSourceHandler : IMediaSourceHandler
    {
        public Task<bool> SetSource(MediaPlayerImpl player, MediaSource source)
        {
            if (source is FileMediaSource fileSource)
            {
                Log.Info(UIControls.Tag, $"Set FileMediaSource");
                player.NativePlayer.SetSource(new MediaUriSource(ResourcePath.GetPath(fileSource.File)));
            }
            return Task.FromResult<bool>(true);
        }
    }
    public sealed class DRMMediaSourceHandler : IMediaSourceHandler
    {
        public Task<bool> SetSource(MediaPlayerImpl player, TV.UIControls.Forms.MediaSource source)
        {
            if (source is DRMMediaSource uriSource)
            {
                var platformDrmMgr = player.DRMManager;
                if (player.IsDRMOpened == true)
                {
                    player.IsDRMOpened = false;
                    platformDrmMgr.Close();
                }
                var appId = Tizen.Applications.Application.Current.ApplicationInfo.ApplicationId;
                platformDrmMgr.Init(appId);
                platformDrmMgr.AddProperty("LicenseServer", uriSource.LicenseUrl);
                foreach (KeyValuePair<string, ExtradataValue> pair in uriSource.Extradata)
                {
                    platformDrmMgr.RemoveProperty(pair.Key);
                    platformDrmMgr.AddProperty(pair.Key, pair.Value.Value);
                }
                platformDrmMgr.Url = uriSource.Uri.ToString();
                if (player.IsDRMOpened == false)
                {
                    player.IsDRMOpened = true;
                    platformDrmMgr.Open();

                }
                player.NativePlayer.SetDrm(platformDrmMgr);
                player.NativePlayer.SetSource(new MediaUriSource(platformDrmMgr.Url));
            }
            return Task.FromResult<bool>(true);
        }
    }
}
