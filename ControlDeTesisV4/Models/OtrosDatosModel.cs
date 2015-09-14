using System;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data.OleDb;
using System.Linq;
using ControlDeTesisV4.Dao;
using ScjnUtilities;

namespace ControlDeTesisV4.Models
{
    public class OtrosDatosModel
    {

        readonly string connectionString = ConfigurationManager.ConnectionStrings["Modulo"].ConnectionString;


        #region Asunto


        public ObservableCollection<OtrosDatos> GetTiposAsunto()
        {
            ObservableCollection<OtrosDatos> tiposAsunto = new ObservableCollection<OtrosDatos>();

            OleDbConnection connection = new OleDbConnection(connectionString);
            OleDbCommand cmd = null;
            OleDbDataReader reader = null;

            String sqlCadena = "SELECT * " +
                               "FROM TipoAsuntos ORDER BY IdTipoAsunto";

            try
            {
                connection.Open();

                cmd = new OleDbCommand(sqlCadena, connection);
                reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        tiposAsunto.Add(new OtrosDatos(reader["IdTipoAsunto"] as int? ?? -1, reader["DescAsunto"].ToString()));
                    }
                }
                cmd.Dispose();
                reader.Close();
            }
            catch (OleDbException ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                ErrorUtilities.SetNewErrorMessage(ex, methodName + " Exception,OtrosDatosModel", "ControlTesis");
            }
            catch (Exception ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                ErrorUtilities.SetNewErrorMessage(ex, methodName + " Exception,OtrosDatosModel", "ControlTesis");
            }
            finally
            {
                connection.Close();
            }

            return tiposAsunto;
        }


        #endregion

        #region TiposJurisprudencia

        public ObservableCollection<OtrosDatos> GetTiposJuris()
        {
            ObservableCollection<OtrosDatos> tiposAsunto = new ObservableCollection<OtrosDatos>();

            OleDbConnection connection = new OleDbConnection(connectionString);
            OleDbCommand cmd = null;
            OleDbDataReader reader = null;

            String sqlCadena = "SELECT * FROM TipoJurisprudencia ORDER BY IdTipoJuris";

            try
            {
                connection.Open();

                cmd = new OleDbCommand(sqlCadena, connection);
                reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        tiposAsunto.Add(new OtrosDatos(reader["IdTipoJuris"] as int? ?? -1, reader["TipoJurisprudencia"].ToString()));
                    }
                }

                cmd.Dispose();
                reader.Close();
            }
            catch (OleDbException ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                ErrorUtilities.SetNewErrorMessage(ex, methodName + " Exception,OtrosDatosModel", "ControlTesis");
            }
            catch (Exception ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                ErrorUtilities.SetNewErrorMessage(ex, methodName + " Exception,OtrosDatosModel", "ControlTesis");
            }
            finally
            {
                connection.Close();
            }

            return tiposAsunto;
        }

        #endregion


        #region Areas Emisoras

        public ObservableCollection<OtrosDatos> GetAreasEmisoras()
        {
            ObservableCollection<OtrosDatos> tiposAsunto = new ObservableCollection<OtrosDatos>();

            OleDbConnection connection = new OleDbConnection(connectionString);
            OleDbCommand cmd = null;
            OleDbDataReader reader = null;

            String sqlCadena = "SELECT * " +
                               "FROM AreasEmisoras ORDER BY IdEmisor";

            try
            {
                connection.Open();

                cmd = new OleDbCommand(sqlCadena, connection);
                reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        tiposAsunto.Add(new OtrosDatos(reader["IdEmisor"] as int? ?? -1, 
                            reader["IdInstancia"] as int? ?? -1,
                            reader["Emisor"].ToString()));
                    }
                }

                cmd.Dispose();
                reader.Close();
            }
            catch (OleDbException ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                ErrorUtilities.SetNewErrorMessage(ex, methodName + " Exception,OtrosDatosModel", "ControlTesis");
            }
            catch (Exception ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                ErrorUtilities.SetNewErrorMessage(ex, methodName + " Exception,OtrosDatosModel", "ControlTesis");
            }
            finally
            {
                connection.Close();
            }

            return tiposAsunto;
        }

        #endregion


        #region Instancias

        public ObservableCollection<OtrosDatos> GetInstancias()
        {
            ObservableCollection<OtrosDatos> instancias = new ObservableCollection<OtrosDatos>();

            OleDbConnection connection = new OleDbConnection(connectionString);
            OleDbCommand cmd = null;
            OleDbDataReader reader = null;

            String sqlCadena = "SELECT * FROM Instancias ORDER BY IdInstancia";

            try
            {
                connection.Open();

                cmd = new OleDbCommand(sqlCadena, connection);
                reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        instancias.Add(new OtrosDatos(reader["IdInstancia"] as int? ?? -1,
                            0,
                            reader["Instancia"].ToString()));
                    }
                }

                cmd.Dispose();
                reader.Close();
            }
            catch (OleDbException ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                ErrorUtilities.SetNewErrorMessage(ex, methodName + " Exception,OtrosDatosModel", "ControlTesis");
            }
            catch (Exception ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                ErrorUtilities.SetNewErrorMessage(ex, methodName + " Exception,OtrosDatosModel", "ControlTesis");
            }
            finally
            {
                connection.Close();
            }

            return instancias;
        }

        #endregion


        #region Votos

        public ObservableCollection<OtrosDatos> GetTipoDeVotos()
        {
            ObservableCollection<OtrosDatos> tipoDeVotos = new ObservableCollection<OtrosDatos>();

            OleDbConnection connection = new OleDbConnection(connectionString);
            OleDbCommand cmd = null;
            OleDbDataReader reader = null;

            String sqlCadena = "SELECT * FROM TipoVotos ORDER BY Id";

            try
            {
                connection.Open();

                cmd = new OleDbCommand(sqlCadena, connection);
                reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        tipoDeVotos.Add(new OtrosDatos(reader["Id"] as int? ?? -1,
                            reader["NumPersonas"] as int? ?? -1,
                            reader["TipoVoto"].ToString()));
                    }
                }

                cmd.Dispose();
                reader.Close();
            }
            catch (OleDbException ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                ErrorUtilities.SetNewErrorMessage(ex, methodName + " Exception,OtrosDatosModel", "ControlTesis");
            }
            catch (Exception ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                ErrorUtilities.SetNewErrorMessage(ex, methodName + " Exception,OtrosDatosModel", "ControlTesis");
            }
            finally
            {
                connection.Close();
            }

            return tipoDeVotos;
        }


        #endregion
    }
}
