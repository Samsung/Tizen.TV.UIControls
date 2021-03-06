﻿/*
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
using CModel = System.ComponentModel;
using Xamarin.Forms;

namespace Tizen.TV.UIControls.Forms
{
    /// <summary>
    /// A TypeConverter that converts to FileMediaSource.
    /// </summary>
    [TypeConverter(typeof(FileMediaSource))]
    [CModel.EditorBrowsable(CModel.EditorBrowsableState.Never)]
    [Obsolete("This class is obsolete as of 1.1.0. Please use FileMediaSourceConverter from Tizen.Theme.Common instead.")]
    public sealed class FileMediaSourceConverter : TypeConverter
    {
        /// <summary>
        /// Creates a file media source given a path to a media.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>FileMediaSource</returns>
        public override object ConvertFromInvariantString(string value)
        {
            if (value != null)
                return (FileMediaSource)MediaSource.FromFile(value);

            throw new InvalidOperationException(string.Format("Cannot convert \"{0}\" into {1}", value, typeof(FileMediaSource)));
        }
    }
}