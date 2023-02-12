using System;
using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;


namespace Tizen.Theme.Common
{
    /// <summary>
    /// Base interface for ContentPopup.
    /// </summary>
    /// <since_tizen> 4 </since_tizen>
    public interface IContentPopup : IElement
    {
        /// <summary>
        /// Gets or sets a content to the popup.
        /// </summary>
        Layout Content { get; set; }

        /// <summary>
        /// Gets or sets a value that indicates whether the popup is opened.
        /// </summary>
        bool IsOpen { get; set; }

        /// <summary>
        /// Gets or sets a value for backgroundcolor of the popup
        /// </summary>
        Color BackgroundColor { get; set; }
    }
}
