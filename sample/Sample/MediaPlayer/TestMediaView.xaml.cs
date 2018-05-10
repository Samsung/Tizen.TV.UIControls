using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Sample
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TestMediaView : ContentPage
    {
        double _x, _y;
        public TestMediaView ()
        {
            InitializeComponent ();
        }

        void OnPanUpdate(object sender, PanUpdatedEventArgs e)
        {
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