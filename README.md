# Tizen TV UIControls
## Quick start
### MediaPlayer
#### Use in Xaml
``` xaml 
<tvcontrols:MediaView 
  xmlns:tvcontrols="clr-namespace:Tizen.TV.UIControls.Forms;assembly=Tizen.TV.UIControls.Forms">
    <tvcontrols:MediaPlayer
        Source="{Binding Source}"
        AutoStart="true"/>
</tvcontrols:MediaView>
```
#### Use by code
```C#
var view = new MediaView();
var player = new MediaPlayer();
player.VideoOutput = view;
player.Source = "a.mp4";
player.Start();
```
