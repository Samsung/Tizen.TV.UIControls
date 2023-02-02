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
using ElmSharp;

namespace Tizen.TV.UIControls.Forms.Handler
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
