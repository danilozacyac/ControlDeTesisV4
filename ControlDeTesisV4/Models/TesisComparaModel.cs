using System;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Windows.Forms;
using ControlDeTesisV4.Dao;
using ScjnUtilities;

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

        /// <summary>
        /// Actualiza el texto de cada una de las etapas de las tesis cuando estas sufren modificaciones
        /// dentro de la ventana de Salas Compare o CcstCompare, estos cambios serán principalmente en el 
        /// formato
        /// </summary>
        /// <param name="tesis"></param>
        public void UpdateTesisCompara(TesisCompara tesis)
        {
            OleDbConnection connection = new OleDbConnection(connectionString);

            string sSql;
            OleDbDataAdapter dataAdapter;

            DataSet dataSet = new DataSet();
            DataRow dr;

            try
            {
                string sqlCadena = "SELECT * FROM TesisCompara WHERE IdTesis = " + tesis.IdTesis;

                dataAdapter = new OleDbDataAdapter();
                dataAdapter.SelectCommand = new OleDbCommand(sqlCadena, connection);

                dataAdapter.Fill(dataSet, "TesisCompara");

                dr = dataSet.Tables["TesisCompara"].Rows[0];

                dr.BeginEdit();
                dr["TextoOriginal"] = tesis.TextoOriginal;
                dr["TOPlano"] = tesis.TOPlano;
                dr["TextoRevision1"] = tesis.TObservaciones;
                dr["TR1Plano"] = tesis.TObservacionesPlano;
                dr["TextoRevision2"] = tesis.TAprobada;
                dr["TR2Plano"] = tesis.TAprobadaPlano;

                dr.EndEdit();

                dataAdapter.UpdateCommand = connection.CreateCommand();

                sSql = "UPDATE TesisCompara SET TextoOriginal = @TextoOriginal, TOPlano = @TOPlano, TextoRevision1 = @TextoRevision1, TR1Plano = @TR1Plano," +
                       "TextoRevision2 = @TextoRevision2, TR2Plano = @TR2Plano" +
                       " WHERE IdTesis = @IdTesis";
                dataAdapter.UpdateCommand.CommandText = sSql;

                dataAdapter.UpdateCommand.Parameters.Add("@TextoOriginal", OleDbType.VarChar, 0, "TextoOriginal");
                dataAdapter.UpdateCommand.Parameters.Add("@TOPlano", OleDbType.VarChar, 0, "TOPlano");
                dataAdapter.UpdateCommand.Parameters.Add("@TextoRevision1", OleDbType.VarChar, 0, "TextoRevision1");
                dataAdapter.UpdateCommand.Parameters.Add("@TR1Plano", OleDbType.VarChar, 0, "TR1Plano");
                dataAdapter.UpdateCommand.Parameters.Add("@TextoRevision2", OleDbType.VarChar, 0, "TextoRevision2");
                dataAdapter.UpdateCommand.Parameters.Add("@TR2Plano", OleDbType.VarChar, 0, "TR2Plano");
                dataAdapter.UpdateCommand.Parameters.Add("@IdTesis", OleDbType.Numeric, 0, "IdTesis");

                dataAdapter.Update(dataSet, "TesisCompara");
                dataSet.Dispose();
                dataAdapter.Dispose();
            }
            catch (OleDbException ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;

                MessageBox.Show("Error ({0}) : {1}" + ex.Source + ex.Message, methodName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                ErrorUtilities.SetNewErrorMessage(ex, methodName, 0);
            }
            catch (Exception ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;

                MessageBox.Show("Error ({0}) : {1}" + ex.Source + ex.Message, methodName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                ErrorUtilities.SetNewErrorMessage(ex, methodName, 0);
            }
            finally
            {
                connection.Close();
            }
        }


    }
}
