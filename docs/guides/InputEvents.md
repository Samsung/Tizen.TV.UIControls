
# InputEvents
`InputEvents` helps developers to handle the remote control events that are emitted from TV devices.
The `RemoteKeyHandler` which contains a Command and key events can be added to a collection of handlers.
The access key can be set to a specific view and the view gets a direct focus when the key is pressed. When the view is a `Button`, `Clicked` event occurs also.

## How to add RemoteKeyHandler
Get a collection of handlers using `GetEventHandlers(BindableObject view)`, and add `RemoteKeyHandler` to it.

### xaml
``` xaml
<ContentPage xmlns="http://xamarin.com/schemas/2014/forms"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             xmlns:uicontrols="clr-namespace:Tizen.TV.UIControls.Forms;assembly=Tizen.TV.UIControls.Forms"
             x:Class="Sample.TestRemoteControl_xaml"
             x:Name="rootPage">
    <ContentPage.Content>
        <StackLayout>
            <Button Text="Button1">
                <uicontrols:InputEvents.EventHandlers>
                    <uicontrols:RemoteKeyHandler Command="{Binding ButtonHandler, Source={x:Reference rootPage}}"/>
                </uicontrols:InputEvents.EventHandlers>
            </Button>
        </StackLayout>
    </ContentPage.Content>
</ContentPage>
```

### C#
``` C#
public class TestRemoteControl : ContentPage
{
    public TestRemoteControl()
    {
        Button button1 = new Button { Text = "Button1" };

        RemoteKeyHandler buttonHandler = new RemoteKeyHandler(new Action<RemoteControlKeyEventArgs>((arg) =>
        {
            button1.Text = $"Button1 : {arg.KeyType} {arg.KeyName} {arg.PlatformKeyName}";
        }));
        InputEvents.GetEventHandlers(button1).Add(buttonHandler);
        
        Content = new StackLayout
        {
            Children =
            {
                button1,
            }
        };
    }
}
```


## How to set an access key

### xaml
``` xaml
<ContentPage xmlns="http://xamarin.com/schemas/2014/forms"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             xmlns:uicontrols="clr-namespace:Tizen.TV.UIControls.Forms;assembly=Tizen.TV.UIControls.Forms"
             x:Class="Sample.TestRemoteControl_xaml"
             x:Name="rootPage">
    <ContentPage.Content>
        <StackLayout>
            <Button Text="Button1 (accesskey 1)" uicontrols:InputEvents.AccessKey="NUM1" Clicked="OnClicked" />
        </StackLayout>
    </ContentPage.Content>
</ContentPage>
```

### C#
``` C#
public class TestRemoteControl : ContentPage
{
    int _clickedTimes = 0;
    public TestRemoteControl()
    {
        Button button1 = new Button { Text = "Button2 (Accesskey 1)" };
        button1.Clicked += (s, e) =>
        {
            button1.Text = $"Button1 (Accesskey 1): {++_clickedTimes} clicked";
        };
        InputEvents.SetAccessKey(button1, RemoteControlKeyNames.NUM1);

        Content = new StackLayout
        {
            Children =
            {
                button1,
            }
        };
    }
}
```