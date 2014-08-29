using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.DataVisualization.Charting;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ControlDeTesisV4.Models;

namespace ControlDeTesisV4.Turno
{
    /// <summary>
    /// Lógica de interacción para AbogadoTotal.xaml
    /// </summary>
    public partial class AbogadoTotal : UserControl
    {

        private ObservableCollection<CargaTrabajo> pieData;

        public AbogadoTotal()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            pieData = CargasDeTrabajoModel.GetCargaPorAbogado();
            ((PieSeries)labeledPieChart.Series[0]).ItemsSource = pieData;

            labeledPieChart.Title = "";

            //labeledPieChart.Height = SystemParameters.FullPrimaryScreenHeight - 30;// System.Windows.SystemParameters.PrimaryScreenHeight
            //pieGrid.Visibility = Visibility.Visible;
            //barGrid.Visibility = Visibility.Collapsed;
        }
    }
}
