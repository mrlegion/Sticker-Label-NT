using System;
using System.Globalization;
using System.Windows.Data;

namespace StickerLib.UI.Common.Converters
{
    public class InverseBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return false;
            if (value is bool result)
                return !result;
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return false;
            if (value is bool result)
                return !result;
            return false;
        }
    }
}