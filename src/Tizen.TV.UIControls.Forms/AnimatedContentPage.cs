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
using Xamarin.Forms;

namespace Tizen.TV.UIControls.Forms
{
    /// <summary>
    /// A ContentPage that support animation when switching pages.
    /// </summary>
    public class AnimatedContentPage : ContentPage
    {
        /// <summary>
        /// Identifies the PageTransition bindable property.
        /// </summary>
        public static readonly BindableProperty PageTransitionProperty = BindableProperty.Create("PageTransition", typeof(PageTransition), typeof(AnimatedContentPage), PageTransition.None, propertyChanged: (b, o, n) => ((AnimatedContentPage)b).UpdatePageTransition());

        /// <summary>
        /// Gets or sets the page transition
        /// </summary>
        public PageTransition PageTranistion
        {
            get { return (PageTransition)GetValue(PageTransitionProperty); }
            set { SetValue(PageTransitionProperty, value); }
        }

        void UpdatePageTransition()
        {
            Animation pushAnimation = null;
            Animation popAnimation = null;
            switch (PageTranistion)
            {
                case PageTransition.Fade:
                    pushAnimation = new Animation {
                        { 0, 1, new Animation (v => Opacity = v, 0.1, 1) },
                    };
                    popAnimation = new Animation {
                        { 0, 1, new Animation (v => Opacity = v, 1, 0.1) },
                    };
                    break;
                case PageTransition.Scale:
                    pushAnimation = new Animation {
                        { 0, 1, new Animation (v => Scale = v, 0.5, 1) },
                        { 0, 1, new Animation (v => Opacity = v, 0.3, 1) },
                    };
                    popAnimation = new Animation {
                        { 0, 1, new Animation (v => Scale = v, 1, 0.5) },
                        { 0, 1, new Animation (v => Opacity = v, 1, 0.3) },
                    };
                    break;
                case PageTransition.SlideFromTop:
                    pushAnimation = new Animation {
                        { 0, 1, new Animation (v => TranslationY = 0 - (1 - v) * this.Height / 4, 0, 1) },
                        { 0, 1, new Animation (v => Opacity = v, 0.3, 1) },
                    };
                    popAnimation = new Animation {
                        { 0, 1, new Animation (v => TranslationY = 0 - (1 - v) * this.Height / 4, 1, 0) },
                        { 0, 1, new Animation (v => Opacity = v, 1, 0.3) },
                    };
                    break;
                case PageTransition.SlideFromBottom:
                    pushAnimation = new Animation {
                        { 0, 1, new Animation (v => TranslationY = (1 - v) * this.Height / 4, 0, 1) },
                        { 0, 1, new Animation (v => Opacity = v, 0.3, 1) },
                    };
                    popAnimation = new Animation {
                        { 0, 1, new Animation (v => TranslationY = (1 - v) * this.Height / 4, 1, 0) },
                        { 0, 1, new Animation (v => Opacity = v, 1, 0.3) },
                    };
                    break;
                case PageTransition.SlideFromLeft:
                    pushAnimation = new Animation {
                        { 0, 1, new Animation (v => TranslationX = 0 - (1 - v) * this.Width, 0, 1) },
                        { 0, 0.5, new Animation (v => Opacity = v, 0.3, 1) },
                    };
                    popAnimation = new Animation {
                        { 0, 1, new Animation (v => TranslationX = 0 - (1 - v) * this.Width, 1, 0) },
                        { 0.5, 1, new Animation (v => Opacity = v, 1, 0.3) },
                    };
                    break;
                case PageTransition.SlideFromRight:
                    pushAnimation = new Animation {
                        { 0, 1, new Animation (v => TranslationX = (1 - v) * this.Width, 0, 1) },
                        { 0, 0.5, new Animation (v => Opacity = v, 0.3, 1) },
                    };
                    popAnimation = new Animation {
                        { 0, 1, new Animation (v => TranslationX = (1 - v) * this.Width, 1, 0) },
                        { 0.5, 1, new Animation (v => Opacity = v, 1, 0.3) },
                    };
                    break;
                default:
                    break;
            }

            this.SetPushAnimation(pushAnimation);
            this.SetPopAnimation(popAnimation);
        }
    }

    /// <summary>
    /// Enumerates values that define the page transition animation type.
    /// </summary>
    public enum PageTransition
    {
        /// <summary>
        /// Do not animate
        /// </summary>
        None,

        /// <summary>
        /// Slide from left to right on push, and slide from right to left on pop
        /// </summary>
        SlideFromLeft,

        /// <summary>
        /// Slide from right to left on push, and slide from left to right on pop
        /// </summary>
        SlideFromRight,

        /// <summary>
        /// Slide from top to bottom on push, and slide from bottom to top on pop
        /// </summary>
        SlideFromTop,

        /// <summary>
        /// Slide from bottom to top on push, and slide from top to bottom on pop
        /// </summary>
        SlideFromBottom,

        /// <summary>
        /// Show a fade animation
        /// </summary>
        Fade,

        /// <summary>
        /// Show a scale animation
        /// </summary>
        Scale,
    }
}
