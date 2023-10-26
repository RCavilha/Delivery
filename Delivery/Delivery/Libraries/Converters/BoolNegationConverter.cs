using System;
using Xamarin.Forms;

namespace Delivery.Libraries.Converters
{
    public class BoolNegationConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is bool boolValue)
                return !boolValue;

            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is bool boolValue && targetType == typeof(bool))
                return !boolValue;

            throw new NotSupportedException($"Cannot convert '{value}' back to the original type.");
        }
    }
}
