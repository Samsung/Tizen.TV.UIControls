using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
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

            using (var renderer = CommonUI.Context.Services.GetService<IContentPopupRenderer>())
            {
                if (renderer == null)
                    return;

                renderer.SetElement(popup);

                await renderer.Open();
            }
        }
    }
}
