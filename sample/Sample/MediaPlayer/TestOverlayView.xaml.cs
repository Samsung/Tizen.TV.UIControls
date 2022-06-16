using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Xaml;
using Microsoft.Maui.Graphics;

namespace Sample
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TestOverlayView : ContentPage
    {
        double _x, _y;
        public TestOverlayView ()
        {
            InitializeComponent ();
        }

        void OnPanUpdate(object sender, PanUpdatedEventArgs e)
        {
            if (e.TotalY != 0 && e.TotalY != 0)
            {
                AbsoluteLayout.SetLayoutBounds(VideoView, new Rect(_x + e.TotalX, _y + e.TotalY, 500, 300));
            }
            else
            {
                var bound = AbsoluteLayout.GetLayoutBounds(VideoView);
                _x = bound.X;
                _y = bound.Y;
            }
        }
    }
}