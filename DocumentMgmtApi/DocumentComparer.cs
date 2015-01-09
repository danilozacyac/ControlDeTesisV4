using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Xml;
using ScjnUtilities;


namespace DocumentMgmtApi
{
    public class DocumentComparer
    {
        public static FlowDocument LoadDocumentContent(string pathFile)
        {

            RichTextBox richTextBox = new RichTextBox();
            FlowDocument flowdocument = null;
            try
            {
                String convertedFilePath = "";

                if (!String.IsNullOrEmpty(pathFile) && !String.IsNullOrWhiteSpace(pathFile))
                {
                    if (!pathFile.EndsWith(".rtf"))
                    {
                        convertedFilePath = Path.GetTempFileName();

                        DocumentConversion.SetNewDocFormat(pathFile, convertedFilePath, "RTF");

                        pathFile = convertedFilePath + ".rtf";
                    }

                    TextRange range;

                    System.IO.FileStream fStream;

                    if (System.IO.File.Exists(pathFile))
                    {
                        range = new TextRange(richTextBox.Document.ContentStart, richTextBox.Document.ContentEnd);

                        fStream = new System.IO.FileStream(pathFile, System.IO.FileMode.OpenOrCreate);

                        range.Load(fStream, System.Windows.DataFormats.Rtf);

                        fStream.Close();
                    }

                    //OriginalText.Font
                    var text = new TextRange(richTextBox.Document.ContentStart, richTextBox.Document.ContentEnd);
                    text.ApplyPropertyValue(TextElement.FontSizeProperty, 10.0);
                    richTextBox.Document.TextAlignment = TextAlignment.Justify;
                }

                flowdocument = richTextBox.Document;
                richTextBox.Document = new FlowDocument();
            }
            catch (Exception ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;

                MessageBox.Show("Error ({0}) : {1}" + ex.Source + ex.Message, methodName);
                ErrorUtilities.SetNewErrorMessage(ex, methodName, 0);

                MessageBox.Show("ATENCIÓN:", "El documento que esta seleccionando no tiene el formato correcto, se sugiere copiar su contenido y crear un documento nuevo");

            }

            return flowdocument;
        }

        /// <summary>
        /// Devuelve una cadena con el contenido del RichTextBox con todo y formato
        /// </summary>
        /// <param name="rich">Cuadro de texto de donde se extraera la información</param>
        /// <returns></returns>
        public static string GetRtfString(RichTextBox rich)
        {
            var doc = rich.Document;
            var range = new TextRange(doc.ContentStart, doc.ContentEnd);
            var ms = new MemoryStream();
            range.Save(ms, DataFormats.Rtf);
            ms.Seek(0, SeekOrigin.Begin);

            var rtfString = new StreamReader(ms).ReadToEnd();

            return rtfString;
        }

        /// <summary>
        /// Regresa la ruta del archivo XML generado después de realizar la comparación entre 
        /// dos archivos de Word
        /// </summary>
        /// <param name="doc1Path">Documento Original</param>
        /// <param name="doc2Path">Documento contra el que se compara</param>
        /// <returns></returns>
        public static String CompareDocuments(string doc1Path, string doc2Path)
        {
            string xmlResult = Path.GetTempFileName() + ".xml";

            WordDocComparer comparer = new WordDocComparer(doc1Path, doc2Path, xmlResult);
            comparer.Compare();

            return xmlResult;
        }

        /// <summary>
        /// Marca los cambios realizados entre diferentes versiones de documentos
        /// </summary>
        /// <param name="filePath">Ruta del archivo XML resultante de la comparación entre dos archivos</param>
        /// <param name="tipoMarcado">1. Marcatextos  --- 2. Subrayado</param>
        public static FlowDocument LoadDocumentRevision(string filePath, int tipoMarcado)
        {
            FlowDocument mcFlowDoc = new FlowDocument();

            XmlDocument xDoc = new XmlDocument();

            //La ruta del documento XML permite rutas relativas 
            //respecto del ejecutable!

            xDoc.Load(filePath);

            XmlNodeList docBody = xDoc.GetElementsByTagName("w:body");

            foreach (XmlElement seccionesDoc in docBody)
            {
                foreach (XmlElement nodoHijo in seccionesDoc)
                {
                    XmlNodeList hijos = nodoHijo.ChildNodes;

                    foreach (XmlElement subHijos in hijos)
                    {
                        Paragraph para = new Paragraph();
                        para.FontFamily = new FontFamily("Arial");
                        para.FontSize = 10;
                        para.TextAlignment = TextAlignment.Justify;

                        foreach (XmlElement texto in subHijos.ChildNodes)
                        {
                            switch (texto.LocalName)
                            {
                                case "sub-section":
                                    foreach (XmlElement textoSubSeccion in texto.ChildNodes)
                                    {
                                        para = SetFormat(textoSubSeccion, tipoMarcado);
                                        mcFlowDoc.Blocks.Add(para);
                                    }
                                    break;
                                case "r":
                                    if (tipoMarcado == 1)
                                    {
                                        string text = texto.InnerText;

                                        XmlNodeList elem = texto.GetElementsByTagName("w:b");

                                        if (elem.Count > 0)
                                            para.Inlines.Add(new Bold(new Run(text)));
                                        else
                                            para.Inlines.Add(new Run(text));
                                        //Aqui es donde lo agrego al RichTextBox}
                                    }
                                    else if (tipoMarcado == 2)
                                    {
                                        XmlNodeList elem = texto.GetElementsByTagName("w:highlight");
                                        XmlNodeList bold = texto.GetElementsByTagName("w:b");

                                        if (elem.Count > 0)
                                        {
                                            Run run = new Run(texto.InnerText);
                                            run.Background = new SolidColorBrush(Colors.SpringGreen);

                                            if (bold.Count > 0 || StringUtilities.IsAllUpper(texto.InnerText))
                                                para.Inlines.Add(new Bold(run));
                                            else
                                                para.Inlines.Add(run);
                                        }
                                        else if (bold.Count > 0)
                                        {
                                            para.Inlines.Add(new Bold(new Run(texto.InnerText)));
                                        }
                                        else
                                            para.Inlines.Add(new Run(texto.InnerText));
                                    }
                                    break;
                                case "annotation":
                                    XmlAttributeCollection attrib = texto.Attributes;
                                    string atribute = attrib["w:type"].Value;

                                    if (atribute.Equals("Word.Insertion"))
                                    {
                                        Run run;

                                        if (tipoMarcado == 1)
                                        {
                                            run = new Run(texto.InnerText);
                                            run.Background = new SolidColorBrush(Colors.SpringGreen);
                                            para.Inlines.Add(run);
                                        }
                                        else
                                        {
                                            XmlNodeList high = texto.GetElementsByTagName("w:highlight");

                                            if (high.Count > 0)
                                            {
                                                run = new Run(texto.InnerText);
                                                run.Background = new SolidColorBrush(Colors.SpringGreen);
                                                para.Inlines.Add(run);
                                            }
                                            else
                                                para.Inlines.Add(new Underline(new Run(texto.InnerText)));
                                        }
                                    }

                                    break;
                            }

                            Console.WriteLine(texto.LocalName);
                        }
                        mcFlowDoc.Blocks.Add(para);
                    }
                }
            }

            return mcFlowDoc;
        }

        private static Paragraph SetFormat(XmlElement texto, int tipoMarcado)
        {
            Paragraph para = new Paragraph();
            para.FontFamily = new FontFamily("Arial");
            para.FontSize = 10;
            para.TextAlignment = TextAlignment.Justify;

            switch (texto.LocalName)
            {
                case "r":
                case "p":
                    if (tipoMarcado == 1)
                    {
                        string text = texto.InnerText;

                        XmlNodeList elem = texto.GetElementsByTagName("w:b");

                        if (elem.Count > 0)
                            para.Inlines.Add(new Bold(new Run(text)));
                        else
                            para.Inlines.Add(new Run(text));
                        //Aqui es donde lo agrego al RichTextBox}
                    }
                    else if (tipoMarcado == 2)
                    {
                        XmlNodeList elem = texto.GetElementsByTagName("w:highlight");
                        XmlNodeList bold = texto.GetElementsByTagName("w:b");

                        if (elem.Count > 0)
                        {
                            Run run = new Run(texto.InnerText);
                            run.Background = new SolidColorBrush(Colors.SpringGreen);

                            if (bold.Count > 0)
                                para.Inlines.Add(new Bold(run));
                            else
                                para.Inlines.Add(run);
                        }
                        else if (bold.Count > 0)
                        {
                            para.Inlines.Add(new Bold(new Run(texto.InnerText)));
                        }
                        else
                            para.Inlines.Add(new Run(texto.InnerText));
                    }
                    break;
                case "annotation":
                    XmlAttributeCollection attrib = texto.Attributes;
                    string atribute = attrib["w:type"].Value;

                    if (atribute.Equals("Word.Insertion"))
                    {
                        Run run;

                        if (tipoMarcado == 1)
                        {
                            run = new Run(texto.InnerText);
                            run.Background = new SolidColorBrush(Colors.SpringGreen);
                            para.Inlines.Add(run);
                        }
                        else
                        {
                            XmlNodeList high = texto.GetElementsByTagName("w:highlight");

                            if (high.Count > 0)
                            {
                                run = new Run(texto.InnerText);
                                run.Background = new SolidColorBrush(Colors.SpringGreen);
                                para.Inlines.Add(run);
                            }
                            else
                                para.Inlines.Add(new Underline(new Run(texto.InnerText)));
                        }
                    }

                    break;
            }

            return para;
        }
    }
}