using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

using Xamarin.Forms;

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
}
