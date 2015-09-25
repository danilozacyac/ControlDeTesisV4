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

            String sqlCadena = "SELECT * FROM Ponentes ORDER BY IdPonente";

            try
            {
                connection.Open();

                cmd = new OleDbCommand(sqlCadena, connection);
                reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Funcionarios nuevo = new Funcionarios(Convert.ToInt32(reader["IdPonente"]),
                            reader["Paterno"].ToString(),
                            reader["Materno"].ToString(),
                            reader["Nombre"].ToString(),
                            reader["Nombre"].ToString() + " " + reader["Paterno"].ToString() + " " + reader["Materno"].ToString());
                        nuevo.IdTipoFuncionario = Convert.ToInt16(reader["IdTipoPonente"]);
                        nuevo.Estado = Convert.ToInt16(reader["Estado"]);

                        tiposAsunto.Add(nuevo);
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

        /// <summary>
        /// Agrega un nuevo ponente a la base de datos
        /// </summary>
        /// <param name="funcionario">Ponente que se esta agregando</param>
        /// <param name="idTipoPonente">Indica si el ponente es de Corte o de Plenos de Circuito</param>
        public void SetNewPonente(Funcionarios funcionario, int idTipoPonente)
        {
            OleDbConnection connection = new OleDbConnection(connectionString);

            string sSql;
            OleDbDataAdapter dataAdapter;

            DataSet dataSet = new DataSet();
            DataRow dr;

            try
            {
                funcionario.IdFuncionario = DataBaseUtilities.GetNextIdForUse("Ponentes", "IdPonente", connection);

                string sqlCadena = "SELECT * FROM Ponentes WHERE IdPonente = 0";

                dataAdapter = new OleDbDataAdapter();
                dataAdapter.SelectCommand = new OleDbCommand(sqlCadena, connection);

                dataAdapter.Fill(dataSet, "Ponentes");

                dr = dataSet.Tables["Ponentes"].NewRow();
                dr["Paterno"] = funcionario.Paterno;
                dr["Materno"] = funcionario.Materno;
                dr["Nombre"] = funcionario.Nombre;
                dr["IdTipoPonente"] = idTipoPonente;
                dr["Estado"] = 1;

                dataSet.Tables["Ponentes"].Rows.Add(dr);

                dataAdapter.InsertCommand = connection.CreateCommand();

                sSql = "INSERT INTO Ponentes (Paterno,Materno,Nombre,IdTipoPonente,Estado) VALUES (@Paterno,@Materno,@Nombre,@IdTipoPonente,@Estado)";

                dataAdapter.InsertCommand.CommandText = sSql;

                dataAdapter.InsertCommand.Parameters.Add("@Paterno", OleDbType.VarChar, 0, "Paterno");
                dataAdapter.InsertCommand.Parameters.Add("@Materno", OleDbType.VarChar, 0, "Materno");
                dataAdapter.InsertCommand.Parameters.Add("@Nombre", OleDbType.VarChar, 0, "Nombre");
                dataAdapter.InsertCommand.Parameters.Add("@IdTipoPonente", OleDbType.Numeric, 0, "IdTipoPonente");
                dataAdapter.InsertCommand.Parameters.Add("@Estado", OleDbType.Numeric, 0, "Estado");

                dataAdapter.Update(dataSet, "Ponentes");
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
                        Funcionarios nuevoFuncionario = new Funcionarios(reader["IdSignatario"] as int? ?? -1,
                             reader["Paterno"].ToString(),
                            reader["Materno"].ToString(),
                            reader["Nombre"].ToString(),
                            reader["Nombre"].ToString() + " " + reader["Paterno"].ToString() + " " + reader["Materno"].ToString());
                        nuevoFuncionario.IdTipoFuncionario = Convert.ToInt32(reader["IdTipoSignatario"]);

                        listaSignatarios.Add(nuevoFuncionario);

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
                ErrorUtilities.SetNewErrorMessage(ex, methodName + " Exception,FuncionariosModel", "ControlTesis");
            }
            finally
            {
                connection.Close();
            }

            return listaSignatarios;
        }


        public void SetNewSignatario(Funcionarios funcionario, int idTipoSignatario)
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
                dr["Paterno"] = funcionario.Paterno;
                dr["Materno"] = funcionario.Materno;
                dr["Nombre"] = funcionario.Nombre;
                dr["IdTipoSignatario"] = idTipoSignatario;
                
                dataSet.Tables["Signatarios"].Rows.Add(dr);

                dataAdapter.InsertCommand = connection.CreateCommand();

                sSql = "INSERT INTO Signatarios (Paterno,Materno,Nombre,IdTipoSignatario) VALUES (@Paterno,@Materno,@Nombre,@IdTipoSignatario)";

                dataAdapter.InsertCommand.CommandText = sSql;

                dataAdapter.InsertCommand.Parameters.Add("@Paterno", OleDbType.VarChar, 0, "Paterno");
                dataAdapter.InsertCommand.Parameters.Add("@Materno", OleDbType.VarChar, 0, "Materno");
                dataAdapter.InsertCommand.Parameters.Add("@Nombre", OleDbType.VarChar, 0, "Nombre");
                dataAdapter.InsertCommand.Parameters.Add("@IdTipoSignatario", OleDbType.Numeric, 0, "IdTipoSignatario");

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