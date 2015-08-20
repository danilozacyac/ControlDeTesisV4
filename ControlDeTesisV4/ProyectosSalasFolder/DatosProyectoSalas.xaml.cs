using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using ControlDeTesisV4.Dao;
using ControlDeTesisV4.Models;
using ControlDeTesisV4.Singletons;
using DocumentMgmtApi;
using Microsoft.Win32;
using ScjnUtilities;

namespace ControlDeTesisV4.ProyectosSalasFolder
{
    /// <summary>
    /// Interaction logic for DatosProyectoSalas.xaml
    /// </summary>
    public partial class DatosProyectoSalas
    {
        private ObservableCollection<ProyectosTesis> proyectosSalas;
        ProyectosTesis proyecto;
        private readonly int idInstancia;
        private readonly bool isUpdating;

        public DatosProyectoSalas(ObservableCollection<ProyectosTesis> proyectosSalas, int idInstancia)
        {
            InitializeComponent();

            if (proyectosSalas == null)
                proyectosSalas = new ObservableCollection<ProyectosTesis>();

            this.proyectosSalas = proyectosSalas;
            this.idInstancia = idInstancia;

        }

        public DatosProyectoSalas(ProyectosTesis proyecto)
        {
            InitializeComponent();
            this.proyecto = proyecto;
            this.isUpdating = true;
            BtnOficio.Visibility = Visibility.Visible;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            if (proyecto == null)
            {
                proyecto = new ProyectosTesis();
                proyecto.Precedente = new PrecedentesTesis();
                proyecto.ComparaTesis = new TesisCompara();
                proyecto.IdInstancia = idInstancia;
            }

            this.DataContext = proyecto;

            CbxAbogado.DataContext = FuncionariosSingleton.AbogResp;
            CbxPonentes.DataContext = FuncionariosSingleton.Ponentes;
            CbxTipoJuris.DataContext = OtrosDatosSingleton.TipoJurisprudencias;
            CbxTipoAsunto.DataContext = OtrosDatosSingleton.TipoAsuntos;

            if (isUpdating)
            {
                LoadOnUpdating();
                BtnGuardar.Content = "Guardar";
            }
            else if (proyectosSalas.Count > 0)
                this.SetDatosIguales();

        }

        private void SetDatosIguales()
        {
            if (proyectosSalas.Count > 0)
            {
                ProyectosTesis proy1 = proyectosSalas[0];
                proyecto.FEnvio = proy1.FEnvio;
                CbxAbogado.SelectedValue = proy1.IdAbogadoResponsable;
                proyecto.OficioEnvio = proy1.OficioEnvio;
                proyecto.OficioEnvioPathOrigen = proy1.OficioEnvioPathOrigen;

                if (proy1.Tatj == 0)
                    RadAislada.IsChecked = true;
                else
                {
                    Radjuris.IsChecked = true;
                    CbxTipoJuris.SelectedValue = proy1.IdTipoJuris;
                }

                proyecto.NumPaginas = proy1.NumPaginas;
                CbxTipoAsunto.SelectedValue = proy1.Precedente.TipoAsunto;
                proyecto.Precedente.NumAsunto = proy1.Precedente.NumAsunto;
                proyecto.Precedente.YearAsunto = proy1.Precedente.YearAsunto;
                proyecto.Precedente.FResolucion = proy1.Precedente.FResolucion;
                CbxPonentes.SelectedValue = proy1.Precedente.IdPonente;
                proyecto.Precedente.Promovente = proy1.Precedente.Promovente;

            }
        }

        private void RadAislada_Checked(object sender, RoutedEventArgs e)
        {
            LblJuris.Visibility = System.Windows.Visibility.Hidden;
            CbxTipoJuris.Visibility = System.Windows.Visibility.Hidden;

        }

        private void Radjuris_Checked(object sender, RoutedEventArgs e)
        {
            LblJuris.Visibility = System.Windows.Visibility.Visible;
            CbxTipoJuris.Visibility = System.Windows.Visibility.Visible;
        }



        private void Numeric_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = StringUtilities.IsTextAllowed(e.Text);
        }

        private void BtnGuardar_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(proyecto.ComparaTesis.ToFilePathOrigen) || String.IsNullOrWhiteSpace(proyecto.ComparaTesis.ToFilePathOrigen))
            {
                MessageBox.Show("Agrega el archivo del proyecto");
                return;
            }
            //else if (!File.Exists(proyecto.ComparaTesis.ToFilePathOrigen))
            //{
            //    MessageBox.Show("La ruta del archivo que ingreso es incorrecta, favor de verificar");
            //    return;
            //}
            if (String.IsNullOrEmpty(TxtRubro.Text) || String.IsNullOrWhiteSpace(TxtRubro.Text))
            {
                MessageBox.Show("Ingresa el rubro de la tesis");
                return;
            }

            proyecto.Tatj = (RadAislada.IsChecked == true) ? 0 : 1;

            if (proyecto.Tatj == 1 && CbxTipoJuris.SelectedIndex == -1)
            {
                MessageBox.Show("Debes seleccionar el tipo de Jurisprudencia");
                return;
            }

            proyecto.Rubro = TxtRubro.Text;
            proyecto.IdTipoJuris = (RadAislada.IsChecked == true) ? 0 : ((OtrosDatos)CbxTipoJuris.SelectedItem).IdDato;
            proyecto.ComparaTesis.TOrigenAlfab = StringUtilities.PrepareToAlphabeticalOrder(proyecto.Rubro.ToUpper());


            proyecto.Precedente.TipoAsunto = ((OtrosDatos)CbxTipoAsunto.SelectedItem).IdDato;
            proyecto.Precedente.IdPonente = ((Funcionarios)CbxPonentes.SelectedItem).IdFuncionario;
            proyecto.IdAbogadoResponsable = ((Funcionarios)CbxAbogado.SelectedItem).IdFuncionario;

            TextRange range = new TextRange(TxtVistaPrevia.Document.ContentStart, TxtVistaPrevia.Document.ContentEnd);
            proyecto.ComparaTesis.TOPlano = range.Text;
            proyecto.ComparaTesis.TextoOriginal = DocumentComparer.GetRtfString(TxtVistaPrevia);

            if (!isUpdating)
                proyectosSalas.Add(proyecto);
            else
            {
                new ProyectoTesisSalasModel().UpdateProyectoTesis(proyecto);
            }

            this.Close();

        }

        private void TxtProyFilePath_PreviewDragOver(object sender, DragEventArgs e)
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

        private void TxtProyFilePath_PreviewDrop(object sender, DragEventArgs e)
        {
            string[] filenames = (string[])e.Data.GetData(DataFormats.FileDrop, true);

            foreach (string filename in filenames)
                TxtProyFilePath.Text = filename;
            //TxtProyFilePath.Text +=  File.ReadAllText(filename);

            var listbox = sender as TextBox;
            listbox.Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));

            e.Handled = true;
        }

        private void TxtProyFilePath_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (File.Exists(TxtProyFilePath.Text))
            {
                this.Cursor = Cursors.Wait;
                TxtVistaPrevia.Document = DocumentComparer.LoadDocumentContent(TxtProyFilePath.Text);

                if (TxtVistaPrevia.Document == null)
                {
                    TxtProyFilePath.Text = String.Empty;
                }
                else
                {
                    proyecto.ComparaTesis.ToFilePathOrigen = TxtProyFilePath.Text;
                }

                this.Cursor = Cursors.Arrow;
            }
        }

        private void BtnLoadPath_Click(object sender, RoutedEventArgs e)
        {
            TxtProyFilePath.Text = FilesUtilities.GetWordFilePath("Selecciona el archivo del proyecto", true);
        }

        private void LoadOnUpdating()
        {
            CbxAbogado.SelectedValue = proyecto.IdAbogadoResponsable;
            CbxTipoAsunto.SelectedValue = proyecto.Precedente.TipoAsunto;
            CbxPonentes.SelectedValue = proyecto.Precedente.IdPonente;

            if (proyecto.Tatj == 0)
            {
                RadAislada.IsChecked = true;
            }
            else
            {
                Radjuris.IsChecked = true;
                CbxTipoJuris.SelectedValue = proyecto.IdTipoJuris;
            }

            TxtProyFilePath_TextChanged(null, null);

        }

        private void BtnOficio_Click(object sender, RoutedEventArgs e)
        {
            DatosLlegadaSalas datos = new DatosLlegadaSalas(proyecto);
            datos.ShowDialog();
        }
    }
}
