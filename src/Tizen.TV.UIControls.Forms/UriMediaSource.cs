using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Tizen.TV.UIControls.Forms
{
    [TypeConverter(typeof(MediaSourceConverter))]
    public sealed class UriMediaSource : MediaSource
    {
        public static readonly BindableProperty UriProperty = BindableProperty.Create("Uri", typeof(Uri), typeof(UriImageSource), default(Uri), validateValue: (bindable, value) => value == null || ((Uri)value).IsAbsoluteUri);

        public Uri Uri
        {
            get { return (Uri)GetValue(UriProperty); }
            set { SetValue(UriProperty, value); }
        }

        public override string ToString()
        {
            return $"Uri: {Uri}";
        }

        public static implicit operator UriMediaSource(Uri uri)
        {
            return (UriMediaSource)FromUri(uri);
        }

        public static implicit operator string(UriMediaSource uri)
        {
            return uri?.ToString();
        }

        protected override void OnPropertyChanged(string propertyName = null)
        {
            if (propertyName == UriProperty.PropertyName)
                OnSourceChanged();
            base.OnPropertyChanged(propertyName);
        }
    }
}