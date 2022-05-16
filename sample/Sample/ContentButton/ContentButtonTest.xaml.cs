using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Xaml;

namespace Sample.ContentButton
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ContentButtonTest : ContentPage
    {
        public ContentButtonTest()
        {
            InitializeComponent();
        }

        int count = 0;
        private void ContentButton_Clicked(object sender, EventArgs e)
        {
            _label.Text = $"Text button ({++count})";
        }

        int imgIndex = 0;
        private void ContentButton_Clicked_1(object sender, EventArgs e)
        {
            string[] imgs =
            {
                "albumarts/1984, Van Halen.jpg",
                "albumarts/I'm in Love (I Wanna Do It), Alex Gaudino.jpg",
                "albumarts/1989, Taylor Swift.jpg"
            };
            _image.Source = imgs[++imgIndex % 3];
        }
    }
}