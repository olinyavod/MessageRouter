using System;
using System.Globalization;
using System.Windows.Data;

namespace Melomans.Windows
{
    class BooleanToValueConverter:IValueConverter
    {
        public object TrueValue { get; set; }

        public object FalseValue { get; set; }

        public object NullValue { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var b = value as bool?;
            switch (b)
            {
                case true:
                    return TrueValue;
                case false:
                    return FalseValue;
                default:
                    return NullValue;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
