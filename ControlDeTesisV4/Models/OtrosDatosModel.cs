using System;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data.OleDb;
using System.Linq;
using System.Windows;
using ControlDeTesisV4.Dao;

namespace ControlDeTesisV4.Models
{
    public class OtrosDatosModel
    {

        readonly string connectionString = ConfigurationManager.ConnectionStrings["Modulo"].ConnectionString;


        #region Asunto


        public ObservableCollection<OtrosDatos> GetTiposAsunto()
        {
            ObservableCollection<OtrosDatos> tiposAsunto = new ObservableCollection<OtrosDatos>();

            OleDbConnection oleConne = new OleDbConnection(connectionString);
            OleDbCommand cmd = null;
            OleDbDataReader reader = null;

            String sqlCadena = "SELECT * " +
                               "FROM TipoAsuntos ORDER BY IdTipoAsunto";

            try
            {
                oleConne.Open();

                cmd = new OleDbCommand(sqlCadena, oleConne);
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

            return tiposAsunto;
        }


        #endregion

        #region TiposJurisprudencia

        public ObservableCollection<OtrosDatos> GetTiposJuris()
        {
            ObservableCollection<OtrosDatos> tiposAsunto = new ObservableCollection<OtrosDatos>();

            OleDbConnection oleConne = new OleDbConnection(connectionString);
            OleDbCommand cmd = null;
            OleDbDataReader reader = null;

            String sqlCadena = "SELECT * " +
                               "FROM TipoJurisprudencia ORDER BY IdTipoJuris";

            try
            {
                oleConne.Open();

                cmd = new OleDbCommand(sqlCadena, oleConne);
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

            return tiposAsunto;
        }

        #endregion


        #region Areas Emisoras

        public ObservableCollection<OtrosDatos> GetAreasEmisoras()
        {
            ObservableCollection<OtrosDatos> tiposAsunto = new ObservableCollection<OtrosDatos>();

            OleDbConnection oleConne = new OleDbConnection(connectionString);
            OleDbCommand cmd = null;
            OleDbDataReader reader = null;

            String sqlCadena = "SELECT * " +
                               "FROM AreasEmisoras ORDER BY IdEmisor";

            try
            {
                oleConne.Open();

                cmd = new OleDbCommand(sqlCadena, oleConne);
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

            return tiposAsunto;
        }

        #endregion


        #region Instancias

        public ObservableCollection<OtrosDatos> GetInstancias()
        {
            ObservableCollection<OtrosDatos> instancias = new ObservableCollection<OtrosDatos>();

            OleDbConnection oleConne = new OleDbConnection(connectionString);
            OleDbCommand cmd = null;
            OleDbDataReader reader = null;

            String sqlCadena = "SELECT * " +
                               "FROM Instancias ORDER BY IdInstancia";

            try
            {
                oleConne.Open();

                cmd = new OleDbCommand(sqlCadena, oleConne);
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

            return instancias;
        }

        #endregion


        #region Votos

        public ObservableCollection<OtrosDatos> GetTipoDeVotos()
        {
            ObservableCollection<OtrosDatos> tipoDeVotos = new ObservableCollection<OtrosDatos>();

            OleDbConnection oleConne = new OleDbConnection(connectionString);
            OleDbCommand cmd = null;
            OleDbDataReader reader = null;

            String sqlCadena = "SELECT * " +
                               "FROM TipoVotos ORDER BY Id";

            try
            {
                oleConne.Open();

                cmd = new OleDbCommand(sqlCadena, oleConne);
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

            return tipoDeVotos;
        }


        #endregion
    }
}
