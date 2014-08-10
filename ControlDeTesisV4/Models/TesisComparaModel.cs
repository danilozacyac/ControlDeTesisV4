using System;
using System.Configuration;
using System.Data.OleDb;
using System.Linq;
using System.Windows.Forms;
using ControlDeTesisV4.Dao;

namespace ControlDeTesisV4.Models
{
    public class TesisComparaModel
    {
        readonly string connectionString = ConfigurationManager.ConnectionStrings["Modulo"].ToString();

        public TesisCompara GetTesisCompara(int idTesis)
        {
            TesisCompara tesis = new TesisCompara();

            OleDbConnection oleConne = new OleDbConnection(connectionString);
            OleDbCommand cmd = null;
            OleDbDataReader reader = null;

            String sqlCadena = "SELECT * FROM TesisCompara WHERE IdTesis = @IdTesis";

            try
            {
                oleConne.Open();

                cmd = new OleDbCommand(sqlCadena, oleConne);
                cmd.Parameters.AddWithValue("@IdTesis", idTesis);
                reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {

                        tesis.IdTesis = idTesis;
                        tesis.TextoOriginal = reader["TextoOriginal"].ToString();
                        tesis.TOPlano = reader["TOPlano"].ToString();
                        tesis.TObservaciones = reader["TextoRevision1"].ToString();
                        tesis.TObservacionesPlano = reader["TR1Plano"].ToString();
                        tesis.TAprobada = reader["TextoRevision2"].ToString();
                        tesis.TAprobadaPlano = reader["TR2Plano"].ToString();
                        tesis.ToFilePathOrigen = reader["ToFilePathOrigen"].ToString();
                        tesis.TObsFilePathOrigen = reader["TobsFilePathOrigen"].ToString();
                        tesis.TAprobFilePathOrigen = reader["TAprobFilePathOrigen"].ToString();
                                                
                    }
                }
                cmd.Dispose();
                reader.Close();
            }
            catch (OleDbException sql)
            {
                MessageBox.Show("Error ({0}) : {1}" + sql.Source + sql.Message, "Error Interno");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error ({0}) : {1}" + ex.Source + ex.Message, "Error Interno");
            }
            finally
            {

                oleConne.Close();
            }

            return tesis;
        }

    }
}
