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
using System.Text;
using Xamarin.Forms;

namespace Tizen.TV.UIControls.Forms
{
    /// <summary>
    /// Arguments for the event that is raised when a remote control key is pressed or released.
    /// </summary>
    public class RemoteControlKeyEventArgs : EventArgs
    {
        /// <summary>
        /// Constructs a new RemoteControlKeyEventArgs object for a key type, key name, and platform key name.
        /// </summary>
        /// <param name="sender">The VisualElement that sends the event.</param>
        /// <param name="keyType">The type of a remote control key.</param>
        /// <param name="keyName">The name of a remote control key.</param>
        /// <param name="platformKeyName">The name of a platform key name.</param>
        public RemoteControlKeyEventArgs(VisualElement sender, RemoteControlKeyTypes keyType, RemoteControlKeyNames keyName, string platformKeyName)
        {
            Sender = sender;
            KeyType = keyType;
            KeyName = keyName;
            PlatformKeyName = platformKeyName;
        }

        /// <summary>
        /// The sender of a remote control key.
        /// </summary>
        public VisualElement Sender { get; }

        /// <summary>
        /// The type of a remote control key.
        /// </summary>
        public RemoteControlKeyTypes KeyType { get; }

        /// <summary>
        /// The name of a remote control key.
        /// </summary>
        public RemoteControlKeyNames KeyName { get; }

        /// <summary>
        /// The name of a platform key name.
        /// </summary>
        public string PlatformKeyName { get; }

        /// <summary>
        /// Gets or sets a value that indicates whether the remote control key event has already been handled.
        /// </summary>
        public bool Handled { get; set; }
    }
}
