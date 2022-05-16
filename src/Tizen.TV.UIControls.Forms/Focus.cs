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

using Microsoft.Maui.Controls;
using System.Linq;

namespace Tizen.TV.UIControls.Forms
{
    public static class Focus
    {
        public static readonly BindableProperty UpProperty =
            BindableProperty.CreateAttached("Up", typeof(VisualElement), typeof(Focus), default(VisualElement), propertyChanged: OnUpPropertyChanged);

        public static readonly BindableProperty DownProperty =
            BindableProperty.CreateAttached("Down", typeof(VisualElement), typeof(Focus), default(VisualElement), propertyChanged: OnDownPropertyChanged);

        public static readonly BindableProperty LeftProperty =
            BindableProperty.CreateAttached("Left", typeof(VisualElement), typeof(Focus), default(VisualElement), propertyChanged: OnLeftPropertyChanged);

        public static readonly BindableProperty RightProperty =
            BindableProperty.CreateAttached("Right", typeof(VisualElement), typeof(Focus), default(VisualElement), propertyChanged: OnRightPropertyChanged);


        static readonly RemoteKeyHandler sKeyHandler = new RemoteKeyHandler(KeyHandler, RemoteControlKeyTypes.KeyDown);


        public static VisualElement GetUp(BindableObject view)
        {
            return (VisualElement)view.GetValue(UpProperty);
        }

        public static void SetUp(BindableObject view, VisualElement value)
        {
            view.SetValue(UpProperty, value);
        }

        public static VisualElement GetDown(BindableObject view)
        {
            return (VisualElement)view.GetValue(DownProperty);
        }

        public static void SetDown(BindableObject view, VisualElement value)
        {
            view.SetValue(DownProperty, value);
        }

        public static VisualElement GetLeft(BindableObject view)
        {
            return (VisualElement)view.GetValue(LeftProperty);
        }

        public static void SetLeft(BindableObject view, VisualElement value)
        {
            view.SetValue(LeftProperty, value);
        }

        public static VisualElement GetRight(BindableObject view)
        {
            return (VisualElement)view.GetValue(RightProperty);
        }

        public static void SetRight(BindableObject view, VisualElement value)
        {
            view.SetValue(RightProperty, value);
        }

        static void OnUpPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (!InputEvents.GetEventHandlers(bindable).Contains(sKeyHandler))
            {
                InputEvents.GetEventHandlers(bindable).Add(sKeyHandler);
            }

            if (bindable is VisualElement ve)
            {
                var effect = ve.Effects.FirstOrDefault(t => t is PlatformFocusUpEffect);
                if (effect != null)
                {
                    ve.Effects.Remove(effect);
                }
                ve.Effects.Add(new PlatformFocusUpEffect());
            }
        }

        static void OnDownPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (!InputEvents.GetEventHandlers(bindable).Contains(sKeyHandler))
            {
                InputEvents.GetEventHandlers(bindable).Add(sKeyHandler);
            }

            if (bindable is VisualElement ve)
            {
                var effect = ve.Effects.FirstOrDefault(t => t is PlatformFocusDownEffect);
                if (effect != null)
                {
                    ve.Effects.Remove(effect);
                }
                ve.Effects.Add(new PlatformFocusDownEffect());
            }
        }
        static void OnLeftPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (!InputEvents.GetEventHandlers(bindable).Contains(sKeyHandler))
            {
                InputEvents.GetEventHandlers(bindable).Add(sKeyHandler);
            }

            if (bindable is VisualElement ve)
            {
                var effect = ve.Effects.FirstOrDefault(t => t is PlatformFocusLeftEffect);
                if (effect != null)
                {
                    ve.Effects.Remove(effect);
                }
                ve.Effects.Add(new PlatformFocusLeftEffect());
            }
        }
        static void OnRightPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (!InputEvents.GetEventHandlers(bindable).Contains(sKeyHandler))
            {
                InputEvents.GetEventHandlers(bindable).Add(sKeyHandler);
            }

            if (bindable is VisualElement ve)
            {
                var effect = ve.Effects.FirstOrDefault(t => t is PlatformFocusRightEffect);
                if (effect != null)
                {
                    ve.Effects.Remove(effect);
                }
                ve.Effects.Add(new PlatformFocusRightEffect());
            }
        }

        static void KeyHandler(RemoteControlKeyEventArgs args)
        {
            if (args.Handled)
                return;

            BindableProperty property = null;

            if (args.KeyName == RemoteControlKeyNames.Up)
            {
                property = UpProperty;
            }
            else if (args.KeyName == RemoteControlKeyNames.Down)
            {
                property = DownProperty;
            }
            else if (args.KeyName == RemoteControlKeyNames.Left)
            {
                property = LeftProperty;
            }
            else if (args.KeyName == RemoteControlKeyNames.Right)
            {
                property = RightProperty;
            }

            if (property != null)
            {
                var next = (args.Sender.GetValue(property) as VisualElement);
                if (next != null && next.Focus())
                {
                    args.Handled = true;
                }
            }
        }
    }

    class PlatformFocusUpEffect : RoutingEffect
    {
        public PlatformFocusUpEffect() : base("TizenTVUIControl.PlatformFocusUpEffect")
        {
        }
    }
    class PlatformFocusDownEffect : RoutingEffect
    {
        public PlatformFocusDownEffect() : base("TizenTVUIControl.PlatformFocusDownEffect")
        {
        }
    }
    class PlatformFocusLeftEffect : RoutingEffect
    {
        public PlatformFocusLeftEffect() : base("TizenTVUIControl.PlatformFocusLeftEffect")
        {
        }
    }
    class PlatformFocusRightEffect : RoutingEffect
    {
        public PlatformFocusRightEffect() : base("TizenTVUIControl.PlatformFocusRightEffect")
        {
        }
    }
}
