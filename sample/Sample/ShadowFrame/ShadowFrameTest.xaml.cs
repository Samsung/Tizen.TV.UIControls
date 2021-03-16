using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Sample.ShadowFrame
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ShadowFrameTest : ContentPage
    {
        public ShadowFrameTest()
        {
            InitializeComponent();
            tlSlider.Value = 20;
            trSlider.Value = 20;
            blSlider.Value = 20;
            brSlider.Value = 20;
            blurSlider.Value = 10;
            offsetXSlider.Value = 0;
            offsetYSlider.Value = 0;
        }

        void radius_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            Frame1.CornerRadius = new CornerRadius(tlSlider.Value, trSlider.Value, blSlider.Value, brSlider.Value);
        }

        void borderColor_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            Frame1.BorderColor = Color.FromRgba(RedColor.Value, GreenColor.Value, BlueColor.Value, AlphaColor.Value);
        }

        void shadowColor_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            Frame1.ShadowColor = Color.FromRgb(RedColorS.Value, GreenColorS.Value, BlueColorS.Value);
        }

        void borderWidth_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            Frame1.BorderWidth = borderWidthSlider.Value;
        }

        void bgColor_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            Frame1.BackgroundColor = Color.FromRgba(RedColorBG.Value, GreenColorBG.Value, BlueColorBG.Value, AlphaColorBG.Value);
        }

        void blurOpacity_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            Console.WriteLine("[ShadowOpacity] : " + Frame1.ShadowOpacity);
        }

        void blurRadius_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            Console.WriteLine("[ShadowBlurRadius] : " + Frame1.ShadowBlurRadius);
        }

        void shadowOffset_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            Console.WriteLine("[ShadowOffset] (X : " + Frame1.ShadowOffsetX + ", Y: " + Frame1.ShadowOffsetY+")");
        }

        private void hasShadow_Clicked(object sender, EventArgs e)
        {
            Frame1.HasShadow = !Frame1.HasShadow;
        }

        private void allowShadowClipping_Clicked(object sender, EventArgs e)
        {
            Frame1.AllowShadowClipping = !Frame1.AllowShadowClipping;
        }
    }
}