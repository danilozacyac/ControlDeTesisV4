using System;
using System.Configuration;
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


    }
}
