# Converters directory
Using this directory for created you converters to UI
## Example converter class


``` csharp
//-----------------
// BoolToIntConverter.cs
//-----------------

// convert bool value to integer (0 or 1)
public class BoolToIntConverter: IValueConverter 
{
    public object Convert(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
    {
        if (int.TryParse(value.ToString(), out int number))
            return (number != 0) && (number > 0)
        return false;
    }

    public object ConvertBack(object value, Type targetType, object parameter,
        System.Globalization.CultureInfo culture)
    {
        if (targerType != typeof(bool))
            throw new InvalidOperationException("The target must be a boolean");

        return (bool) value ? 1 : 0; 
    }
}
```

Use in XAML:
``` xml
<!-- Connect namespace to Window or Page -->
<Window xmlns:converters="clr-namespace:YouApp.UI.Common.Converters" >
</Window>

<!-- Register in resource Page or Window -->
<Window.Resource>
    <converters:BoolToIntConverter x:Key="BoolToIntConverter"/>
</Window.Resource>

<!-- Element -->
<Button Content="Someone button"
        VerticalAligment="Center"
        HorizontalAligment="Center"
        Command="{Binding SomeoneCommand, Mode=OneWay}"
        IsEnable="{Binding SomeoneBooleanValue, Converter={StaticResource BoolToIntConverter}" />
```