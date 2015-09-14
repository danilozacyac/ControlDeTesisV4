using System;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data.OleDb;
using System.Linq;
using ControlDeTesisV4.Turno;
using ScjnUtilities;

namespace ControlDeTesisV4.Models
{
    public class CargasDeTrabajoModel
    {
        private static readonly string connectionString = ConfigurationManager.ConnectionStrings["Modulo"].ToString();

        
        public static ObservableCollection<CargaTrabajo> GetCargaPorTipoDocto(int tipoDocto,string docto)
        {
            ObservableCollection<CargaTrabajo> cargas = new ObservableCollection<CargaTrabajo>();

            OleDbConnection connection = new OleDbConnection(connectionString);
            OleDbCommand cmd;
            OleDbDataReader reader = null;


            try
            {
                connection.Open();

                string sqlCadena = "SELECT A.Nombre, SUM(T.NumPaginas) AS Paginas FROM Turno T " + 
                    " INNER JOIN Abogados A On A.IdAbogado = T.IdAbogado WHERE IdTipoDocto = @TipoDocto Group By  A.nombre";

                cmd = new OleDbCommand(sqlCadena, connection);
                cmd.Parameters.AddWithValue("@TipoDocto", tipoDocto);
                reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        CargaTrabajo carga = new CargaTrabajo(reader["Nombre"].ToString(), docto, Convert.ToInt32(reader["Paginas"]));
                        cargas.Add(carga);
                        
                    }
                }
                reader.Close();
            }
            catch (OleDbException ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                ErrorUtilities.SetNewErrorMessage(ex, methodName + " Exception,CargasDeTrabajoModel", "ControlTesis");
            }
            catch (Exception ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                ErrorUtilities.SetNewErrorMessage(ex, methodName + " Exception,CargasDeTrabajoModel", "ControlTesis");
            }
            finally
            {
                connection.Close();
            }

            return cargas;
        }


        public static ObservableCollection<CargaTrabajo> GetCargaPorAbogado()
        {
            ObservableCollection<CargaTrabajo> cargas = new ObservableCollection<CargaTrabajo>();

            OleDbConnection connection = new OleDbConnection(connectionString);
            OleDbCommand cmd;
            OleDbDataReader reader = null;


            try
            {
                connection.Open();

                string sqlCadena = "SELECT A.Nombre, SUM(T.NumPaginas) AS Paginas FROM Turno T " +
                    " INNER JOIN Abogados A On A.IdAbogado = T.IdAbogado Group By  A.nombre";

                cmd = new OleDbCommand(sqlCadena, connection);
                reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        CargaTrabajo carga = new CargaTrabajo(reader["Nombre"].ToString() + "     NP: " + reader["Paginas"].ToString(), "", Convert.ToInt32(reader["Paginas"]));
                        cargas.Add(carga);

                    }
                }
                reader.Close();
            }
            catch (OleDbException ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                ErrorUtilities.SetNewErrorMessage(ex, methodName + " Exception,CargasDeTrabajoModel", "ControlTesis");
            }
            catch (Exception ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                ErrorUtilities.SetNewErrorMessage(ex, methodName + " Exception,CargasDeTrabajoModel", "ControlTesis");
            }
            finally
            {
                connection.Close();
            }

            return cargas;
        }
    }
}
