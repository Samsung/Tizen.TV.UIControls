using System;
using System.ComponentModel;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;

namespace Tizen.TV.UIControls.Forms
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Obsolete("This class is obsolete as of 1.1.0. Please use ContentPopupManager from Tizen.Theme.Common instead.")]
    public static class ContentPopupManager
    {
        public static async Task ShowPopup(this INavigation navigation, ContentPopup popup)
        {
            await ShowPopup(popup);
        }

        public static async Task ShowPopup(ContentPopup popup)
        {
            if (popup == null)
                return;

            using (var renderer = DependencyService.Get<IContentPopupRenderer>(DependencyFetchTarget.NewInstance))
            {
                if (renderer == null)
                    return;

                renderer.SetElement(popup);

                await renderer.Open();
            }
        }
    }
}
