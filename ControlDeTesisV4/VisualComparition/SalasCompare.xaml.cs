using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using ControlDeTesisV4.Dao;
using ControlDeTesisV4.Models;

namespace ControlDeTesisV4.VisualComparition
{
    /// <summary>
    /// Lógica de interacción para SalasCompare.xaml
    /// </summary>
    public partial class SalasCompare : Window
    {
        ProyectosTesis tesis;

        public SalasCompare(ProyectosTesis tesis)
        {
            InitializeComponent();
            this.tesis = tesis;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.LoadRichTextBoxContent(OriginalText, tesis.ComparaTesis.TextoOriginal);
            this.LoadRichTextBoxContent(ObservText, tesis.ComparaTesis.TObservaciones);
            this.LoadRichTextBoxContent(AprobaText, tesis.ComparaTesis.TAprobada);
        }

        private void LoadRichTextBoxContent(RichTextBox rtb, string contentString)
        {
            try
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
            catch (ArgumentException) { }
        }

        private void SlFontSize_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            double sizeValue = SlFontSize.Value;

            var range = new TextRange(OriginalText.Document.ContentStart, OriginalText.Document.ContentEnd);
            range.ApplyPropertyValue(TextElement.FontSizeProperty, sizeValue);

            range = new TextRange(ObservText.Document.ContentStart, ObservText.Document.ContentEnd);
            range.ApplyPropertyValue(TextElement.FontSizeProperty, sizeValue);

            range = new TextRange(AprobaText.Document.ContentStart, AprobaText.Document.ContentEnd);
            range.ApplyPropertyValue(TextElement.FontSizeProperty, sizeValue);
        }

        private void BtnGuardar_Click(object sender, RoutedEventArgs e)
        {
            TextRange range = new TextRange(OriginalText.Document.ContentStart, OriginalText.Document.ContentEnd);
            tesis.ComparaTesis.TOPlano = range.Text;

            range = new TextRange(ObservText.Document.ContentStart, ObservText.Document.ContentEnd);
            tesis.ComparaTesis.TObservacionesPlano = range.Text;

            range = new TextRange(AprobaText.Document.ContentStart, AprobaText.Document.ContentEnd);
            tesis.ComparaTesis.TAprobadaPlano = range.Text;

            tesis.ComparaTesis.TextoOriginal = this.GetRtfString(OriginalText);
            tesis.ComparaTesis.TObservaciones = this.GetRtfString(ObservText);
            tesis.ComparaTesis.TAprobada = this.GetRtfString(AprobaText);

            tesis.ComparaTesis.IdTesis = tesis.IdTesis;

            new TesisCommonModel(1).UpdateTesisCompara(tesis.ComparaTesis);
            this.Close();
        }

        private string GetRtfString(RichTextBox rich)
        {
            var doc = rich.Document;
            var range = new TextRange(doc.ContentStart, doc.ContentEnd);
            var ms = new MemoryStream();
            range.Save(ms, DataFormats.Rtf);
            ms.Seek(0, SeekOrigin.Begin);

            var rtfString = new StreamReader(ms).ReadToEnd();

            return rtfString;
        }
    }
}
