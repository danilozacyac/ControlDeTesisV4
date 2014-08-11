﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace ControlDeTesisV4.Converter
{
    public class SemaforoConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value != null)
            {
                int semaforo = (Int32)value;

                if (semaforo > 5)
                    semaforo = 5;


                switch (semaforo)
                {
                    case 1:
                    case 2: return "/ControlDeTesisV4;component/Resources/semVerde.png";
                    case 3:
                    case 4: return "/ControlDeTesisV4;component/Resources/semAmarillo.png";
                    case 5: return "/ControlDeTesisV4;component/Resources/semRojo.png";
                }
            }

            return " ";

        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}