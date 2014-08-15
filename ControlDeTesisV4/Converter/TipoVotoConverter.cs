using System;
using System.Linq;
using System.Windows.Data;
using ControlDeTesisV4.Singletons;

namespace ControlDeTesisV4.Converter
{
    public class TipoVotoConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            int date = value as int? ?? 0;

            if (date > 0)
            {
                return (from n in OtrosDatosSingleton.TipoVotos
                       where n.IdDato == date
                       select n.Descripcion).ToList()[0];
            }
            else
            {
                return " ";
            }

        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
