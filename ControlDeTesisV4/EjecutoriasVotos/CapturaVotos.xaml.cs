using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ControlDeTesisV4.Dao;
using ModuloInterconexionCommonApi;
using Telerik.Windows.Controls;

namespace ControlDeTesisV4.EjecutoriasVotos
{
    /// <summary>
    /// Interaction logic for CapturaVotos.xaml
    /// </summary>
    public partial class CapturaVotos
    {
        private PrecedentesTesis precedente;
        private ObservableCollection<Votos> listaVotos;

        public CapturaVotos()
        {
            InitializeComponent();
            precedente = new PrecedentesTesis();
            listaVotos = new ObservableCollection<Votos>();
        }

        private void RadWindow_Loaded(object sender, RoutedEventArgs e)
        {
            this.DataContext = precedente;
        }

        private void Numeric_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = StringUtilities.IsTextAllowed(e.Text);
        }

        private void BtnGuardar_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnAgregar_Click(object sender, RoutedEventArgs e)
        {
            DetalleVotos detalle = new DetalleVotos();
            detalle.ShowDialog();
        }
    }
}
