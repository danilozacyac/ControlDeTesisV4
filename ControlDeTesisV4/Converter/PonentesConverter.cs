using System;
using System.Linq;
using System.Windows.Data;
using ControlDeTesisV4.Singletons;

namespace ControlDeTesisV4.Converter
{
    public class PonentesConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value != null)
            {
                int idPonente = (Int32)value;

                return  (from n in FuncionariosSingleton.Ponentes
                       where n.IdFuncionario == idPonente
                       select n.NombreCompleto).ToList()[0];
            }

            return " ";

        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
