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
using ControlDeTesisV4.Turno;
using ControlDeTesisV4.UtilitiesFolder;
using DocumentMgmtApi;
using ModuloInterconexionCommonApi;

namespace ControlDeTesisV4.EjecutoriasVotos
{
    /// <summary>
    /// Interaction logic for VotoSencillo.xaml
    /// </summary>
    public partial class VotoSencillo
    {
        private Votos voto;
        private readonly bool isUpdating;
        ObservableCollection<Votos> listaVotos;

        public VotoSencillo()
        {
            InitializeComponent();
            this.voto = new Votos();
            this.isUpdating = false;
        }

        public VotoSencillo(Votos voto, bool isUpdating)
        {
            InitializeComponent();
            this.voto = voto;
            this.isUpdating = isUpdating;
        }

        private void RadWindow_Loaded(object sender, RoutedEventArgs e)
        {
            if (voto.Precedente == null)
                voto.Precedente = new PrecedentesTesis();

            this.DataContext = voto;
            CbxMinistros.DataContext = FuncionariosSingleton.Ponentes;
            CbxPonentes.DataContext = FuncionariosSingleton.Ponentes;
            CbxTipoAsunto.DataContext = OtrosDatosSingleton.TipoAsuntos;
            CbxTipoVoto.DataContext = OtrosDatosSingleton.TipoVotos;
            LstMinistros.DataContext = FuncionariosSingleton.Ponentes;
        }

        private void Numeric_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = StringUtilities.IsTextAllowed(e.Text);
        }

        private void CbxMinistros_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(voto.Ministros == null)
                voto.Ministros = new ObservableCollection<int>();

            voto.Ministros.Add(Convert.ToInt32(CbxMinistros.SelectedValue));
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
            

            voto.Precedente.TipoAsunto = Convert.ToInt32(CbxTipoAsunto.SelectedValue);
            voto.Precedente.IdPonente = Convert.ToInt32(CbxPonentes.SelectedValue);
            voto.IdtipoVoto = Convert.ToInt32(CbxTipoVoto.SelectedValue);

            if (tipoVotoSeleccionado.IdAuxiliar == 1)
            {
                voto.Ministros = new ObservableCollection<int>();
                voto.Ministros.Add(Convert.ToInt32(CbxMinistros.SelectedValue));
            }

            voto.CcFilePathOrigen = TxtCertificada.Text;
            voto.VpFilePathOrigen = TxtPublica.Text;

            if (new VotosModel().SetNewProyectoVoto(voto, voto.Precedente))
            {
                TurnarWin turnar = new TurnarWin(voto);
                turnar.ShowDialog();

                Constants.ListadoDeVotos.Add(voto);
                this.Close();
            }
        }

        private void BtnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
