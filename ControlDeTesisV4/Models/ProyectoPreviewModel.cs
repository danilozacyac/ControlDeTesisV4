using System;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data.OleDb;
using System.Linq;
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
        public ObservableCollection<ProyectoPreview> GetPreviewSalasSinTurnar(int idTipoProyecto,int idInstancia)
        {
            ObservableCollection<ProyectoPreview> listaProyectosSalas = new ObservableCollection<ProyectoPreview>();

            OleDbConnection connection = new OleDbConnection(connectionString);
            OleDbCommand cmd = null;
            OleDbDataReader reader = null;

            String sqlCadena = "SELECT PT.IdTesis, PT.IdProyecto AS ProyectoID, PT.Rubro, PT.Tatj, PT.EstadoTesis, PT.IdAbogado, P.IdEmisor " +
                               "FROM ProyectosTesis PT INNER JOIN Proyectos P ON PT.IdProyecto = P.IdProyecto WHERE EstadoTesis < 4 AND IdTipoProyecto = @IdTipoProyecto AND PT.IdInstancia = @IdInstancias";
            
            try
            {
                connection.Open();

                cmd = new OleDbCommand(sqlCadena, connection);
                cmd.Parameters.AddWithValue("@IdTipoProyecto", idTipoProyecto);
                cmd.Parameters.AddWithValue("@IdInstancias", idInstancia);
                reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        ProyectoPreview preview = new ProyectoPreview();
                        preview.IdTesis = Convert.ToInt32(reader["IdTesis"]);
                        preview.IdProyecto = Convert.ToInt32(reader["ProyectoID"]);
                        preview.IdTipoProyecto = idTipoProyecto;
                        preview.Rubro = reader["Rubro"].ToString();
                        preview.Tatj = Convert.ToInt32(reader["tatj"]);
                        preview.EstadoTesis = Convert.ToInt32(reader["EstadoTesis"]);
                        preview.IdAbogadoResponsable = Convert.ToInt32(reader["IdAbogado"]);
                        preview.IdEmisor = Convert.ToInt32(reader["IdEmisor"]);

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
            catch (OleDbException ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                ErrorUtilities.SetNewErrorMessage(ex, methodName + " Exception,ProyectoPreviewModel", "ControlTesis");
            }
            catch (Exception ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                ErrorUtilities.SetNewErrorMessage(ex, methodName + " Exception,ProyectoPreviewModel", "ControlTesis");
            }
            finally
            {
                connection.Close();
            }
            
            return listaProyectosSalas;
        }

        private void GetProyectoInfoPreview(ProyectoPreview preview)
        {
            OleDbConnection connection = new OleDbConnection(connectionString);
            OleDbCommand cmd = null;
            OleDbDataReader reader = null;

            String sqlCadena = "SELECT FRecepcion,OficioRecepcion FROM Proyectos WHERE IdProyecto = @IdProyecto";

            try
            {
                connection.Open();

                cmd = new OleDbCommand(sqlCadena, connection);
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
            catch (OleDbException ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                ErrorUtilities.SetNewErrorMessage(ex, methodName + " Exception,ProyectoPreviewModel", "ControlTesis");
            }
            catch (Exception ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                ErrorUtilities.SetNewErrorMessage(ex, methodName + " Exception,ProyectoPreviewModel", "ControlTesis");
            }
            finally
            {
                connection.Close();
            }
        }

        private void GetProyectoInfoPreviewCcst(ProyectoPreview preview)
        {
            OleDbConnection connection = new OleDbConnection(connectionString);
            OleDbCommand cmd = null;
            OleDbDataReader reader = null;

            String sqlCadena = "SELECT FechaOficioAtn,OficioAtn FROM ProyectosCcst WHERE IdProyecto = @IdProyecto";

            try
            {
                connection.Open();

                cmd = new OleDbCommand(sqlCadena, connection);
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
            catch (OleDbException ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                ErrorUtilities.SetNewErrorMessage(ex, methodName + " Exception,ProyectoPreviewModel", "ControlTesis");
            }
            catch (Exception ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                ErrorUtilities.SetNewErrorMessage(ex, methodName + " Exception,ProyectoPreviewModel", "ControlTesis");
            }
            finally
            {
                connection.Close();
            }
        }

        private void GetPrecedenteInfoPreview(ProyectoPreview preview)
        {
            OleDbConnection connection = new OleDbConnection(connectionString);
            OleDbCommand cmd = null;
            OleDbDataReader reader = null;

            String sqlCadena = "SELECT * FROM PrecedentesTesis WHERE IdTesis = @IdTesis";

            try
            {
                connection.Open();

                cmd = new OleDbCommand(sqlCadena, connection);
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
            catch (OleDbException ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                ErrorUtilities.SetNewErrorMessage(ex, methodName + " Exception,ProyectoPreviewModel", "ControlTesis");
            }
            catch (Exception ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                ErrorUtilities.SetNewErrorMessage(ex, methodName + " Exception,ProyectoPreviewModel", "ControlTesis");
            }
            finally
            {
                connection.Close();
            }
        }


    }
}