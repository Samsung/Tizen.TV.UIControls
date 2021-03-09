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

using System.Collections.Generic;
using System.Threading.Tasks;
using Tizen.TV.Multimedia;
using Tizen.TV.UIControls.Forms.Renderer;
using Xamarin.Forms.Internals;
using Tizen.Theme.Common.Renderer;

using TApplication = Tizen.Applications.Application;
using MPlayer = Tizen.Multimedia.Player;
using TVPlayer  = Tizen.TV.Multimedia.Player;

[assembly: Xamarin.Forms.Dependency(typeof(TVMediaPlayerImpl))]
namespace Tizen.TV.UIControls.Forms.Renderer
{
    public class TVMediaPlayerImpl : MediaPlayerImpl
    {
        DRMManager _drmManager;

        public DRMManager DRMManager
        {
            get => _drmManager;
            set
            {
                if (_player is TVPlayer tvPlayer)
                {
                    _drmManager = value;
                    if (value != null)
                    {
                        tvPlayer.SetDrm(value);
                    }
                }
                else
                {
                    Log.Debug(UIControls.Tag, "DRMManager is avaialbe only on TVPlayer.");
                }
            }
        }

        public override void Stop()
        {
            base.Stop();
            if (DRMManager != null)
            {
                DRMManager.Close();
                DRMManager.Dispose();
                DRMManager = null;
            }
        }

        protected override async Task ApplySource()
        {
            if (_source == null)
            {
                return;
            }
            IMediaSourceHandler handler = Registrar.Registered.GetHandlerForObject<IMediaSourceHandler>(_source);
            if (_source is DRMMediaSource drmMediaSource)
            {
                var drmManager = DRMManager.CreateDRMManager(DRMType.Playready);
                drmManager.Init(TApplication.Current.ApplicationInfo.ApplicationId);
                foreach (KeyValuePair<string, DRMPropertyValue> pair in drmMediaSource.ExtraData)
                {
                    _drmManager.AddProperty(pair.Key, pair.Value.Value);
                }
                drmManager.RemoveProperty("LicenseServer");
                drmManager.AddProperty("LicenseServer", drmMediaSource.LicenseUrl);
                drmManager.Url = drmMediaSource.Uri.AbsoluteUri;
                drmManager.Open();
                DRMManager = drmManager;
            }
            await handler.SetSource(_player, _source);
        }

        protected override MPlayer CreateMediaPlayer()
        {
            return new TVPlayer();
        }
    }
}
