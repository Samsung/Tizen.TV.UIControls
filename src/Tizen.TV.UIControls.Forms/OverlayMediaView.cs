using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace Tizen.TV.UIControls.Forms
{
    public class OverlayMediaView : MediaView, IOverlayOutput
    {
        internal static readonly BindablePropertyKey OverlayAreaPropertyKey = BindableProperty.CreateReadOnly(nameof(OverlayArea), typeof(Rectangle), typeof(OverlayMediaView), default(Rectangle));
        public static readonly BindableProperty OverlayAreaProperty = OverlayAreaPropertyKey.BindableProperty;

        public OverlayMediaView()
        {
            BatchCommitted += OnBatchCommitted;
        }
        
        public event EventHandler AreaUpdated;

        public Rectangle OverlayArea
        {
            get { return (Rectangle)GetValue(OverlayAreaProperty); }
            private set { SetValue(OverlayAreaPropertyKey, value); }
        }

        public override VideoOuputType OuputType => VideoOuputType.Overlay;


        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);
            if (new List<string> { nameof(X), nameof(Y), nameof(Width), nameof(Height)}.Contains(propertyName) && !Batched)
            {
                OverlayArea = Bounds;
            }
            if (propertyName == nameof(OverlayArea))
            {
                AreaUpdated?.Invoke(this, EventArgs.Empty);
            }
        }

        void OnBatchCommitted(object sender, Xamarin.Forms.Internals.EventArg<VisualElement> e)
        {
            OverlayArea = Bounds;
        }
    }
}
