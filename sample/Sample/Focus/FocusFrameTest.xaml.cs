using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Xaml;
using Tizen.Theme.Common;

namespace Sample.Focus
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FocusFrameTest : ContentPage
    {
        public FocusFrameTest()
        {
            InitializeComponent();
            _focsusFrame1.ContentUnfocusedCommand = new Command(() =>
            {
                FocusedItemLabel.Text = $"Focused Item : ";
            });
        }

        int id = 0;

        void OnAddedClicked(object sender, System.EventArgs e)
        {
            var addedBtn = new Button
            {
                Text = $"Remove({id++})"
            };
            addedBtn.Clicked += (s, evt) =>
            {
                AddedContainer.Children.Remove(addedBtn);
            };
            AddedContainer.Children.Add(addedBtn);
        }

        void OnContentFocused(object sender, FocusEventArgs e)
        {
            FocusedItemLabel.Text = $"Focused Item : {e.VisualElement}";
        }
    }

    public class MyFocusFrame : FocusFrame
    {
        protected override void OnContentFocused(bool isFocused)
        {
            if (isFocused)
            {
                Content.ScaleTo(1.5);
            }
            else
            {
                Content.ScaleTo(1);
            }
        }
    }
}