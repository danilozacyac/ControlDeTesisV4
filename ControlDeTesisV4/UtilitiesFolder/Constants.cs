using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using ControlDeTesisV4.Dao;

namespace ControlDeTesisV4.UtilitiesFolder
{
    public class Constants
    {

        public static SolidColorBrush TextBoxDragColor = new SolidColorBrush(Color.FromRgb(133, 194, 255));
        public static SolidColorBrush TextBoxDropColor = new SolidColorBrush(Color.FromRgb(255, 255, 255));


        #region Variables UserControls

        public static ProyectosTesis TesisTurno = null;
        public static Ejecutorias EjecutoriaTurno = null;
        public static Votos VotoTurno = null;


        public static ObservableCollection<ProyectoPreview> ProyectosSalas;
        public static ObservableCollection<ProyectoPreview> ProyectosCcst;


        public static ObservableCollection<TesisTurnadaPreview> ListadoDeTesis;

        #endregion

    }
}
