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
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace Tizen.TV.UIControls.Forms
{
    public class RemoteKeyHandler : BindableObject
    {
        public static readonly BindableProperty CommandProperty = BindableProperty.Create("Command", typeof(ICommand), typeof(RemoteKeyHandler), null);

        RemoteControlKeyTypes _commandKeyType = RemoteControlKeyTypes.KeyDown | RemoteControlKeyTypes.KeyUp;

        public RemoteKeyHandler()
        {
        }

        public RemoteKeyHandler(Action<RemoteControlKeyEventArgs> action)
        {
            Command = new Command<RemoteControlKeyEventArgs>(action);
        }

        public RemoteKeyHandler(Action<RemoteControlKeyEventArgs> action, RemoteControlKeyTypes keyType)
        {
            Command = new Command<RemoteControlKeyEventArgs>(action);
            _commandKeyType = keyType;
        }

        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        public event EventHandler<RemoteControlKeyEventArgs> KeyDown;

        public event EventHandler<RemoteControlKeyEventArgs> KeyUp;

        [EditorBrowsable(EditorBrowsableState.Never)]
        public void SendKeyEvent(RemoteControlKeyEventArgs args)
        {
            if (_commandKeyType.HasFlag(args.KeyType))
            {
                ICommand cmd = Command;
                if (cmd != null && cmd.CanExecute(args))
                {
                    cmd.Execute(args);
                }
                if (args.KeyType == RemoteControlKeyTypes.KeyDown)
                    KeyDown?.Invoke(this, args);
                else
                    KeyUp?.Invoke(this, args);
            }
        }
    }
}
