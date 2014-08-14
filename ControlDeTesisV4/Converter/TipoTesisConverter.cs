using System;
using System.Linq;
using System.Windows.Data;
using System.Windows.Media;

namespace ControlDeTesisV4.Converter
{
    public class TipoTesisConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            int  date = value as int? ?? 0;

            if (date == 0)
            {
                return new SolidColorBrush(Colors.White);
            }
            else if (date == 1)
            {
                return new SolidColorBrush(Colors.Orange);
            }
            else
                return new SolidColorBrush(Colors.Blue);

        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
