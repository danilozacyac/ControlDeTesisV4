using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text.RegularExpressions;

namespace ModuloInterconexionCommonApi
{
    public class StringUtilities
    {
        /// <summary>
        /// Verifica si el caracter ingresado es un caracter numérico
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static bool IsTextAllowed(string text)
        {
            // Regex NumEx = new Regex(@"^\d+(?:.\d{0,2})?$"); 
            Regex regex = new Regex("[^0-9.-]+"); //regex that matches disallowed text 
            return regex.IsMatch(text);
        }

        /// <summary>
        /// Verifica si el caracter ingresado es un caracter numérico
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static bool IsTextAllowed(char text)
        {
            if (Char.IsDigit(text))
                return true;
            else
                return false;
        }

        /// <summary>
        /// Da el formato yyyMMdd a la fecha seleccionada
        /// </summary>
        /// <returns></returns>
        public static string DateToInt(DateTime? fecha)
        {
            return fecha.Value.Year + StringUtilities.GetMonthDayTwoDigitFormat(fecha.Value.Month) + StringUtilities.GetMonthDayTwoDigitFormat(fecha.Value.Day); 
        }


        public static DateTime? GetDateFromReader(DbDataReader reader,string columnName)
        {
            if (reader[columnName] == DBNull.Value)
            {
                return null;
            }
            else
            {
                return Convert.ToDateTime(reader[columnName]);
            }

        }

        private static string GetMonthDayTwoDigitFormat(int diaMes)
        {
            if (diaMes < 10)
                return "0" + diaMes;
            else
                return diaMes.ToString();
        }


        public static string GetFileNameForContainer(int fileTipe)
        {
            Guid guid = new Guid();
            return guid.ToString();
        }

        /// <summary>
        /// Verifica si todas las letras de una palabra son mayúsculas
        /// </summary>
        /// <param name="input">Texto a verificar</param>
        /// <returns></returns>
        public static bool IsAllUpper(string input)
        {
            for (int i = 0; i < input.Length; i++)
            {
                if (Char.IsLetter(input[i]) && !Char.IsUpper(input[i]))
                    return false;
            }
            return true;
        }


        private static Dictionary<char, int> valorRomano;

        public StringUtilities()
        {
            valorRomano = new Dictionary<char, int>();

            valorRomano.Add('I', 1);
            valorRomano.Add('V', 5);
            valorRomano.Add('X', 10);
            valorRomano.Add('L', 50);
            valorRomano.Add('C', 100);
            valorRomano.Add('D', 500);
            valorRomano.Add('M', 1000);
        }

        /// <summary>
        /// Convierte un númeor romano a su equivalente en decimal
        /// </summary>
        /// <param name="romano"></param>
        /// <returns></returns>
        public static int RomanosADecimal(String romano)
        {
            char[] nums = romano.ToCharArray();
            //List<int> dec = new List<int>();
            char ultimoNumero = ' ';
            int numeroDecimal = 0;

            foreach (char letra in nums)
            {
                if (ultimoNumero.Equals(' '))
                {
                    numeroDecimal = valorRomano[letra];
                }
                else if (valorRomano[letra] > valorRomano[ultimoNumero])
                {
                    numeroDecimal -= valorRomano[ultimoNumero];

                    numeroDecimal += ((valorRomano[letra] - valorRomano[ultimoNumero]));
                }
                else if ((valorRomano[ultimoNumero] > valorRomano[letra]) || (valorRomano[ultimoNumero] == valorRomano[letra]))
                {
                    numeroDecimal += valorRomano[letra];
                }

                ultimoNumero = letra;
            }

            return numeroDecimal;
        }



        static string CambiaLtr123(string cLineaPr, string cCarOld, string cCarNvo)
        {
            string cLinea = cLineaPr;
            string cLin;
            string cLineaTmp;
            int nPos, nAct;
            int nLengthLinea;
            int nLengthCarOld;

            nLengthCarOld = cCarOld.Length;
            nAct = 1;
            cLin = "";
            nPos = cLinea.IndexOf(cCarOld, nAct);

            while (nPos > 0)
            {
                cLin = cLin + cLinea.Substring(0, nPos) + cCarNvo;
                nLengthLinea = cLinea.Length - (nPos + nLengthCarOld);
                cLineaTmp = cLinea.Substring(nPos + nLengthCarOld, nLengthLinea);
                cLinea = cLineaTmp;

                if (cLinea != "")
                {
                    nPos = cLinea.IndexOf(cCarOld, nAct);
                }
                else
                {
                    nPos = 0;
                }

            }
            cLin = cLin + cLinea;

            return cLin;
        }  // fin CambiaLtr123

        public static string QuitaCarCad(string cCadena)
        {
            string cChr = "";
            string sCadena = cCadena;

            sCadena = CambiaLtr123(sCadena, "+", " ");
            sCadena = CambiaLtr123(sCadena, "=", " ");
            sCadena = CambiaLtr123(sCadena, "*", " ");
            sCadena = CambiaLtr123(sCadena, "&", " ");
            sCadena = CambiaLtr123(sCadena, "^", " ");
            sCadena = CambiaLtr123(sCadena, "$", " ");

            sCadena = CambiaLtr123(sCadena, "#", " ");
            sCadena = CambiaLtr123(sCadena, "@", " ");
            sCadena = CambiaLtr123(sCadena, "!", " ");
            sCadena = CambiaLtr123(sCadena, "¡", " ");
            sCadena = CambiaLtr123(sCadena, "?", " ");
            sCadena = CambiaLtr123(sCadena, "¿", " ");
            sCadena = CambiaLtr123(sCadena, "<", " ");
            sCadena = CambiaLtr123(sCadena, ">", " ");
            sCadena = CambiaLtr123(sCadena, "~", " ");

            sCadena = CambiaLtr123(sCadena, "|", " ");
            sCadena = CambiaLtr123(sCadena, "°", " ");
            sCadena = CambiaLtr123(sCadena, "ª", " ");
            sCadena = CambiaLtr123(sCadena, "º", " ");

            sCadena = CambiaLtr123(sCadena, ".", " ");
            sCadena = CambiaLtr123(sCadena, ",", " ");
            sCadena = CambiaLtr123(sCadena, ":", " ");
            sCadena = CambiaLtr123(sCadena, ";", " ");
            sCadena = CambiaLtr123(sCadena, "%", " ");

            sCadena = CambiaLtr123(sCadena, "(", " ");
            sCadena = CambiaLtr123(sCadena, ")", " ");
            sCadena = CambiaLtr123(sCadena, "[", " ");
            sCadena = CambiaLtr123(sCadena, "]", " ");
            sCadena = CambiaLtr123(sCadena, "{", " ");
            sCadena = CambiaLtr123(sCadena, "}", " ");
            sCadena = CambiaLtr123(sCadena, "`", " ");
            sCadena = CambiaLtr123(sCadena, "-", " ");
            sCadena = CambiaLtr123(sCadena, "_", " ");
            sCadena = CambiaLtr123(sCadena, "/", " ");


            cChr = Convert.ToChar(92).ToString();
            sCadena = CambiaLtr123(sCadena, cChr, " ");
            sCadena = CambiaLtr123(sCadena, "'", " ");

            cChr = Convert.ToChar(34).ToString();
            sCadena = CambiaLtr123(sCadena, cChr, " ");

            cChr = Convert.ToChar(13).ToString();
            sCadena = CambiaLtr123(sCadena, cChr, " ");

            cChr = Convert.ToChar(10).ToString();
            sCadena = CambiaLtr123(sCadena, cChr, " ");


            return sCadena;
        }  // fin QuitaCarCad

        public static string QuitaCarOrden(string cCadena)
        {
            string cChr = "";
            string sCadena = cCadena;

            sCadena = CambiaLtr123(sCadena, "+", "");
            sCadena = CambiaLtr123(sCadena, "=", "");
            sCadena = CambiaLtr123(sCadena, "*", "");
            sCadena = CambiaLtr123(sCadena, "&", "");
            sCadena = CambiaLtr123(sCadena, "^", "");
            sCadena = CambiaLtr123(sCadena, "$", "");

            sCadena = CambiaLtr123(sCadena, "#", "");
            sCadena = CambiaLtr123(sCadena, "@", "");
            sCadena = CambiaLtr123(sCadena, "!", "");
            sCadena = CambiaLtr123(sCadena, "¡", "");
            sCadena = CambiaLtr123(sCadena, "?", "");
            sCadena = CambiaLtr123(sCadena, "¿", "");
            sCadena = CambiaLtr123(sCadena, "<", "");
            sCadena = CambiaLtr123(sCadena, ">", "");
            sCadena = CambiaLtr123(sCadena, "~", "");

            sCadena = CambiaLtr123(sCadena, "|", "");
            sCadena = CambiaLtr123(sCadena, "°", "");
            sCadena = CambiaLtr123(sCadena, "ª", "");
            sCadena = CambiaLtr123(sCadena, "º", "");

            sCadena = CambiaLtr123(sCadena, ".", "");
            sCadena = CambiaLtr123(sCadena, ",", "");
            sCadena = CambiaLtr123(sCadena, ":", "");
            sCadena = CambiaLtr123(sCadena, ";", "");
            sCadena = CambiaLtr123(sCadena, "%", "");

            sCadena = CambiaLtr123(sCadena, "(", "");
            sCadena = CambiaLtr123(sCadena, ")", "");
            sCadena = CambiaLtr123(sCadena, "[", "");
            sCadena = CambiaLtr123(sCadena, "]", "");
            sCadena = CambiaLtr123(sCadena, "{", "");
            sCadena = CambiaLtr123(sCadena, "}", "");
            sCadena = CambiaLtr123(sCadena, "`", "");
            sCadena = CambiaLtr123(sCadena, "-", "");
            sCadena = CambiaLtr123(sCadena, "_", "");
            sCadena = CambiaLtr123(sCadena, "/", "");



            cChr = Convert.ToChar(92).ToString();
            sCadena = CambiaLtr123(sCadena, cChr, "");
            sCadena = CambiaLtr123(sCadena, "'", "");

            cChr = Convert.ToChar(34).ToString();
            sCadena = CambiaLtr123(sCadena, cChr, "");

            cChr = Convert.ToChar(13).ToString();
            sCadena = CambiaLtr123(sCadena, cChr, "");

            cChr = Convert.ToChar(10).ToString();
            sCadena = CambiaLtr123(sCadena, cChr, "");

            char comienza = Convert.ToChar(sCadena.Substring(0, 1));
            if (Char.IsDigit(comienza))
            {
                sCadena = sCadena.Replace("0", "");
                sCadena = sCadena.Replace("1", "");
                sCadena = sCadena.Replace("2", "");
                sCadena = sCadena.Replace("3", "");
                sCadena = sCadena.Replace("4", "");
                sCadena = sCadena.Replace("5", "");
                sCadena = sCadena.Replace("6", "");
                sCadena = sCadena.Replace("7", "");
                sCadena = sCadena.Replace("8", "");
                sCadena = sCadena.Replace("9", "");

            }


            return sCadena;
        }  // fin QuitaCarCad

        public static string ConvMay(string cCadena)
        {
            string sCadena = cCadena;

            sCadena = CambiaLtr123(sCadena, "á", "A");
            sCadena = CambiaLtr123(sCadena, "é", "E");
            sCadena = CambiaLtr123(sCadena, "í", "I");
            sCadena = CambiaLtr123(sCadena, "ó", "O");
            sCadena = CambiaLtr123(sCadena, "ú", "U");
            sCadena = CambiaLtr123(sCadena, "ñ", "Ñ");
            sCadena = CambiaLtr123(sCadena, "ü", "U");
            sCadena = CambiaLtr123(sCadena, "Ü", "U");
            sCadena = CambiaLtr123(sCadena, "Á", "A");
            sCadena = CambiaLtr123(sCadena, "É", "E");
            sCadena = CambiaLtr123(sCadena, "Í", "I");
            sCadena = CambiaLtr123(sCadena, "Ó", "O");
            sCadena = CambiaLtr123(sCadena, "Ú", "U");

            sCadena.ToUpper();
            return sCadena;
        } // fin ConvMay

        public static string PrepareToAlphabeticalOrder(string cCadena)
        {
            cCadena = QuitaCarCad(cCadena);
            cCadena = QuitaCarOrden(cCadena);
            cCadena = ConvMay(cCadena);

            return cCadena;
        }

        
    }
}
