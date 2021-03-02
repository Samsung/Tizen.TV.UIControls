using Xamarin.Forms;

namespace Tizen.Theme.Common
{
    /// <summary>
    /// Arguments for the event that is raised when one item of GridView has received focus. 
    /// </summary>
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