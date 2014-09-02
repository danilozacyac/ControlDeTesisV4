using System;
using System.Configuration;
using System.IO;
using System.Linq;

namespace ModuloInterconexionCommonApi
{
    public class Utilities
    {


        /// <summary>
        /// Añade la información del error generado a un archivo de texto
        /// </summary>
        /// <param name="ex">Excepcion lanzada</param>
        /// <param name="methodName">Nombre del método donde se genera el error</param>
        /// <param name="numIus">Número de registro IUS (En caso de existir)</param>
        public static void SetNewErrorMessage(Exception ex, String methodName, long numIus)
        {
            String errorFilePath = ConfigurationManager.AppSettings.Get("RutaTxtErrorFile");

            using (System.IO.StreamWriter file = new System.IO.StreamWriter(errorFilePath, true))
            {
                file.WriteLine(" ");
                file.WriteLine(" ");
                file.WriteLine(" ");
                file.WriteLine("*********************" + DateTime.Now.ToString() + "***************************");
                file.WriteLine("Equipo:  " + Environment.MachineName);
                file.WriteLine("Usuario:   " + Environment.UserName);
                file.WriteLine("Método:    " + methodName);
                file.WriteLine("Núm. IUS:  " + numIus);
                file.WriteLine(ex.Message);
                file.WriteLine(ex.StackTrace);
                file.WriteLine("***************************************************************************************");
            }
        }

        /// <summary>
        /// Copia un archivo a la carpeta detinada dentro de la ubicación en que se 
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
