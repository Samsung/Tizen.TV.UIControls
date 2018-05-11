using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Sample.RecycleItemsView
{

    public class MyRecycleItemsView : Tizen.TV.UIControls.Forms.RecycleItemsView
    {
        protected override void OnItemFocused(object data, View targetView, bool isFocused)
        {
            AbsoluteLayout layout = (AbsoluteLayout)targetView;
            View textarea = layout.Children[1];
            if (isFocused)
            {
                targetView.ScaleTo(1.2);
                var animation = new Animation((rate) =>
                {
                    AbsoluteLayout.SetLayoutBounds(textarea, new Rectangle(0, 1, 480, 100 + rate * 100));
                });
                animation.Commit(this, $"Focused - {data.GetHashCode()}");
            }
            else
            {
                targetView.ScaleTo(1.0);
                var animation = new Animation((rate) =>
                {
                    AbsoluteLayout.SetLayoutBounds(textarea, new Rectangle(0, 1, 480, 200 - rate * 100));
                });
                animation.Commit(this, $"Focused - {data.GetHashCode()}");
            }
        }
    }

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CustomFocus : ContentPage
    {
        public CustomFocus ()
        {
            InitializeComponent();
        }

        void OnSelected(object sender, EventArgs args)
        {
            var text = ((sender as Tizen.TV.UIControls.Forms.RecycleItemsView).SelectedItem as PosterModel).Text;
            System.Console.WriteLine("{0} is selected", text);
            this.DisplayAlert("Selected", text, "OK");
        }
    }
}