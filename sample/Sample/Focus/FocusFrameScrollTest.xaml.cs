using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Tizen.Theme.Common;

namespace Sample.Focus
{

    class ScrollFocusFrame : FocusFrame
    {
        protected override void OnContentFocused(bool isFocused)
        {
            if (isFocused)
            {
                var element = Parent;
                while (element != null)
                {
                    if (element is ScrollView scrollview)
                    {
                        Device.BeginInvokeOnMainThread(() =>
                        {
                            scrollview.ScrollToAsync(this, ScrollToPosition.Center, true);
                        });
                        return;
                    }
                    element = element.Parent;
                }
            }
        }
    }

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FocusFrameScrollTest : ContentPage
    {
        public FocusFrameScrollTest()
        {
            InitializeComponent();
        }
    }
}