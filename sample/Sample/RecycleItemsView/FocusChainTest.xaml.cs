using Xamarin.Forms;
using Xamarin.Forms.Xaml;
namespace Sample.RecycleItemsView
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class FocusChainTest : ContentPage
	{
		public FocusChainTest ()
		{
			InitializeComponent ();
		}

        protected override void OnAppearing()
        {
            Xamarin.Forms.PlatformConfiguration.TizenSpecific.VisualElement.SetNextFocusLeftView(ItemsView1, ItemsView4);
            Xamarin.Forms.PlatformConfiguration.TizenSpecific.VisualElement.SetNextFocusLeftView(ItemsView2, ItemsView1);
            Xamarin.Forms.PlatformConfiguration.TizenSpecific.VisualElement.SetNextFocusLeftView(ItemsView3, ItemsView2);
            Xamarin.Forms.PlatformConfiguration.TizenSpecific.VisualElement.SetNextFocusLeftView(ItemsView4, ItemsView3);

            Xamarin.Forms.PlatformConfiguration.TizenSpecific.VisualElement.SetNextFocusRightView(ItemsView1, ItemsView2);
            Xamarin.Forms.PlatformConfiguration.TizenSpecific.VisualElement.SetNextFocusRightView(ItemsView2, ItemsView3);
            Xamarin.Forms.PlatformConfiguration.TizenSpecific.VisualElement.SetNextFocusRightView(ItemsView3, ItemsView4);
            Xamarin.Forms.PlatformConfiguration.TizenSpecific.VisualElement.SetNextFocusRightView(ItemsView4, ItemsView1);
        }
    }
}