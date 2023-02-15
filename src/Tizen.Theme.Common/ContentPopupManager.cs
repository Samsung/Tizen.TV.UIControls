using System;
using System.ComponentModel;
using System.Threading.Tasks;
using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Platform;

namespace Tizen.Theme.Common
{
    public static class ContentPopupManager
    {
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Obsolete("This class is obsolete as of 1.1.0. Please use ShowPopup(this Pae, ContentPopup) from Tizen.Theme.Common instead.")]
        public static async Task ShowPopup(this INavigation navigation, ContentPopup popup)
        {
        }

        public static void ShowPopup(this Page page, ContentPopup popup)
        {
            if (popup == null)
                return;

            var mauiContext = page?.Handler?.MauiContext;
            popup.ToPlatform(mauiContext);
            popup.IsOpen = true;
        }
    }
}
