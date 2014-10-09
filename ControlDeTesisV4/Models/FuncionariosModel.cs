using System;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Windows.Forms;
using ControlDeTesisV4.Dao;
using ModuloInterconexionCommonApi;

namespace ControlDeTesisV4.Models
{
    public class FuncionariosModel
    {
        readonly string connectionString = ConfigurationManager.ConnectionStrings["Modulo"].ConnectionString;
        //readonly string mantesisConnectionString = ConfigurationManager.ConnectionStrings["MantesisSql"].ConnectionString;

        #region Ponentes

        public ObservableCollection<Funcionarios> GetPonentes()
        {
            ObservableCollection<Funcionarios> tiposAsunto = new ObservableCollection<Funcionarios>();

            OleDbConnection oleConne = new OleDbConnection(connectionString);
            OleDbCommand cmd = null;
            OleDbDataReader reader = null;

            String sqlCadena = "SELECT * " +
                               "FROM Ponentes ORDER BY IdPonente";

            try
            {
                oleConne.Open();

                cmd = new OleDbCommand(sqlCadena, oleConne);
                reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        tiposAsunto.Add(new Funcionarios(reader["IdPonente"] as int? ?? -1,
                            reader["Paterno"].ToString(),
                            reader["Materno"].ToString(),
                            reader["Nombre"].ToString(),
                            reader["Nombre"].ToString() + " " + reader["Paterno"].ToString() + " " + reader["Materno"].ToString()));
                    }
                }
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
                cmd.Dispose();
                reader.Close();
                oleConne.Close();
            }

            return tiposAsunto;
        }
        
        #endregion

        #region Signatarios

        public ObservableCollection<Funcionarios> GetSignatarios()
        {
            ObservableCollection<Funcionarios> listaSignatarios = new ObservableCollection<Funcionarios>();

            OleDbConnection oleConne = new OleDbConnection(connectionString);
            OleDbCommand cmd = null;
            OleDbDataReader reader = null;

            String sqlCadena = "SELECT * FROM Signatarios ORDER BY Nombre";

            try
            {
                oleConne.Open();

                cmd = new OleDbCommand(sqlCadena, oleConne);
                reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        listaSignatarios.Add(new Funcionarios(reader["IdSignatario"] as int? ?? -1,
                             reader["Paterno"].ToString(),
                            reader["Materno"].ToString(),
                            reader["Nombre"].ToString(),
                            reader["Nombre"].ToString() + " " + reader["Paterno"].ToString() + " " + reader["Materno"].ToString()));
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

            return listaSignatarios;
        }

        

        public int SetNewSignatario(Funcionarios funcionario)
        {
            OleDbConnection connection = new OleDbConnection(connectionString);

            string sSql;
            OleDbDataAdapter dataAdapter;

            DataSet dataSet = new DataSet();
            DataRow dr;

            try
            {
                string sqlCadena = "SELECT * FROM Signatarios WHERE IdSignatario= 0";

                dataAdapter = new OleDbDataAdapter();
                dataAdapter.SelectCommand = new OleDbCommand(sqlCadena, connection);

                dataAdapter.Fill(dataSet, "Signatarios");

                dr = dataSet.Tables["Signatarios"].NewRow();
                dr["Nombre"] = funcionario.NombreCompleto;
                
                dataSet.Tables["Signatarios"].Rows.Add(dr);

                dataAdapter.InsertCommand = connection.CreateCommand();

                sSql = "INSERT INTO Signatarios (Nombre) VALUES (@Nombre)";

                dataAdapter.InsertCommand.CommandText = sSql;

                dataAdapter.InsertCommand.Parameters.Add("@Nombre", OleDbType.VarChar, 0, "Nombre");

                dataAdapter.Update(dataSet, "Signatarios");
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

            return this.GetLastInsertId(funcionario.NombreCompleto);
        }

        private int GetLastInsertId(string nombre)
        {
            OleDbConnection connection = new OleDbConnection(connectionString);
            OleDbCommand cmd;
            OleDbDataReader reader = null;

            int idSignatario = 0;

            try
            {
                connection.Open();

                string sqlCadena = "SELECT IdSignatario FROM Signatarios WHERE nombre = @nombre";

                cmd = new OleDbCommand(sqlCadena, connection);
                cmd.Parameters.AddWithValue("@nombre", nombre);
                reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();

                    idSignatario = reader["IdSignatario"] as int? ?? -1;
                }
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
                reader.Close();
                connection.Close();
            }

            return idSignatario;
        }
        
        #endregion

        #region Abogados

        public ObservableCollection<Funcionarios> GetAbogados(int nivel)
        {
            ObservableCollection<Funcionarios> tiposAsunto = new ObservableCollection<Funcionarios>();

            OleDbConnection oleConne = new OleDbConnection(connectionString);
            OleDbCommand cmd = null;
            OleDbDataReader reader = null;

            String sqlCadena = "SELECT * " +
                               "FROM Abogados WHERE Nivel = @Nivel ORDER BY Nombre";

            try
            {
                oleConne.Open();

                cmd = new OleDbCommand(sqlCadena, oleConne);
                cmd.Parameters.AddWithValue("@Nivel", nivel);
                reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        tiposAsunto.Add(new Funcionarios(reader["IdAbogado"] as int? ?? -1,
                            "","","",
                            reader["Nombre"].ToString(),
                            reader["Nivel"] as int? ?? -1,
                            reader["Perfil"] as int? ?? -1));
                    }
                }
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
                cmd.Dispose();
                reader.Close();
                oleConne.Close();
            }

            return tiposAsunto;
        }

        #endregion
    }
}