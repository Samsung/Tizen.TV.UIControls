using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ElmSharp;

namespace Tizen.TV.UIControls.Forms
{
    public class EcoreKeyEvents
    {
        static EcoreKeyEvents _instance;

        EcoreEvent<EcoreKeyEventArgs> _ecoreKeyDown;
        EcoreEvent<EcoreKeyEventArgs> _ecoreKeyUp;

        EcoreKeyEvents()
        {
            _ecoreKeyDown = new EcoreEvent<EcoreKeyEventArgs>(EcoreEventType.KeyDown, EcoreKeyEventArgs.Create);
            _ecoreKeyUp = new EcoreEvent<EcoreKeyEventArgs>(EcoreEventType.KeyUp, EcoreKeyEventArgs.Create);
            _ecoreKeyDown.On += OnEcoreKeyDown;
            _ecoreKeyUp.On += OnEcoreKeyUp;
        }

        public static EcoreKeyEvents Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new EcoreKeyEvents();
                }
                return _instance;
            }
        }

        public EventHandler<EcoreKeyEventArgs> KeyDown;

        public EventHandler<EcoreKeyEventArgs> KeyUp;

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
