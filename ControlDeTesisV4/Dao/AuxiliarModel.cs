using System;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Windows.Forms;
using ScjnUtilities;

namespace ControlDeTesisV4.Dao
{
    public class AuxiliarModel
    {
        static readonly string connectionString = ConfigurationManager.ConnectionStrings["Modulo"].ToString();

        public static int GetLastId(string tabla, string columna)
        {
            OleDbConnection connection = new OleDbConnection(connectionString);
            OleDbCommand cmd;
            OleDbDataReader reader = null;

            int id = 0;

            try
            {
                connection.Open();

                string sqlCadena = "SELECT MAX(" + columna + ") AS ID FROM " + tabla;

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

        /// <summary>
        /// Actualiza el estado de un documento dentro del proceso de publicacion
        /// 1. Recepcion
        /// 2. ENvio de Observaciones o envío del proyecto
        /// 3. Aprobación
        /// 4. Espera de Turno
        /// 5. Turno
        /// 6. Entrega
        /// 7. Recepcion
        /// 8. Publicación
        /// </summary>
        /// <param name="idDocumento">Identificador del documento que cambio de estado</param>
        /// <param name="estadoDocumento">Estado al que pasa el documento</param>
        /// <param name="tabla">Repositorio de datos del documento</param>
        /// <param name="identificador">Nombre del identificador dentro del repositorio</param>
        /// <param name="estado">Columna que se va a actualizar</param>
        public void UpdateEstadoDocumento(int idDocumento, int estadoDocumento, string tabla, string identificador, string estado)
        {
            OleDbConnection connection = new OleDbConnection(connectionString);

            string sSql;
            OleDbDataAdapter dataAdapter;

            DataSet dataSet = new DataSet();
            DataRow dr;

            try
            {
                string sqlCadena = "SELECT * FROM " + tabla + " WHERE " + identificador + " = " + idDocumento;

                dataAdapter = new OleDbDataAdapter();
                dataAdapter.SelectCommand = new OleDbCommand(sqlCadena, connection);

                dataAdapter.Fill(dataSet, tabla);

                dr = dataSet.Tables[tabla].Rows[0];
                dr.BeginEdit();

                dr[estado] = estadoDocumento;

                dr.EndEdit();

                dataAdapter.UpdateCommand = connection.CreateCommand();

                sSql = "UPDATE " + tabla + " SET  " + estado + " = @EstadoEjecutoria " +
                       " WHERE " + identificador + " = @IdEjecutoria";
                dataAdapter.UpdateCommand.CommandText = sSql;

                dataAdapter.UpdateCommand.Parameters.Add("@EstadoEjecutoria", OleDbType.Numeric, 0, estado);
                dataAdapter.UpdateCommand.Parameters.Add("@IdEjecutoria", OleDbType.Numeric, 0, identificador);

                dataAdapter.Update(dataSet, tabla);
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