using System;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Windows.Forms;
using ControlDeTesisV4.Dao;
using DocumentMgmtApi;
using ModuloInterconexionCommonApi;

namespace ControlDeTesisV4.Models
{
    public class EjecutoriasModel
    {
        readonly string connectionString = ConfigurationManager.ConnectionStrings["Modulo"].ToString();

        public void SetNewProyectoEjecutoria(Ejecutorias ejecutoria)
        {
            OleDbConnection connection = new OleDbConnection(connectionString);

            string sSql;
            OleDbDataAdapter dataAdapter;

            DataSet dataSet = new DataSet();
            DataRow dr;

            try
            {
                ejecutoria.IdEjecutoria = AuxiliarModel.GetLastId("Ejecutorias", "IdEjecutoria");

                string sqlCadena = "SELECT * FROM Ejecutorias WHERE IdEjecutoria = 0";

                dataAdapter = new OleDbDataAdapter();
                dataAdapter.SelectCommand = new OleDbCommand(sqlCadena, connection);

                dataAdapter.Fill(dataSet, "Ejecutorias");

                dr = dataSet.Tables["Ejecutorias"].NewRow();
                dr["IdEjecutoria"] = ejecutoria.IdEjecutoria;
                dr["IdTesis"] = ejecutoria.IdTesis;
                dr["ForObservaciones"] = ejecutoria.ForObservaciones;
                dr["ProvFilePathOrigen"] = ejecutoria.ProvFilePathOrigen;
                dr["ProvFilePathConten"] = ejecutoria.ProvFilePathConten;
                dr["ProvNumFojas"] = ejecutoria.ProvNumFojas;
                dr["Obs"] = (ejecutoria.Observaciones != null && ejecutoria.Observaciones.Count > 0) ? 1 : 0;
                dr["ObsFilePathOrigen"] = ejecutoria.ObsFilePathOrigen;
                dr["ObsFilePathConten"] = ejecutoria.ObsFilePathConten;

                if (ejecutoria.FRecepcion != null)
                {
                    dr["FRecepcion"] = ejecutoria.FRecepcion;
                    dr["FRecepcionInt"] = StringUtilities.DateToInt(ejecutoria.FRecepcion);
                }
                else
                {
                    dr["FRecepcion"] = DBNull.Value;
                    dr["FRecepcionInt"] = 0;
                }

                if (ejecutoria.FEnvioObs != null)
                {
                    dr["FEnvioObs"] = ejecutoria.FEnvioObs;
                    dr["FEnvioObsInt"] = StringUtilities.DateToInt(ejecutoria.FEnvioObs);
                }
                else
                {
                    dr["FEnvioObs"] = DBNull.Value;
                    dr["FEnvioObsInt"] = 0;
                }

                if (ejecutoria.FDevolucion != null)
                {
                    dr["FDevolucion"] = ejecutoria.FDevolucion;
                    dr["FDevolucionInt"] = StringUtilities.DateToInt(ejecutoria.FDevolucion);
                }
                else
                {
                    dr["FDevolucion"] = DBNull.Value;
                    dr["FDevolucionInt"] = 0;
                }

                dr["CCFilePathOrigen"] = ejecutoria.CcFilePathOrigen;
                dr["CCFilePathConten"] = ejecutoria.CcFilePathConten;
                dr["CCNumFojas"] = ejecutoria.CcNumFojas;
                dr["VPFilePathOrigen"] = ejecutoria.VpFilePathOrigen;
                dr["VPFilePathConten"] = ejecutoria.VpFilePathConten;
                dr["VPNumFojas"] = ejecutoria.VpNumFojas;
                dr["EstadoEjecutoria"] = ejecutoria.EstadoEjecutoria;

                dataSet.Tables["Ejecutorias"].Rows.Add(dr);

                dataAdapter.InsertCommand = connection.CreateCommand();

                sSql = "INSERT INTO Ejecutorias (IdEjecutoria,IdTesis,ForObservaciones,ProvFilePathOrigen,ProvFilePathConten,ProvNumFojas,Obs,ObsFilePathOrigen,ObsFilePathConten," +
                       "FRecepcion,FRecepcionInt,FEnvioObs,FEnvioObsInt,FDevolucion,FDevolucionInt,CCFilePathOrigen,CCFilePathConten,CCNumFojas,VPFilePathOrigen,VPFilePathConten,VPNumFojas,EstadoEjecutoria) " +
                       " VALUES (@IdEjecutoria,@IdTesis,@ForObservaciones,@ProvFilePathOrigen,@ProvFilePathConten,@ProvNumFojas,@Obs,@ObsFilePathOrigen,@ObsFilePathConten," +
                       "@FRecepcion,@FRecepccionInt,@FEnvioObs,@FEnvioObsInt,@FDevolucion,@FDevolucionInt,@CCFilePathOrigen,@CCFilePathConten,@CCNumFojas,@VPFilePathOrigen,@VPFilePathConten,@VPNumFojas,@EstadoEjecutoria)";

                dataAdapter.InsertCommand.CommandText = sSql;

                dataAdapter.InsertCommand.Parameters.Add("@IdEjecutoria", OleDbType.Numeric, 0, "IdEjecutoria");
                dataAdapter.InsertCommand.Parameters.Add("@IdTesis", OleDbType.Numeric, 0, "IdTesis");
                dataAdapter.InsertCommand.Parameters.Add("@ForObservaciones", OleDbType.Numeric, 0, "ForObservaciones");
                dataAdapter.InsertCommand.Parameters.Add("@ProvFilePathOrigen", OleDbType.VarChar, 0, "ProvFilePathOrigen");
                dataAdapter.InsertCommand.Parameters.Add("@ProvFilePathConten", OleDbType.VarChar, 0, "ProvFilePathConten");
                dataAdapter.InsertCommand.Parameters.Add("@ProvNumFojas", OleDbType.Numeric, 0, "ProvNumFojas");
                dataAdapter.InsertCommand.Parameters.Add("@Obs", OleDbType.Numeric, 0, "Obs");
                dataAdapter.InsertCommand.Parameters.Add("@ObsFilePathOrigen", OleDbType.VarChar, 0, "ObsFilePathOrigen");
                dataAdapter.InsertCommand.Parameters.Add("@ObsFilePathConten", OleDbType.VarChar, 0, "ObsFilePathConten");
                dataAdapter.InsertCommand.Parameters.Add("@FRecepcion", OleDbType.Date, 0, "FRecepcion");
                dataAdapter.InsertCommand.Parameters.Add("@FRecepcionInt", OleDbType.Numeric, 0, "FRecepcionInt");
                dataAdapter.InsertCommand.Parameters.Add("@FEnvioObs", OleDbType.Date, 0, "FEnvioObs");
                dataAdapter.InsertCommand.Parameters.Add("@FEnvioObsInt", OleDbType.Numeric, 0, "FEnvioObsInt");
                dataAdapter.InsertCommand.Parameters.Add("@FDevolucion", OleDbType.Date, 0, "FDevolucion");
                dataAdapter.InsertCommand.Parameters.Add("@FDevolucionInt", OleDbType.Numeric, 0, "FDevolucionInt");
                dataAdapter.InsertCommand.Parameters.Add("@CCFilePathOrigen", OleDbType.VarChar, 0, "CCFilePathOrigen");
                dataAdapter.InsertCommand.Parameters.Add("@CCFilePathConten", OleDbType.VarChar, 0, "CCFilePathConten");
                dataAdapter.InsertCommand.Parameters.Add("@CCNumFojas", OleDbType.Numeric, 0, "CCNumFojas");
                dataAdapter.InsertCommand.Parameters.Add("@VPFilePathOrigen", OleDbType.VarChar, 0, "VPFilePathOrigen");
                dataAdapter.InsertCommand.Parameters.Add("@VPFilePathConten", OleDbType.VarChar, 0, "VPFilePathConten");
                dataAdapter.InsertCommand.Parameters.Add("@VPNumFojas", OleDbType.Numeric, 0, "VPNumFojas");
                dataAdapter.InsertCommand.Parameters.Add("@EstadoEjecutoria", OleDbType.Numeric, 0, "EstadoEjecutoria");

                dataAdapter.Update(dataSet, "Ejecutorias");
                dataSet.Dispose();
                dataAdapter.Dispose();

                this.SetPrecedentes(ejecutoria.Precedente, ejecutoria.IdEjecutoria);

                if (ejecutoria.Observaciones != null)
                    this.SetNewObservacion(ejecutoria.Observaciones, ejecutoria.IdEjecutoria);

                
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
        }

        public void SetNewObservacion(ObservableCollection<Observaciones> observaciones, int idEjecutoria)
        {
            foreach (Observaciones observacion in observaciones)
                SetNewObservacion(observacion, idEjecutoria);
        }

        public void SetNewObservacion(Observaciones observacion, int idEjecutoria)
        {
            OleDbConnection connection = new OleDbConnection(connectionString);

            string sSql;
            OleDbDataAdapter dataAdapter;

            DataSet dataSet = new DataSet();
            DataRow dr;

            try
            {
                observacion.IdObservacion = AuxiliarModel.GetLastId("ObservacionesEjecutoria", "IdObservacion");

                string sqlCadena = "SELECT * FROM ObservacionesEjecutoria WHERE IdObservacion = 0";

                dataAdapter = new OleDbDataAdapter();
                dataAdapter.SelectCommand = new OleDbCommand(sqlCadena, connection);

                dataAdapter.Fill(dataSet, "ObservacionesEjecutoria");

                dr = dataSet.Tables["ObservacionesEjecutoria"].NewRow();
                dr["IdObservacion"] = observacion.IdObservacion;
                dr["IdEjecutoria"] = idEjecutoria;
                dr["Foja"] = observacion.Foja;
                dr["Parrafo"] = observacion.Parrafo;
                dr["Renglon"] = observacion.Renglon;
                dr["Dice"] = observacion.Dice;
                dr["DicePlano"] = observacion.Dice;
                dr["SeSugiere"] = observacion.Sugiere;
                dr["SeSugierePlano"] = observacion.Sugiere;
                dr["Aceptada"] = observacion.IsAcepted;

                dataSet.Tables["ObservacionesEjecutoria"].Rows.Add(dr);

                dataAdapter.InsertCommand = connection.CreateCommand();

                sSql = "INSERT INTO ObservacionesEjecutoria (IdObservacion,IdEjecutoria,Foja,Parrafo,Renglon,Dice,DicePlano,SeSugiere,SeSugierePlano,Aceptada) " +
                       " VALUES (@IdObservacion,@IdEjecutoria,@Parrafo,@Renglon,@Dice,@DicePlano,@SeSugiere,@SeSugierePlano,@Aceptada)";

                dataAdapter.InsertCommand.CommandText = sSql;

                dataAdapter.InsertCommand.Parameters.Add("@IdObservacion", OleDbType.Numeric, 0, "IdObservacion");
                dataAdapter.InsertCommand.Parameters.Add("@IdEjecutoria", OleDbType.Numeric, 0, "IdEjecutoria");
                dataAdapter.InsertCommand.Parameters.Add("@Foja", OleDbType.Numeric, 0, "Foja");
                dataAdapter.InsertCommand.Parameters.Add("@Parrafo", OleDbType.VarChar, 0, "Parrafo");
                dataAdapter.InsertCommand.Parameters.Add("@Renglon", OleDbType.VarChar, 0, "Renglon");
                dataAdapter.InsertCommand.Parameters.Add("@Dice", OleDbType.VarChar, 0, "Dice");
                dataAdapter.InsertCommand.Parameters.Add("@DicePlano", OleDbType.VarChar, 0, "DicePlano");
                dataAdapter.InsertCommand.Parameters.Add("@SeSugiere", OleDbType.VarChar, 0, "SeSugiere");
                dataAdapter.InsertCommand.Parameters.Add("@SeSugierePlano", OleDbType.VarChar, 0, "SeSugierePlano");
                dataAdapter.InsertCommand.Parameters.Add("@Aceptada", OleDbType.VarChar, 0, "Aceptada");

                dataAdapter.Update(dataSet, "ObservacionesEjecutoria");
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
        }

        public void SetPrecedentes(PrecedentesTesis precedente, int idEjecutoria)
        {
            OleDbConnection connection = new OleDbConnection(connectionString);

            string sSql;
            OleDbDataAdapter dataAdapter;

            DataSet dataSet = new DataSet();
            DataRow dr;

            try
            {
                string sqlCadena = "SELECT * FROM PrecedentesEjecutorias WHERE IdPrecedente = 0";
                precedente.IdPrecedente = AuxiliarModel.GetLastId("PrecedentesEjecutorias", "IdPrecedente");

                dataAdapter = new OleDbDataAdapter();
                dataAdapter.SelectCommand = new OleDbCommand(sqlCadena, connection);

                dataAdapter.Fill(dataSet, "PrecedentesEjecutorias");

                dr = dataSet.Tables["PrecedentesEjecutorias"].NewRow();
                dr["IdPrecedente"] = precedente.IdPrecedente;
                dr["IdEjecutoria"] = idEjecutoria;
                dr["IdTipoAsunto"] = precedente.TipoAsunto;
                dr["NumAsunto"] = precedente.NumAsunto;
                dr["YearAsunto"] = precedente.YearAsunto;

                if (precedente.FResolucion != null)
                {
                    dr["FResolucion"] = precedente.FResolucion;
                    dr["FResolucionInt"] = StringUtilities.DateToInt(precedente.FResolucion);
                }
                else
                {
                    dr["FResolucion"] = DBNull.Value;
                    dr["FResolucionInt"] = 0;
                }

                dr["IdPonente"] = precedente.IdPonente;
                dr["Promovente"] = precedente.Promovente;

                dataSet.Tables["PrecedentesEjecutorias"].Rows.Add(dr);

                dataAdapter.InsertCommand = connection.CreateCommand();

                sSql = "INSERT INTO PrecedentesEjecutorias (IdPrecedente,IdEjecutoria,IdTipoAsunto,NumAsunto,YearAsunto,FResolucion,FResolucionInt,IdPonente,Promovente) " +
                       " VALUES (@IdPrecedente,@IdTesis,@IdTipoAsunto,@NumAsunto,@YearAsunto,@FResolucion,@FResolucionInt,@IdPonente,@Promovente)";

                dataAdapter.InsertCommand.CommandText = sSql;

                dataAdapter.InsertCommand.Parameters.Add("@IdPrecedente", OleDbType.Numeric, 0, "IdPrecedente");
                dataAdapter.InsertCommand.Parameters.Add("@IdEjecutoria", OleDbType.Numeric, 0, "IdEjecutoria");
                dataAdapter.InsertCommand.Parameters.Add("@IdTipoAsunto", OleDbType.Numeric, 0, "IdTipoAsunto");
                dataAdapter.InsertCommand.Parameters.Add("@NumAsunto", OleDbType.Numeric, 0, "NumAsunto");
                dataAdapter.InsertCommand.Parameters.Add("@YearAsunto", OleDbType.Numeric, 0, "YearAsunto");
                dataAdapter.InsertCommand.Parameters.Add("@FResolucion", OleDbType.Date, 0, "FResolucion");
                dataAdapter.InsertCommand.Parameters.Add("@FResolucionInt", OleDbType.Numeric, 0, "FResolucionInt");
                dataAdapter.InsertCommand.Parameters.Add("@IdPonente", OleDbType.Numeric, 0, "IdPonente");
                dataAdapter.InsertCommand.Parameters.Add("@Promovente", OleDbType.VarChar, 0, "Promovente");

                dataAdapter.Update(dataSet, "PrecedentesEjecutorias");
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
        }

        public void UpdateProyectoEjecutoria(Ejecutorias ejecutoria)
        {
            OleDbConnection connection = new OleDbConnection(connectionString);

            string sSql;
            OleDbDataAdapter dataAdapter;

            DataSet dataSet = new DataSet();
            DataRow dr;

            try
            {
                string sqlCadena = "SELECT * FROM Ejecutorias WHERE IdEjecutoria = " + ejecutoria.IdEjecutoria;

                dataAdapter = new OleDbDataAdapter();
                dataAdapter.SelectCommand = new OleDbCommand(sqlCadena, connection);

                dataAdapter.Fill(dataSet, "Ejecutorias");

                dr = dataSet.Tables["Ejecutorias"].Rows[0];
                dr.BeginEdit();
                dr["IdTesis"] = ejecutoria.IdTesis;
                dr["ProvFilePathOrigen"] = ejecutoria.ProvFilePathOrigen;
                dr["ProvFilePathConten"] = ejecutoria.ProvFilePathConten;
                dr["ProvNumFojas"] = ejecutoria.ProvNumFojas;
                dr["Obs"] = (ejecutoria.Observaciones.Count > 0) ? 1 : 0;
                dr["ObsFilePathOrigen"] = ejecutoria.ObsFilePathOrigen;
                dr["ObsFilePathConten"] = ejecutoria.ObsFilePathConten;

                if (ejecutoria.FRecepcion != null)
                {
                    dr["FRecepcion"] = ejecutoria.FRecepcion;
                    dr["FRecepcionInt"] = StringUtilities.DateToInt(ejecutoria.FRecepcion);
                }
                else
                {
                    dr["FRecepcion"] = DBNull.Value;
                    dr["FRecepcionInt"] = 0;
                }

                if (ejecutoria.FEnvioObs != null)
                {
                    dr["FEnvioObs"] = ejecutoria.FEnvioObs;
                    dr["FEnvioObsInt"] = StringUtilities.DateToInt(ejecutoria.FEnvioObs);
                }
                else
                {
                    dr["FEnvioObs"] = DBNull.Value;
                    dr["FEnvioObsInt"] = 0;
                }

                if (ejecutoria.FDevolucion != null)
                {
                    dr["FDevolucion"] = ejecutoria.FDevolucion;
                    dr["FDevolucionInt"] = StringUtilities.DateToInt(ejecutoria.FDevolucion);
                }
                else
                {
                    dr["FDevolucion"] = DBNull.Value;
                    dr["FDevolucionInt"] = 0;
                }

                dr["CCFilePathOrigen"] = ejecutoria.CcFilePathOrigen;
                dr["CCFilePathConten"] = ejecutoria.CcFilePathConten;
                dr["CCNumFojas"] = ejecutoria.CcNumFojas;
                dr["VPFilePathOrigen"] = ejecutoria.VpFilePathOrigen;
                dr["VPFilePathConten"] = ejecutoria.VpFilePathConten;
                dr["VPNumFojas"] = ejecutoria.VpNumFojas;
                dr["EstadoEjecutoria"] = ejecutoria.EstadoEjecutoria;

                dr.EndEdit();

                dataAdapter.UpdateCommand = connection.CreateCommand();

                sSql = "UPDATE Ejecutorias SET IdTesis = @IdTesis, ProvFilePathOrigen = @ProvFilePathOrigen,ProvFilePathConten = @ProvFilePathConten,ProvNumFojas = @ProvNumFojas,Obs = @Obs, " +
                       "ObsFilePathOrigen = @ObsFilePathOrigen,ObsFilePathConten = @ObsFilePathConten," +
                       "FRecepcion = @FRecepcion,FRecepcionInt = @FRecepcionInt,FEnvioObs = @FEnvioObs,FEnvioObsInt = @FEnvioObsInt,FDevolucion = @FDevolucion,FDevolucionInt = @FDevolucionInt, " +
                       "CCFilePathOrigen = @CCFilePathOrigen,CCFilePathConten = @CCFilePathConten,CCNumFojas = @CCNumFojas,VPFilePathOrigen = @VPFilePathOrigen,VPFilePathConten = @VPFilePathConten," +
                       "VPNumFojas = @VPNumFojas,EstadoEjecutoria = @EstadoEjecutoria " +
                       " WHERE IdEjecutoria = @IdEjecutoria";

                dataAdapter.UpdateCommand.CommandText = sSql;

                dataAdapter.UpdateCommand.Parameters.Add("@IdTesis", OleDbType.Numeric, 0, "IdTesis");
                dataAdapter.UpdateCommand.Parameters.Add("@ProvFilePathOrigen", OleDbType.VarChar, 0, "ProvFilePathOrigen");
                dataAdapter.UpdateCommand.Parameters.Add("@ProvFilePathConten", OleDbType.VarChar, 0, "ProvFilePathConten");
                dataAdapter.UpdateCommand.Parameters.Add("@ProvNumFojas", OleDbType.Numeric, 0, "ProvNumFojas");
                dataAdapter.UpdateCommand.Parameters.Add("@Obs", OleDbType.Numeric, 0, "Obs");
                dataAdapter.UpdateCommand.Parameters.Add("@ObsFilePathOrigen", OleDbType.VarChar, 0, "ObsFilePathOrigen");
                dataAdapter.UpdateCommand.Parameters.Add("@ObsFilePathConten", OleDbType.VarChar, 0, "ObsFilePathConten");
                dataAdapter.UpdateCommand.Parameters.Add("@FRecepcion", OleDbType.Date, 0, "FRecepcion");
                dataAdapter.UpdateCommand.Parameters.Add("@FRecepcionInt", OleDbType.Numeric, 0, "FRecepcionInt");
                dataAdapter.UpdateCommand.Parameters.Add("@FEnvioObs", OleDbType.Date, 0, "FEnvioObs");
                dataAdapter.UpdateCommand.Parameters.Add("@FEnvioObsInt", OleDbType.Numeric, 0, "FEnvioObsInt");
                dataAdapter.UpdateCommand.Parameters.Add("@FDevolucion", OleDbType.Date, 0, "FDevolucion");
                dataAdapter.UpdateCommand.Parameters.Add("@FDevolucionInt", OleDbType.Numeric, 0, "FDevolucionInt");
                dataAdapter.UpdateCommand.Parameters.Add("@CCFilePathOrigen", OleDbType.VarChar, 0, "CCFilePathOrigen");
                dataAdapter.UpdateCommand.Parameters.Add("@CCFilePathConten", OleDbType.VarChar, 0, "CCFilePathConten");
                dataAdapter.UpdateCommand.Parameters.Add("@CCNumFojas", OleDbType.Numeric, 0, "CCNumFojas");
                dataAdapter.UpdateCommand.Parameters.Add("@VPFilePathOrigen", OleDbType.VarChar, 0, "VPFilePathOrigen");
                dataAdapter.UpdateCommand.Parameters.Add("@VPFilePathConten", OleDbType.VarChar, 0, "VPFilePathConten");
                dataAdapter.UpdateCommand.Parameters.Add("@VPNumFojas", OleDbType.Numeric, 0, "VPNumFojas");
                dataAdapter.UpdateCommand.Parameters.Add("@EstadoEjecutoria", OleDbType.Numeric, 0, "EstadoEjecutoria");
                dataAdapter.UpdateCommand.Parameters.Add("@IdEjecutoria", OleDbType.Numeric, 0, "IdEjecutoria");

                dataAdapter.Update(dataSet, "Ejecutorias");
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
        }

        public void UpdateObservacion(Observaciones observacion)
        {
            OleDbConnection connection = new OleDbConnection(connectionString);

            string sSql;
            OleDbDataAdapter dataAdapter;

            DataSet dataSet = new DataSet();
            DataRow dr;

            try
            {
                string sqlCadena = "SELECT * FROM ObservacionesEjecutoria WHERE IdObservacion = " + observacion.IdObservacion;

                dataAdapter = new OleDbDataAdapter();
                dataAdapter.SelectCommand = new OleDbCommand(sqlCadena, connection);

                dataAdapter.Fill(dataSet, "ObservacionesEjecutoria");

                dr = dataSet.Tables["ObservacionesEjecutoria"].Rows[0];
                dr.BeginEdit();
                dr["Foja"] = observacion.Foja;
                dr["Parrafo"] = observacion.Parrafo;
                dr["Renglon"] = observacion.Renglon;
                dr["Dice"] = observacion.Dice;
                dr["DicePlano"] = observacion.Dice;
                dr["SeSugiere"] = observacion.Sugiere;
                dr["SeSugierePlano"] = observacion.Sugiere;
                dr["Aceptada"] = observacion.IsAcepted;

                dr.EndEdit();

                dataAdapter.UpdateCommand = connection.CreateCommand();

                sSql = "UPDATE ObservacionesEjecutoria SET Foja = @Foja,Parrafo = @Parrafo,Renglon = @Renglon,Dice = @Dice,DicePlano = @DicePlano,SeSugiere = @SeSugiere,SeSugierePlano = @SeSugierePlano, Aceptada = @Aceptada " +
                       " WHERE IdObservacion = @IdObservacion";

                dataAdapter.InsertCommand.CommandText = sSql;

                dataAdapter.UpdateCommand.Parameters.Add("@Foja", OleDbType.Numeric, 0, "Foja");
                dataAdapter.UpdateCommand.Parameters.Add("@Parrafo", OleDbType.VarChar, 0, "Parrafo");
                dataAdapter.UpdateCommand.Parameters.Add("@Renglon", OleDbType.VarChar, 0, "Renglon");
                dataAdapter.UpdateCommand.Parameters.Add("@Dice", OleDbType.VarChar, 0, "Dice");
                dataAdapter.UpdateCommand.Parameters.Add("@DicePlano", OleDbType.VarChar, 0, "DicePlano");
                dataAdapter.UpdateCommand.Parameters.Add("@SeSugiere", OleDbType.VarChar, 0, "SeSugiere");
                dataAdapter.UpdateCommand.Parameters.Add("@SeSugierePlano", OleDbType.VarChar, 0, "SeSugierePlano");
                dataAdapter.UpdateCommand.Parameters.Add("@Aceptada", OleDbType.VarChar, 0, "Aceptada");
                dataAdapter.UpdateCommand.Parameters.Add("@IdObservacion", OleDbType.Numeric, 0, "IdObservacion");

                dataAdapter.Update(dataSet, "ObservacionesEjecutoria");
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
        }

        public void UpdatePrecedentes(PrecedentesTesis precedente)
        {
            OleDbConnection connection = new OleDbConnection(connectionString);

            string sSql;
            OleDbDataAdapter dataAdapter;

            DataSet dataSet = new DataSet();
            DataRow dr;

            try
            {
                string sqlCadena = "SELECT * FROM PrecedentesEjecutorias WHERE IdPrecedente = " + precedente.IdPrecedente;
                dataAdapter = new OleDbDataAdapter();
                dataAdapter.SelectCommand = new OleDbCommand(sqlCadena, connection);

                dataAdapter.Fill(dataSet, "PrecedentesEjecutorias");

                dr = dataSet.Tables["PrecedentesEjecutorias"].Rows[0];
                dr.BeginEdit();
                dr["IdTipoAsunto"] = precedente.TipoAsunto;
                dr["NumAsunto"] = precedente.NumAsunto;
                dr["YearAsunto"] = precedente.YearAsunto;

                if (precedente.FResolucion != null)
                {
                    dr["FResolucion"] = precedente.FResolucion;
                    dr["FResolucionInt"] = StringUtilities.DateToInt(precedente.FResolucion);
                }
                else
                {
                    dr["FResolucion"] = DBNull.Value;
                    dr["FResolucionInt"] = 0;
                }

                dr["IdPonente"] = precedente.IdPonente;
                dr["Promovente"] = precedente.Promovente;

                dr.EndEdit();

                dataAdapter.UpdateCommand = connection.CreateCommand();

                sSql = "UPDATE PrecedentesEjecutorias SET IdTipoAsunto = @IdTipoAsunto,NumAsunto = @NumAsunto,YearAsunto = @YearAsunto,FResolucion = @FResolucion," +
                       " FResolucionInt = @FResolucionInt,IdPonente = @IdPonente,Promovente = @Promovente " +
                       " WHERE IdPrecedente = @IdPrecedente";

                dataAdapter.UpdateCommand.CommandText = sSql;

                dataAdapter.UpdateCommand.Parameters.Add("@IdTipoAsunto", OleDbType.Numeric, 0, "IdTipoAsunto");
                dataAdapter.UpdateCommand.Parameters.Add("@NumAsunto", OleDbType.Numeric, 0, "NumAsunto");
                dataAdapter.UpdateCommand.Parameters.Add("@YearAsunto", OleDbType.Numeric, 0, "YearAsunto");
                dataAdapter.UpdateCommand.Parameters.Add("@FResolucion", OleDbType.Date, 0, "FResolucion");
                dataAdapter.UpdateCommand.Parameters.Add("@FResolucionInt", OleDbType.Numeric, 0, "FResolucionInt");
                dataAdapter.UpdateCommand.Parameters.Add("@IdPonente", OleDbType.Numeric, 0, "IdPonente");
                dataAdapter.UpdateCommand.Parameters.Add("@Promovente", OleDbType.VarChar, 0, "Promovente");
                dataAdapter.UpdateCommand.Parameters.Add("@IdPrecedente", OleDbType.Numeric, 0, "IdPrecedente");

                dataAdapter.Update(dataSet, "PrecedentesEjecutorias");
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
        }

        /// <summary>
        /// Devuelve la ejecutoria relacionada a una tesis
        /// </summary>
        /// <param name="idTesis"></param>
        /// <returns></returns>
        public Ejecutorias GetEjecutorias(int idTesis)
        {
            Ejecutorias ejecutoria = null;

            OleDbConnection oleConne = new OleDbConnection(connectionString);
            OleDbCommand cmd = null;
            OleDbDataReader reader = null;

            String sqlCadena = "SELECT * FROM Ejecutorias WHERE IdTesis = @idTesis";

            try
            {
                oleConne.Open();

                cmd = new OleDbCommand(sqlCadena, oleConne);
                cmd.Parameters.AddWithValue("@idTesis", idTesis);
                reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        ejecutoria = new Ejecutorias();
                        ejecutoria.IdEjecutoria = reader["IdEjecutoria"] as int? ?? -1;
                        ejecutoria.IdTesis = reader["IdTesis"] as int? ?? -1;
                        ejecutoria.ProvFilePathOrigen = reader["ProvFilePathOrigen"].ToString();
                        ejecutoria.ProvFilePathConten = reader["ProvFilePathConten"].ToString();
                        ejecutoria.ProvNumFojas = reader["Provnumfojas"] as int? ?? -1;
                        ejecutoria.ObsFilePathOrigen = reader["ObsFilePathOrigen"].ToString();
                        ejecutoria.ObsFilePathConten = reader["ObsFilePathConten"].ToString();
                        ejecutoria.FRecepcion = StringUtilities.GetDateFromReader(reader, "FRecepcion");
                        ejecutoria.FRecepcionInt = reader["FRecepcionInt"] as int? ?? -1;
                        ejecutoria.FEnvioObs = StringUtilities.GetDateFromReader(reader, "FEnvioObs");
                        ejecutoria.FEnvioObsInt = reader["FEnvioObsInt"] as int? ?? -1;
                        ejecutoria.FDevolucion = StringUtilities.GetDateFromReader(reader, "FDevolucion");
                        ejecutoria.FDevolucionInt = reader["FDevolucionInt"] as int? ?? -1;
                        ejecutoria.CcFilePathOrigen = reader["CcFilePathOrigen"].ToString();
                        ejecutoria.CcFilePathConten = reader["CcFilePathConten"].ToString();
                        ejecutoria.CcNumFojas = reader["CcNumFojas"] as int? ?? -1;
                        ejecutoria.VpFilePathOrigen = reader["VpFilePathOrigen"].ToString();
                        ejecutoria.VpFilePathConten = reader["VpFilePathConten"].ToString();
                        ejecutoria.VpNumFojas = reader["CcNumFojas"] as int? ?? -1;
                        ejecutoria.EstadoEjecutoria = reader["EstadoEjecutoria"] as int? ?? -1;
                        ejecutoria.Observaciones = this.GetObservaciones(ejecutoria.IdEjecutoria);
                        ejecutoria.Precedente = this.GetPrecedenteEjecutoria(ejecutoria.IdEjecutoria);
                    }
                }
                cmd.Dispose();
                reader.Close();
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
                oleConne.Close();
            }

            return ejecutoria;
        }

        public ObservableCollection<Ejecutorias> GetEjecutorias()
        {
            ObservableCollection<Ejecutorias> listadoEjecutorias = new ObservableCollection<Ejecutorias>();

            OleDbConnection oleConne = new OleDbConnection(connectionString);
            OleDbCommand cmd = null;
            OleDbDataReader reader = null;

            String sqlCadena = "SELECT * FROM Ejecutorias WHERE EstadoEjecutoria <= 5";

            try
            {
                oleConne.Open();

                cmd = new OleDbCommand(sqlCadena, oleConne);
                reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Ejecutorias ejecutoria = new Ejecutorias();
                        ejecutoria.IdEjecutoria = reader["IdEjecutoria"] as int? ?? -1;
                        ejecutoria.IdTesis = reader["IdTesis"] as int? ?? -1;
                        ejecutoria.ProvFilePathOrigen = reader["ProvFilePathOrigen"].ToString();
                        ejecutoria.ProvFilePathConten = reader["ProvFilePathConten"].ToString();
                        ejecutoria.ProvNumFojas = reader["Provnumfojas"] as int? ?? -1;
                        ejecutoria.ObsFilePathOrigen = reader["ObsFilePathOrigen"].ToString();
                        ejecutoria.ObsFilePathConten = reader["ObsFilePathConten"].ToString();
                        ejecutoria.FRecepcion = StringUtilities.GetDateFromReader(reader, "FRecepcion");
                        ejecutoria.FRecepcionInt = reader["FRecepcionInt"] as int? ?? -1;
                        ejecutoria.FEnvioObs = StringUtilities.GetDateFromReader(reader, "FEnvioObs");
                        ejecutoria.FEnvioObsInt = reader["FEnvioObsInt"] as int? ?? -1;
                        ejecutoria.FDevolucion = StringUtilities.GetDateFromReader(reader, "FDevolucion");
                        ejecutoria.FDevolucionInt = reader["FDevolucionInt"] as int? ?? -1;
                        ejecutoria.CcFilePathOrigen = reader["CcFilePathOrigen"].ToString();
                        ejecutoria.CcFilePathConten = reader["CcFilePathConten"].ToString();
                        ejecutoria.CcNumFojas = reader["CcNumFojas"] as int? ?? -1;

                        ejecutoria.VpFilePathOrigen = reader["VpFilePathOrigen"].ToString();
                        ejecutoria.VpFilePathConten = reader["VpFilePathConten"].ToString();
                        ejecutoria.VpNumFojas = reader["CcNumFojas"] as int? ?? -1;
                        ejecutoria.EstadoEjecutoria = reader["EstadoEjecutoria"] as int? ?? -1;
                        ejecutoria.Observaciones = this.GetObservaciones(ejecutoria.IdEjecutoria);
                        ejecutoria.Precedente = this.GetPrecedenteEjecutoria(ejecutoria.IdEjecutoria);
                        ejecutoria.Turno = new TurnoModel().GetTurno(3, ejecutoria.IdEjecutoria);
                        ejecutoria.Votos = new VotosModel().GetVoto(ejecutoria.IdEjecutoria);

                        listadoEjecutorias.Add(ejecutoria);
                    }
                }
                cmd.Dispose();
                reader.Close();
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
                oleConne.Close();
            }

            return listadoEjecutorias;
        }

        public ObservableCollection<Observaciones> GetObservaciones(int idEjecutoria)
        {
            ObservableCollection<Observaciones> listadoObservaciones = new ObservableCollection<Observaciones>();

            OleDbConnection oleConne = new OleDbConnection(connectionString);
            OleDbCommand cmd = null;
            OleDbDataReader reader = null;

            String sqlCadena = "SELECT * FROM ObservacionesEjecutoria WHERE IdEjecutoria = @idEjecutoria";

            try
            {
                oleConne.Open();

                cmd = new OleDbCommand(sqlCadena, oleConne);
                cmd.Parameters.AddWithValue("@idEjecutoria", idEjecutoria);
                reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Observaciones observacion = new Observaciones();
                        observacion.IdDocumento = reader["IdEjecutoria"] as int? ?? -1;
                        observacion.IdObservacion = reader["IdObservacion"] as int? ?? -1;
                        observacion.Foja = reader["Foja"].ToString();
                        observacion.Parrafo = reader["Parrafo"].ToString();
                        observacion.Renglon = reader["Renglon"].ToString();
                        observacion.Dice = reader["Dice"].ToString();
                        observacion.Dice = reader["DicePlano"].ToString();
                        observacion.Sugiere = reader["SeSugiere"].ToString();
                        observacion.Sugiere = reader["SeSugierePlano"].ToString();
                        observacion.IsAcepted = reader["Aceptada"] as int? ?? -1;

                        listadoObservaciones.Add(observacion);
                    }
                }
                cmd.Dispose();
                reader.Close();
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
                oleConne.Close();
            }

            return listadoObservaciones;
        }

        public PrecedentesTesis GetPrecedenteEjecutoria(int idEjecutoria)
        {
            PrecedentesTesis precedente = new PrecedentesTesis();

            OleDbConnection oleConne = new OleDbConnection(connectionString);
            OleDbCommand cmd = null;
            OleDbDataReader reader = null;

            String sqlCadena = "SELECT * FROM PrecedentesEjecutorias WHERE IdEjecutoria = @IdEjecutoria";

            try
            {
                oleConne.Open();

                cmd = new OleDbCommand(sqlCadena, oleConne);
                cmd.Parameters.AddWithValue("@IdEjecutoria", idEjecutoria);
                reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        precedente.IdPrecedente = reader["IdPrecedente"] as int? ?? -1;
                        precedente.IdTesis = idEjecutoria;
                        precedente.TipoAsunto = reader["IdTipoAsunto"] as int? ?? -1;
                        precedente.NumAsunto = reader["NumAsunto"] as int? ?? -1;
                        precedente.YearAsunto = reader["YearAsunto"] as int? ?? -1;
                        precedente.FResolucion = StringUtilities.GetDateFromReader(reader, "FResolucion");
                        precedente.IdPonente = reader["IdPonente"] as int? ?? -1;
                        precedente.Promovente = reader["Promovente"].ToString();
                    }
                }
                cmd.Dispose();
                reader.Close();
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
                oleConne.Close();
            }

            return precedente;
        }


        /// <summary>
        /// Elimina todas las observaciones asociadas al documento seleccionado
        /// </summary>
        /// <param name="observaciones">Colección de observaciones</param>
        public void DeleteObservaciones(ObservableCollection<Observaciones> observaciones)
        {
            foreach (Observaciones observ in observaciones)
                this.DeleteObservaciones(observ);
        }

        /// <summary>
        /// Elimina la observación seleccionada
        /// </summary>
        /// <param name="observacion">Observación que se va a eliminar</param>
        /// <returns></returns>
        public int DeleteObservaciones(Observaciones observacion)
        {

            OleDbConnection oleConne = new OleDbConnection(connectionString);
            OleDbCommand cmd = null;

            String sqlCadena = "DELETE FROM ObservacionesEjecutoria WHERE IdObservacion = @IdObservacion";
            int affectedRows = 0;
            try
            {
                oleConne.Open();

                cmd = new OleDbCommand(sqlCadena, oleConne);
                cmd.Parameters.AddWithValue("@IdObservacion", observacion.IdObservacion);
                affectedRows = cmd.ExecuteNonQuery();
                
                cmd.Dispose();
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
                oleConne.Close();
            }

            return affectedRows;
        }


        
    }
}