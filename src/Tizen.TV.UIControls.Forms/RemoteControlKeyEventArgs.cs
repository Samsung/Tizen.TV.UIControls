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
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Tizen.TV.UIControls.Forms
{
    public class RemoteControlKeyEventArgs : EventArgs
    {
        public RemoteControlKeyEventArgs(VisualElement sender, RemoteControlKeyTypes keyType, RemoteControlKeyNames keyName, string platformKeyName)
        {
            Sender = sender;
            KeyType = keyType;
            KeyName = keyName;
            PlatformKeyName = platformKeyName;
        }

        public VisualElement Sender { get; }

        public RemoteControlKeyTypes KeyType { get; }

        public RemoteControlKeyNames KeyName { get; }

        public string PlatformKeyName { get; }

        public bool Handled { get; set; }
    }
}
