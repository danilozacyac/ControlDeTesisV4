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

        /// <summary>
        /// La ejecutoria es nueva, ya sea que llegue para observaciones o que 
        /// llegue directamente para publicar
        /// </summary>
        public CapturaEjecutoria(int forObservaciones)
        {
            InitializeComponent();
            this.ejecutoria = new Ejecutorias();
            this.ejecutoria.Precedente = new PrecedentesTesis();
            this.ejecutoria.ForObservaciones = forObservaciones;

            if (forObservaciones == 0)
                estadoEjecutoria = 4;
            else
                estadoEjecutoria = 1;
        }

        /// <summary>
        /// Actualización de la ejecutoria, actualización de los detalles de la misma, también funciona
        /// para las ejecutorias que serán asociadas a una tesis
        /// </summary>
        /// <param name="ejecutoria"></param>
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

            if (ejecutoria.ForObservaciones == 1)
                ObsPanel.Visibility = Visibility.Visible;
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
            ejecutoria.EstadoEjecutoria = estadoEjecutoria;
            ejecutoria.Precedente.TipoAsunto = Convert.ToInt32(CbxTipoAsunto.SelectedValue);
            ejecutoria.Precedente.IdPonente = Convert.ToInt32(CbxPonentes.SelectedValue);
            ejecutoria.CcFilePathConten = Utilities.CopyToLocalResource(ejecutoria.CcFilePathOrigen);
            ejecutoria.VpFilePathConten = Utilities.CopyToLocalResource(ejecutoria.VpFilePathOrigen);


            if (new EjecutoriasModel().SetNewProyectoEjecutoria(ejecutoria))
            {
                TurnarWin turnar = new TurnarWin(ejecutoria);
                turnar.ShowDialog();

                this.Close();
            }
            else
            {
                MessageBox.Show("Revisa los datos de la ejecutoria");
            }
        }

        private void BtnObservaciones_Click(object sender, RoutedEventArgs e)
        {
            if (ejecutoria.Observaciones == null)
                ejecutoria.Observaciones = new ObservableCollection<Observaciones>();

            int idEjecutoria = (isUpdating) ? ejecutoria.IdEjecutoria : -1;
            CapturaObservaciones observ = new CapturaObservaciones(ejecutoria.Observaciones,ejecutoria.ObsFilePathOrigen,idEjecutoria,3,isUpdating);
            observ.ShowDialog();
        }

        private void RadObsSi_Checked(object sender, RoutedEventArgs e)
        {
            ObsPanel.Visibility = Visibility.Visible;
        }

        private void RadObsNo_Checked(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Al seleccionar esta opción eliminará todas las observaciones asociadas a este documento. ¿Desea continuar?",
                "ATENCIÓN:", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                ObsPanel.Visibility = Visibility.Collapsed;
                TxtProvisional.Text = String.Empty;
                DtpEnvioObserv.SelectedDate = null;
                DtpFDevolucion.SelectedDate = null;
                RadCompleta.IsChecked = false;
                RadParcial.IsChecked = false;
                RadNoAcept.IsChecked = false;
                ejecutoria.Observaciones = new ObservableCollection<Observaciones>();

                //Eliminar todas las observaciones que esten asociadas, enviar mensaje de confimarción

                //this.Height = originalWindowHeight;
            }
        }

        private void BtnLoadProvisional_Click(object sender, RoutedEventArgs e)
        {
            TxtProvisional.Text = DocumentConversion.GetFilePath();
        }

        private void TxtProvisional_TextChanged(object sender, TextChangedEventArgs e)
        {
          
        }

        private void ChkVotos_Checked(object sender, RoutedEventArgs e)
        {
            BtnVotos.Visibility = Visibility.Visible;
        }

        private void ChkVotos_Unchecked(object sender, RoutedEventArgs e)
        {

            BtnVotos.Visibility = Visibility.Collapsed;
        }

    }
}
