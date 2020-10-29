using System;
using System.ComponentModel;
using Xamarin.Forms;

namespace Tizen.TV.UIControls.Forms
{
    /// <summary>
    /// The ContentPopup is a Popup, which allows you to customize the Layout to be displayed.
    /// </summary>
    /// <since_tizen> 4 </since_tizen>
    public class ContentPopup : Element
    {
        /// <summary>
        /// BindableProperty. Identifies the Content bindable property.
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        public static readonly BindableProperty ContentProperty = BindableProperty.Create(nameof(Content), typeof(Layout), typeof(ContentPopup), null, propertyChanged: (b, o, n) => ((ContentPopup)b).UpdateContent());

        /// <summary>
        /// BindableProperty. Identifies the IsOpen bindable property.
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        public static readonly BindableProperty IsOpenProperty = BindableProperty.Create(nameof(IsOpen), typeof(bool), typeof(ContentPopup), false, defaultBindingMode: BindingMode.TwoWay);

        /// <summary>
        /// BindableProperty. Identifies the BackgroundColor bindable property.
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        public static readonly BindableProperty BackgroundColorProperty = BindableProperty.Create(nameof(BackgroundColor), typeof(Color), typeof(ContentPopup), Color.Default);

        /// <summary>
        /// Occurs when the popup is dismissed.
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        public event EventHandler Dismissed;

        /// <summary>
        /// Gets or sets content of the Popup.
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        public Layout Content
        {
            get { return (Layout)GetValue(ContentProperty); }
            set { SetValue(ContentProperty, value); }
        }

        /// <summary>
        /// Gets or sets the popup is opened.
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        public bool IsOpen
        {
            get { return (bool)GetValue(IsOpenProperty); }
            set { SetValue(IsOpenProperty, value); }
        }

        /// <summary>
        /// Gets or sets the background color of popup.
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        public Color BackgroundColor
        {
            get { return (Color)GetValue(BackgroundColorProperty); }
            set { SetValue(BackgroundColorProperty, value); }
        }

        /// <summary>
        /// Dismisses the popup.
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        public void Dismiss()
        {
            IsOpen = false;
        }

        /// <summary>
        /// For internal use.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void SendDismissed()
        {
            Dismissed?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// For internal use.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public bool SendBackButtonPressed()
        {
            return OnBackButtonPressed();
        }

        /// <summary>
        /// To change the default behavior of the BackButton. Default behavior is dismiss.
        /// </summary>
        /// <returns>Default is false</returns>
        protected virtual bool OnBackButtonPressed()
        {
            return false;
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            if (Content != null)
                SetInheritedBindingContext(Content, BindingContext);
        }

        void UpdateContent()
        {
            if (Content != null)
                OnChildAdded(Content);
        }
    }
}
