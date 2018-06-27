using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Sample.RecycleItemsView
{
    public class ColorListView : Tizen.TV.UIControls.Forms.RecycleItemsView
    {
        protected override void OnItemFocused(object data, View targetView, bool isFocused)
        {
            if (data == Header)
                return;
            base.OnItemFocused(data, targetView, isFocused);
        }
    }


	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class HeaderTest : ContentPage
	{
		public HeaderTest ()
		{
			InitializeComponent ();
		}
	}
}