using System.Threading.Tasks;
using Microsoft.Maui.Controls;

namespace Tizen.Theme.Common
{
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
