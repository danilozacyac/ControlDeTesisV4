using System;
using System.Collections.ObjectModel;
using System.IO;
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
    /// Interaction logic for CapturaEjecutoria.xaml
    /// </summary>
    public partial class CapturaEjecutoria
    {
        private double originalWindowHeight;
        private Ejecutorias ejecutoria;
        private readonly bool isUpdating;
        private int estadoEjecutoria;

        public CapturaEjecutoria()
        {
            InitializeComponent();
            this.ejecutoria = new Ejecutorias();
            this.ejecutoria.Precedente = new PrecedentesTesis();
            estadoEjecutoria = 1;
        }

        public CapturaEjecutoria(Ejecutorias ejecutoria)
        {
            InitializeComponent();
            this.ejecutoria = ejecutoria;
            isUpdating = true;
        }

        private void RadWindow_Loaded(object sender, RoutedEventArgs e)
        {
            this.DataContext = ejecutoria;

            originalWindowHeight = this.Height;

            CbxPonentes.DataContext = FuncionariosSingleton.Ponentes;
            CbxTipoAsunto.DataContext = OtrosDatosSingleton.TipoAsuntos;
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
            TextBox txt = sender as TextBox;

            string[] filenames = (string[])e.Data.GetData(DataFormats.FileDrop, true);

            foreach (string filename in filenames)
                txt.Text = filename;

            var listbox = sender as TextBox;
            listbox.Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));

            e.Handled = true;
        }



        private void BtnGuardar_Click(object sender, RoutedEventArgs e)
        {
            //if (RadObsSi.IsChecked == true && (ejecutoria.Observaciones.Count == 0 || ejecutoria.Observaciones == null))
            //{
            //    MessageBox.Show("Debe agregar al menos una observación o seleccionar la casilla de No");
            //    return;
            //}
            ejecutoria.Precedente.TipoAsunto = Convert.ToInt32(CbxTipoAsunto.SelectedValue);
            ejecutoria.Precedente.IdPonente = Convert.ToInt32(CbxPonentes.SelectedValue);
            ejecutoria.FRecepcionInt = Convert.ToInt32(StringUtilities.DateToInt(ejecutoria.FRecepcion));
            ejecutoria.FEnvioObsInt = Convert.ToInt32(StringUtilities.DateToInt(ejecutoria.FEnvioObs));
            ejecutoria.FDevolucionInt = Convert.ToInt32(StringUtilities.DateToInt(ejecutoria.FDevolucion));

            new EjecutoriasModel().SetNewProyectoEjecutoria(ejecutoria);

            this.Close();
        }

        private void BtnObservaciones_Click(object sender, RoutedEventArgs e)
        {
            if (ejecutoria.Observaciones == null)
                ejecutoria.Observaciones = new ObservableCollection<Observaciones>();

            int idEjecutoria = (isUpdating) ? ejecutoria.IdEjecutoria : -1;
            CapturaObservaciones observ = new CapturaObservaciones(ejecutoria.Observaciones,ejecutoria.ObsFilePathOrigen,idEjecutoria);
            observ.ShowDialog();
        }

        private void RadObsSi_Checked(object sender, RoutedEventArgs e)
        {
            ObsPanel.Visibility = Visibility.Visible;
        }

        private void RadObsNo_Checked(object sender, RoutedEventArgs e)
        {
            ObsPanel.Visibility = Visibility.Collapsed;
            TxtProvisional.Text = String.Empty;
            DtpEnvioObserv.SelectedDate = null;
            DtpFDevolucion.SelectedDate = null;
            RadCompleta.IsChecked = false;
            RadParcial.IsChecked = false;
            RadNoAcept.IsChecked = false;
            //this.Height = originalWindowHeight;

            //Eliminar todas las observaciones que esten asociadas, enviar mensaje de confimarción
        }

        private void BtnLoadProvisional_Click(object sender, RoutedEventArgs e)
        {
            TxtProvisional.Text = DocumentConversion.GetFilePath();
        }

        private void TxtProvisional_TextChanged(object sender, TextChangedEventArgs e)
        {
          
        }

    }
}
