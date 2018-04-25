using System;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace Tizen.TV.UIControls.Forms
{
    public class OverlayPage : ContentPage, IOverlayOutput
    {
        public static readonly BindableProperty OverlayAreaProperty = BindableProperty.Create("OverlayArea", typeof(Rectangle), typeof(OverlayPage), default(Rectangle));
        public static readonly BindableProperty PlayerProperty = BindableProperty.Create("Player", typeof(MediaPlayer), typeof(OverlayPage), default(MediaPlayer), propertyChanged: (b, o, n) => ((OverlayPage)b).OnPlayerChanged());

        public Rectangle OverlayArea
        {
            get { return (Rectangle)GetValue(OverlayAreaProperty); }
            set { SetValue(OverlayAreaProperty, value); }
        }

        public MediaPlayer Player
        {
            get { return (MediaPlayer)GetValue(PlayerProperty); }
            set { SetValue(PlayerProperty, value); }
        }

        VisualElement IVideoOutput.MediaView => this;

        public event EventHandler AreaUpdated;

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);
            AreaUpdated?.Invoke(this, EventArgs.Empty);
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            if (Player != null)
            {
                SetInheritedBindingContext(Player, BindingContext);
            }
        }

        void OnPlayerChanged()
        {
            if (Player != null)
            {
                Player.VideoOutput = this;
                SetInheritedBindingContext(Player, BindingContext);
            }
        }

        public VideoOuputType OuputType => VideoOuputType.Overlay;
    }
}