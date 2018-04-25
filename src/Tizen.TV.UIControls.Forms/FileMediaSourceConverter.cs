using System;
using Xamarin.Forms;

namespace Tizen.TV.UIControls.Forms
{
    [TypeConverter(typeof(FileMediaSource))]
    public sealed class FileMediaSourceConverter : TypeConverter
    {
        public override object ConvertFromInvariantString(string value)
        {
            if (value != null)
                return (FileMediaSource)MediaSource.FromFile(value);

            throw new InvalidOperationException(string.Format("Cannot convert \"{0}\" into {1}", value, typeof(FileMediaSource)));
        }
    }
}