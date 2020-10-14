using Tizen.TV.UIControls.Forms;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Sample.Focus
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FocusFrameTest : ContentPage
    {
        public FocusFrameTest()
        {
            InitializeComponent();
        }
    }

    public class MyFocusFrame : FocusFrame
    {
        protected override void OnContentFocused(bool isFocused)
        {
            if (isFocused)
            {
                Content.ScaleTo(1.5);
            }
            else
            {
                Content.ScaleTo(1);
            }
        }
    }
}