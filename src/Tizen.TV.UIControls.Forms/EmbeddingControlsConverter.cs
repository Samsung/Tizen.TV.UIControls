using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace Tizen.TV.UIControls.Forms
{
    public class ProgressToBoundTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double progress = (double)value;
            if (Double.IsNaN(progress))
            {
                progress = 0d;
            }
            return new Rectangle(0, 0, progress, 1);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Rectangle rect = (Rectangle)value;
            return rect.Width;
        }
    }
    public class MillisecondToTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int millisecond = (int)value;
            int second = (millisecond / 1000) % 60;
            int min = (millisecond / 1000 / 60) % 60;
            int hour = (millisecond / 1000 / 60 / 60);
            if (hour > 0)
            {
                return string.Format("{0:d2}:{1:d2}:{2:d2}", hour, min, second);
            }
            else
            {
                return string.Format("{0:d2}:{1:d2}", min, second);
            }
            
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
