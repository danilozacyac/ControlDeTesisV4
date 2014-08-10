using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ControlDeTesisV4.Dao;

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
    }
}
