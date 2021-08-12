using Avalonia.Data.Converters;
using Avalonia.Styling;
using System;
using System.Globalization;

namespace Aldwych.Logging.Converters
{
    public class ObjectToStyleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
          
            return value.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
