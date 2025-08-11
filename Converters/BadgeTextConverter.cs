using System;
using System.Globalization;
using System.Windows.Data;

namespace WorkweekChecker.Converters
{
    public class BadgeTextConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            bool isRest = values.Length > 0 && values[0] is bool b && b;
            string text = values.Length > 1 ? values[1]?.ToString() ?? string.Empty : string.Empty;
            string emph = values.Length > 2 ? values[2]?.ToString() ?? string.Empty : string.Empty;
            return (isRest && !string.IsNullOrWhiteSpace(emph)) ? emph : text;
        }
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture) => throw new NotSupportedException();
    }
}
