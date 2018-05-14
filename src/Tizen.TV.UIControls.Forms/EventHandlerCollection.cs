using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace Tizen.TV.UIControls.Forms
{
    internal class EventHandlerCollection<T> : ObservableCollection<T>
    {
        public BindableObject Bindable { get; set; }
    }
}
