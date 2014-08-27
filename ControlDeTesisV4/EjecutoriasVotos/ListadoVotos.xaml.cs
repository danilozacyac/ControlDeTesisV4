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
using ControlDeTesisV4.UtilitiesFolder;

namespace ControlDeTesisV4.EjecutoriasVotos
{
    /// <summary>
    /// Lógica de interacción para ListadoVotos.xaml
    /// </summary>
    public partial class ListadoVotos : UserControl
    {
        public ListadoVotos()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            ListaVotos.DataContext = Constants.ListadoDeVotos;
        }

        private void GListadoTurno_SelectionChanged(object sender, Telerik.Windows.Controls.SelectionChangeEventArgs e)
        {

        }
    }
}
