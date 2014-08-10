using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using ControlDeTesisV4.Singletons;

namespace ControlDeTesisV4.Converter
{
    public class AbogadoConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value != null)
            {
                int idAbogado = (Int32)value;

                return (from n in FuncionariosSingleton.AbogResp
                        where n.IdFuncionario == idAbogado
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
