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
	public partial class TestEmbeddingControl : ContentPage
	{
		public TestEmbeddingControl ()
		{
			InitializeComponent ();
		}

        void OnToggled(object sender, EventArgs e)
        {
            Player.UsesEmbeddingControls = !Player.UsesEmbeddingControls;
        }
    }
}