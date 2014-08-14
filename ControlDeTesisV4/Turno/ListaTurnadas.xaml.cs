using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ControlDeTesisV4.Dao;
using ControlDeTesisV4.Models;
using ControlDeTesisV4.UtilitiesFolder;

namespace ControlDeTesisV4.Turno
{
    /// <summary>
    /// Lógica de interacción para ListaTurnadas.xaml
    /// </summary>
    public partial class ListaTurnadas : UserControl
    {
        public static TesisTurnadaPreview TesisTurnada;
        private int estadoTesis;

        public ListaTurnadas(int estadoTesis)
        {
            InitializeComponent();
            this.estadoTesis = estadoTesis;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            
            GListadoTurno.DataContext = new TesisTurnadasModel().GetPreviewTesisTurnadas(estadoTesis);
        }

        private void GListadoTurno_SelectionChanged(object sender, Telerik.Windows.Controls.SelectionChangeEventArgs e)
        {


            TesisTurnada = GListadoTurno.SelectedItem as TesisTurnadaPreview;

            if (estadoTesis == 4)
                Constants.TesisPorTurnar = new ProyectoTesisSalasModel().GetProyectoTesis(TesisTurnada.IdTesis);
            else if (estadoTesis == 5)
                Constants.TesisTurnada = new ProyectoTesisSalasModel().GetProyectoTesis(TesisTurnada.IdTesis);

        }
    }
}
