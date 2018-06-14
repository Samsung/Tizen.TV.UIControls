using System;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace Tizen.TV.UIControls.Forms
{
    public class RemoteKeyHandler : BindableObject
    {
        public static readonly BindableProperty CommandProperty = BindableProperty.Create("Command", typeof(ICommand), typeof(RemoteKeyHandler), null);

        public RemoteKeyHandler()
        {
        }

        public RemoteKeyHandler(Action<RemoteControlKeyEventArgs> action)
        {
            Command = new Command<RemoteControlKeyEventArgs>(action);
        }

        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        public event EventHandler<RemoteControlKeyEventArgs> KeyEvent;

        [EditorBrowsable(EditorBrowsableState.Never)]
        public void SendKeyEvent(RemoteControlKeyEventArgs args)
        {
            ICommand cmd = Command;
            if (cmd != null && cmd.CanExecute(args))
                cmd.Execute(args);

            KeyEvent?.Invoke(this, args);
        }
    }
}
