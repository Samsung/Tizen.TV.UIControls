using Xamarin.Forms;

namespace Tizen.TV.UIControls.Forms
{
    interface IGridViewController : IViewController
    {
        void SendItemFocused(GridViewFocusedEventArgs args);

        void SendItemSelected(SelectedItemChangedEventArgs args);
    }
}
