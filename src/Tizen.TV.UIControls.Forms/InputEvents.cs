using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace Tizen.TV.UIControls.Forms
{
    public sealed class InputEvents
    {
        public static readonly BindablePropertyKey EventHandlersPropertyKey = BindableProperty.CreateAttachedReadOnly("EventHandlers", typeof(IList<RemoteKeyHandler>), typeof(InputEvents), null,
            defaultValueCreator: bindable =>
            {
                var collection = new EventHandlerCollection<RemoteKeyHandler>();
                collection.Bindable = bindable;
                collection.CollectionChanged += Collection_CollectionChanged;
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

        static void Collection_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            var bindable = (sender as EventHandlerCollection<RemoteKeyHandler>).Bindable;
            var visualElement = bindable as VisualElement;
            if (visualElement == null)
                return;

            var collection = (EventHandlerCollection<RemoteKeyHandler>)sender;
            if (collection?.Count == 0)
            {
                var toRemove = visualElement.Effects.FirstOrDefault(t => t is RemoteKeyEventEffect);
                if (toRemove != null)
                {
                    visualElement.Effects.Remove(toRemove);
                }
            }
            else
            {
                var existingEffect = visualElement.Effects.FirstOrDefault(t => t is RemoteKeyEventEffect);
                if (existingEffect == null)
                {
                    visualElement.Effects.Add(new RemoteKeyEventEffect());
                }
            }
        }

        static void OnAccessKeyPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var visualElement = bindable as VisualElement;
            if (visualElement == null)
                return;

            if ((RemoteControlKeyNames)newValue == RemoteControlKeyNames.Undefined )
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