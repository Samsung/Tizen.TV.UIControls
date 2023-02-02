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
using Tizen.NUI;

namespace Tizen.TV.UIControls.Forms.Handler
{
    public class WindowKeyEvents
    {
        static Lazy<WindowKeyEvents> _instance = new Lazy<WindowKeyEvents>(() => new WindowKeyEvents());

        EventHandler<Window.KeyEventArgs> _keyDownHandler;
        EventHandler<Window.KeyEventArgs> _keyUpHandler;
        
        WindowKeyEvents()
        {
            Window.Instance.KeyEvent += OnKeyEvent;          
        }

        private void OnKeyEvent(object sender, Window.KeyEventArgs e)
        {
            if (e.Key.State == Key.StateType.Down)
            {
                OnWindowKeyDown(sender, e);
            }
            else if (e.Key.State == Key.StateType.Up)
            {
                OnWindowKeyUp(sender, e);
            }
        }

        public static WindowKeyEvents Instance
        {
            get
            {
                return _instance.Value;
            }
        }

        public event EventHandler<Window.KeyEventArgs> KeyDown
        {
            add
            {
                _keyDownHandler += value;
            }
            remove
            {
                _keyDownHandler -= value;
            }
        }

        public event EventHandler<Window.KeyEventArgs> KeyUp
        {
            add
            {
                _keyUpHandler += value;
            }
            remove
            {
                _keyUpHandler -= value;
            }
        }

        void OnWindowKeyDown(object sender, Window.KeyEventArgs e)
        {
            _keyDownHandler?.Invoke(this, e);
        }

        void OnWindowKeyUp(object sender, Window.KeyEventArgs e)
        {
            _keyUpHandler?.Invoke(this, e);
        }
    }
}
