using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using ControlDeTesisV4.Dao;
using ControlDeTesisV4.Models;
using ControlDeTesisV4.Turno;
using ControlDeTesisV4.UtilitiesFolder;
using DocumentMgmtApi;
using ModuloInterconexionCommonApi;

namespace ControlDeTesisV4.ProyectosSalasFolder
{
    /// <summary>
    /// Interaction logic for CapturaAprobacion.xaml
    /// </summary>
    public partial class CapturaAprobacion
    {
        private ProyectosTesis tesis;

        public CapturaAprobacion(ProyectosTesis tesis)
        {
            InitializeComponent();
            this.tesis = tesis;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.DataContext = tesis;

            if (!String.IsNullOrEmpty(tesis.ComparaTesis.TAprobada) && !String.IsNullOrWhiteSpace(tesis.ComparaTesis.TAprobada))
                this.LoadRichTextBoxContent(TxtVistaPrevia, tesis.ComparaTesis.TAprobada);
        }

        private void TxtYearTesis_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = StringUtilities.IsTextAllowed(e.Text);
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
            //TxtProyFilePath.Text +=  File.ReadAllText(filename);

            var listbox = sender as TextBox;
            listbox.Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));

            e.Handled = true;

            this.PreviewChanges();
        }

        private void TxtProyFilePath_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!File.Exists(TxtProyFilePath.Text))
            {
                MessageBox.Show("Verifica que la ruta del archivo del oficio sea correcta");
            }
        }

        private void BtnLoadFilePath_Click(object sender, RoutedEventArgs e)
        {
            TxtProyFilePath.Text = DocumentConversion.GetFilePath();
            this.PreviewChanges();
        }

        private void PreviewChanges()
        {
            string docFile = Path.GetTempFileName() + ".doc";
            System.IO.StreamWriter writer = new StreamWriter(docFile, false, System.Text.Encoding.Default);
            writer.WriteLine(tesis.ComparaTesis.TObservaciones);
            writer.Close();

            tesis.ComparaTesis.TAprobFilePathOrigen = TxtProyFilePath.Text;

            string xmlResult = Path.GetTempFileName() + ".xml";

            WordDocComparer comparer = new WordDocComparer(docFile, tesis.ComparaTesis.TAprobFilePathOrigen, xmlResult);
            comparer.Compare();

            TxtVistaPrevia.Document = DocumentComparer.LoadDocumentRevision(xmlResult, 2);
        }

        private void BtnGuardar_Click(object sender, RoutedEventArgs e)
        {
            int number;
            bool result = Int32.TryParse(tesis.NumTesis, out number);
            if (result)
            {
                tesis.NumTesisInt = Convert.ToInt32(tesis.NumTesis);
            }
            else
            {
                tesis.NumTesisInt = StringUtilities.RomanosADecimal(tesis.NumTesis);
            }

            tesis.ComparaTesis.TAprobada = DocumentComparer.GetRtfString(TxtVistaPrevia);
            TextRange range = new TextRange(TxtVistaPrevia.Document.ContentStart, TxtVistaPrevia.Document.ContentEnd);
            tesis.ComparaTesis.TAprobadaPlano = range.Text;
            tesis.EstadoTesis = 3;

            ProyectoTesisSalasModel model = new ProyectoTesisSalasModel();
            model.UpdateProyectoTesis(tesis);

            MessageBoxResult qResult = MessageBox.Show("¿Deseas enviar esta tesis al listado de tesis pendientes de turno?", "Atención:", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (MessageBoxResult.Yes == qResult)
            {
                tesis.EstadoTesis = 4;
                new AuxiliarModel().UpdateEstadoDocumento(tesis.IdTesis, tesis.EstadoTesis, "ProyectosTesis", "IdTesis", "EstadoTesis");

                TesisTurnadaPreview tesisTurnada = new TesisTurnadasModel().GetPreviewTesisTurnada(tesis.IdTesis);

                if(Constants.ListadoDeTesis != null)
                    Constants.ListadoDeTesis.Add(tesisTurnada);

                //Falta elimianrlo del listado de proyectos

            }

            this.Close();
        }

        private void BtnGuardarTurn_Click(object sender, RoutedEventArgs e)
        {
            BtnGuardar_Click(null, null);

            // this.Hide();
            int tipoDocto = (tesis.Tatj == 0) ? 1 : 2;

            TurnarWin turno = new TurnarWin(tesis, tipoDocto);
            turno.ShowDialog();
            this.Close();
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