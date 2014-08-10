using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;
using Word = Microsoft.Office.Interop.Word;

namespace DocumentMgmtApi
{
    public class DocumentConversion
    {

        /// <summary>
        /// Convierte un documento de formato .Doc o .Docx a cualquiera de los formatos compatibles
        /// con Microsoft Word
        /// </summary>
        /// <param name="filePath">Ruta del archivo a convertir</param>
        /// <param name="newformatPath">Ruta del archivo generado</param>
        /// <param name="format">Tipo de Archivo Requerido (PDF, XML, RTf, ...)</param>
        public static void SetNewDocFormat(String filePath, String newformatPath, String format)
        {
            try
            {
                Word.Application wordApp = new Word.Application();
                Word.Document wordDoc = new Word.Document();

                object docNoParam = Type.Missing;
                object docReadOnly = false;
                object docVisible = false;
                object saveToFormat = "";

                wordDoc = wordApp.Documents.Open(filePath,
                    ref docNoParam,
                    ref docReadOnly,
                    ref docNoParam,
                    ref docNoParam,
                    ref docNoParam,
                    ref docNoParam,
                    ref docNoParam,
                    ref docNoParam,
                    ref docNoParam,
                    ref docVisible,
                    ref docNoParam,
                    ref docNoParam,
                    ref docNoParam,
                    ref docNoParam,
                    ref docNoParam);
                wordDoc.Activate();

                switch (format)
                {
                    case "PDF":
                        saveToFormat = Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatPDF;
                        wordDoc.SaveAs(newformatPath + ".pdf", ref saveToFormat, ref docNoParam, ref docNoParam, ref docNoParam, ref docNoParam, ref docNoParam, ref docNoParam, ref docNoParam, ref docNoParam, ref docNoParam, ref docNoParam, ref docNoParam, ref docNoParam, ref docNoParam, ref docNoParam);
                        break;
                    case "HTML":
                        saveToFormat = Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatHTML;
                        wordDoc.SaveAs(newformatPath + ".html", ref saveToFormat, ref docNoParam, ref docNoParam, ref docNoParam, ref docNoParam, ref docNoParam, ref docNoParam, ref docNoParam, ref docNoParam, ref docNoParam, ref docNoParam, ref docNoParam, ref docNoParam, ref docNoParam, ref docNoParam);
                        break;
                    case "RTF":
                        saveToFormat = Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatRTF;
                        wordDoc.SaveAs(newformatPath + ".rtf", ref saveToFormat, ref docNoParam, ref docNoParam, ref docNoParam, ref docNoParam, ref docNoParam, ref docNoParam, ref docNoParam, ref docNoParam, ref docNoParam, ref docNoParam, ref docNoParam, ref docNoParam, ref docNoParam, ref docNoParam);
                        break;
                    case "TXT":
                        saveToFormat = Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatText;
                        wordDoc.SaveAs(newformatPath + ".txt", ref saveToFormat, ref docNoParam, ref docNoParam, ref docNoParam, ref docNoParam, ref docNoParam, ref docNoParam, ref docNoParam, ref docNoParam, ref docNoParam, ref docNoParam, ref docNoParam, ref docNoParam, ref docNoParam, ref docNoParam);
                        break;
                    case "XML":
                        saveToFormat = Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatXML;
                        wordDoc.SaveAs(newformatPath + ".xml", ref saveToFormat, ref docNoParam, ref docNoParam, ref docNoParam, ref docNoParam, ref docNoParam, ref docNoParam, ref docNoParam, ref docNoParam, ref docNoParam, ref docNoParam, ref docNoParam, ref docNoParam, ref docNoParam, ref docNoParam);
                        break;
                    case "XPS":
                        saveToFormat = Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatXPS;
                        wordDoc.SaveAs(newformatPath + ".xps", ref saveToFormat, ref docNoParam, ref docNoParam, ref docNoParam, ref docNoParam, ref docNoParam, ref docNoParam, ref docNoParam, ref docNoParam, ref docNoParam, ref docNoParam, ref docNoParam, ref docNoParam, ref docNoParam, ref docNoParam);
                        break;

                }

                wordDoc.Close();
                wordApp.Application.Quit(ref docNoParam, ref docNoParam, ref docNoParam);
                wordDoc = null;
                wordApp = null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static String GetFilePath()
        {
            OpenFileDialog dialog = new OpenFileDialog();

            dialog.Filter = "Office Documents|*.doc;*.docx| RichTextFiles |*.rtf";

            dialog.InitialDirectory = @"C:\Users\" + Environment.UserName + @"\Documents";
            dialog.Title = "Selecciona el archivo del proyecto";
            dialog.ShowDialog();

            return dialog.FileName;
        }
    }
}
