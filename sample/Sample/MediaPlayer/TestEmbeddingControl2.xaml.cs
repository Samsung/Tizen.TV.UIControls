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
	public partial class TestEmbeddingControl2 : OverlayPage
	{
		public TestEmbeddingControl2 ()
		{
			InitializeComponent ();
            Btn.Clicked += OnClick;
		}

        void OnClick(object sender, EventArgs e)
        {
            Player.UsesEmbeddingControls = !Player.UsesEmbeddingControls;
        }
    }
}