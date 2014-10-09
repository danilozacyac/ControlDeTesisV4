using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using ControlDeTesisV4.Dao;
using ScjnUtilities;
using Word = Microsoft.Office.Interop.Word;

namespace ControlDeTesisV4.Reportes.Proyectos
{
    public class TesisCcstRtfWordTable
    {
        private readonly ObservableCollection<ProyectosTesis> listaTesis;

        readonly string filepath = Path.GetTempFileName() + ".docx";

        int fila = 1;

       // private List<ProyectosTesis> tesisImprime = new List<ProyectosTesis>();
        Word.Application oWord;
        Word.Document oDoc;
        object oMissing = System.Reflection.Missing.Value;
        object oEndOfDoc = "\\endofdoc";



        public TesisCcstRtfWordTable(ObservableCollection<ProyectosTesis> listaTesis)
        {
            this.listaTesis = listaTesis;
        }

        public void GeneraWord()
        {
            oWord = new Word.Application();
            oDoc = oWord.Documents.Add(ref oMissing, ref oMissing, ref oMissing, ref oMissing);
            oDoc.PageSetup.PaperSize = Word.WdPaperSize.wdPaperLegal;
            oDoc.PageSetup.Orientation = Word.WdOrientation.wdOrientLandscape;

            //Insert a paragraph at the beginning of the document.
            Word.Paragraph oPara1;
            oPara1 = oDoc.Content.Paragraphs.Add(ref oMissing);
            oPara1.Range.Text = "SISTEMATIZACIÓN DE TESIS JURISPRUDENCIALES Y AISLADAS PUBLICADAS EN EL SEMANARIO JUDICIAL DE LA FEDERACIÓN Y SU GACETA";
            oPara1.Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
            oPara1.Range.Font.Bold = 1;
            oPara1.Format.SpaceAfter = 24;    //24 pt spacing after paragraph.
            oPara1.Range.InsertParagraphAfter();

            Word.Range wrdRng = oDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;

            try
            {
                ImprimeDocumento();

                oWord.ActiveDocument.SaveAs(filepath);
                oWord.ActiveDocument.Saved = true;
                oWord.Visible = true;
                oDoc.Close();
                Process.Start(filepath);
            }
            catch (Exception ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;

                MessageBox.Show("Error ({0}) : {1}" + ex.Source + ex.Message, methodName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void ImprimeDocumento()
        {
            int numTesis = 0;
            int instancia = 1;

            try
            {
                while (instancia < 5)
                {
                    List<ProyectosTesis> listaImprimir = (from n in listaTesis
                                                          where n.IdInstancia == instancia
                                                          orderby n.Tatj descending, n.ComparaTesis.TOPlano
                                                          select n).ToList();

                    foreach (ProyectosTesis tesis in listaImprimir)
                    {
                        Word.Range wrdRng = oDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;

                        Word.Table oTable = oDoc.Tables.Add(wrdRng, 3, 2, ref oMissing, ref oMissing);
                        oTable.Range.ParagraphFormat.SpaceAfter = 6;
                        oTable.Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                        oTable.Range.Font.Size = 10;
                        oTable.Borders.Enable = 1;

                        oTable.Columns[1].SetWidth(300, Word.WdRulerStyle.wdAdjustSameWidth);
                        oTable.Columns[2].SetWidth(300, Word.WdRulerStyle.wdAdjustSameWidth);
                        oTable.Columns[3].SetWidth(300, Word.WdRulerStyle.wdAdjustSameWidth);

                        oTable.Cell(1, 2).Merge(oTable.Cell(1, 3));
                        oTable.Cell(1, 1).Merge(oTable.Cell(1, 2));
                        oTable.Cell(1, 1).Range.Text = (tesis.Tatj == 1) ? "JURISPRUDENCIA" : "AISLADA";
                        oTable.Cell(1, 1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;

                        oTable.Cell(2, 1).Split(2, 1);
                        oTable.Cell(2, 1).Range.Text = "TEXTO PROPUESTO POR LA COORDINACIÓN DE COMPILACIÓN Y SISTEMATIZACIÓN DE TESIS DE LA SCJN";
                        oTable.Cell(3, 1).Split(1, 4);

                        oTable.Cell(3, 1).Range.Text = "Fecha de entrega: ";
                        oTable.Cell(3, 2).Range.Text = DateTimeUtilities.ToLongDateFormat(tesis.FEnvio);
                        oTable.Cell(3, 3).Range.Text = "Oficio número: ";
                        oTable.Cell(3, 4).Range.Text = tesis.OficioEnvio;

                        oTable.Cell(2, 5).Range.Text = "TEXTO APROBADO POR LOS MINISTROS DE LA SCJN";
                        oTable.Cell(2, 2).Range.Font.Bold = 1;

                        oTable.Cell(2, 6).Range.Text = (String.IsNullOrEmpty(tesis.ClaveTesis)) ? "TESIS PENDIENTE DE APROBACIÓN" : tesis.ClaveTesis;
                        oTable.Cell(2, 2).Range.Font.Bold = 1;


                        
                        oTable.Range.Font.Name = "Arial";
                        oTable.Range.Font.Size = 9;

                        fila++;

                        Clipboard.SetText(tesis.ComparaTesis.TextoOriginal, TextDataFormat.Rtf);
                        oTable.Cell(6, 1).Select();
                        oWord.Selection.Paste();
                        oTable.Cell(6, 1).Range.Font.Name = "Arial";
                        oTable.Cell(6, 1).Range.Font.Size = 10;

                        Clipboard.SetText(tesis.ComparaTesis.TAprobada, TextDataFormat.Rtf);
                        oTable.Cell(6, 2).Select();
                        oWord.Selection.Paste();
                        oTable.Cell(6, 2).Range.Font.Name = "Arial";
                        oTable.Cell(6, 2).Range.Font.Size = 10;

                        fila++;
                        numTesis++;

                        oTable = null;

                        object start = oWord.Selection.End - 1;
                        object end = oWord.Selection.End;

                        Word.Range rng = oDoc.Range(ref start, ref end);
                        rng.Select();

                        Word.Paragraph oPara1;
                        oPara1 = oDoc.Content.Paragraphs.Add(ref oMissing);
                        oPara1.Range.Text = " ";

                        oDoc.Words.Last.InsertBreak(Word.WdBreakType.wdPageBreak);
                    }
                    Clipboard.Clear();
                    instancia++;
                }

            }
            catch (Exception ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                MessageBox.Show("Error ({0}) : {1}" + ex.Source + ex.Message, methodName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

       

        

        
    }
}
