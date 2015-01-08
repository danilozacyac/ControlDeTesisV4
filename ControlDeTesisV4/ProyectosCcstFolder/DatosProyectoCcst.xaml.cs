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


namespace ControlDeTesisV4.ProyectosCcstFolder
{
    /// <summary>
    /// Interaction logic for DatosProyectoCcst.xaml
    /// </summary>
    public partial class DatosProyectoCcst
    {
        private ObservableCollection<ProyectosTesis> listaProyectos;
        private readonly int idInstancia;
        ProyectosTesis tesis;
        private readonly bool isUpdating;

        public DatosProyectoCcst(ObservableCollection<ProyectosTesis> listaProyectos, int idInstancia)
        {
            InitializeComponent();

            this.listaProyectos = listaProyectos;
            this.idInstancia = idInstancia;
        }

        public DatosProyectoCcst(ProyectosTesis tesis)
        {
            InitializeComponent();
            this.tesis = tesis;
            this.isUpdating = true;
            BtnLlegada.Visibility = Visibility.Visible;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (!isUpdating)
            {
                tesis = new ProyectosTesis();
                tesis.Precedente = new PrecedentesTesis();
                tesis.ComparaTesis = new TesisCompara();
                tesis.IdInstancia = idInstancia;
            }

            this.DataContext = tesis;

            CbxPonentes.DataContext = FuncionariosSingleton.Ponentes;
            CbxTipoAsunto.DataContext = OtrosDatosSingleton.TipoAsuntos;
            CbxTipoJuris.DataContext = OtrosDatosSingleton.TipoJurisprudencias;
            CbxAbogados.DataContext = FuncionariosSingleton.AbogResp;

            if (isUpdating)
                LoadOnUpdating();
            else if (listaProyectos.Count > 0)
                this.SetDatosIguales();

        }

        private void SetDatosIguales()
        {
            if (listaProyectos.Count > 0)
            {
                ProyectosTesis proy1 = listaProyectos[0];
                tesis.FEnvio = proy1.FEnvio;
                CbxAbogados.SelectedValue = proy1.IdAbogadoResponsable;
                tesis.OficioEnvio = proy1.OficioEnvio;
                tesis.OficioEnvioPathOrigen = proy1.OficioEnvioPathOrigen;

                if (proy1.Tatj == 0)
                    RadAislada.IsChecked = true;
                else
                {
                    Radjuris.IsChecked = true;
                    CbxTipoJuris.SelectedValue = proy1.IdTipoJuris;
                }

                tesis.NumPaginas = proy1.NumPaginas;
                CbxTipoAsunto.SelectedValue = proy1.Precedente.TipoAsunto;
                tesis.Precedente.NumAsunto = proy1.Precedente.NumAsunto;
                tesis.Precedente.YearAsunto = proy1.Precedente.YearAsunto;
                tesis.Precedente.FResolucion = proy1.Precedente.FResolucion;
                CbxPonentes.SelectedValue = proy1.Precedente.IdPonente;
                tesis.Precedente.Promovente = proy1.Precedente.Promovente;

            }
        }


        private void NumericTxt_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = StringUtilities.IsTextAllowed(e.Text);
        }

        private void TextBoxPath_PreviewDragOver(object sender, DragEventArgs e)
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

        private void TextBoxPath_PreviewDrop(object sender, DragEventArgs e)
        {
            var listbox = sender as TextBox;
            string[] filenames = (string[])e.Data.GetData(DataFormats.FileDrop, true);

            foreach (string filename in filenames)
                listbox.Text = filename;
            //TxtProyFilePath.Text +=  File.ReadAllText(filename);

            listbox.Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));

            e.Handled = true;
        }

        private void Radjuris_Checked(object sender, RoutedEventArgs e)
        {
            LblJuris.Visibility = Visibility.Visible;
            CbxTipoJuris.Visibility = Visibility.Visible;
        }

        private void RadAislada_Checked(object sender, RoutedEventArgs e)
        {
            LblJuris.Visibility = Visibility.Hidden;
            CbxTipoJuris.Visibility = Visibility.Hidden;
        }

        private void BtnGuardar_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(tesis.ComparaTesis.ToFilePathOrigen) || String.IsNullOrWhiteSpace(tesis.ComparaTesis.ToFilePathOrigen))
            {
                MessageBox.Show("Agrega el archivo del proyecto");
                return;
            }
            else if (!File.Exists(tesis.ComparaTesis.ToFilePathOrigen))
            {
                MessageBox.Show("La ruta del archivo que ingreso es incorrecta, favor de verificar");
                return;
            }

            if (CbxPonentes.SelectedItem == null || CbxTipoAsunto.SelectedItem == null)
            {
                MessageBox.Show("Seleccione el tipo de asunto y/o ponente");
                return;
            }

            tesis.Tatj = (RadAislada.IsChecked == true) ? 0 : 1;
            tesis.IdTipoJuris = (RadAislada.IsChecked == true) ? 0 : ((OtrosDatos)CbxTipoJuris.SelectedItem).IdDato;

            tesis.Precedente.TipoAsunto = ((OtrosDatos)CbxTipoAsunto.SelectedItem).IdDato;
            tesis.Precedente.IdPonente = ((Funcionarios)CbxPonentes.SelectedItem).IdFuncionario;
            tesis.IdAbogadoResponsable = ((Funcionarios)CbxAbogados.SelectedItem).IdFuncionario;

            TextRange range = new TextRange(TxtVistaPrevia.Document.ContentStart, TxtVistaPrevia.Document.ContentEnd);
            tesis.ComparaTesis.TOPlano = range.Text;
            tesis.ComparaTesis.TextoOriginal = DocumentComparer.GetRtfString(TxtVistaPrevia);
            tesis.ComparaTesis.TOrigenAlfab = StringUtilities.PrepareToAlphabeticalOrder(tesis.Rubro.ToUpper());

            if(isUpdating)
            new ProyectoTesisCcstModel().UpdateProyectoTesis(tesis);
            else
            listaProyectos.Add(tesis);
            this.Close();
        }

        private void TxtProyFilePath_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (File.Exists(TxtProyFilePath.Text))
            {
                this.Cursor = Cursors.Wait;
                TxtVistaPrevia.Document = DocumentComparer.LoadDocumentContent(TxtProyFilePath.Text);
                tesis.ComparaTesis.ToFilePathOrigen = TxtProyFilePath.Text;
                this.Cursor = Cursors.Arrow;
            }

        }

        private void TxtOficioPathOrigen_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (File.Exists(TxtOficioPathOrigen.Text))
            {
                BtnViewDoc.IsEnabled = true;
                tesis.OficioEnvioPathOrigen = TxtOficioPathOrigen.Text;
            }
            else
            {
                BtnViewDoc.IsEnabled = false;
                MessageBox.Show("Verifica que la ruta del archivo del oficio sea correcta");
            }
        }

        private String GetFilePath()
        {
            OpenFileDialog dialog = new OpenFileDialog();

            dialog.Filter = "Office Documents|*.doc;*.docx| RichTextFiles |*.rtf";

            dialog.InitialDirectory = @"C:\Users\" + Environment.UserName + @"\Documents";
            dialog.Title = "Selecciona el archivo del proyecto";
            dialog.ShowDialog();

            return dialog.FileName;
        }

        private void BtnLoadFilePath_Click(object sender, RoutedEventArgs e)
        {
            TxtProyFilePath.Text = this.GetFilePath();
        }

        private void BtnLoadPath_Click(object sender, RoutedEventArgs e)
        {
            TxtOficioPathOrigen.Text = this.GetFilePath();
        }

        private void LoadOnUpdating()
        {
            CbxAbogados.SelectedValue = tesis.IdAbogadoResponsable;
            CbxTipoAsunto.SelectedValue = tesis.Precedente.TipoAsunto;
            CbxPonentes.SelectedValue = tesis.Precedente.IdPonente;

            if (tesis.Tatj == 0)
            {
                RadAislada.IsChecked = true;
            }
            else
            {
                Radjuris.IsChecked = true;
                CbxTipoJuris.SelectedValue = tesis.IdTipoJuris;
            }

        }

        private void BtnLlegada_Click(object sender, RoutedEventArgs e)
        {
            DatosLlegada llegada = new DatosLlegada(tesis);
            llegada.ShowDialog();
        }
    }
}
