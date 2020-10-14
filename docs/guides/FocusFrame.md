
# FocusFrame
`FocusFrame` helps developers to decorate a focused view. it contain a view to represent and if it got a focus, backgroud color of `FocusFrame` is changed

## How to use it
 Set a `Content` property using a view that a target of focus, set `FocusedColor` to want. (default color is Orange)

### xaml
``` xml
<ContentPage xmlns="http://xamarin.com/schemas/2014/forms"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             xmlns:tv="clr-namespace:Tizen.TV.UIControls.Forms;assembly=Tizen.TV.UIControls.Forms"
             x:Class="Sample.FocusFrameTest"
             x:Name="rootPage">
    <ContentPage.Content>
        <StackLayout>
            <tv:FocusFrame FocusedColor="Blue">
                <Button Text="Button1"/>
            </tv:FocusFrame>
        </StackLayout>
    </ContentPage.Content>
</ContentPage>
```

### C#
``` C#
public class FocusFrameTest : ContentPage
{
    public FocusFrameTest()
    {
        Button button1 = new Button { Text = "Button1" };
        FocusFrame focusFrame = new FocusFrame
        {
            FocusedColor = Color.Blue,
            Content = Button1
        };

        Content = new StackLayout
        {
            Children =
            {
                focusFrame,
            }
        };
    }
}
```


## How to override the focus effect
 Overriding `OnContentFocused` method and implements decorating code
``` C#
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
```
``` xml
<ContentPage xmlns="http://xamarin.com/schemas/2014/forms"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             xmlns:tv="clr-namespace:Tizen.TV.UIControls.Forms;assembly=Tizen.TV.UIControls.Forms"
             xmlns:local="clr-namespace:Sample.Focus"
             x:Class="Sample.FocusFrameTest">
    <ContentPage.Content>
        <StackLayout>
            <local:MyFocusFrame>
                <Button Text="Button1"/>
            </tv:MyFocusFrame>
        </StackLayout>
    </ContentPage.Content>
</ContentPage>
```