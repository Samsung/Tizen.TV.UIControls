using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TMDb
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CastListView : ContentView
    {
        public CastListView ()
        {
            InitializeComponent ();
        }
    }
}