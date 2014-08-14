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

            if (estadoTesis == 99) //Publicada
            {
                return new SolidColorBrush(Colors.Orange);
            }
           
            else if (estadoTesis == 0) //Espera Turno
            {
                return new SolidColorBrush(Colors.LightBlue);
            }
            else 
            {
                return new SolidColorBrush(Colors.White);
            }
            

        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
