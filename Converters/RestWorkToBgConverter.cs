using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace WorkweekChecker.Converters
{
    public class RestWorkToBgConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool isRest = value is bool b && b;
            return (Brush)new BrushConverter().ConvertFromString(isRest ? "#16A34A" : "#DC2626")!;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotSupportedException();
    }
}
