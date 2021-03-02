using Xamarin.Forms;

namespace Tizen.Theme.Common
{
    public interface IGridViewController : IViewController
    {
        void SendItemFocused(GridViewFocusedEventArgs args);

        void SendItemSelected(SelectedItemChangedEventArgs args);
    }
}
