using System;
using ElmSharp;

namespace Tizen.TV.UIControls.Forms.Renderer
{
    public class EcoreKeyEvents
    {
        static Lazy<EcoreKeyEvents> _instance = new Lazy<EcoreKeyEvents>(() => new EcoreKeyEvents());

        EcoreEvent<EcoreKeyEventArgs> _ecoreKeyDown;
        EcoreEvent<EcoreKeyEventArgs> _ecoreKeyUp;

        EventHandler<EcoreKeyEventArgs> _keyDownHandler;
        EventHandler<EcoreKeyEventArgs> _keyUpHandler;

        EcoreKeyEvents()
        {
            _ecoreKeyDown = new EcoreEvent<EcoreKeyEventArgs>(EcoreEventType.KeyDown, EcoreKeyEventArgs.Create);
            _ecoreKeyUp = new EcoreEvent<EcoreKeyEventArgs>(EcoreEventType.KeyUp, EcoreKeyEventArgs.Create);
        }

        public static EcoreKeyEvents Instance
        {
            get
            {
                return _instance.Value;
            }
        }

        public event EventHandler<EcoreKeyEventArgs> KeyDown
        {
            add
            {
                if (_keyDownHandler == null)
                {
                    _ecoreKeyDown.On += OnEcoreKeyDown;
                }
                _keyDownHandler += value;
            }
            remove
            {
                _keyDownHandler -= value;
                if (_keyDownHandler == null)
                {
                    _ecoreKeyDown.On -= OnEcoreKeyDown;
                }
            }
        }

        public event EventHandler<EcoreKeyEventArgs> KeyUp
        {
            add
            {
                if (_keyUpHandler == null)
                {
                    _ecoreKeyUp.On += OnEcoreKeyUp;
                }
                _keyUpHandler += value;
            }
            remove
            {
                _keyUpHandler -= value;
                if (_keyUpHandler == null)
                {
                    _ecoreKeyUp.On -= OnEcoreKeyUp;
                }
            }
        }

        void OnEcoreKeyDown(object sender, EcoreKeyEventArgs e)
        {
            _keyDownHandler?.Invoke(this, e);
        }

        void OnEcoreKeyUp(object sender, EcoreKeyEventArgs e)
        {
            _keyUpHandler?.Invoke(this, e);
        }
    }
}
