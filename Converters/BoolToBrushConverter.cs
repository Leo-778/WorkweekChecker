using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace WorkweekChecker.Converters
{
    public class BoolToBrushConverter : IValueConverter
    {
        public Brush TrueBrush { get; set; } = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#16A34A"));
        public Brush FalseBrush { get; set; } = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#DC2626"));
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool b = value is bool bb && bb;
            return b ? TrueBrush : FalseBrush;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotSupportedException();
    }
}
