using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Tizen.TV.UIControls.Forms;
namespace Sample
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TestAspect : OverlayPage
    {
        public TestAspect ()
        {
            InitializeComponent ();
        }

        void OnAspectFill(object sender, EventArgs e)
        {
            Player.AspectMode = DisplayAspectMode.AspectFill;
        }
        void OnAspectFit(object sender, EventArgs e)
        {
            Player.AspectMode = DisplayAspectMode.AspectFit;
        }
        void OnOriginal(object sender, EventArgs e)
        {
            Player.AspectMode = DisplayAspectMode.OrignalSize;
        }

        void OnFill(object sender, EventArgs e)
        {
            Player.AspectMode = DisplayAspectMode.Fill;
        }
    }
}