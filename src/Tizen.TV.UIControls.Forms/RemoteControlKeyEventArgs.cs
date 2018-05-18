using System;
using System.Collections.Generic;
using System.Text;

namespace Tizen.TV.UIControls.Forms
{
    public class RemoteControlKeyEventArgs
    {
        public RemoteControlKeyEventArgs(RemoteControlKeyTypes keyType, RemoteControlKeyNames keyName)
        {
            KeyType = keyType;
            KeyName = keyName;
        }

        public RemoteControlKeyTypes KeyType { get; private set; }

        public RemoteControlKeyNames KeyName { get; private set; }

        public bool Handled { get; set; }
    }
}
