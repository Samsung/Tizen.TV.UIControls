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