using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

using Xamarin.Forms;
using Tizen.TV.UIControls.Forms;

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
