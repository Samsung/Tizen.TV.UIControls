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
using System.Globalization;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;

namespace Tizen.Theme.Common
{
    /// <summary>
    /// Class that the XAML parser uses to convert Progress to Bound.
    /// </summary>
    internal class ProgressToBoundTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double progress = (double)value;
            if (Double.IsNaN(progress))
            {
                progress = 0d;
            }
            return new Rect(0, 0, progress, 1);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Rect rect = (Rect)value;
            return rect.Width;
        }
    }

    /// <summary>
    /// Class that the XAML parser uses to convert milliseconds to Text format.
    /// </summary>
    internal class MillisecondToTextConverter : IValueConverter
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
