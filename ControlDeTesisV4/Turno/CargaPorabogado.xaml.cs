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
using ControlDeTesisV4.Models;
using Telerik.Windows.Controls.Charting;
using Telerik.Windows.Controls.ChartView;

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
