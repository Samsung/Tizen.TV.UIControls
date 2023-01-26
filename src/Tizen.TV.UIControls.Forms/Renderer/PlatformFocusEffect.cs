///*
// * Copyright (c) 2018 Samsung Electronics Co., Ltd All Rights Reserved
// *
// * Licensed under the Apache License, Version 2.0 (the License);
// * you may not use this file except in compliance with the License.
// * You may obtain a copy of the License at
// *
// * http://www.apache.org/licenses/LICENSE-2.0
// *
// * Unless required by applicable law or agreed to in writing, software
// * distributed under the License is distributed on an AS IS BASIS,
// * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// * See the License for the specific language governing permissions and
// * limitations under the License.
// */

//using Microsoft.Maui.Controls;
//using Microsoft.Maui.Controls.Compatibility.Platform.Tizen;
//using Microsoft.Maui.Controls.Platform;
//using ElmSharp;

//namespace Tizen.TV.UIControls.Forms.Renderer
//{
//    public class PlatformFocusEffect : PlatformEffect
//    {
//        FocusDirection _direction;
//        VisualElement _nextView;
//        protected PlatformFocusEffect(FocusDirection direction)
//        {
//            _direction = direction;
//        }
//        protected override void OnAttached()
//        {
//            _nextView = null;
//            switch(_direction)
//            {
//                case FocusDirection.Up:
//                    _nextView = Focus.GetUp(Element);
//                    break;
//                case FocusDirection.Down:
//                    _nextView = Focus.GetDown(Element);
//                    break;
//                case FocusDirection.Left:
//                    _nextView = Focus.GetLeft(Element);
//                    break;
//                case FocusDirection.Right:
//                    _nextView = Focus.GetRight(Element);
//                    break;
//                default:
//                    _nextView = null;
//                    break;
//            }
//            if (_nextView != null)
//            {
//                if (_nextView.IsPlatformEnabled)
//                {
//                    SetCustomFocus(Control, _nextView, _direction);
//                }
//                else
//                {
//                    _nextView.PropertyChanged += OnNextViewPropertyChanged;
//                }
//            }
//            else
//            {
//                (Control as Widget)?.SetNextFocusObject(null, _direction);
//            }
//        }
        
//        protected override void OnDetached()
//        {
//            if (_nextView != null)
//            {
//                _nextView.PropertyChanged -= OnNextViewPropertyChanged;
//            }
//        }

//        void OnNextViewPropertyChanged(object sender, global::System.ComponentModel.PropertyChangedEventArgs e)
//        {
//            if (e.PropertyName == "Renderer" && ((Element as VisualElement)?.IsPlatformEnabled ?? false))
//            {
//                SetCustomFocus(Control, _nextView, _direction);
//            }
//        }

//        static void SetCustomFocus(EvasObject view, VisualElement next, FocusDirection direction)
//        {
//            var renderer = Platform.GetRenderer(next);
//            if (renderer != null)
//            {
//                (view as Widget)?.SetNextFocusObject(renderer.NativeView, direction);
//            }
//        }
//    }

//    public class PlatformFocusUpEffect : PlatformFocusEffect
//    {
//        public PlatformFocusUpEffect() : base(FocusDirection.Up) { }
//    }

//    public class PlatformFocusDownEffect : PlatformFocusEffect
//    {
//        public PlatformFocusDownEffect() : base(FocusDirection.Down) { }
//    }

//    public class PlatformFocusLeftEffect : PlatformFocusEffect
//    {
//        public PlatformFocusLeftEffect() : base(FocusDirection.Left) { }
//    }

//    public class PlatformFocusRightEffect : PlatformFocusEffect
//    {
//        public PlatformFocusRightEffect() : base(FocusDirection.Right) { }
//    }
//}