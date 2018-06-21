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

using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace Tizen.TV.UIControls.Forms
{
    public sealed class InputEvents
    {
        public static readonly BindablePropertyKey EventHandlersPropertyKey = BindableProperty.CreateAttachedReadOnly("EventHandlers", typeof(IList<RemoteKeyHandler>), typeof(InputEvents), null,
            defaultValueCreator: bindable =>
            {
                var collection = new EventHandlerCollection();
                collection.Target = bindable as VisualElement;
                collection.CollectionChanged += OnCollectionChanged;
                return collection;
            });

        public static readonly BindableProperty EventHandlersProperty = EventHandlersPropertyKey.BindableProperty;

        public static readonly BindableProperty AccessKeyProperty = BindableProperty.CreateAttached("AccessKey", typeof(RemoteControlKeyNames), typeof(InputEvents), default(RemoteControlKeyNames), propertyChanged: OnAccessKeyPropertyChanged);

        InputEvents()
        {
        }

        public static IList<RemoteKeyHandler> GetEventHandlers(BindableObject view)
        {
            return (IList<RemoteKeyHandler>)view.GetValue(EventHandlersProperty);
        }

        public static RemoteControlKeyNames GetAccessKey(BindableObject view)
        {
            return (RemoteControlKeyNames)view.GetValue(AccessKeyProperty);
        }

        public static void SetAccessKey(BindableObject view, RemoteControlKeyNames value)
        {
            view.SetValue(AccessKeyProperty, value);
        }

        static void OnCollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if ((sender as EventHandlerCollection).Target == null)
                return;

            var collection = (EventHandlerCollection)sender;
            if (collection.Count == 0)
            {
                var toRemove = collection.Target.Effects.FirstOrDefault(t => t is RemoteKeyEventEffect);
                if (toRemove != null)
                {
                    collection.Target.Effects.Remove(toRemove);
                }
            }
            else
            {
                var existingEffect = collection.Target.Effects.FirstOrDefault(t => t is RemoteKeyEventEffect);
                if (existingEffect == null)
                {
                    collection.Target.Effects.Add(new RemoteKeyEventEffect());
                }
            }
        }

        static void OnAccessKeyPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var visualElement = bindable as VisualElement;
            if (visualElement == null)
                return;

            if ((RemoteControlKeyNames)newValue == RemoteControlKeyNames.Unknown )
            {
                var toRemove = visualElement.Effects.FirstOrDefault(t => t is AccessKeyEffect);
                if (toRemove != null)
                {
                    visualElement.Effects.Remove(toRemove);
                }
            }
            else
            {
                var existingEffect = visualElement.Effects.FirstOrDefault(t => t is AccessKeyEffect);
                if (existingEffect == null)
                {
                    visualElement.Effects.Add(new AccessKeyEffect());
                }
            }
        }

        class RemoteKeyEventEffect : RoutingEffect
        {
            public RemoteKeyEventEffect() : base("TizenTVUIControl.RemoteKeyEventEffect")
            {
            }
        }

        class AccessKeyEffect : RoutingEffect
        {
            public AccessKeyEffect() : base("TizenTVUIControl.AccessKeyEffect")
            {
            }
        }
    }
}