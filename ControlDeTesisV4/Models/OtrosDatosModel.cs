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
        readonly string connectionStringDirectorio = ConfigurationManager.ConnectionStrings["Directorio"].ConnectionString;


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

            String sqlCadena = "SELECT * FROM AreasEmisoras ORDER BY IdEmisor";

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

        /// <summary>
        /// Obtiene el listado de plenos de circuito que se encuentran registrados en el Directorio del 
        /// Semanario Judicial de la Federación
        /// </summary>
        /// <returns></returns>
        public ObservableCollection<OtrosDatos> GetAreasEmisorasPlenos()
        {
            ObservableCollection<OtrosDatos> plenosDeCircuito = new ObservableCollection<OtrosDatos>();

            OleDbConnection connection = new OleDbConnection(connectionStringDirectorio);
            OleDbCommand cmd = null;
            OleDbDataReader reader = null;

            String sqlCadena = "SELECT IdOrg,Organismo,TpoOrg FROM Organismos WHERE TpoOrg = 4 ORDER BY Organismo";

            try
            {
                connection.Open();

                cmd = new OleDbCommand(sqlCadena, connection);
                reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        plenosDeCircuito.Add(new OtrosDatos(Convert.ToInt32(reader["IdOrg"]), 
                            4,
                            reader["Organismo"].ToString()));
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

            return plenosDeCircuito;
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



        public ObservableCollection<OtrosDatos> GetMeses()
        {
            ObservableCollection<OtrosDatos> meses = new ObservableCollection<OtrosDatos>();

            meses.Add(new OtrosDatos(1, "Enero"));
            meses.Add(new OtrosDatos(2, "Febrero"));
            meses.Add(new OtrosDatos(3, "Marzo"));
            meses.Add(new OtrosDatos(4, "Abril"));
            meses.Add(new OtrosDatos(5, "Mayo"));
            meses.Add(new OtrosDatos(6, "Junio"));
            meses.Add(new OtrosDatos(7, "Julio"));
            meses.Add(new OtrosDatos(8, "Agosto"));
            meses.Add(new OtrosDatos(9, "Septiembre"));
            meses.Add(new OtrosDatos(10, "Octubre"));
            meses.Add(new OtrosDatos(11, "Noviembre"));
            meses.Add(new OtrosDatos(12, "Diciembre"));

            return meses;
        }
    }
}
