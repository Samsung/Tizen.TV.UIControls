using Xamarin.Forms;

namespace Tizen.TV.UIControls.Forms
{
    [ContentProperty("Player")]
    public class MediaView : View, IVideoOutput
    {
        public static readonly BindableProperty PlayerProperty = BindableProperty.Create("Player", typeof(MediaPlayer), typeof(MediaView), default(MediaPlayer), propertyChanged: (b, o, n) => ((MediaView)b).OnPlayerChanged());
        public MediaPlayer Player
        {
            get { return (MediaPlayer)GetValue(PlayerProperty); }
            set { SetValue(PlayerProperty, value); }
        }

        public virtual VideoOuputType OuputType => VideoOuputType.Buffer;

        VisualElement IVideoOutput.MediaView => this;

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
    }
}
