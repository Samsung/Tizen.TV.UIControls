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

namespace Tizen.Theme.Common
{
    /// <summary>
    /// Manages animated page transitions on push and pop.
    /// </summary>
    public static class AnimatablePage
    {
        /// <summary>
        /// Identifies the PushAnimation bindable property.
        /// </summary>
        public static readonly BindableProperty PushAnimationProperty = BindableProperty.CreateAttached("PushAnimation", typeof(Animation), typeof(Page), default(Animation));

        /// <summary>
        /// Get the push animation.
        /// </summary>
        /// <param name="page">The page to be animated</param>
        /// <returns></returns>
        public static Animation GetPushAnimation(this Page page)
        {
            return (Animation)page.GetValue(PushAnimationProperty);
        }

        /// <summary>
        /// Set the push animation.
        /// </summary>
        /// <param name="page">The page to be animated</param>
        /// <param name="value">The push animation</param>
        public static void SetPushAnimation(this Page page, Animation value)
        {
            page.SetValue(PushAnimationProperty, value);
        }


        /// <summary>
        /// Identifies the PopAnimation bindable property.
        /// </summary>
        public static readonly BindableProperty PopAnimationProperty = BindableProperty.CreateAttached("PopAnimation", typeof(Animation), typeof(Page), default(Animation));

        /// <summary>
        /// Get the pop animation.
        /// </summary>
        /// <param name="page">The page to be animated</param>
        /// <returns></returns>
        public static Animation GetPopAnimation(this Page page)
        {
            return (Animation)page.GetValue(PopAnimationProperty);
        }

        /// <summary>
        /// Set the push animation.
        /// </summary>
        /// <param name="page">The page to be animated</param>
        /// <param name="value">The pop animation</param>
        public static void SetPopAnimation(this Page page, Animation value)
        {
            page.SetValue(PopAnimationProperty, value);
        }

        /// <summary>
        /// Identifies the PushAnimationRate bindable property.
        /// </summary>
        public static readonly BindableProperty PushAnimationRateProperty = BindableProperty.CreateAttached("PushAnimationRate", typeof(uint), typeof(Page), (uint)16);

        /// <summary>
        /// Get the push animation rate.
        /// </summary>
        /// <param name="page">The page to be animated</param>
        /// <returns></returns>
        public static uint GetPushAnimationRate(this Page page)
        {
            return (uint)page.GetValue(PushAnimationRateProperty);
        }

        /// <summary>
        /// Set the push animation rate.
        /// </summary>
        /// <param name="page">The page to be animated</param>
        /// <param name="value">The push animation rate</param>
        public static void SetPushAnimationRate(this Page page, uint value)
        {
            page.SetValue(PushAnimationRateProperty, value);
        }

        /// <summary>
        /// Identifies the PopAnimationRate bindable property.
        /// </summary>
        public static readonly BindableProperty PopAnimationRateProperty = BindableProperty.CreateAttached("PopAnimationRate", typeof(uint), typeof(Page), (uint)16);

        /// <summary>
        /// Get the pop animation rate.
        /// </summary>
        /// <param name="page">The page to be animated</param>
        /// <returns></returns>
        public static uint GetPopAnimationRate(this Page page)
        {
            return (uint)page.GetValue(PopAnimationRateProperty);
        }

        /// <summary>
        /// Set the pop animation rate.
        /// </summary>
        /// <param name="page">The page to be animated</param>
        /// <param name="value">The pop animation rate</param>
        public static void SetPopAnimationRate(this Page page, uint value)
        {
            page.SetValue(PopAnimationRateProperty, value);
        }

        /// <summary>
        /// Identifies the PushAnimationLength bindable property.
        /// </summary>
        public static readonly BindableProperty PushAnimationLengthProperty = BindableProperty.CreateAttached("PushAnimationLength", typeof(uint), typeof(Page), (uint)250);

        /// <summary>
        /// Get the push animation length.
        /// </summary>
        /// <param name="page">The page to be animated</param>
        /// <returns></returns>
        public static uint GetPushAnimationLength(this Page page)
        {
            return (uint)page.GetValue(PushAnimationLengthProperty);
        }

        /// <summary>
        /// Set the push animation length.
        /// </summary>
        /// <param name="page">The page to be animated</param>
        /// <param name="value">The push animation length</param>
        public static void SetPushAnimationLength(this Page page, uint value)
        {
            page.SetValue(PushAnimationRateProperty, value);
        }

        /// <summary>
        /// Identifies the PopAnimationLength bindable property.
        /// </summary>
        public static readonly BindableProperty PopAnimationLengthProperty = BindableProperty.CreateAttached("PopAnimationLength", typeof(uint), typeof(Page), (uint)250);

        /// <summary>
        /// Get the pop animation length.
        /// </summary>
        /// <param name="page">The page to be animated</param>
        /// <returns></returns>
        public static uint GetPopAnimationLength(this Page page)
        {
            return (uint)page.GetValue(PopAnimationLengthProperty);
        }

        /// <summary>
        /// Set the pop animation length.
        /// </summary>
        /// <param name="page">The page to be animated</param>
        /// <param name="value">The pop animation length</param>
        public static void SetPopAnimationLength(this Page page, uint value)
        {
            page.SetValue(PopAnimationLengthProperty, value);
        }
    }
}
