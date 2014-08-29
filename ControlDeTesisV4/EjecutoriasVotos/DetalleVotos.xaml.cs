using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using ControlDeTesisV4.Dao;
using ControlDeTesisV4.Models;
using ControlDeTesisV4.Singletons;
using DocumentMgmtApi;
using ModuloInterconexionCommonApi;

namespace ControlDeTesisV4.EjecutoriasVotos
{
    /// <summary>
    /// Interaction logic for DetalleVotos.xaml
    /// </summary>
    public partial class DetalleVotos
    {
        private Votos voto;
        private readonly bool isUpdating;
        ObservableCollection<Votos> listaVotos;


        public DetalleVotos(ObservableCollection<Votos> listaVotos)
        {
            InitializeComponent();
            this.listaVotos = listaVotos;
            this.voto = new Votos();
            this.voto.Ministros = new ObservableCollection<int>();
            this.isUpdating = false;
        }

        public DetalleVotos(Votos voto, bool isUpdating)
        {
            InitializeComponent();
            this.voto = voto;
            this.isUpdating = isUpdating;
        }

        private void RadWindow_Loaded(object sender, RoutedEventArgs e)
        {
            CbxMinistros.DataContext = FuncionariosSingleton.Ponentes;
            LstMinistros.DataContext = FuncionariosSingleton.Ponentes;
            CbxTipoVoto.DataContext = OtrosDatosSingleton.TipoVotos;

            this.DataContext = voto;
        }

        private void CheckBoxZone_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox chek = sender as CheckBox;
            voto.Ministros.Add(Convert.ToInt16(chek.Tag));
        }

        private void CheckBoxZone_Unchecked(object sender, RoutedEventArgs e)
        {
            CheckBox chek = sender as CheckBox;
            voto.Ministros.Remove(Convert.ToInt16(chek.Tag));
        }

        OtrosDatos tipoVotoSeleccionado;
        private void CbxTipoVoto_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            voto.Ministros = new ObservableCollection<int>();
            tipoVotoSeleccionado = CbxTipoVoto.SelectedItem as OtrosDatos;

            if (tipoVotoSeleccionado.IdAuxiliar == 1)
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
            TxtProvisional.Text = DocumentConversion.GetFilePath();
        }

        private void BtnObservaciones_Click(object sender, RoutedEventArgs e)
        {
            if(voto.Observaciones ==  null)
                voto.Observaciones = new ObservableCollection<Observaciones>();

            CapturaObservaciones obs = new CapturaObservaciones(voto.Observaciones, voto.ObsFilePathOrigen, voto.IdVoto, 4, false);
            obs.ShowDialog();
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

        private void BtnGuardar_Click(object sender, RoutedEventArgs e)
        {
            if (voto.Ministros.Count == 0)
            {
                MessageBox.Show("Debes seleccionar el tipo de voto que se registrará y al menos un sustentante");
                return;
            }

            if (voto.FEnvioObs != null && voto.FDevolucion != null)
            {
                if (voto.FDevolucion < voto.FEnvioObs)
                {
                    MessageBox.Show("La fecha de devolución de las devoluciones debe ser posterior a la fecha de envio");
                    return;
                }
            }

            voto.IdtipoVoto = Convert.ToInt32(CbxTipoVoto.SelectedValue);

            if (!isUpdating)
            {
                listaVotos.Add(voto);
                this.Close();
            }
            else if (isUpdating && voto.IdVoto == 0)
            {
                this.Close();
            }
            else if (isUpdating && voto.IdVoto != 0)
            {
                new VotosModel().UpdateProyectoVoto(voto);
                this.Close();
            }



        }

        private void CbxMinistros_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            voto.Ministros.Add(Convert.ToInt32(CbxMinistros.SelectedValue));// = new ObservableCollection<int>();
        }
    }
}
