using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
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
