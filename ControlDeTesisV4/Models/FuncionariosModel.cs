using System;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using ControlDeTesisV4.Dao;
using ScjnUtilities;

namespace ControlDeTesisV4.Models
{
    public class FuncionariosModel
    {
        readonly string connectionString = ConfigurationManager.ConnectionStrings["Modulo"].ConnectionString;

        #region Ponentes

        public ObservableCollection<Funcionarios> GetPonentes()
        {
            ObservableCollection<Funcionarios> tiposAsunto = new ObservableCollection<Funcionarios>();

            OleDbConnection connection = new OleDbConnection(connectionString);
            OleDbCommand cmd = null;
            OleDbDataReader reader = null;

            String sqlCadena = "SELECT * " +
                               "FROM Ponentes ORDER BY IdPonente";

            try
            {
                connection.Open();

                cmd = new OleDbCommand(sqlCadena, connection);
                reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        tiposAsunto.Add(new Funcionarios(Convert.ToInt32(reader["IdPonente"]),
                            reader["Paterno"].ToString(),
                            reader["Materno"].ToString(),
                            reader["Nombre"].ToString(),
                            reader["Nombre"].ToString() + " " + reader["Paterno"].ToString() + " " + reader["Materno"].ToString()));
                    }
                }
                cmd.Dispose();
                reader.Close();
            }
            catch (OleDbException ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                ErrorUtilities.SetNewErrorMessage(ex, methodName + " Exception,FuncionariosModel", "ControlTesis");
            }
            catch (Exception ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                ErrorUtilities.SetNewErrorMessage(ex, methodName + " Exception,FuncionarioModel", "ControlTesis");
            }
            finally
            {
                connection.Close();
            }

            return tiposAsunto;
        }
        
        #endregion

        #region Signatarios

        public ObservableCollection<Funcionarios> GetSignatarios()
        {
            ObservableCollection<Funcionarios> listaSignatarios = new ObservableCollection<Funcionarios>();

            OleDbConnection connection = new OleDbConnection(connectionString);
            OleDbCommand cmd = null;
            OleDbDataReader reader = null;

            String sqlCadena = "SELECT * FROM Signatarios ORDER BY Nombre";

            try
            {
                connection.Open();

                cmd = new OleDbCommand(sqlCadena, connection);
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
            catch (OleDbException ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                ErrorUtilities.SetNewErrorMessage(ex, methodName + " Exception,FuncionariosModel", "ControlTesis");
            }
            catch (Exception ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                ErrorUtilities.SetNewErrorMessage(ex, methodName + " Exception,FuncionarioModel", "ControlTesis");
            }
            finally
            {
                connection.Close();
            }

            return listaSignatarios;
        }


        public void SetNewSignatario(Funcionarios funcionario)
        {
            OleDbConnection connection = new OleDbConnection(connectionString);

            string sSql;
            OleDbDataAdapter dataAdapter;

            DataSet dataSet = new DataSet();
            DataRow dr;

            try
            {
                funcionario.IdFuncionario = DataBaseUtilities.GetNextIdForUse("Signatarios", "IdSignatario", connection);

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
                ErrorUtilities.SetNewErrorMessage(ex, methodName + " Exception,FuncionariosModel", "ControlTesis");
            }
            catch (Exception ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                ErrorUtilities.SetNewErrorMessage(ex, methodName + " Exception,FuncionarioModel", "ControlTesis");
            }
            finally
            {
                connection.Close();
            }

            
        }
        
        #endregion

        #region Abogados

        public ObservableCollection<Funcionarios> GetAbogados(int nivel)
        {
            ObservableCollection<Funcionarios> tiposAsunto = new ObservableCollection<Funcionarios>();

            OleDbConnection connection = new OleDbConnection(connectionString);
            OleDbCommand cmd = null;
            OleDbDataReader reader = null;

            String sqlCadena = "SELECT * " +
                               "FROM Abogados WHERE Nivel = @Nivel ORDER BY Nombre";

            try
            {
                connection.Open();

                cmd = new OleDbCommand(sqlCadena, connection);
                cmd.Parameters.AddWithValue("@Nivel", nivel);
                reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        tiposAsunto.Add(new Funcionarios(Convert.ToInt32(reader["IdAbogado"]),
                            "","","",
                            reader["Nombre"].ToString(),
                            reader["Nivel"] as int? ?? -1,
                            reader["Perfil"] as int? ?? -1));
                    }
                }
                cmd.Dispose();
                reader.Close();
            }
            catch (OleDbException ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                ErrorUtilities.SetNewErrorMessage(ex, methodName + " Exception,FuncionariosModel", "ControlTesis");
            }
            catch (Exception ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                ErrorUtilities.SetNewErrorMessage(ex, methodName + " Exception,FuncionarioModel", "ControlTesis");
            }
            finally
            {
                connection.Close();
            }

            return tiposAsunto;
        }

        #endregion
    }
}