using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using ControlDeTesisV4.Models;

namespace ControlDeTesisV4.Turno
{
    /// <summary>
    /// Lógica de interacción para CargaPorabogado.xaml
    /// </summary>
    public partial class CargaPorabogado : UserControl
    {
        public CargaPorabogado()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            ChartDataSource1.DataContext = CargasDeTrabajoModel.GetCargaPorAbogado();
        }
    }
}
