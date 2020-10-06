using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace Tizen.TV.UIControls.Forms
{
    public class FocusFrame : Frame
    {
        public FocusFrame()
        {
            Padding = 10;
            BorderColor = Color.Transparent;
            HasShadow = false;
        }

        protected override void OnPropertyChanging([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanging(propertyName);
            if (propertyName == nameof(Content))
            {
                if (Content != null)
                {
                    Content.Focused -= OnContentFocused;
                    Content.Unfocused -= OnContentFocused;
                }
            }
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);
            if (propertyName == nameof(Content))
            {
                if (Content != null)
                {
                    Content.Focused += OnContentFocused;
                    Content.Unfocused += OnContentFocused;
                }
            }
        }

        protected virtual void OnContentFocused(bool isFocused)
        {
            BackgroundColor = isFocused ? Color.Orange : Color.Transparent;
        }

        void OnContentFocused(object sender, FocusEventArgs e)
        {
            OnContentFocused(e.IsFocused);
        }
    }
}
