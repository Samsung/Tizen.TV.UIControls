
# ContentButton
`ContentButton` is a type of Xamarin.Forms.ContentView that contains a single child element (called Content) and is typically used for custom, reusable controls. Also, as its name implies, ContentButton is designed to be used like a Button that implements `Xamarin.Forms.IButtonController`.

## How to customize the button using `ContentButton`?

`ContentButton` provides the view to show and the states(Clicked, Pressed and Released) of the button. You can customize the button through changing the view according to the state.
The following example shows the CustomButton composed of a combination of Images that define the icon, background, and border of a button.
To show a border, this example has set an outlined image with blending color as a Content, and the background color of the button will change to gray when the button is pressed for click-effect.

For more information, see the following links:

- [ContentButton API reference](https://samsung.github.io/Tizen.TV.UIControls/api/Tizen.TV.UIControls.Forms.ContentButton.html)

## Create ContentButton

**C# file**

```cs
public class ContentButtonTest : ContentPage
{
    public ContentButtonTest()
    {
        ContentButton button = new ContentButton 
        { 
            Content = new Label { Text = "Text Button" }
        };

        Content = new StackLayout
        {
            Children =
            {
                button,
            }
        };
    }
}
```

**XAML file**

```xml
<?xml version="1.0" encoding="utf-8" ?>
<ContentPage xmlns="http://xamarin.com/schemas/2014/forms"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             xmlns:tv="clr-namespace:Tizen.TV.UIControls.Forms;assembly=Tizen.TV.UIControls.Forms"
             Title="ContentButton Test"
             x:Class="Sample.ContentButton.ContentButtonTest">
    <ContentPage.Content>
        <StackLayout HorizontalOptions="Center">
            <tv:ContentButton Clicked="ContentButton_Clicked">
                <VisualStateManager.VisualStateGroups>
                    <VisualStateGroup x:Name="CommonStates">
                        <VisualState x:Name="Normal">
                            <VisualState.Setters>
                                <Setter Property="BackgroundColor" Value="Transparent"/>
                            </VisualState.Setters>
                        </VisualState>
                        <VisualState x:Name="Focused">
                            <VisualState.Setters>
                                <Setter Property="BackgroundColor" Value="Orange"/>
                            </VisualState.Setters>
                        </VisualState>
                    </VisualStateGroup>
                </VisualStateManager.VisualStateGroups>
                <Label FontSize="Header" x:Name="_label" Text="Text Button" />
            </tv:ContentButton>
        </StackLayout>
    </ContentPage.Content>
</ContentPage>
```
