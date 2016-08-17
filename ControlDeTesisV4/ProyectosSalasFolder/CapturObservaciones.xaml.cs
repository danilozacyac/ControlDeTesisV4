using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using ControlDeTesisV4.Dao;
using ControlDeTesisV4.Models;
using DocumentMgmtApi;
using System.Configuration;

namespace ControlDeTesisV4.ProyectosSalasFolder
{
    /// <summary>
    /// Interaction logic for CapturObservaciones.xaml
    /// </summary>
    public partial class CapturObservaciones
    {
        ProyectosTesis tesis;

        public CapturObservaciones(ProyectosTesis tesis)
        {
            InitializeComponent();
            this.tesis = tesis;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.DataContext = tesis;

            if (!String.IsNullOrEmpty(tesis.ComparaTesis.TObservaciones) && !String.IsNullOrWhiteSpace(tesis.ComparaTesis.TObservaciones))
                this.LoadRichTextBoxContent(TxtVistaPrevia, tesis.ComparaTesis.TObservaciones);
        }

        //private void TextBoxPath_PreviewDragOver(object sender, DragEventArgs e)
        //{
        //    if (e.Data.GetDataPresent(DataFormats.FileDrop))
        //    {
        //        e.Effects = DragDropEffects.Copy;
        //        var listbox = sender as TextBox;
        //        listbox.Background = new SolidColorBrush(Color.FromRgb(155, 155, 155));
        //    }
        //    else
        //    {
        //        e.Effects = DragDropEffects.None;
        //    }

        //    e.Handled = true;
        //}

        //private void TextBoxPath_PreviewDrop(object sender, DragEventArgs e)
        //{
        //    var listbox = sender as TextBox;
        //    string[] filenames = (string[])e.Data.GetData(DataFormats.FileDrop, true);

        //    foreach (string filename in filenames)
        //        listbox.Text = filename;
        //    //TxtProyFilePath.Text +=  File.ReadAllText(filename);

        //    listbox.Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));

        //    e.Handled = true;

        //    this.PreviewChanges(sender);
        //}

        private void TxtOficioPathOrigen_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!String.IsNullOrEmpty(tesis.ComparaTesis.TObservaciones) && !String.IsNullOrWhiteSpace(tesis.ComparaTesis.TObservaciones))
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
        }

        private void BtnLoadPath_Click(object sender, RoutedEventArgs e)
        {
            TxtOficioPathOrigen.Text = DocumentConversion.GetFilePath();
        }

        private void TxtProyFilePath_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!File.Exists(TxtProyFilePath.Text))
            {
                MessageBox.Show("Verifica que la ruta del archivo del oficio sea correcta");
            }

        }

        private void BtnGuardar_Click(object sender, RoutedEventArgs e)
        {
            TextRange range = new TextRange(TxtVistaPrevia.Document.ContentStart, TxtVistaPrevia.Document.ContentEnd);
            tesis.ComparaTesis.TObservacionesPlano = range.Text;
            tesis.ComparaTesis.TObservaciones = DocumentComparer.GetRtfString(TxtVistaPrevia);
            tesis.EstadoTesis = 2;
            ProyectoTesisSalasModel model = new ProyectoTesisSalasModel();
            //tesis.ComparaTesis.TObsFilePathOrigen = tesis.ComparaTesis.TObsFilePathOrigen.Replace(ConfigurationManager.AppSettings["ArchivosSoporte"], "");

            int indexOf = tesis.ComparaTesis.ToFilePathOrigen.ToUpper().IndexOf(@"PLENO_Y_SALAS");
            string ruta = tesis.ComparaTesis.ToFilePathOrigen.Substring(indexOf);

            tesis.ComparaTesis.TObsFilePathOrigen = ruta;

            tesis.OficioEnvioPathOrigen = tesis.OficioEnvioPathOrigen.Replace(ConfigurationManager.AppSettings["ArchivosSoporte"], "");
            model.UpdateProyectoTesis(tesis);

            this.Close();
        }

        private void BtnLoadFilePath_Click(object sender, RoutedEventArgs e)
        {
            TxtProyFilePath.Text = DocumentConversion.GetFilePath();
            this.PreviewChanges();
        }

        private void PreviewChanges()
        {
            tesis.ComparaTesis.TObsFilePathOrigen = TxtProyFilePath.Text;
            string resultingXml = DocumentComparer.CompareDocuments(tesis.ComparaTesis.ToFilePathOrigen, tesis.ComparaTesis.TObsFilePathOrigen);
            TxtVistaPrevia.Document = DocumentComparer.LoadDocumentRevision(resultingXml, 1);
        }

        private void PreviewChanges(object sender)
        {
            TextBox box = sender as TextBox;

            if (box.Name.Equals("TxtProyFilePath"))
            {
                tesis.ComparaTesis.TObsFilePathOrigen = TxtProyFilePath.Text;
                string resultingXml = DocumentComparer.CompareDocuments(tesis.ComparaTesis.ToFilePathOrigen, tesis.ComparaTesis.TObsFilePathOrigen);
                TxtVistaPrevia.Document = DocumentComparer.LoadDocumentRevision(resultingXml, 1);
            }
        }



        private void LoadRichTextBoxContent(RichTextBox rtb, string contentString)
        {
            var doc = rtb.Document;
            var range = new TextRange(doc.ContentStart, doc.ContentEnd);
            var ms = new MemoryStream();
            var sw = new StreamWriter(ms);
            sw.Write(contentString);
            sw.Flush();
            ms.Seek(0, SeekOrigin.Begin);
            range.Load(ms, DataFormats.Rtf);
        }
    }
}
