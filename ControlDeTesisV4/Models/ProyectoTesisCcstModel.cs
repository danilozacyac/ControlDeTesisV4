﻿using System;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using ControlDeTesisV4.Dao;
using ScjnUtilities;

namespace ControlDeTesisV4.Models
{
    public class ProyectoTesisCcstModel : TesisCommonModel
    {
        readonly ProyectosCcst tesisCcst;
        readonly string connectionString = ConfigurationManager.ConnectionStrings["Modulo"].ToString();

        public ProyectoTesisCcstModel() : base(2) { }

        public ProyectoTesisCcstModel(ProyectosCcst tesisCcst)
            : base(tesisCcst.IdTipoProyecto)
        {
            this.tesisCcst = tesisCcst;
        }

        public void SetNewProyecto()
        {
            OleDbConnection connection = new OleDbConnection(connectionString);

            string sSql;
            OleDbDataAdapter dataAdapter;

            DataSet dataSet = new DataSet();
            DataRow dr;

            try
            {
                tesisCcst.IdProyecto = AuxiliarModel.GetLastId("ProyectosCCST", "IdProyecto");

                string sqlCadena = "SELECT * FROM ProyectosCcst WHERE IdProyecto = 0";

                dataAdapter = new OleDbDataAdapter();
                dataAdapter.SelectCommand = new OleDbCommand(sqlCadena, connection);

                dataAdapter.Fill(dataSet, "ProyectosCcst");

                dr = dataSet.Tables["ProyectosCcst"].NewRow();

                dr["IdProyecto"] = tesisCcst.IdProyecto;
                dr["Destinatario"] = tesisCcst.Destinatario;
                dr["OficioAtn"] = tesisCcst.OficioAtn;

                if (tesisCcst.FOficioAtn != null)
                {
                    dr["FechaOficioAtn"] = tesisCcst.FOficioAtn;
                    dr["FechaOficioAtnInt"] = DateTimeUtilities.DateToInt(tesisCcst.FOficioAtn);
                }
                else
                {
                    dr["FechaOficioAtn"] = DBNull.Value;
                    dr["FechaOficioAtnInt"] = 0;
                }

                dr["FileOficioAtnOrigen"] = tesisCcst.FileOficioAtnOrigen;
                dr["FileOficioAtnConten"] = tesisCcst.FileOficioAtnConten;


                dataSet.Tables["ProyectosCcst"].Rows.Add(dr);

                dataAdapter.InsertCommand = connection.CreateCommand();

                sSql = "INSERT INTO ProyectosCcst (IdProyecto,Destinatario,OficioAtn,FechaOficioAtn,FechaOficioAtnInt,FileOficioAtnOrigen,FileOficioAtnConten) " +
                       " VALUES (@IdProyecto,@Destinatario,@OficioAtn,@FechaOficioAtn,@FechaOficioAtnInt,@FileOficioAtnOrigen,@FileOficioAtnConten)";

                dataAdapter.InsertCommand.CommandText = sSql;

                dataAdapter.InsertCommand.Parameters.Add("@IdProyecto", OleDbType.Numeric, 0, "IdProyecto");
                dataAdapter.InsertCommand.Parameters.Add("@Destinatario", OleDbType.Numeric, 0, "Destinatario");
                dataAdapter.InsertCommand.Parameters.Add("@OficioAtn", OleDbType.VarChar, 0, "OficioAtn");
                dataAdapter.InsertCommand.Parameters.Add("@FechaOficioAtn", OleDbType.Date, 0, "FechaOficioAtn");
                dataAdapter.InsertCommand.Parameters.Add("@FechaOficioAtnInt", OleDbType.Numeric, 0, "FechaOficioAtnInt");
                dataAdapter.InsertCommand.Parameters.Add("@FileOficioAtnOrigen", OleDbType.VarChar, 0, "FileOficioAtnOrigen");
                dataAdapter.InsertCommand.Parameters.Add("@FileOficioAtnConten", OleDbType.VarChar, 0, "FileOficioAtnConten");

                dataAdapter.Update(dataSet, "ProyectosCcst");
                dataSet.Dispose();
                dataAdapter.Dispose();

                SetNewTesisProyecto(tesisCcst.Proyectos, tesisCcst.IdProyecto);
            }
            catch (OleDbException ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                ErrorUtilities.SetNewErrorMessage(ex, methodName + " Exception,ProyectoTesisCcstModel", "ControlTesis");
            }
            catch (Exception ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                ErrorUtilities.SetNewErrorMessage(ex, methodName + " Exception,ProyectoTesisCcstModel", "ControlTesis");
            }
            finally
            {
                connection.Close();
            }
        }

        /// <summary>
        /// Actualiza el estado de la tesis dentro del proceso de publicacion
        /// 1. Recepcion
        /// 2. ENvio de Observaciones o envío del proyecto
        /// 3. Aprobación
        /// 4. Espera de Turno
        /// 5. Turno
        /// 6. Publicación
        /// </summary>
        /// <param name="tesis"></param>
        public void UpdateProyectoTesis(ProyectosTesis tesis)
        {
            OleDbConnection connection = new OleDbConnection(connectionString);

            string sSql;
            OleDbDataAdapter dataAdapter;

            DataSet dataSet = new DataSet();
            DataRow dr;

            try
            {
                string sqlCadena = "SELECT * FROM ProyectosTesis WHERE IdTesis = " + tesis.IdTesis;

                dataAdapter = new OleDbDataAdapter();
                dataAdapter.SelectCommand = new OleDbCommand(sqlCadena, connection);

                dataAdapter.Fill(dataSet, "ProyectosTesis");

                dr = dataSet.Tables["ProyectosTesis"].Rows[0];
                dr.BeginEdit();
                dr["OficioEnvio"] = tesis.OficioEnvio;

                if (tesis.FEnvio != null)
                {
                    dr["FechaEnvioOficio"] = tesis.FEnvio;
                    dr["FechaEnvioOficioInt"] = DateTimeUtilities.DateToInt(tesis.FEnvio);
                }
                else
                {
                    dr["FechaEnvioOficio"] = DBNull.Value;
                    dr["FechaEnvioOficioInt"] = 0;
                }

                dr["OficioEnvioPathOrigen"] = tesis.OficioEnvioPathOrigen;
                dr["OficioEnvioPathConten"] = tesis.OficioEnvioPathConten;

                if (tesis.FAprobacion != null)
                {
                    dr["FAprobacion"] = tesis.FAprobacion;
                    dr["FAprobacionInt"] = DateTimeUtilities.DateToInt(tesis.FAprobacion);
                }
                else
                {
                    dr["FAprobacion"] = DBNull.Value;
                    dr["FAprobacionInt"] = 0;
                }

                dr["NumTesis"] = tesis.NumTesis;
                dr["NumTesisInt"] = tesis.NumTesisInt;
                dr["YearTesis"] = tesis.YearTesis;
                dr["ClaveTesis"] = tesis.ClaveTesis;
                dr["EstadoTesis"] = tesis.EstadoTesis;

                dr.EndEdit();

                dataAdapter.UpdateCommand = connection.CreateCommand();

                sSql = "UPDATE ProyectosTesis SET OficioEnvio = @OficioEnvio, FechaEnvioOficio = @FechaEnvioOficio,FechaEnvioOficioInt = @FechaEnvioOficioInt," +
                       "OficioEnvioPathOrigen = @OficioEnvioPathOrigen,OficioEnvioPathConten = @OficioEnvioPathConten, " +
                       "FAprobacion = @FAprobacion, FAprobacionInt = @FAprobacionInt, NumTesis = @NumTesis, NumTesisInt = @NumTesisInt, YearTesis = @YearTesis, ClaveTesis = @ClaveTesis, EstadoTesis = @EstadoTesis  " +
                       " WHERE IdTesis = @IdTesis";
                dataAdapter.UpdateCommand.CommandText = sSql;

                dataAdapter.UpdateCommand.Parameters.Add("@OficioEnvio", OleDbType.VarChar, 0, "OficioEnvio");
                dataAdapter.UpdateCommand.Parameters.Add("@FechaEnvioOficio", OleDbType.Date, 0, "FechaEnvioOficio");
                dataAdapter.UpdateCommand.Parameters.Add("@FechaEnvioOficioInt", OleDbType.Numeric, 0, "FechaEnvioOficioInt");
                dataAdapter.UpdateCommand.Parameters.Add("@OficioEnvioPathOrigen", OleDbType.VarChar, 0, "OficioEnvioPathOrigen");
                dataAdapter.UpdateCommand.Parameters.Add("@OficioEnvioPathConten", OleDbType.VarChar, 0, "OficioEnvioPathConten");
                dataAdapter.UpdateCommand.Parameters.Add("@FAprobacion", OleDbType.Date, 0, "FAprobacion");
                dataAdapter.UpdateCommand.Parameters.Add("@FAprobacionInt", OleDbType.Numeric, 0, "FAprobacionInt");
                dataAdapter.UpdateCommand.Parameters.Add("@NumTesis", OleDbType.VarChar, 0, "NumTesis");
                dataAdapter.UpdateCommand.Parameters.Add("@NumTesisInt", OleDbType.Numeric, 0, "NumTesisInt");
                dataAdapter.UpdateCommand.Parameters.Add("@YearTesis", OleDbType.Numeric, 0, "YearTesis");
                dataAdapter.UpdateCommand.Parameters.Add("@ClaveTesis", OleDbType.VarChar, 0, "ClaveTesis");
                dataAdapter.UpdateCommand.Parameters.Add("@EstadoTesis", OleDbType.Numeric, 0, "EstadoTesis");
                dataAdapter.UpdateCommand.Parameters.Add("@IdTesis", OleDbType.Numeric, 0, "IdTesis");

                dataAdapter.Update(dataSet, "ProyectosTesis");
                dataSet.Dispose();
                dataAdapter.Dispose();

                this.UpdateTesisCompara(tesis.ComparaTesis);
                this.UpdatePrecedentes(tesis.Precedente, tesis.IdTesis);
            }
            catch (OleDbException ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                ErrorUtilities.SetNewErrorMessage(ex, methodName + " Exception,ProyectoTesisCcstModel", "ControlTesis");
            }
            catch (Exception ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                ErrorUtilities.SetNewErrorMessage(ex, methodName + " Exception,ProyectoTesisCcstModel", "ControlTesis");
            }
            finally
            {
                connection.Close();
            }
        }

        /// <summary>
        /// Devuelve un listado con las tesis que abarcan un periodo determinado, este periodo puede ser mensual o anual
        /// </summary>
        /// <param name="inicio">Dia inicial que se toma para obtener las tesis</param>
        /// <param name="fin">Último día considerado para regresar las tesis</param>
        /// <returns></returns>
        public ObservableCollection<ProyectosTesis> GetProyectoTesis(int inicio, int fin)
        {
            ObservableCollection<ProyectosTesis> listaDeTesis = new ObservableCollection<ProyectosTesis>();


            OleDbConnection connection = new OleDbConnection(connectionString);
            OleDbCommand cmd = null;
            OleDbDataReader reader = null;

            String sqlCadena = "";

            if (inicio == fin)
                sqlCadena = "SELECT * FROM ProyectosTesis WHERE FechaEnvioOficioInt LIKE '" + inicio + "%' AND idTipoProyecto = 2";
            else
                sqlCadena = "SELECT * FROM ProyectosTesis WHERE (FechaEnvioOficioInt Between " + inicio + " and " + fin + ") AND idTipoProyecto = 2";

            try
            {
                connection.Open();

                cmd = new OleDbCommand(sqlCadena, connection);
                reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        ProyectosTesis tesis = new ProyectosTesis();

                        tesis.IdTesis = Convert.ToInt32(reader["IdTesis"]);
                        tesis.IdProyecto = Convert.ToInt32(reader["IdProyecto"]);
                        tesis.OficioEnvio = reader["OficioEnvio"].ToString();
                        tesis.FEnvio = DateTimeUtilities.GetDateFromReader(reader, "FechaEnvioOficio");
                        tesis.OficioEnvioPathOrigen = reader["OficioEnvioPathOrigen"].ToString();
                        tesis.Rubro = reader["Rubro"].ToString();
                        tesis.Tatj = Convert.ToInt16(reader["Tatj"]);
                        tesis.IdTipoJuris = Convert.ToInt32(reader["TipoJuris"]);
                        tesis.NumPaginas = Convert.ToInt32(reader["NumPaginas"]);
                        tesis.IdAbogadoResponsable = Convert.ToInt32(reader["IdAbogado"]);
                        tesis.IdInstancia = Convert.ToInt32(reader["Idinstancia"]);
                        tesis.IdSubInstancia = Convert.ToInt32(reader["IdSubinstancia"]);
                        tesis.Aprobada = Convert.ToInt32(reader["Aprobada"]);
                        tesis.FAprobacion = DateTimeUtilities.GetDateFromReader(reader, "FAprobacion");
                        tesis.NumTesis = reader["numTesis"].ToString();
                        tesis.NumTesisInt = Convert.ToInt32(reader["NumTesisInt"]);
                        tesis.YearTesis = Convert.ToInt32(reader["YearTesis"]);
                        tesis.ClaveTesis = reader["ClaveTesis"].ToString();
                        tesis.EstadoTesis = Convert.ToInt32(reader["EstadoTesis"]);
                        tesis.Precedente = this.GetPrecedenteTesis(tesis.IdTesis);
                        tesis.ComparaTesis = GetTesisCompara(tesis.IdTesis);

                        listaDeTesis.Add(tesis);
                    }
                }
                cmd.Dispose();
                reader.Close();
            }
            catch (OleDbException ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                ErrorUtilities.SetNewErrorMessage(ex, methodName + " Exception,ProyectoTesisCcstModel", "ControlTesis");
            }
            catch (Exception ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                ErrorUtilities.SetNewErrorMessage(ex, methodName + " Exception,ProyectoTesisCcstModel", "ControlTesis");
            }
            finally
            {
                connection.Close();
            }

            return listaDeTesis;
        }


        /// <summary>
        /// Elimina los datos de recepción de un proyecto
        /// </summary>
        /// <param name="idProyecto">Identificador del Proyecto que se va a eliminar</param>
        public void DeleteProyecto(int idProyecto)
        {
            int tesisNumber = GetTesisNumberByProyecto(idProyecto);

            if (tesisNumber > 1)
            {
                OleDbConnection connection = new OleDbConnection(connectionString);
                OleDbCommand cmd = new OleDbCommand();

                try
                {
                    cmd = connection.CreateCommand();
                    cmd.CommandText = "DELETE FROM ProyectosCcst WHERE IdProyecto = @IdProyecto";
                    cmd.Parameters.AddWithValue("@IdProyecto", idProyecto);

                    connection.Open();
                    cmd.ExecuteNonQuery();
                    connection.Close();
                }
                catch (OleDbException ex)
                {
                    string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                    ErrorUtilities.SetNewErrorMessage(ex, methodName + " Exception,ProyectoTesisCcstModel", "ControlTesis");
                }
                catch (Exception ex)
                {
                    string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                    ErrorUtilities.SetNewErrorMessage(ex, methodName + " Exception,ProyectoTesisCcstModel", "ControlTesis");
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        /// <summary>
        /// Obtiene los datos generales de llegada del proyecto correspondiente
        /// </summary>
        /// <param name="idProyecto"></param>
        /// <returns></returns>
        public ProyectosCcst GetDatosLlegada(int idTesis)
        {
            ProyectosCcst proyecto = null;

            OleDbConnection connection = new OleDbConnection(connectionString);
            OleDbCommand cmd = null;
            OleDbDataReader reader = null;

            String sqlCadena = "SELECT PC.*, PT.IdProyecto FROM ProyectosCcst PC INNER JOIN ProyectosTesis PT ON PC.IdProyecto = PT.IdProyecto WHERE IdTesis = @IdTesis";

            try
            {
                connection.Open();

                cmd = new OleDbCommand(sqlCadena, connection);
                cmd.Parameters.AddWithValue("@IdTesis", idTesis);
                reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        proyecto = new ProyectosCcst();
                        proyecto.IdProyecto = reader["PT.IdProyecto"] as int? ?? -1;
                        proyecto.Destinatario = reader["Destinatario"] as int? ?? -1;
                        proyecto.OficioAtn = reader["OficioAtn"].ToString();
                        proyecto.FOficioAtn = DateTimeUtilities.GetDateFromReader(reader, "FechaOficioAtn");
                        proyecto.FileOficioAtnOrigen = reader["FileOficioAtnOrigen"].ToString();
                        proyecto.FileOficioAtnConten = reader["FileOficioAtnConten"].ToString();
                    }
                }
                cmd.Dispose();
                reader.Close();
            }
            catch (OleDbException ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                ErrorUtilities.SetNewErrorMessage(ex, methodName + " Exception,ProyectoTesisCcstModel", "ControlTesis");
            }
            catch (Exception ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                ErrorUtilities.SetNewErrorMessage(ex, methodName + " Exception,ProyectoTesisCcstModel", "ControlTesis");
            }
            finally
            {
                connection.Close();
            }

            return proyecto;
        }

        public void UpdateDatosLlegada(ProyectosCcst tesis)
        {
            OleDbConnection connection = new OleDbConnection(connectionString);

            string sSql;
            OleDbDataAdapter dataAdapter;

            DataSet dataSet = new DataSet();
            DataRow dr;

            try
            {
                string sqlCadena = "SELECT * FROM ProyectosCcst WHERE IdProyecto = @IdProyecto";

                dataAdapter = new OleDbDataAdapter();
                dataAdapter.SelectCommand = new OleDbCommand(sqlCadena, connection);
                dataAdapter.SelectCommand.Parameters.AddWithValue("@IdProyecto", tesis.IdProyecto);

                dataAdapter.Fill(dataSet, "ProyectosCcst");

                dr = dataSet.Tables["ProyectosCcst"].Rows[0];
                dr.BeginEdit();
                dr["Destinatario"] = tesis.Destinatario;
                dr["OficioAtn"] = tesis.OficioAtn;

                if (tesis.FOficioAtn != null)
                {
                    dr["FechaOficioAtn"] = tesis.FOficioAtn;
                    dr["FechaOficioAtnInt"] = DateTimeUtilities.DateToInt(tesis.FOficioAtn);
                }
                else
                {
                    dr["FechaOficioAtn"] = DBNull.Value;
                    dr["FechaOficioAtnInt"] = 0;
                }

                dr["FileOficioAtnOrigen"] = tesis.FileOficioAtnOrigen;
                dr["FileOficioAtnConten"] = tesis.FileOficioAtnConten;

                dr.EndEdit();

                dataAdapter.UpdateCommand = connection.CreateCommand();

                sSql = "UPDATE ProyectosCcst SET Destinatario = @Destinatario, OficioAtn = @OficioAtn,FechaOficioAtn = @FechaOficioAtn," +
                       "FechaOficioAtnInt = @FechaOficioAtnInt,FileOficioAtnOrigen = @FileOficioAtnOrigen, " +
                       "FileOficioAtnConten = @FileOficioAtnConten " +
                       " WHERE IdProyecto = @IdProyecto";
                dataAdapter.UpdateCommand.CommandText = sSql;

                dataAdapter.UpdateCommand.Parameters.Add("@Destinatario", OleDbType.Numeric, 0, "Destinatario");
                dataAdapter.UpdateCommand.Parameters.Add("@OficioAtn", OleDbType.VarChar, 0, "OficioAtn");
                dataAdapter.UpdateCommand.Parameters.Add("@FechaOficioAtn", OleDbType.Date, 0, "FechaOficioAtn");
                dataAdapter.UpdateCommand.Parameters.Add("@FechaOficioAtnInt", OleDbType.Numeric, 0, "FechaOficioAtnInt");
                dataAdapter.UpdateCommand.Parameters.Add("@FileOficioAtnOrigen", OleDbType.VarChar, 0, "FileOficioAtnOrigen");
                dataAdapter.UpdateCommand.Parameters.Add("@FileOficioAtnConten", OleDbType.VarChar, 0, "FileOficioAtnConten");
                dataAdapter.UpdateCommand.Parameters.Add("@IdProyecto", OleDbType.Numeric, 0, "IdProyecto");

                dataAdapter.Update(dataSet, "ProyectosCcst");
                dataSet.Dispose();
                dataAdapter.Dispose();

            }
            catch (OleDbException ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                ErrorUtilities.SetNewErrorMessage(ex, methodName + " Exception,ProyectoTesisCcstModel", "ControlTesis");
            }
            catch (Exception ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                ErrorUtilities.SetNewErrorMessage(ex, methodName + " Exception,ProyectoTesisCcstModel", "ControlTesis");
            }
            finally
            {
                connection.Close();
            }
        }




        public void UpdateTesisCompara(TesisCompara tesis)
        {
            OleDbConnection connection = new OleDbConnection(connectionString);

            string sSql;
            OleDbDataAdapter dataAdapter;

            DataSet dataSet = new DataSet();
            DataRow dr;

            try
            {
                string sqlCadena = "SELECT * FROM TesisCompara WHERE IdTesis = " + tesis.IdTesis;

                dataAdapter = new OleDbDataAdapter();
                dataAdapter.SelectCommand = new OleDbCommand(sqlCadena, connection);

                dataAdapter.Fill(dataSet, "TesisCompara");

                dr = dataSet.Tables["TesisCompara"].Rows[0];

                dr.BeginEdit();

                dr["TextoRevision1"] = tesis.TObservaciones;
                dr["TR1Plano"] = tesis.TObservacionesPlano;
                dr["TObsFilePathOrigen"] = tesis.TObsFilePathOrigen;
                dr["TObsFilePathConten"] = tesis.TObsFilePathConten;
                dr["TextoRevision2"] = tesis.TAprobada;
                dr["TR2Plano"] = tesis.TAprobadaPlano;
                dr["TAprobFilePathOrigen"] = tesis.TAprobFilePathOrigen;
                dr["TAprobFilePathConten"] = tesis.TAprobFilePathConten;

                dr.EndEdit();

                dataAdapter.UpdateCommand = connection.CreateCommand();

                sSql = "UPDATE TesisCompara SET TextoRevision1 = @TextoRevision1, TR1Plano = @TR1Plano,TObsFilePathOrigen = @TObsFilePathOrigen," +
                       "TObsFilePathConten = @TObsFilePathConten, TextoRevision2 = @TextoRevision2, TR2Plano = @TR2Plano,TAprobFilePathOrigen = @TAprobFilePathOrigen," +
                       "TAprobFilePathConten = @TAprobFilePathConten " +
                       " WHERE IdTesis = @IdTesis";
                dataAdapter.UpdateCommand.CommandText = sSql;

                dataAdapter.UpdateCommand.Parameters.Add("@TextoRevision1", OleDbType.VarChar, 0, "TextoRevision1");
                dataAdapter.UpdateCommand.Parameters.Add("@TR1Plano", OleDbType.VarChar, 0, "TR1Plano");
                dataAdapter.UpdateCommand.Parameters.Add("@TObsFilePathOrigen", OleDbType.VarChar, 0, "TObsFilePathOrigen");
                dataAdapter.UpdateCommand.Parameters.Add("@TObsFilePathConten", OleDbType.VarChar, 0, "TObsFilePathConten");
                dataAdapter.UpdateCommand.Parameters.Add("@TextoRevision2", OleDbType.VarChar, 0, "TextoRevision2");
                dataAdapter.UpdateCommand.Parameters.Add("@TR2Plano", OleDbType.VarChar, 0, "TR2Plano");
                dataAdapter.UpdateCommand.Parameters.Add("@TAprobFilePathOrigen", OleDbType.VarChar, 0, "TAprobFilePathOrigen");
                dataAdapter.UpdateCommand.Parameters.Add("@TAprobFilePathConten", OleDbType.VarChar, 0, "TAprobFilePathConten");
                dataAdapter.UpdateCommand.Parameters.Add("@IdTesis", OleDbType.Numeric, 0, "IdTesis");

                dataAdapter.Update(dataSet, "TesisCompara");
                dataSet.Dispose();
                dataAdapter.Dispose();
            }
            catch (OleDbException ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                ErrorUtilities.SetNewErrorMessage(ex, methodName + " Exception,ProyectoTesisCcstModel", "ControlTesis");
            }
            catch (Exception ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                ErrorUtilities.SetNewErrorMessage(ex, methodName + " Exception,ProyectoTesisCcstModel", "ControlTesis");
            }
            finally
            {
                connection.Close();
            }
        }






    }
}