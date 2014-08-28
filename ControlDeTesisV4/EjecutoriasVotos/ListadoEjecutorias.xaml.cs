using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using ControlDeTesisV4.Dao;
using ControlDeTesisV4.UtilitiesFolder;

namespace ControlDeTesisV4.EjecutoriasVotos
{
    /// <summary>
    /// Lógica de interacción para ListadoEjecutorias.xaml
    /// </summary>
    public partial class ListadoEjecutorias : UserControl
    {
        public ListadoEjecutorias()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            ListaEjecuto.DataContext = Constants.ListadoDeEjecutorias;
        }

        private void GListadoTurno_SelectionChanged(object sender, Telerik.Windows.Controls.SelectionChangeEventArgs e)
        {
            Constants.EjecutoriaTurno = ListaEjecuto.SelectedItem as Ejecutorias;
        }
    }
}
