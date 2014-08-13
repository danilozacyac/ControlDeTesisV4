using System;
using System.Collections.Generic;
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
using ControlDeTesisV4.Singletons;
using DocumentMgmtApi;
using ModuloInterconexionCommonApi;
using Telerik.Windows.Controls;

namespace ControlDeTesisV4.EjecutoriasVotos
{
    /// <summary>
    /// Interaction logic for DetalleVotos.xaml
    /// </summary>
    public partial class DetalleVotos
    {
        private List<int> minSeleccionados = new List<int>();
        private Votos voto;

        public DetalleVotos()
        {
            InitializeComponent();
        }

        private void RadWindow_Loaded(object sender, RoutedEventArgs e)
        {
            CbxMinistros.DataContext = FuncionariosSingleton.Ponentes;
            LstMinistros.DataContext = FuncionariosSingleton.Ponentes;
        }

        private void CheckBoxZone_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox chek = sender as CheckBox;
            minSeleccionados.Add(Convert.ToInt16(chek.Tag));
        }

        private void CheckBoxZone_Unchecked(object sender, RoutedEventArgs e)
        {
            CheckBox chek = sender as CheckBox;
            minSeleccionados.Remove(Convert.ToInt16(chek.Tag));
        }

        private void CbxTipoVoto_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            OtrosDatos dato = CbxTipoVoto.SelectedItem as OtrosDatos;

            if (dato.IdAuxiliar == 1)
            {
                CbxMinistros.Visibility = Visibility.Visible;
                LstMinistros.Visibility = Visibility.Collapsed;
            }
            else
            {
                CbxMinistros.Visibility = Visibility.Collapsed;
                LstMinistros.Visibility = Visibility.Visible;
            }
        }

        private void TxtDrag_PreviewDragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effects = DragDropEffects.Copy;
                var listbox = sender as TextBox;
                listbox.Background = new SolidColorBrush(Color.FromRgb(155, 155, 155));
            }
            else
            {
                e.Effects = DragDropEffects.None;
            }

            e.Handled = true;
        }

        private void TxtDrop_PreviewDrop(object sender, DragEventArgs e)
        {
            var listbox = sender as TextBox;
            string[] filenames = (string[])e.Data.GetData(DataFormats.FileDrop, true);

            foreach (string filename in filenames)
                listbox.Text = filename;
            //TxtProyFilePath.Text +=  File.ReadAllText(filename);

            listbox.Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));

            e.Handled = true;

        }

        private void TxtProvisional_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void BtnLoadProvisional_Click(object sender, RoutedEventArgs e)
        {
            TxtPublica.Text = DocumentConversion.GetFilePath();
        }

        private void BtnObservaciones_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void Numeric_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = StringUtilities.IsTextAllowed(e.Text);
        }

        private void BtnLoadCCPath_Click(object sender, RoutedEventArgs e)
        {
            TxtCertificada.Text = DocumentConversion.GetFilePath();
        }

        private void BtnLoadVPPath_Click(object sender, RoutedEventArgs e)
        {
            TxtPublica.Text = DocumentConversion.GetFilePath();
        }
    }
}
