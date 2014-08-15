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
    public class VotosModel
    {

        readonly string connectionString = ConfigurationManager.ConnectionStrings["Modulo"].ToString();

        public void SetNewProyectoVoto(Votos voto,PrecedentesTesis precedente)
        {
            OleDbConnection connection = new OleDbConnection(connectionString);

            string sSql;
            OleDbDataAdapter dataAdapter;

            DataSet dataSet = new DataSet();
            DataRow dr;

            try
            {
                voto.IdVoto = AuxiliarModel.GetLastId("Votos", "IdVoto");

                string sqlCadena = "SELECT * FROM Votos WHERE IdVoto = 0";

                dataAdapter = new OleDbDataAdapter();
                dataAdapter.SelectCommand = new OleDbCommand(sqlCadena, connection);

                dataAdapter.Fill(dataSet, "Votos");

                dr = dataSet.Tables["Votos"].NewRow();
                dr["IdVoto"] = voto.IdVoto;
                dr["IdEjecutoria"] = voto.IdEjecutoria;
                dr["IdTipoVoto"] = voto.IdtipoVoto;
                dr["ForObservaciones"] = voto.ForObservaciones;
                dr["ProvFilePathOrigen"] = voto.ProvFilePathOrigen;
                dr["ProvFilePathConten"] = voto.ProvFilePathConten;
                dr["ProvNumFojas"] = voto.ProvNumFojas;
                dr["Obs"] = (voto.Observaciones.Count > 0) ? 1 : 0;
                dr["ObsFilePathOrigen"] = voto.ObsFilePathOrigen;
                dr["ObsFilePathConten"] = voto.ObsFilePathConten;

                if (voto.FRecepcion != null)
                {
                    dr["FRecepcion"] = voto.FRecepcion;
                    dr["FRecepcionInt"] = StringUtilities.DateToInt(voto.FRecepcion);
                }
                else
                {
                    dr["FRecepcion"] = DBNull.Value;
                    dr["FRecepcionInt"] = 0;
                }

                if (voto.FEnvioObs != null)
                {
                    dr["FEnvioObs"] = voto.FEnvioObs;
                    dr["FEnvioObsInt"] = StringUtilities.DateToInt(voto.FEnvioObs);
                }
                else
                {
                    dr["FEnvioObs"] = DBNull.Value;
                    dr["FEnvioObsInt"] = 0;
                }

                if (voto.FDevolucion != null)
                {
                    dr["FDevolucion"] = voto.FDevolucion;
                    dr["FDevolucionInt"] = StringUtilities.DateToInt(voto.FDevolucion);
                }
                else
                {
                    dr["FDevolucion"] = DBNull.Value;
                    dr["FDevolucionInt"] = 0;
                }

                dr["CCFilePathOrigen"] = voto.CcFilePathOrigen;
                dr["CCFilePathConten"] = voto.CcFilePathConten;
                dr["CCNumFojas"] = voto.CcNumFojas;
                dr["VPFilePathOrigen"] = voto.VpFilePathOrigen;
                dr["VPFilePathConten"] = voto.VpFilePathConten;
                dr["VPNumFojas"] = voto.VpNumFojas;
                dr["EstadoVoto"] = voto.EstadoVoto;

                dataSet.Tables["Votos"].Rows.Add(dr);

                dataAdapter.InsertCommand = connection.CreateCommand();

                sSql = "INSERT INTO Votos (IdVoto,IdEjecutoria,IdTipoVoto,ForObservaciones,ProvFilePathOrigen,ProvFilePathConten,ProvNumFojas,Obs,ObsFilePathOrigen,ObsFilePathConten," +
                       "FRecepcion,FRecepcionInt,FEnvioObs,FEnvioObsInt,FDevolucion,FDevolucionInt,CCFilePathOrigen,CCFilePathConten,CCNumFojas,VPFilePathOrigen,VPFilePathConten,VPNumFojas,EstadoVoto) " +
                       " VALUES (@IdVoto,@IdEjecutoria,@IdTipoVoto,@ForObservaciones,@ProvFilePathOrigen,@ProvFilePathConten,@ProvNumFojas,@Obs,@ObsFilePathOrigen,@ObsFilePathConten," +
                       "@FRecepcion,@FRecepccionInt,@FEnvioObs,@FEnvioObsInt,@FDevolucion,@FDevolucionInt,@CCFilePathOrigen,@CCFilePathConten,@CCNumFojas,@VPFilePathOrigen,@VPFilePathConten,@VPNumFojas,@EstadoVoto)";

                dataAdapter.InsertCommand.CommandText = sSql;

                dataAdapter.InsertCommand.Parameters.Add("@IdVoto", OleDbType.Numeric, 0, "IdVoto");
                dataAdapter.InsertCommand.Parameters.Add("@IdEjecutoria", OleDbType.Numeric, 0, "IdEjecutoria");
                dataAdapter.InsertCommand.Parameters.Add("@IdTipoVoto", OleDbType.Numeric, 0, "IdTipoVoto");
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
                dataAdapter.InsertCommand.Parameters.Add("@EstadoVoto", OleDbType.Numeric, 0, "EstadoVoto");

                dataAdapter.Update(dataSet, "Votos");
                dataSet.Dispose();
                dataAdapter.Dispose();

                this.SetPrecedentes(precedente, voto.IdVoto);
                this.SetNewObservacion(voto.Observaciones, voto.IdVoto);
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

        public void SetNewObservacion(ObservableCollection<Observaciones> observaciones, int idVoto)
        {
            foreach (Observaciones observacion in observaciones)
                SetNewObservacion(observacion, idVoto);
        }

        public void SetNewObservacion(Observaciones observacion, int idVoto)
        {
            OleDbConnection connection = new OleDbConnection(connectionString);

            string sSql;
            OleDbDataAdapter dataAdapter;

            DataSet dataSet = new DataSet();
            DataRow dr;

            try
            {
                observacion.IdObservacion = AuxiliarModel.GetLastId("ObservacionesVoto", "IdObservacion");

                string sqlCadena = "SELECT * FROM ObservacionesVoto WHERE IdObservacion = 0";

                dataAdapter = new OleDbDataAdapter();
                dataAdapter.SelectCommand = new OleDbCommand(sqlCadena, connection);

                dataAdapter.Fill(dataSet, "ObservacionesVoto");

                dr = dataSet.Tables["ObservacionesVoto"].NewRow();
                dr["IdObservacion"] = observacion.IdObservacion;
                dr["IdVoto"] = idVoto;
                dr["Foja"] = observacion.Foja;
                dr["Parrafo"] = observacion.Parrafo;
                dr["Renglon"] = observacion.Renglon;
                dr["Dice"] = observacion.Dice;
                dr["DicePlano"] = observacion.Dice;
                dr["SeSugiere"] = observacion.Sugiere;
                dr["SeSugierePlano"] = observacion.Sugiere;
                dr["Aceptada"] = observacion.IsAcepted;

                dataSet.Tables["ObservacionesVoto"].Rows.Add(dr);

                dataAdapter.InsertCommand = connection.CreateCommand();

                sSql = "INSERT INTO ObservacionesVoto (IdObservacion,IdVoto,Foja,Parrafo,Renglon,Dice,DicePlano,SeSugiere,SeSugierePlano,Aceptada) " +
                       " VALUES (@IdObservacion,@IdVoto,@Parrafo,@Renglon,@Dice,@DicePlano,@SeSugiere,@SeSugierePlano,@Aceptada)";

                dataAdapter.InsertCommand.CommandText = sSql;

                dataAdapter.InsertCommand.Parameters.Add("@IdObservacion", OleDbType.Numeric, 0, "IdObservacion");
                dataAdapter.InsertCommand.Parameters.Add("@IdVoto", OleDbType.Numeric, 0, "IdVoto");
                dataAdapter.InsertCommand.Parameters.Add("@Foja", OleDbType.Numeric, 0, "Foja");
                dataAdapter.InsertCommand.Parameters.Add("@Parrafo", OleDbType.VarChar, 0, "Parrafo");
                dataAdapter.InsertCommand.Parameters.Add("@Renglon", OleDbType.VarChar, 0, "Renglon");
                dataAdapter.InsertCommand.Parameters.Add("@Dice", OleDbType.VarChar, 0, "Dice");
                dataAdapter.InsertCommand.Parameters.Add("@DicePlano", OleDbType.VarChar, 0, "DicePlano");
                dataAdapter.InsertCommand.Parameters.Add("@SeSugiere", OleDbType.VarChar, 0, "SeSugiere");
                dataAdapter.InsertCommand.Parameters.Add("@SeSugierePlano", OleDbType.VarChar, 0, "SeSugierePlano");
                dataAdapter.InsertCommand.Parameters.Add("@Aceptada", OleDbType.VarChar, 0, "Aceptada");

                dataAdapter.Update(dataSet, "ObservacionesVoto");
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

        public void SetPrecedentes(PrecedentesTesis precedente, int idVoto)
        {
            OleDbConnection connection = new OleDbConnection(connectionString);

            string sSql;
            OleDbDataAdapter dataAdapter;

            DataSet dataSet = new DataSet();
            DataRow dr;

            try
            {
                precedente.IdPrecedente = AuxiliarModel.GetLastId("PrecedentesVotos", "IdPrecedente");

                string sqlCadena = "SELECT * FROM PrecedentesVotos WHERE IdPrecedente = 0";

                dataAdapter = new OleDbDataAdapter();
                dataAdapter.SelectCommand = new OleDbCommand(sqlCadena, connection);

                dataAdapter.Fill(dataSet, "PrecedentesVotos");

                dr = dataSet.Tables["PrecedentesVotos"].NewRow();
                dr["IdPrecedente"] = precedente.IdPrecedente;
                dr["IdVoto"] = idVoto;
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

                dataSet.Tables["PrecedentesVotos"].Rows.Add(dr);

                dataAdapter.InsertCommand = connection.CreateCommand();

                sSql = "INSERT INTO PrecedentesVotos (IdPrecedente,IdVoto,IdTipoAsunto,NumAsunto,YearAsunto,FResolucion,FResolucionInt,IdPonente,Promovente) " +
                       " VALUES (@IdPrecedente,@IdVoto,@IdTipoAsunto,@NumAsunto,@YearAsunto,@FResolucion,@FResolucionInt,@IdPonente,@Promovente)";

                dataAdapter.InsertCommand.CommandText = sSql;

                dataAdapter.InsertCommand.Parameters.Add("@IdPrecedente", OleDbType.Numeric, 0, "IdPrecedente");
                dataAdapter.InsertCommand.Parameters.Add("@IdVoto", OleDbType.Numeric, 0, "IdVoto");
                dataAdapter.InsertCommand.Parameters.Add("@IdTipoAsunto", OleDbType.Numeric, 0, "IdTipoAsunto");
                dataAdapter.InsertCommand.Parameters.Add("@NumAsunto", OleDbType.Numeric, 0, "NumAsunto");
                dataAdapter.InsertCommand.Parameters.Add("@YearAsunto", OleDbType.Numeric, 0, "YearAsunto");
                dataAdapter.InsertCommand.Parameters.Add("@FResolucion", OleDbType.Date, 0, "FResolucion");
                dataAdapter.InsertCommand.Parameters.Add("@FResolucionInt", OleDbType.Numeric, 0, "FResolucionInt");
                dataAdapter.InsertCommand.Parameters.Add("@IdPonente", OleDbType.Numeric, 0, "IdPonente");
                dataAdapter.InsertCommand.Parameters.Add("@Promovente", OleDbType.VarChar, 0, "Promovente");

                dataAdapter.Update(dataSet, "PrecedentesVotos");
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

        public void UpdateProyectoVoto(Votos votos)
        {
            OleDbConnection connection = new OleDbConnection(connectionString);

            string sSql;
            OleDbDataAdapter dataAdapter;

            DataSet dataSet = new DataSet();
            DataRow dr;

            try
            {
                string sqlCadena = "SELECT * FROM Votos WHERE IdVoto = " + votos.IdVoto;

                dataAdapter = new OleDbDataAdapter();
                dataAdapter.SelectCommand = new OleDbCommand(sqlCadena, connection);

                dataAdapter.Fill(dataSet, "Votos");

                dr = dataSet.Tables["Votos"].Rows[0];
                dr.BeginEdit();
                dr["IdEjecutoria"] = votos.IdEjecutoria;
                dr["ProvFilePathOrigen"] = votos.ProvFilePathOrigen;
                dr["ProvFilePathConten"] = votos.ProvFilePathConten;
                dr["ProvNumFojas"] = votos.ProvNumFojas;
                dr["Obs"] = (votos.Observaciones.Count > 0) ? 1 : 0;
                dr["ObsFilePathOrigen"] = votos.ObsFilePathOrigen;
                dr["ObsFilePathConten"] = votos.ObsFilePathConten;

                if (votos.FRecepcion != null)
                {
                    dr["FRecepcion"] = votos.FRecepcion;
                    dr["FRecepcionInt"] = StringUtilities.DateToInt(votos.FRecepcion);
                }
                else
                {
                    dr["FRecepcion"] = DBNull.Value;
                    dr["FRecepcionInt"] = 0;
                }

                if (votos.FEnvioObs != null)
                {
                    dr["FEnvioObs"] = votos.FEnvioObs;
                    dr["FEnvioObsInt"] = StringUtilities.DateToInt(votos.FEnvioObs);
                }
                else
                {
                    dr["FEnvioObs"] = DBNull.Value;
                    dr["FEnvioObsInt"] = 0;
                }

                if (votos.FDevolucion != null)
                {
                    dr["FDevolucion"] = votos.FDevolucion;
                    dr["FDevolucionInt"] = StringUtilities.DateToInt(votos.FDevolucion);
                }
                else
                {
                    dr["FDevolucion"] = DBNull.Value;
                    dr["FDevolucionInt"] = 0;
                }

                dr["CCFilePathOrigen"] = votos.CcFilePathOrigen;
                dr["CCFilePathConten"] = votos.CcFilePathConten;
                dr["CCNumFojas"] = votos.CcNumFojas;
                dr["VPFilePathOrigen"] = votos.VpFilePathOrigen;
                dr["VPFilePathConten"] = votos.VpFilePathConten;
                dr["VPNumFojas"] = votos.VpNumFojas;
                dr["EstadoVoto"] = votos.EstadoVoto;

                dr.EndEdit();

                dataAdapter.UpdateCommand = connection.CreateCommand();

                sSql = "UPDATE Votos SET IdEjecutoria = @IdEjecutoria, ProvFilePathOrigen = @ProvFilePathOrigen,ProvFilePathConten = @ProvFilePathConten,ProvNumFojas = @ProvNumFojas,Obs = @Obs, " +
                       "ObsFilePathOrigen = @ObsFilePathOrigen,ObsFilePathConten = @ObsFilePathConten," +
                       "FRecepcion = @FRecepcion,FRecepcionInt = @FRecepcionInt,FEnvioObs = @FEnvioObs,FEnvioObsInt = @FEnvioObsInt,FDevolucion = @FDevolucion,FDevolucionInt = @FDevolucionInt, " +
                       "CCFilePathOrigen = @CCFilePathOrigen,CCFilePathConten = @CCFilePathConten,CCNumFojas = @CCNumFojas,VPFilePathOrigen = @VPFilePathOrigen,VPFilePathConten = @VPFilePathConten," +
                       "VPNumFojas = @VPNumFojas,EstadoVoto = @EstadoVoto " +
                       " WHERE IdVoto = @IdVoto";

                dataAdapter.UpdateCommand.CommandText = sSql;

                dataAdapter.UpdateCommand.Parameters.Add("@IdEjecutoria", OleDbType.Numeric, 0, "IdEjecutoria");
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
                dataAdapter.UpdateCommand.Parameters.Add("@EstadoVoto", OleDbType.Numeric, 0, "EstadoVoto");
                dataAdapter.UpdateCommand.Parameters.Add("@IdVoto", OleDbType.Numeric, 0, "IdVoto");

                dataAdapter.Update(dataSet, "Votos");
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
                string sqlCadena = "SELECT * FROM ObservacionesVoto WHERE IdObservacion = " + observacion.IdObservacion;

                dataAdapter = new OleDbDataAdapter();
                dataAdapter.SelectCommand = new OleDbCommand(sqlCadena, connection);

                dataAdapter.Fill(dataSet, "ObservacionesVoto");

                dr = dataSet.Tables["ObservacionesVoto"].Rows[0];
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

                sSql = "UPDATE ObservacionesVoto SET Foja = @Foja,Parrafo = @Parrafo,Renglon = @Renglon,Dice = @Dice,DicePlano = @DicePlano,SeSugiere = @SeSugiere,SeSugierePlano = @SeSugierePlano, Aceptada = @Aceptada " +
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

                dataAdapter.Update(dataSet, "ObservacionesVoto");
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
                string sqlCadena = "SELECT * FROM PrecedentesVotos WHERE IdPrecedente = " + precedente.IdPrecedente;
                dataAdapter = new OleDbDataAdapter();
                dataAdapter.SelectCommand = new OleDbCommand(sqlCadena, connection);

                dataAdapter.Fill(dataSet, "PrecedentesVotos");

                dr = dataSet.Tables["PrecedentesVotos"].Rows[0];
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

                sSql = "UPDATE PrecedentesVotos SET IdTipoAsunto = @IdTipoAsunto,NumAsunto = @NumAsunto,YearAsunto = @YearAsunto,FResolucion = @FResolucion," +
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

                dataAdapter.Update(dataSet, "PrecedentesVotos");
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

        public ObservableCollection<Votos> GetVoto()
        {
            ObservableCollection<Votos> listadoVotos = new ObservableCollection<Votos>();

            OleDbConnection oleConne = new OleDbConnection(connectionString);
            OleDbCommand cmd = null;
            OleDbDataReader reader = null;

            String sqlCadena = "SELECT * FROM Votos WHERE EstadoVoto < 5";

            try
            {
                oleConne.Open();

                cmd = new OleDbCommand(sqlCadena, oleConne);
                reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Votos voto = new Votos();
                        voto.IdVoto = reader["IdVoto"] as int? ?? -1;
                        voto.IdEjecutoria = reader["IdEjecutoria"] as int? ?? -1;
                        voto.ProvFilePathOrigen = reader["ProvFilePathOrigen"].ToString();
                        voto.ProvFilePathConten = reader["ProvFilePathConten"].ToString();
                        voto.ProvNumFojas = reader["Provnumfojas"] as int? ?? -1;
                        voto.ObsFilePathOrigen = reader["ObsFilePathOrigen"].ToString();
                        voto.ObsFilePathConten = reader["ObsFilePathConten"].ToString();
                        voto.FRecepcion = StringUtilities.GetDateFromReader(reader, "FRecepcion");
                        voto.FRecepcionInt = reader["FRecepcionInt"] as int? ?? -1;
                        voto.FEnvioObs = StringUtilities.GetDateFromReader(reader, "FEnvioObs");
                        voto.FEnvioObsInt = reader["FEnvioObsInt"] as int? ?? -1;
                        voto.FDevolucion = StringUtilities.GetDateFromReader(reader, "FDevolucion");
                        voto.FDevolucionInt = reader["FDevolucionInt"] as int? ?? -1;
                        voto.CcFilePathOrigen = reader["CcFilePathOrigen"].ToString();
                        voto.CcFilePathConten = reader["CcFilePathConten"].ToString();
                        voto.CcNumFojas = reader["CcNumFojas"] as int? ?? -1;

                        voto.VpFilePathOrigen = reader["VpFilePathOrigen"].ToString();
                        voto.VpFilePathConten = reader["VpFilePathConten"].ToString();
                        voto.VpNumFojas = reader["CcNumFojas"] as int? ?? -1;
                        voto.EstadoVoto = reader["EstadoVoto"] as int? ?? -1;
                        voto.Observaciones = this.GetObservaciones(voto.IdVoto);
                        voto.Precedente = this.GetPrecedenteEjecutoria(voto.IdVoto);
                        listadoVotos.Add(voto);
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

            return listadoVotos;
        }

        
        /// <summary>
        /// Devuelve la coleccion de votos relacionados a una ejecutoria en particular
        /// </summary>
        /// <param name="idEjecutoria">Identificador de la ejecutoria de la cual queremos obtener todos sus votos relacionados</param>
        /// <returns></returns>
        public ObservableCollection<Votos> GetVoto(int idEjecutoria)
        {
            ObservableCollection<Votos> listadoVotos = new ObservableCollection<Votos>();

            OleDbConnection oleConne = new OleDbConnection(connectionString);
            OleDbCommand cmd = null;
            OleDbDataReader reader = null;

            String sqlCadena = "SELECT * FROM Votos WHERE EstadoVoto < 5 AND IdEjecutoria = @IdEjecutoria";

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
                        Votos voto = new Votos();
                        voto.IdVoto = reader["IdVoto"] as int? ?? -1;
                        voto.IdEjecutoria = reader["IdEjecutoria"] as int? ?? -1;
                        voto.ProvFilePathOrigen = reader["ProvFilePathOrigen"].ToString();
                        voto.ProvFilePathConten = reader["ProvFilePathConten"].ToString();
                        voto.ProvNumFojas = reader["Provnumfojas"] as int? ?? -1;
                        voto.ObsFilePathOrigen = reader["ObsFilePathOrigen"].ToString();
                        voto.ObsFilePathConten = reader["ObsFilePathConten"].ToString();
                        voto.FRecepcion = StringUtilities.GetDateFromReader(reader, "FRecepcion");
                        voto.FRecepcionInt = reader["FRecepcionInt"] as int? ?? -1;
                        voto.FEnvioObs = StringUtilities.GetDateFromReader(reader, "FEnvioObs");
                        voto.FEnvioObsInt = reader["FEnvioObsInt"] as int? ?? -1;
                        voto.FDevolucion = StringUtilities.GetDateFromReader(reader, "FDevolucion");
                        voto.FDevolucionInt = reader["FDevolucionInt"] as int? ?? -1;
                        voto.CcFilePathOrigen = reader["CcFilePathOrigen"].ToString();
                        voto.CcFilePathConten = reader["CcFilePathConten"].ToString();
                        voto.CcNumFojas = reader["CcNumFojas"] as int? ?? -1;

                        voto.VpFilePathOrigen = reader["VpFilePathOrigen"].ToString();
                        voto.VpFilePathConten = reader["VpFilePathConten"].ToString();
                        voto.VpNumFojas = reader["CcNumFojas"] as int? ?? -1;
                        voto.EstadoVoto = reader["EstadoVoto"] as int? ?? -1;
                        voto.Observaciones = this.GetObservaciones(voto.IdVoto);
                        voto.Precedente = this.GetPrecedenteEjecutoria(voto.IdVoto);
                        listadoVotos.Add(voto);
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

            return listadoVotos;
        }

        public ObservableCollection<Observaciones> GetObservaciones(int idVoto)
        {
            ObservableCollection<Observaciones> listadoObservaciones = new ObservableCollection<Observaciones>();

            OleDbConnection oleConne = new OleDbConnection(connectionString);
            OleDbCommand cmd = null;
            OleDbDataReader reader = null;

            String sqlCadena = "SELECT * FROM ObservacionesVoto WHERE IdVoto = @IdVoto";

            try
            {
                oleConne.Open();

                cmd = new OleDbCommand(sqlCadena, oleConne);
                cmd.Parameters.AddWithValue("@IdVoto", idVoto);
                reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Observaciones observacion = new Observaciones();
                        observacion.IdDocumento = reader["IdVoto"] as int? ?? -1;
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

        public PrecedentesTesis GetPrecedenteEjecutoria(int idVoto)
        {
            PrecedentesTesis precedente = new PrecedentesTesis();

            OleDbConnection oleConne = new OleDbConnection(connectionString);
            OleDbCommand cmd = null;
            OleDbDataReader reader = null;

            String sqlCadena = "SELECT * FROM PrecedentesVotos WHERE IdVoto = @IdVoto";

            try
            {
                oleConne.Open();

                cmd = new OleDbCommand(sqlCadena, oleConne);
                cmd.Parameters.AddWithValue("@IdVoto", idVoto);
                reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        precedente.IdPrecedente = reader["IdPrecedente"] as int? ?? -1;
                        precedente.IdTesis = idVoto;
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


        public int DeleteObservaciones(Observaciones observacion)
        {

            OleDbConnection oleConne = new OleDbConnection(connectionString);
            OleDbCommand cmd = null;

            String sqlCadena = "DELETE FROM ObservacionesVoto WHERE IdObservacion = @IdObservacion";
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