using System;
using System.Linq;
using System.Windows.Data;
using ControlDeTesisV4.Singletons;

namespace ControlDeTesisV4.Converter
{
    public class AsuntosConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value != null)
            {
                int idAsunto = (Int32)value;

                return (from n in OtrosDatosSingleton.TipoAsuntos
                        where n.IdDato == idAsunto
                        select n.Descripcion).ToList()[0];
            }

            return " ";

        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
