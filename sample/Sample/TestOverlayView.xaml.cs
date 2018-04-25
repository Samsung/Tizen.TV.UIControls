using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

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
            Console.WriteLine("Update Panupdate");
            if (e.TotalY != 0 && e.TotalY != 0)
            {
                AbsoluteLayout.SetLayoutBounds(VideoView, new Rectangle(_x + e.TotalX, _y + e.TotalY, 500, 300));
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