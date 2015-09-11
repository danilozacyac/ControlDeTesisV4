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

        /// <summary>
        /// Obtiene el precedente de la tesis que se esta solicitando
        /// </summary>
        /// <param name="idTesis">Identificador de la tesis</param>
        /// <returns></returns>
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
                        precedente.IdPrecedente = Convert.ToInt32(reader["IdPrecedente"]);
                        precedente.IdTesis = idTesis;
                        precedente.TipoAsunto = Convert.ToInt32(reader["IdTipoAsunto"]);
                        precedente.NumAsunto = Convert.ToInt32(reader["NumAsunto"]);
                        precedente.YearAsunto = Convert.ToInt32(reader["YearAsunto"]);
                        precedente.FResolucion = DateTimeUtilities.GetDateFromReader(reader, "FResolucion");
                        precedente.IdPonente = Convert.ToInt32(reader["IdPonente"]);
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
                precedente.IdPrecedente = DataBaseUtilities.GetNextIdForUse("PrecedentesTesis", "IdPrecedente",connection);

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

        public void UpdatePrecedentes(PrecedentesTesis precedente, int idTesis)
        {
            OleDbConnection connection = new OleDbConnection(connectionString);

            string sSql;
            OleDbDataAdapter dataAdapter;

            DataSet dataSet = new DataSet();
            DataRow dr;

            try
            {
                string sqlCadena = "SELECT * FROM PrecedentesTesis WHERE IdPrecedente = " + precedente.IdPrecedente;

                dataAdapter = new OleDbDataAdapter();
                dataAdapter.SelectCommand = new OleDbCommand(sqlCadena, connection);

                dataAdapter.Fill(dataSet, "PrecedentesTesis");

                dr = dataSet.Tables["PrecedentesTesis"].Rows[0];
                dr.BeginEdit();
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

                dr.EndEdit();

                dataAdapter.UpdateCommand = connection.CreateCommand();

                sSql = "Update PrecedentesTesis SET IdTipoAsunto = @IdTipoAsunto, NumAsunto = @NumAsunto, YearAsunto = @YearAsunto, " +
                            "FResolucion = @FResolucion,FResolucionInt = @FResolucionInt,IdPonente = @IdPonente,Promovente = @Promovente " +
                            "WHERE IdPrecedente = @IdPrecedente ";

                dataAdapter.UpdateCommand.CommandText = sSql;

                dataAdapter.UpdateCommand.Parameters.Add("@IdTipoAsunto", OleDbType.Numeric, 0, "IdTipoAsunto");
                dataAdapter.UpdateCommand.Parameters.Add("@NumAsunto", OleDbType.Numeric, 0, "NumAsunto");
                dataAdapter.UpdateCommand.Parameters.Add("@YearAsunto", OleDbType.Numeric, 0, "YearAsunto");
                dataAdapter.UpdateCommand.Parameters.Add("@FResolucion", OleDbType.Date, 0, "FResolucion");
                dataAdapter.UpdateCommand.Parameters.Add("@FResolucionInt", OleDbType.Numeric, 0, "FResolucionInt");
                dataAdapter.UpdateCommand.Parameters.Add("@IdPonente", OleDbType.Numeric, 0, "IdPonente");
                dataAdapter.UpdateCommand.Parameters.Add("@Promovente", OleDbType.VarChar, 0, "Promovente");
                dataAdapter.UpdateCommand.Parameters.Add("@IdPrecedente", OleDbType.Numeric, 0, "IdPrecedente");

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

        /// <summary>
        /// Elimina los precedentes asociados a la tesis que se esta eliminando
        /// </summary>
        /// <param name="idTesis">Identificador de la tesis que se esta eliminando</param>
        public void DeletePrecedentes(int idTesis)
        {
            OleDbConnection connection = new OleDbConnection(connectionString);
            OleDbCommand cmd = new OleDbCommand();

            try
            {
                cmd = connection.CreateCommand();
                cmd.CommandText = "DELETE FROM PresedentesTesis WHERE IdTesis = @IdTesis";
                cmd.Parameters.AddWithValue("@IdTesis", idTesis);

                connection.Open();
                cmd.ExecuteNonQuery();
                connection.Close();
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
