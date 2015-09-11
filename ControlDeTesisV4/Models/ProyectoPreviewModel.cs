using System;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data.OleDb;
using System.Linq;
using System.Windows;
using ControlDeTesisV4.Dao;
using ControlDeTesisV4.Singletons;
using ScjnUtilities;

namespace ControlDeTesisV4.Models
{
    public class ProyectoPreviewModel
    {
        readonly string connectionString = ConfigurationManager.ConnectionStrings["Modulo"].ToString();

        /// <summary>
        /// Regresa el listado de las tesis que aún no han sido turnadas
        /// </summary>
        /// <param name="idTipoProyecto"></param>
        /// <returns></returns>
        public ObservableCollection<ProyectoPreview> GetPreviewSalasSinTurnar(int idTipoProyecto)
        {
            ObservableCollection<ProyectoPreview> listaProyectosSalas = new ObservableCollection<ProyectoPreview>();

            OleDbConnection oleConne = new OleDbConnection(connectionString);
            OleDbCommand cmd = null;
            OleDbDataReader reader = null;

            String sqlCadena = "SELECT * FROM ProyectosTesis WHERE EstadoTesis < 4 AND IdTipoProyecto = " + idTipoProyecto;

            try
            {
                oleConne.Open();

                cmd = new OleDbCommand(sqlCadena, oleConne);
                reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        ProyectoPreview preview = new ProyectoPreview();
                        preview.IdTesis = Convert.ToInt32(reader["IdTesis"]);
                        preview.IdProyecto = Convert.ToInt32(reader["IdProyecto"]);
                        preview.IdTipoProyecto = idTipoProyecto;
                        preview.Rubro = reader["Rubro"].ToString();
                        preview.Tatj = Convert.ToInt32(reader["tatj"]);
                        preview.EstadoTesis = Convert.ToInt32(reader["EstadoTesis"]);
                        preview.IdAbogadoResponsable = Convert.ToInt32(reader["IdAbogado"]);

                        if (idTipoProyecto == 1)
                            this.GetProyectoInfoPreview(preview);
                        else if (idTipoProyecto == 2)
                            this.GetProyectoInfoPreviewCcst(preview);

                        this.GetPrecedenteInfoPreview(preview);
                        listaProyectosSalas.Add(preview);
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
            
            return listaProyectosSalas;
        }

        private void GetProyectoInfoPreview(ProyectoPreview preview)
        {
            OleDbConnection oleConne = new OleDbConnection(connectionString);
            OleDbCommand cmd = null;
            OleDbDataReader reader = null;

            String sqlCadena = "SELECT * FROM Proyectos WHERE IdProyecto = @IdProyecto";

            try
            {
                oleConne.Open();

                cmd = new OleDbCommand(sqlCadena, oleConne);
                cmd.Parameters.AddWithValue("@IdProyecto", preview.IdProyecto);
                reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        preview.FRecepcion = DateTimeUtilities.GetDateFromReader(reader,"FRecepcion");
                        preview.OficioRecepcion = reader["OficioRecepcion"].ToString();
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
        }

        private void GetProyectoInfoPreviewCcst(ProyectoPreview preview)
        {
            OleDbConnection oleConne = new OleDbConnection(connectionString);
            OleDbCommand cmd = null;
            OleDbDataReader reader = null;

            String sqlCadena = "SELECT * FROM ProyectosCcst WHERE IdProyecto = @IdProyecto";

            try
            {
                oleConne.Open();

                cmd = new OleDbCommand(sqlCadena, oleConne);
                cmd.Parameters.AddWithValue("@IdProyecto", preview.IdProyecto);
                reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        preview.FRecepcion = Convert.ToDateTime(reader["FechaOficioAtn"]);
                        preview.OficioRecepcion = reader["OficioAtn"].ToString();
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
        }

        private void GetPrecedenteInfoPreview(ProyectoPreview preview)
        {
            OleDbConnection oleConne = new OleDbConnection(connectionString);
            OleDbCommand cmd = null;
            OleDbDataReader reader = null;

            String sqlCadena = "SELECT * " +
                               "FROM PrecedentesTesis WHERE IdTesis = @IdTesis";

            try
            {
                oleConne.Open();

                cmd = new OleDbCommand(sqlCadena, oleConne);
                cmd.Parameters.AddWithValue("@IdTesis", preview.IdTesis);
                reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        int tipoAsunto = Convert.ToInt32(reader["IdTipoAsunto"]);
                        preview.Asunto = (from n in OtrosDatosSingleton.TipoAsuntos
                                          where n.IdDato == tipoAsunto
                                          select n.Descripcion).ToList()[0] + " " +
                                         reader["NumAsunto"].ToString() + "/" + reader["YearAsunto"].ToString();
                        preview.IdPonente = reader["IdPonente"] as int? ?? -1;
                        preview.FResolucion = DateTimeUtilities.GetDateFromReader(reader, "FResolucion");
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
        }
    }
}