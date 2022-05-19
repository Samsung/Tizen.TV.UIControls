using Microsoft.Maui.Controls;
using System;
using System.ComponentModel;

namespace Tizen.TV.UIControls.Forms
{
    /// <summary>
    /// Arguments for the event that is raised when one item of GridView has received focus. 
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Obsolete("This class is obsolete as of 1.1.0. Please use GridView from Tizen.Theme.Common instead.")]
    public class GridViewFocusedEventArgs : FocusEventArgs
    {
        /// <summary>
        /// The focused item.
        /// </summary>
        public object Item { get; }

        /// <summary>
        /// Constructs a new GridViewFocusedEventArgs object.
        /// </summary>
        public GridViewFocusedEventArgs(object item, View targetView, bool isFocused) : base(targetView, isFocused)
        {
            Item = item;
        }
    }
}