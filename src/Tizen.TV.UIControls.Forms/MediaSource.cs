using System;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Tizen.TV.UIControls.Forms
{
    [TypeConverter(typeof(MediaSourceConverter))]
    public abstract class MediaSource : Element
    {
        protected MediaSource()
        {
        }

        public static MediaSource FromFile(string file)
        {
            return new FileMediaSource { File = file };
        }


        public static MediaSource FromUri(Uri uri)
        {
            if (!uri.IsAbsoluteUri)
                throw new ArgumentException("uri is relative");
            return new UriMediaSource { Uri = uri };
        }

        public static implicit operator MediaSource(string source)
        {
            return Uri.TryCreate(source, UriKind.Absolute, out Uri uri) && uri.Scheme != "file" ? FromUri(uri) : FromFile(source);
        }

        public static implicit operator MediaSource(Uri uri)
        {
            if (!uri.IsAbsoluteUri)
                throw new ArgumentException("uri is relative");
            return FromUri(uri);
        }

        protected void OnSourceChanged()
        {
            SourceChanged?.Invoke(this, EventArgs.Empty);
        }

        internal event EventHandler SourceChanged;
    }
}