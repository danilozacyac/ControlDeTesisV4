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
    public class PrecedentesModel
    {

        readonly string connectionString = ConfigurationManager.ConnectionStrings["Modulo"].ToString();

        public PrecedentesTesis GetPrecedenteTesis(int idTesis)
        {
            PrecedentesTesis precedente = new PrecedentesTesis();

            OleDbConnection oleConne = new OleDbConnection(connectionString);
            OleDbCommand cmd = null;
            OleDbDataReader reader = null;

            String sqlCadena = "SELECT * FROM PrecedentesTesis WHERE IdTesis = @IdTesis";

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
                        precedente.IdPrecedente = reader["IdPrecedente"] as int? ?? -1;
                        precedente.IdTesis = idTesis;
                        precedente.TipoAsunto = reader["IdTipoAsunto"] as int? ?? -1;
                        precedente.NumAsunto = reader["NumAsunto"] as int? ?? -1;
                        precedente.YearAsunto = reader["YearAsunto"] as int? ?? -1;
                        precedente.FResolucion = DateTimeUtilities.GetDateFromReader(reader, "FResolucion");
                        precedente.IdPonente = reader["IdPonente"] as int? ?? -1;
                        precedente.Promovente = reader["Promovente"].ToString();
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

            return precedente;
        }

        public void SetPrecedentes(PrecedentesTesis precedente, int idTesis)
        {
            OleDbConnection connection = new OleDbConnection(connectionString);

            string sSql;
            OleDbDataAdapter dataAdapter;

            DataSet dataSet = new DataSet();
            DataRow dr;

            try
            {
                string sqlCadena = "SELECT * FROM PrecedentesTesis WHERE IdPrecedente = 0";
                precedente.IdPrecedente = this.GetLastId("PrecedentesTesis", "IdPrecedente");

                dataAdapter = new OleDbDataAdapter();
                dataAdapter.SelectCommand = new OleDbCommand(sqlCadena, connection);

                dataAdapter.Fill(dataSet, "PrecedentesTesis");

                dr = dataSet.Tables["PrecedentesTesis"].NewRow();
                dr["IdPrecedente"] = precedente.IdPrecedente;
                dr["IdTesis"] = idTesis;
                dr["IdTipoAsunto"] = precedente.TipoAsunto;
                dr["NumAsunto"] = precedente.NumAsunto;
                dr["YearAsunto"] = precedente.YearAsunto;

                if (precedente.FResolucion != null)
                {
                    dr["FResolucion"] = precedente.FResolucion;
                    dr["FResolucionInt"] = DateTimeUtilities.DateToInt(precedente.FResolucion);
                }
                else
                {
                    dr["FResolucion"] = DBNull.Value;
                    dr["FResolucionInt"] = 0;
                }

                dr["IdPonente"] = precedente.IdPonente;
                dr["Promovente"] = precedente.Promovente;

                dataSet.Tables["PrecedentesTesis"].Rows.Add(dr);

                dataAdapter.InsertCommand = connection.CreateCommand();

                sSql = "INSERT INTO PrecedentesTesis (IdPrecedente,IdTesis,IdTipoAsunto,NumAsunto,YearAsunto,FResolucion,FResolucionInt,IdPonente,Promovente) " +
                       " VALUES (@IdPrecedente,@IdTesis,@IdTipoAsunto,@NumAsunto,@YearAsunto,@FResolucion,@FResolucionInt,@IdPonente,@Promovente)";

                dataAdapter.InsertCommand.CommandText = sSql;

                dataAdapter.InsertCommand.Parameters.Add("@IdPrecedente", OleDbType.Numeric, 0, "IdPrecedente");
                dataAdapter.InsertCommand.Parameters.Add("@IdTesis", OleDbType.Numeric, 0, "IdTesis");
                dataAdapter.InsertCommand.Parameters.Add("@IdTipoAsunto", OleDbType.Numeric, 0, "IdTipoAsunto");
                dataAdapter.InsertCommand.Parameters.Add("@NumAsunto", OleDbType.Numeric, 0, "NumAsunto");
                dataAdapter.InsertCommand.Parameters.Add("@YearAsunto", OleDbType.Numeric, 0, "YearAsunto");
                dataAdapter.InsertCommand.Parameters.Add("@FResolucion", OleDbType.Date, 0, "FResolucion");
                dataAdapter.InsertCommand.Parameters.Add("@FResolucionInt", OleDbType.Numeric, 0, "FResolucionInt");
                dataAdapter.InsertCommand.Parameters.Add("@IdPonente", OleDbType.Numeric, 0, "IdPonente");
                dataAdapter.InsertCommand.Parameters.Add("@Promovente", OleDbType.VarChar, 0, "Promovente");

                dataAdapter.Update(dataSet, "PrecedentesTesis");
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


        private int GetLastId(string tabla, string columna)
        {
            OleDbConnection connection = new OleDbConnection(connectionString);
            OleDbCommand cmd;
            OleDbDataReader reader = null;

            int id = 0;

            try
            {
                connection.Open();

                string sqlCadena = "SELECT MAX(" + columna + " ) AS ID FROM " + tabla;

                cmd = new OleDbCommand(sqlCadena, connection);
                reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();

                    id = reader["ID"] as int? ?? -1;
                }
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
                reader.Close();
                connection.Close();
            }

            return id + 1;
        }

    }
}
