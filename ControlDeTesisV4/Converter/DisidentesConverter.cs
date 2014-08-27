using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Data;
using ControlDeTesisV4.Singletons;

namespace ControlDeTesisV4.Converter
{
    public class DisidentesConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            ObservableCollection<int> ministros = value as ObservableCollection<int>;

            if (ministros == null || ministros.Count == 0)
            {
                return " ";
            }
            else
            {
                string ministrosStr = "";

                foreach (int id in ministros)
                {
                    ministrosStr += (from n in FuncionariosSingleton.Ponentes
                                     where n.IdFuncionario == id
                                     select n.NombreCompleto).ToList()[0] + ", ";
                }

                return ministrosStr;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}