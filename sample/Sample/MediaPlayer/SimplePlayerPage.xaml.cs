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

using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Tizen.Theme.Common;

namespace Sample
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SimplePlayerPage : OverlayPage
    {
		public SimplePlayerPage ()
		{
			InitializeComponent ();
		}

        void OnClickPlay(object sender, ClickedEventArgs e)
        {
            if (MediaPlayer.State == PlaybackState.Playing)
                MediaPlayer.Pause();
            else
            {
                var unused = MediaPlayer.Start();
            }
        }

        void OnClickStop(object sender, ClickedEventArgs e)
        {
            MediaPlayer.Stop();
        }

        async void OnSeekChanged(object sender, ValueChangedEventArgs e)
        {
            await MediaPlayer.Seek((int)(MediaPlayer.Duration * e.NewValue));
        }

        void OnPlayClicked(object sender, EventArgs e)
        {
            if (MediaPlayer.State != PlaybackState.Playing)
                MediaPlayer.Start();
            else
                MediaPlayer.Pause();
        }

        void OnStopClicked(object sender, EventArgs e)
        {
            MediaPlayer.Stop();
        }
    }
}