using System;
using System.Threading.Tasks;

namespace Tizen.TV.UIControls.Forms
{
    /// <summary>
    /// Base interface for ContentPopup renderer.
    /// </summary>
    /// <since_tizen> 4 </since_tizen>
    public interface IContentPopupRenderer : IDisposable
    {
        /// <summary>
        /// Sets the Element associated with this renderer.
        /// </summary>
        /// <param name="element">New element.</param>
        void SetElement(ContentPopup element);

        /// <summary>
        /// Open a popup.
        /// </summary>
        /// <returns>Returns a Task with the dismiss result of the popup.</returns>
        Task Open();
    }
}
