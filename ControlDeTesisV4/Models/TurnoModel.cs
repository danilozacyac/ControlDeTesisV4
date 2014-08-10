using System;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Windows.Forms;
using ControlDeTesisV4.Dao;
using ModuloInterconexionCommonApi;

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
                dr["FTurnoInt"] = StringUtilities.DateToInt(turno.FTurno);
                dr["FSugerida"] = turno.FTurno.AddDays(5);
                dr["FSugeridaInt"] = StringUtilities.DateToInt(turno.FTurno.AddDays(5));
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

                MessageBox.Show("Error ({0}) : {1}" + ex.Source + ex.Message, methodName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Utilities.SetNewErrorMessage(ex, methodName, 0);
            }
            catch (Exception ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;

                MessageBox.Show("Error ({0}) : {1}" + ex.Source + ex.Message, methodName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Utilities.SetNewErrorMessage(ex, methodName, 0);
            }
            finally
            {
                connection.Close();
            }
        }
    }
}