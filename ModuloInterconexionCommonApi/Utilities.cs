using System;
using System.IO;
using System.Linq;

namespace ModuloInterconexionCommonApi
{
    public class Utilities
    {


        

        /// <summary>
        /// Copia un archivo a la carpeta destinada dentro de la ubicación en que se 
        /// encuentra la aplicacion
        /// </summary>
        public static string CopyToLocalResource(string docPath)
        {
            try
            {
                string newPath = Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName);

                newPath += @"\Docs\" + Path.GetFileName(docPath);

                File.Copy(docPath, newPath);

                return newPath;
            }
            catch (Exception)
            {
                return "";
            }
        }
    }
}
