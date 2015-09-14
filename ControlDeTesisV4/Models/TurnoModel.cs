using System;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using ControlDeTesisV4.Dao;
using ScjnUtilities;

namespace ControlDeTesisV4.Models
{
    public class TurnoModel
    {
        readonly string connectionString = ConfigurationManager.ConnectionStrings["Modulo"].ToString();

        public void SetNewTurno(TurnoDao turno)
        {
            OleDbConnection connection = new OleDbConnection(connectionString);

            string sSql;
            OleDbDataAdapter dataAdapter;

            DataSet dataSet = new DataSet();
            DataRow dr;

            try
            {
                string sqlCadena = "SELECT * FROM Turno WHERE IdTurno = 0";

                dataAdapter = new OleDbDataAdapter();
                dataAdapter.SelectCommand = new OleDbCommand(sqlCadena, connection);

                dataAdapter.Fill(dataSet, "Turno");

                dr = dataSet.Tables["Turno"].NewRow();
                dr["IdAbogado"] = turno.IdAbogado;
                dr["idTipoDocto"] = turno.IdTipoDocto;
                dr["IdDocto"] = turno.IdDocto;
                dr["NumPaginas"] = turno.NumPaginas;
                dr["FTurno"] = turno.FTurno;
                dr["FTurnoInt"] = DateTimeUtilities.DateToInt(turno.FTurno);
                dr["FSugerida"] = turno.FSugerida;
                dr["FSugeridaInt"] = DateTimeUtilities.DateToInt(turno.FSugerida);
                dr["EnTiempo"] = 1;
                dr["DiasAtraso"] = 0;
                dr["Returno"] = 0;
                dr["Observaciones"] = turno.Observaciones;

                dataSet.Tables["Turno"].Rows.Add(dr);

                dataAdapter.InsertCommand = connection.CreateCommand();

                sSql = "INSERT INTO Turno (IdAbogado,IdTipoDocto,IdDocto,NumPaginas,FTurno,FTurnoInt,FSugerida," +
                       "FSugeridaInt,EnTiempo,DiasAtraso,Returno,Observaciones) " +
                       " VALUES (@IdAbogado,@IdTipoDocto,@IdDocto,@NumPaginas,@FTurno,@FTurnoInt,@FSugerida," +
                       "@FSugeridaInt,@EnTiempo,@DiasAtraso,@Returno,@Observaciones)";

                dataAdapter.InsertCommand.CommandText = sSql;

                dataAdapter.InsertCommand.Parameters.Add("@IdAbogado", OleDbType.Numeric, 0, "IdAbogado");
                dataAdapter.InsertCommand.Parameters.Add("@IdTipoDocto", OleDbType.Numeric, 0, "IdTipoDocto");
                dataAdapter.InsertCommand.Parameters.Add("@IdDocto", OleDbType.Numeric, 0, "IdDocto");
                dataAdapter.InsertCommand.Parameters.Add("@NumPaginas", OleDbType.Numeric, 0, "NumPaginas");
                dataAdapter.InsertCommand.Parameters.Add("@FTurno", OleDbType.Date, 0, "FTurno");
                dataAdapter.InsertCommand.Parameters.Add("@FTurnoInt", OleDbType.Numeric, 0, "FTurnoInt");
                dataAdapter.InsertCommand.Parameters.Add("@FSugerida", OleDbType.Date, 0, "FSugerida");
                dataAdapter.InsertCommand.Parameters.Add("@FSugeridaInt", OleDbType.Numeric, 0, "FSugeridaInt");
                dataAdapter.InsertCommand.Parameters.Add("@EnTiempo", OleDbType.Numeric, 0, "EnTiempo");
                dataAdapter.InsertCommand.Parameters.Add("@DiasAtraso", OleDbType.Numeric, 0, "DiasAtraso");
                dataAdapter.InsertCommand.Parameters.Add("@Returno", OleDbType.Numeric, 0, "Returno");
                dataAdapter.InsertCommand.Parameters.Add("@Observaciones", OleDbType.VarChar, 0, "Observaciones");

                dataAdapter.Update(dataSet, "Turno");
                dataSet.Dispose();
                dataAdapter.Dispose();
            }
            catch (OleDbException ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                ErrorUtilities.SetNewErrorMessage(ex, methodName + " Exception,TurnoModel", "ControlTesis");
            }
            catch (Exception ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                ErrorUtilities.SetNewErrorMessage(ex, methodName + " Exception,TurnoModel", "ControlTesis");
            }
            finally
            {
                connection.Close();
            }
        }

        public void SetNewReturno(TurnoDao turnoActual,TurnoDao turnoAnterior)
        {
            OleDbConnection connection = new OleDbConnection(connectionString);

            string sSql;
            OleDbDataAdapter dataAdapter;

            DataSet dataSet = new DataSet();
            DataRow dr;

            try
            {
                string sqlCadena = "SELECT * FROM Returno WHERE Id = 0";

                dataAdapter = new OleDbDataAdapter();
                dataAdapter.SelectCommand = new OleDbCommand(sqlCadena, connection);

                dataAdapter.Fill(dataSet, "Returno");

                dr = dataSet.Tables["Returno"].NewRow();
                dr["IdTurno"] = turnoAnterior.IdTurno;
                dr["IdAbogadoAnt"] = turnoAnterior.IdAbogado;
                dr["IdAbogadoNue"] = turnoActual.IdAbogado;
                dr["FReturno"] = DateTime.Now;
                dr["FReturnoInt"] = DateTimeUtilities.DateToInt(DateTime.Now);
                dr["MantieneFecha"] = 0;
                dr["FAnterior"] = turnoAnterior.FSugerida;
                dr["FAnteriorInt"] = DateTimeUtilities.DateToInt(turnoAnterior.FSugerida);
                dr["FNueva"] = turnoActual.FSugerida;
                dr["FNuevaInt"] = DateTimeUtilities.DateToInt(turnoActual.FSugerida); 
                dr["Observaciones"] = turnoActual.Observaciones;
                dr["IdAbogadoAuth"] = AccesoUsuarios.Llave;

                dataSet.Tables["Returno"].Rows.Add(dr);

                dataAdapter.InsertCommand = connection.CreateCommand();

                sSql = "INSERT INTO Returno (IdTurno,IdAbogadoAnt,IdAbogadoNue,FReturno,FReturnoInt,MantieneFecha,FAnterior," +
                       "FAnteriorInt,FNueva,FNuevaInt,Observaciones,IdAbogadoAuth) " +
                       " VALUES (@IdTurno,@IdAbogadoAnt,@IdAbogadoNue,@FReturno,@FReturnoInt,@MantieneFecha,@FAnterior," +
                       "@FAnteriorInt,@FNueva,@FNuevaInt,@Observaciones,@IdAbogadoAuth)";

                dataAdapter.InsertCommand.CommandText = sSql;

                dataAdapter.InsertCommand.Parameters.Add("@IdTurno", OleDbType.Numeric, 0, "IdTurno");
                dataAdapter.InsertCommand.Parameters.Add("@IdAbogadoAnt", OleDbType.Numeric, 0, "IdAbogadoAnt");
                dataAdapter.InsertCommand.Parameters.Add("@IdAbogadoNue", OleDbType.Numeric, 0, "IdAbogadoNue");
                dataAdapter.InsertCommand.Parameters.Add("@FReturno", OleDbType.Numeric, 0, "FReturno");
                dataAdapter.InsertCommand.Parameters.Add("@FReturnoInt", OleDbType.Date, 0, "FReturnoInt");
                dataAdapter.InsertCommand.Parameters.Add("@MantieneFecha", OleDbType.Numeric, 0, "MantieneFecha");
                dataAdapter.InsertCommand.Parameters.Add("@FAnterior", OleDbType.Date, 0, "FAnterior");
                dataAdapter.InsertCommand.Parameters.Add("@FAnteriorInt", OleDbType.Numeric, 0, "FAnteriorInt");
                dataAdapter.InsertCommand.Parameters.Add("@FNueva", OleDbType.Numeric, 0, "FNueva");
                dataAdapter.InsertCommand.Parameters.Add("@FNuevaInt", OleDbType.Numeric, 0, "FNuevaInt");
                dataAdapter.InsertCommand.Parameters.Add("@Observaciones", OleDbType.Numeric, 0, "Observaciones");
                dataAdapter.InsertCommand.Parameters.Add("@IdAbogadoAuth", OleDbType.VarChar, 0, "IdAbogadoAuth");

                dataAdapter.Update(dataSet, "Returno");
                dataSet.Dispose();
                dataAdapter.Dispose();
            }
            catch (OleDbException ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                ErrorUtilities.SetNewErrorMessage(ex, methodName + " Exception,TurnoModel", "ControlTesis");
            }
            catch (Exception ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                ErrorUtilities.SetNewErrorMessage(ex, methodName + " Exception,TurnoModel", "ControlTesis");
            }
            finally
            {
                connection.Close();
            }
        }

        public void UpdateTurno(TurnoDao turno)
        {
            OleDbConnection connection = new OleDbConnection(connectionString);

            string sSql;
            OleDbDataAdapter dataAdapter;

            DataSet dataSet = new DataSet();
            DataRow dr;

            try
            {
                string sqlCadena = "SELECT * FROM Turno WHERE IdTurno = " + turno.IdTurno;
                dataAdapter = new OleDbDataAdapter();
                dataAdapter.SelectCommand = new OleDbCommand(sqlCadena, connection);

                dataAdapter.Fill(dataSet, "Turno");

                dr = dataSet.Tables["Turno"].Rows[0];
                dr.BeginEdit();

                dr["IdAbogado"] = turno.IdAbogado;
                dr["NumPaginas"] = turno.NumPaginas;
                dr["FTurno"] = turno.FTurno;
                dr["FTurnoInt"] = DateTimeUtilities.DateToInt(turno.FTurno);
                dr["FSugerida"] = turno.FSugerida;
                dr["FSugeridaInt"] = DateTimeUtilities.DateToInt(turno.FSugerida);
                dr["EnTiempo"] = 1;
                dr["DiasAtraso"] = 0;
                dr["Returno"] = 1;
                dr["Observaciones"] = turno.Observaciones;

                dr.EndEdit();

                dataAdapter.UpdateCommand = connection.CreateCommand();

                sSql = "UPDATE Turno SET IdAbogado = @IdAbogado,FTurno = @FTurno,FTurnoInt = @FTurnoInt,FSugerida = @FSugeridaInt," +
                       " EnTiempo = @EnTiempo,DiasAtraso = @DiasAtraso,Returno = @Returno " +
                       " WHERE IdTurno = @IdTurno";

                dataAdapter.UpdateCommand.CommandText = sSql;

                dataAdapter.UpdateCommand.Parameters.Add("@IdAbogado", OleDbType.Numeric, 0, "IdAbogado");
                dataAdapter.UpdateCommand.Parameters.Add("@FTurno", OleDbType.Date, 0, "FTurno");
                dataAdapter.UpdateCommand.Parameters.Add("@FTurnoInt", OleDbType.Numeric, 0, "FTurnoInt");
                dataAdapter.UpdateCommand.Parameters.Add("@FSugerida", OleDbType.Date, 0, "FSugerida");
                dataAdapter.UpdateCommand.Parameters.Add("@FSugeridaInt", OleDbType.Numeric, 0, "FSugeridaInt");
                dataAdapter.UpdateCommand.Parameters.Add("@EnTiempo", OleDbType.Numeric, 0, "EnTiempo");
                dataAdapter.UpdateCommand.Parameters.Add("@DiasAtraso", OleDbType.Numeric, 0, "DiasAtraso");
                dataAdapter.UpdateCommand.Parameters.Add("@Returno", OleDbType.Numeric, 0, "Returno");
                dataAdapter.UpdateCommand.Parameters.Add("@IdTurno", OleDbType.Numeric, 0, "IdTurno");

                dataAdapter.Update(dataSet, "Turno");
                dataSet.Dispose();
                dataAdapter.Dispose();
            }
            catch (OleDbException ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                ErrorUtilities.SetNewErrorMessage(ex, methodName + " Exception,TurnoModel", "ControlTesis");
            }
            catch (Exception ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                ErrorUtilities.SetNewErrorMessage(ex, methodName + " Exception,TurnoModel", "ControlTesis");
            }
            finally
            {
                connection.Close();
            }
        }


        public TurnoDao GetTurno(int idTipoDocto, int idDocto)
        {
            TurnoDao turno = null;

            OleDbConnection connection = new OleDbConnection(connectionString);
            OleDbCommand cmd = null;
            OleDbDataReader reader = null;

            String sqlCadena = "SELECT * FROM Turno WHERE IdTipoDocto = @IdTipoDocto AND IdDocto = @IdDocto";

            try
            {
                connection.Open();

                cmd = new OleDbCommand(sqlCadena, connection);
                cmd.Parameters.AddWithValue("@IdTipoDocto", idTipoDocto);
                cmd.Parameters.AddWithValue("@IdDocto", idDocto);
                reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        turno = new TurnoDao();
                        turno.IdTurno = reader["IdTurno"] as int? ?? -1;
                        turno.IdAbogado = reader["IdAbogado"] as int? ?? -1;
                        turno.IdTipoDocto = reader["IdTipoDocto"] as int? ?? -1;
                        turno.IdDocto = reader["IdDocto"] as int? ?? -1;
                        turno.NumPaginas = reader["NumPaginas"] as int? ?? -1;
                        turno.FTurno = DateTimeUtilities.GetDateFromReader(reader, "FTurno");
                        turno.FEntrega = DateTimeUtilities.GetDateFromReader(reader, "FEntrega");
                        turno.FSugerida = DateTimeUtilities.GetDateFromReader(reader, "FSugerida");
                        turno.EnTiempo = reader["EnTiempo"] as int? ?? -1;
                        turno.DiasAtraso = reader["DiasAtraso"] as int? ?? -1;
                        turno.IsReturn = reader["Returno"] as int? ?? -1;
                    }
                }
                cmd.Dispose();
                reader.Close();
            }
            catch (OleDbException ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                ErrorUtilities.SetNewErrorMessage(ex, methodName + " Exception,TurnoModel", "ControlTesis");
            }
            catch (Exception ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                ErrorUtilities.SetNewErrorMessage(ex, methodName + " Exception,TurnoModel", "ControlTesis");
            }
            finally
            {
                connection.Close();
            }

            return turno;
        }


    }
}