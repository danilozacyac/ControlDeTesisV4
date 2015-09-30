using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentMgmtApi
{
    public class TxtFileCreate
    {

        public void CreaArchivo(string texto, int instancia, int numAsunto, int year, int etapa)
        {

            string rootPath = ConfigurationManager.AppSettings["ArchivosRelacionados"].ToString();

            if (!Directory.Exists(rootPath + year))
            {
                Directory.CreateDirectory(rootPath + year);
            }


            Int64 nombredeArchivo = instancia * 100000000;
            nombredeArchivo = nombredeArchivo + (year * 10000);
            nombredeArchivo += numAsunto;

            string fileName = nombredeArchivo + "_" + etapa + ".txt";


            using (System.IO.StreamWriter file = new System.IO.StreamWriter(rootPath + year + @"\" + fileName, true))
            {
                file.WriteLine(texto);
                
            }

        }



    }
}
