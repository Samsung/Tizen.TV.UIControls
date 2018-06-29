
## Problem definition
 * MediaPlayer.Position is int value, but Progressbar.Progress is double value (0 to 1.0)
 * Need duration to convert

### Create a custom Converter
 Implements [Xamarin.Forms.IValueConverter](https://developer.xamarin.com/api/type/Xamarin.Forms.IValueConverter/)

```C#
    public class PositionToProgressConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((int)value / (double)(GetParameter(parameter)));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (int)((double)value * GetParameter(parameter));
        }
        int GetParameter(object parameter)
        {
            
            var duration = ((Tizen.TV.UIControls.Forms.MediaPlayer)parameter).Duration;
            if (duration == 0)
                duration = 1;
            return duration;
        }
    }
```
 Pass a MediaPlayer instance as the parameter

### Use custom converter in Xaml
```xaml
....

    <ContentPage.Resources>
        <ResourceDictionary>
            <local:PositionToProgressConverter x:Key="positionToProgress"/>
        </ResourceDictionary>
    </ContentPage.Resources>
...

    <ProgressBar Progress="{Binding Source={x:Reference Player}, Path=Position, Converter={StaticResource positionToProgress}, ConverterParameter={x:Reference Player}}}"/>

....
```

