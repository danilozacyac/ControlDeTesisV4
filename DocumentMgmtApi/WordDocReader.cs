using System;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.Office.Interop.Word;
using ModuloInterconexionCommonApi;
using Word = Microsoft.Office.Interop.Word;

namespace DocumentMgmtApi
{
    public class WordDocReader
    {
        public static void ReadFileContent(string path, ObservableCollection<Observaciones> observaciones)
        {

            int i = 1;
            Application word = new Application();
            object file = path;
            object nullobj = System.Reflection.Missing.Value;
            Word.Document doc = word.Documents.Open(ref file, ref nullobj, ref nullobj,
                ref nullobj, ref nullobj, ref nullobj,
                ref nullobj, ref nullobj, ref nullobj,
                ref nullobj, ref nullobj, ref nullobj);

            Word.Paragraphs docPar = doc.Paragraphs;
            // Count number of paragraphs in the file
            long parCount = docPar.Count;
            // Step through the paragraphs
            while (i < parCount)
            {
                if (docPar[i].Range.Text.Contains("foja"))
                {
                    observaciones.Add(WordDocReader.GetObservacionFromParagraph(docPar[i].Range.Text));
                }
                
                i++;
            }
            doc.Close(ref nullobj, ref nullobj, ref nullobj);
            word.Quit(ref nullobj, ref nullobj, ref nullobj);

        }

        private static Observaciones GetObservacionFromParagraph(string text)
        {
            Observaciones observ = new Observaciones();

            int indexOf = text.IndexOf(',');

            string tempString = text.Substring(0, indexOf);

            observ.Foja = WordDocReader.GetFojaNumber(tempString);

            text = text.Substring(indexOf + 1);

            indexOf = text.IndexOf(',');

            observ.Parrafo = text.Substring(0, indexOf);

            text = text.Substring(indexOf + 1);

            indexOf = text.IndexOf("dice:");

            if(indexOf == -1)
                indexOf = text.IndexOf("dicen:");

            observ.Renglon = text.Substring(0, indexOf - 1);

            text = text.Substring(indexOf - 1);

            text = text.Replace("dice:", "");

            indexOf = text.IndexOf("se sugiere:");

            observ.Dice = text.Substring(0, indexOf - 2);

            text = text.Substring(indexOf);

            text = text.Replace("se sugiere:", "");

            observ.Sugiere = text;

            return observ;
        }

        private static string GetFojaNumber(string text)
        {
            string foja = "";

            int caractIndex = 0;
            foreach (char caract in text.ToList())
            {
                if (StringUtilities.IsTextAllowed(caract))
                {
                    foja = text.Substring(caractIndex);
                    break;
                }

                caractIndex++;
            }

            return foja;
        }
    }
}