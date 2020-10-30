
# ContentPopup
## Introduction
`ContentPopup` is a type of Xamarin.Forms.Element (not a Xamarin.Forms.VisualElement) that contains a single child element (called Content) and allows to open it as a popup.

## Create a ContentPopup
You can easily create a `ContentPopup` like any other Xamarin.Forms views.

``` c#
// Creates a ContentPopup
var popup = new ContentPopup
{
    BackgroundColor = Color.FromHex("#CCF0F8FF"),
    Content = new StackLayout
    {
        Children =
        {
            new Label
            {
                Text = "This ContentPopup is dismissed as a back key.",
            }
        }
    }
}; 
```

## Show a ContentPopup
Similar to Xamarin.Forms `DisplayActionSheet()` and `DisplayAlert()`, to open a `ContentPopup`, you can call `ShowPopup()` as shown below.

```cs
await Navigation.ShowPopup(popup);
```

> An awaitable Task will be returned when popup is closed. 

## Content
You can customize the content of the popup as you want by setting the `Content` property. All Xamarin.Forms layouts can be set as `Content` (not a Xamarin.Forms.View).

## BackgroundColor
The background color refers to the color dimmed by `ContentPopup`. The default background color of `ContentPopup` is `Tranparent`. So if you want to change this, just set `BackgroundColor` to the color you want.

## Back Button
Basically, you can use the back button of remote controller to close the `ContentPopup`. If you want the back button to behave differently, override `OnBackButtonPressed` as shown below. This example shows that when the back button is pressed, the `ContentPopup` doesn't do the default behavior to close. In this case, the user must explicitly use `Dismiss()` to close the `ContentPopup`.

```cs
public class MyPopup : CPopup
{
    protected override bool OnBackButtonPressed()
    {
        return true;
    }
}
```

## Related Links
 * [API reference](https://samsung.github.io/Tizen.TV.UIControls/api/Tizen.TV.UIControls.Forms.ContentPopup.html)
 * [Sample](https://github.com/Samsung/Tizen.TV.UIControls/tree/master/sample/Sample/ContentPopup)
