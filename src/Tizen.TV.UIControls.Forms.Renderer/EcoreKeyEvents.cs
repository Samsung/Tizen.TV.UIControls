using System;
using System.Text;
using ElmSharp;

namespace Tizen.TV.UIControls.Forms.Renderer
{
    public class EcoreKeyEvents
    {
        static Lazy<EcoreKeyEvents> _instance = new Lazy<EcoreKeyEvents>(()=>new EcoreKeyEvents());

        EcoreEvent<EcoreKeyEventArgs> _ecoreKeyDown;
        EcoreEvent<EcoreKeyEventArgs> _ecoreKeyUp;

        EcoreKeyEvents()
        {
            _ecoreKeyDown = new EcoreEvent<EcoreKeyEventArgs>(EcoreEventType.KeyDown, EcoreKeyEventArgs.Create);
            _ecoreKeyUp = new EcoreEvent<EcoreKeyEventArgs>(EcoreEventType.KeyUp, EcoreKeyEventArgs.Create);
            // Todo: Add event when KeyDown is added.
            _ecoreKeyDown.On += OnEcoreKeyDown;
            _ecoreKeyUp.On += OnEcoreKeyUp;
        }

        public static EcoreKeyEvents Instance
        {
            get
            {
                return _instance.Value;
            }
        }

        public event EventHandler<EcoreKeyEventArgs> KeyDown;

        public event EventHandler<EcoreKeyEventArgs> KeyUp;

        void OnEcoreKeyDown(object sender, EcoreKeyEventArgs e)
        {
            KeyDown?.Invoke(this, e);
        }

        void OnEcoreKeyUp(object sender, EcoreKeyEventArgs e)
        {
            KeyUp?.Invoke(this, e);
        }
    }
}
