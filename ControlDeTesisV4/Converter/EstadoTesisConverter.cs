using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace ControlDeTesisV4.Converter
{
    public class EstadoTesisConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            int estadoTesis = value as int? ?? 0;

            switch (estadoTesis)
            {
                case 4: return new SolidColorBrush(Colors.LightBlue);
                case 5: return new SolidColorBrush(Colors.White);
                case 6: return new SolidColorBrush(Colors.LightGreen);
                case 7: return new SolidColorBrush(Colors.LightPink);
                case 8: return new SolidColorBrush(Colors.Yellow);
                default: return new SolidColorBrush(Colors.White);
            }

        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
