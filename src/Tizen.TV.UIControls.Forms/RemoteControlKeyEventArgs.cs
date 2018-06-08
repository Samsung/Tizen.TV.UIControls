using System;
using System.Collections.Generic;
using System.Text;

namespace Tizen.TV.UIControls.Forms
{
    public class RemoteControlKeyEventArgs : EventArgs
    {
        public RemoteControlKeyEventArgs(RemoteControlKeyTypes keyType, RemoteControlKeyNames keyName)
        {
            KeyType = keyType;
            KeyName = keyName;
        }

        public RemoteControlKeyTypes KeyType { get; }

        public RemoteControlKeyNames KeyName { get; }

        public bool Handled { get; set; }
    }
}
