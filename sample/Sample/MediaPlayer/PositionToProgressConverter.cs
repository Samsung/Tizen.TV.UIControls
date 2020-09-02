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
using System.Collections.Generic;
using System.Globalization;
using System.Text;

using Xamarin.Forms;
using Tizen.TV.UIControls.Forms;
using Xamarin.Forms.PlatformConfiguration.TizenSpecific;

namespace Sample
{
    public class PositionToProgressConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((int)value / (double)(GetParameter(parameter)));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (int)((double)value * GetParameter(parameter));
        }
        int GetParameter(object parameter)
        {
            
            var duration = ((Tizen.TV.UIControls.Forms.MediaPlayer)parameter).Duration;
            if (duration == 0)
                duration = 1;
            return duration;
        }
    }

    public class StateToButtonTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            PlaybackState state = (PlaybackState)value;
            if (state == PlaybackState.Playing)
            {
                return "||";
            }
            else
            {
                return "▷";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string strValue = (string)value;
            if (strValue == "||")
                return PlaybackState.Playing;
            else
                return PlaybackState.Stopped;
        }
    }

}
