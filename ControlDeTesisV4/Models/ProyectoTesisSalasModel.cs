using System;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Windows.Forms;
using ControlDeTesisV4.Dao;
using ControlDeTesisV4.UtilitiesFolder;
using ScjnUtilities;

namespace ControlDeTesisV4.Models
{
    public class ProyectoTesisSalasModel : TesisCommonModel
    {
        readonly ProyectosSalas proyectoSalas;
        readonly string connectionString = ConfigurationManager.ConnectionStrings["Modulo"].ToString();

        public ProyectoTesisSalasModel(ProyectosSalas proyectoSalas)
            : base(proyectoSalas.IdTipoProyecto)
        {
            this.proyectoSalas = proyectoSalas;
        }

        public ProyectoTesisSalasModel()
            : base(1)
        {
        }

        #region Proyecto

        public void SetNewProyecto()
        {
            OleDbConnection connection = new OleDbConnection(connectionString);

            string sSql;
            OleDbDataAdapter dataAdapter;

            DataSet dataSet = new DataSet();
            DataRow dr;

            try
            {
                proyectoSalas.IdProyecto = AuxiliarModel.GetLastId("Proyectos", "IdProyecto");

                string sqlCadena = "SELECT * FROM Proyectos WHERE IdProyecto = 0";

                dataAdapter = new OleDbDataAdapter();
                dataAdapter.SelectCommand = new OleDbCommand(sqlCadena, connection);

                dataAdapter.Fill(dataSet, "Proyectos");

                dr = dataSet.Tables["Proyectos"].NewRow();

                dr["IdProyecto"] = proyectoSalas.IdProyecto;
                dr["ReferenciaOficialia"] = proyectoSalas.Referencia;

                if (proyectoSalas.FRecepcion != null)
                {
                    dr["FRecepcion"] = proyectoSalas.FRecepcion;
                    dr["FRecepcionInt"] = DateTimeUtilities.DateToInt(proyectoSalas.FRecepcion);
                }
                else
                {
                    dr["FRecepcion"] = DBNull.Value;
                    dr["FRecepcionInt"] = 0;
                }

                dr["OficioRecepcion"] = proyectoSalas.OficioRecepcion;
                dr["IdEmisor"] = proyectoSalas.IdEmisor;
                dr["IdSignatario"] = proyectoSalas.IdSignatario;
                dr["OficioPathOrigen"] = proyectoSalas.OfRecepcionPathOrigen;
                dr["OficioPathConten"] = proyectoSalas.OfRecepcionPathConten;
                dr["Ejecutoria"] = proyectoSalas.Ejecutoria;

                if (proyectoSalas.FRecepcionPrograma != null)
                {
                    dr["FRecPrograma"] = proyectoSalas.FRecepcionPrograma;
                    dr["FRecProgramaInt"] = DateTimeUtilities.DateToInt(proyectoSalas.FRecepcionPrograma);
                }
                else
                {
                    dr["FRecPrograma"] = DBNull.Value;
                    dr["FRecProgramaInt"] = 0;
                }

                if (proyectoSalas.FTentSesion != null)
                {
                    dr["FTentSesion"] = proyectoSalas.FTentSesion;
                    dr["FTentSesionInt"] = DateTimeUtilities.DateToInt(proyectoSalas.FTentSesion);
                }
                else
                {
                    dr["FTentSesion"] = DBNull.Value;
                    dr["FTentSesionInt"] = 0;
                }

                dataSet.Tables["Proyectos"].Rows.Add(dr);

                dataAdapter.InsertCommand = connection.CreateCommand();

                sSql = "INSERT INTO Proyectos (IdProyecto,ReferenciaOficialia,FRecepcion,FRecepcionInt,OficioRecepcion,IdEmisor,IdSignatario,OficioPathOrigen,OficioPathConten,Ejecutoria," +
                       "FRecPrograma,FRecProgramaInt,FTentSesion,FTentSesionInt) " +
                       " VALUES (@IdProyecto,@ReferenciaOficialia,@FRecepcion,@FRecepcionInt,@OficioRecepcion,@IdEmisor,@IdSignatario,@OficioPathOrigen,@OficioPathConten,@Ejecutoria," +
                       "@FRecPrograma,@FRecProgramaInt,@FTentSesion,@FTentSesionInt)";

                dataAdapter.InsertCommand.CommandText = sSql;

                dataAdapter.InsertCommand.Parameters.Add("@IdProyecto", OleDbType.Numeric, 0, "IdProyecto");
                dataAdapter.InsertCommand.Parameters.Add("@ReferenciaOficialia", OleDbType.VarChar, 0, "ReferenciaOficialia");
                dataAdapter.InsertCommand.Parameters.Add("@FRecepcion", OleDbType.Date, 0, "FRecepcion");
                dataAdapter.InsertCommand.Parameters.Add("@FRecepcionInt", OleDbType.Numeric, 0, "FRecepcionInt");
                dataAdapter.InsertCommand.Parameters.Add("@OficioRecepcion", OleDbType.VarChar, 0, "OficioRecepcion");
                dataAdapter.InsertCommand.Parameters.Add("@IdEmisor", OleDbType.Numeric, 0, "IdEmisor");
                dataAdapter.InsertCommand.Parameters.Add("@IdSignatario", OleDbType.Numeric, 0, "IdSignatario");
                dataAdapter.InsertCommand.Parameters.Add("@OficioPathOrigen", OleDbType.VarChar, 0, "OficioPathOrigen");
                dataAdapter.InsertCommand.Parameters.Add("@OficioPathConten", OleDbType.VarChar, 0, "OficioPathConten");
                dataAdapter.InsertCommand.Parameters.Add("@Ejecutoria", OleDbType.Numeric, 0, "Ejecutoria");
                dataAdapter.InsertCommand.Parameters.Add("@FRecPrograma", OleDbType.Date, 0, "FRecPrograma");
                dataAdapter.InsertCommand.Parameters.Add("@FRecProgramaInt", OleDbType.Numeric, 0, "FRecProgramaInt");
                dataAdapter.InsertCommand.Parameters.Add("@FTentSesion", OleDbType.Date, 0, "FTentSesion");
                dataAdapter.InsertCommand.Parameters.Add("@FTentSesionInt", OleDbType.Numeric, 0, "FTentSesionInt");

                dataAdapter.Update(dataSet, "Proyectos");
                dataSet.Dispose();
                dataAdapter.Dispose();

                if (proyectoSalas.Proyectos != null)
                    this.SetNewTesisProyecto(proyectoSalas.Proyectos,proyectoSalas.IdProyecto);
                else if (proyectoSalas.Proyecto != null)
                    this.SetNewProyectoTesis(proyectoSalas.Proyecto);
                
            }
            catch (OleDbException ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                ErrorUtilities.SetNewErrorMessage(ex, methodName + " Exception,ProyectoTesisSalasModel", "ControlTesis");
            }
            catch (Exception ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                ErrorUtilities.SetNewErrorMessage(ex, methodName + " Exception,ProyectoTesisSalasModel", "ControlTesis");
            }
            finally
            {
                connection.Close();
            }
        }

        /// <summary>
        /// Actualiza la información de las tesis en proceso de publicación, los estados en los que se puede encontrar la tesis son
        /// los siguientes:
        /// 1. Recepcion
        /// 2. ENvio de Observaciones o envío del proyecto
        /// 3. Aprobación
        /// 4. Turno
        /// 5. Publicación
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

                dr["Rubro"] = tesis.Rubro;
                dr["Tatj"] = tesis.Tatj;
                dr["TipoJuris"] = tesis.IdTipoJuris;
                dr["NumTesis"] = tesis.NumTesis;
                dr["NumTesisInt"] = tesis.NumTesisInt;
                dr["YearTesis"] = tesis.YearTesis;
                dr["ClaveTesis"] = tesis.ClaveTesis;
                dr["EstadoTesis"] = tesis.EstadoTesis;
                dr["IdAbogado"] = tesis.IdAbogadoResponsable;

                dr.EndEdit();

                dataAdapter.UpdateCommand = connection.CreateCommand();

                sSql = "UPDATE ProyectosTesis SET OficioEnvio = @OficioEnvio, FechaEnvioOficio = @FechaEnvioOficio,FechaEnvioOficioInt = @FechaEnvioOficioInt," +
                       "OficioEnvioPathOrigen = @OficioEnvioPathOrigen,OficioEnvioPathConten = @OficioEnvioPathConten,Rubro = @Rubro,Tatj = @Tatj, TipoJuris = @TipoJuris," +
                       "FAprobacion = @FAprobacion, FAprobacionInt = @FAprobacionInt, NumTesis = @NumTesis, NumTesisInt = @NumTesisInt, YearTesis = @YearTesis, ClaveTesis = @ClaveTesis, EstadoTesis = @EstadoTesis, IdAbogado = @IdAbogado " +
                       " WHERE IdTesis = @IdTesis";
                dataAdapter.UpdateCommand.CommandText = sSql;

                dataAdapter.UpdateCommand.Parameters.Add("@OficioEnvio", OleDbType.VarChar, 0, "OficioEnvio");
                dataAdapter.UpdateCommand.Parameters.Add("@FechaEnvioOficio", OleDbType.Date, 0, "FechaEnvioOficio");
                dataAdapter.UpdateCommand.Parameters.Add("@FechaEnvioOficioInt", OleDbType.Numeric, 0, "FechaEnvioOficioInt");
                dataAdapter.UpdateCommand.Parameters.Add("@OficioEnvioPathOrigen", OleDbType.VarChar, 0, "OficioEnvioPathOrigen");
                dataAdapter.UpdateCommand.Parameters.Add("@OficioEnvioPathConten", OleDbType.VarChar, 0, "OficioEnvioPathConten");
                dataAdapter.UpdateCommand.Parameters.Add("@Rubro", OleDbType.VarChar, 0, "Rubro");
                dataAdapter.UpdateCommand.Parameters.Add("@Tatj", OleDbType.Numeric, 0, "Tatj");
                dataAdapter.UpdateCommand.Parameters.Add("@TipoJuris", OleDbType.Numeric, 0, "TipoJuris");
                dataAdapter.UpdateCommand.Parameters.Add("@FAprobacion", OleDbType.Date, 0, "FAprobacion");
                dataAdapter.UpdateCommand.Parameters.Add("@FAprobacionInt", OleDbType.Numeric, 0, "FAprobacionInt");
                dataAdapter.UpdateCommand.Parameters.Add("@NumTesis", OleDbType.VarChar, 0, "NumTesis");
                dataAdapter.UpdateCommand.Parameters.Add("@NumTesisInt", OleDbType.Numeric, 0, "NumTesisInt");
                dataAdapter.UpdateCommand.Parameters.Add("@YearTesis", OleDbType.Numeric, 0, "YearTesis");
                dataAdapter.UpdateCommand.Parameters.Add("@ClaveTesis", OleDbType.VarChar, 0, "ClaveTesis");
                dataAdapter.UpdateCommand.Parameters.Add("@EstadoTesis", OleDbType.Numeric, 0, "EstadoTesis");
                dataAdapter.UpdateCommand.Parameters.Add("@IdAbogado", OleDbType.Numeric, 0, "IdAbogado");
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
                ErrorUtilities.SetNewErrorMessage(ex, methodName + " Exception,ProyectoTesisSalasModel", "ControlTesis");
            }
            catch (Exception ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                ErrorUtilities.SetNewErrorMessage(ex, methodName + " Exception,ProyectoTesisSalasModel", "ControlTesis");
            }
            finally
            {
                connection.Close();
            }
        }

        public void UpdateProyectoTesis(ProyectosTesis tesis,int mesPublicacion)
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

                dr["Rubro"] = tesis.Rubro;
                dr["Tatj"] = tesis.Tatj;
                dr["TipoJuris"] = tesis.IdTipoJuris;
                dr["NumTesis"] = tesis.NumTesis;
                dr["NumTesisInt"] = tesis.NumTesisInt;
                dr["YearTesis"] = tesis.YearTesis;
                dr["ClaveTesis"] = tesis.ClaveTesis;
                dr["EstadoTesis"] = tesis.EstadoTesis;
                dr["IdAbogado"] = tesis.IdAbogadoResponsable;
                dr["MesPublica"] = mesPublicacion;

                if (tesis.FPublicacion != null)
                {
                    dr["FechaPublicacion"] = tesis.FPublicacion;
                    dr["FechaPublicacionInt"] = DateTimeUtilities.DateToInt(tesis.FPublicacion);
                }
                else
                {
                    dr["FechaPublicacion"] = DBNull.Value;
                    dr["FechaPublicacionInt"] = 0;
                }

                dr.EndEdit();

                dataAdapter.UpdateCommand = connection.CreateCommand();

                sSql = "UPDATE ProyectosTesis SET OficioEnvio = @OficioEnvio, FechaEnvioOficio = @FechaEnvioOficio,FechaEnvioOficioInt = @FechaEnvioOficioInt," +
                       "OficioEnvioPathOrigen = @OficioEnvioPathOrigen,OficioEnvioPathConten = @OficioEnvioPathConten,Rubro = @Rubro,Tatj = @Tatj, TipoJuris = @TipoJuris," +
                       "FAprobacion = @FAprobacion, FAprobacionInt = @FAprobacionInt, NumTesis = @NumTesis, NumTesisInt = @NumTesisInt, " +
                       "YearTesis = @YearTesis, ClaveTesis = @ClaveTesis, EstadoTesis = @EstadoTesis, IdAbogado = @IdAbogado, MesPublica = @MesPublica, FechaPublicacion = @FechaPublicacion, FechaPublicacionInt = @FechaPublicacionInt " +
                       " WHERE IdTesis = @IdTesis";
                dataAdapter.UpdateCommand.CommandText = sSql;

                dataAdapter.UpdateCommand.Parameters.Add("@OficioEnvio", OleDbType.VarChar, 0, "OficioEnvio");
                dataAdapter.UpdateCommand.Parameters.Add("@FechaEnvioOficio", OleDbType.Date, 0, "FechaEnvioOficio");
                dataAdapter.UpdateCommand.Parameters.Add("@FechaEnvioOficioInt", OleDbType.Numeric, 0, "FechaEnvioOficioInt");
                dataAdapter.UpdateCommand.Parameters.Add("@OficioEnvioPathOrigen", OleDbType.VarChar, 0, "OficioEnvioPathOrigen");
                dataAdapter.UpdateCommand.Parameters.Add("@OficioEnvioPathConten", OleDbType.VarChar, 0, "OficioEnvioPathConten");
                dataAdapter.UpdateCommand.Parameters.Add("@Rubro", OleDbType.VarChar, 0, "Rubro");
                dataAdapter.UpdateCommand.Parameters.Add("@Tatj", OleDbType.Numeric, 0, "Tatj");
                dataAdapter.UpdateCommand.Parameters.Add("@TipoJuris", OleDbType.Numeric, 0, "TipoJuris");
                dataAdapter.UpdateCommand.Parameters.Add("@FAprobacion", OleDbType.Date, 0, "FAprobacion");
                dataAdapter.UpdateCommand.Parameters.Add("@FAprobacionInt", OleDbType.Numeric, 0, "FAprobacionInt");
                dataAdapter.UpdateCommand.Parameters.Add("@NumTesis", OleDbType.VarChar, 0, "NumTesis");
                dataAdapter.UpdateCommand.Parameters.Add("@NumTesisInt", OleDbType.Numeric, 0, "NumTesisInt");
                dataAdapter.UpdateCommand.Parameters.Add("@YearTesis", OleDbType.Numeric, 0, "YearTesis");
                dataAdapter.UpdateCommand.Parameters.Add("@ClaveTesis", OleDbType.VarChar, 0, "ClaveTesis");
                dataAdapter.UpdateCommand.Parameters.Add("@EstadoTesis", OleDbType.Numeric, 0, "EstadoTesis");
                dataAdapter.UpdateCommand.Parameters.Add("@IdAbogado", OleDbType.Numeric, 0, "IdAbogado");
                dataAdapter.UpdateCommand.Parameters.Add("@MesPublica", OleDbType.Numeric, 0, "MesPublica");
                dataAdapter.UpdateCommand.Parameters.Add("@FechaPublicacion", OleDbType.Date, 0, "FechaPublicacion");
                dataAdapter.UpdateCommand.Parameters.Add("@FechaPublicacionInt", OleDbType.Numeric, 0, "FechaPublicacionInt");
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
                ErrorUtilities.SetNewErrorMessage(ex, methodName + " Exception,ProyectoTesisSalasModel", "ControlTesis");
            }
            catch (Exception ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                ErrorUtilities.SetNewErrorMessage(ex, methodName + " Exception,ProyectoTesisSalasModel", "ControlTesis");
            }
            finally
            {
                connection.Close();
            }
        }

        /// <summary>
        /// Elimina los datos de recepción de un proyecto
        /// </summary>
        /// <param name="idProyecto">Identificador del Proyecto que se va a eliminar</param>
        public void DeleteProyecto(int idProyecto)
        {
            int tesisNumber = this.GetTesisNumberByProyecto(idProyecto);

            if (tesisNumber > 1)
            {
                OleDbConnection connection = new OleDbConnection(connectionString);
                OleDbCommand cmd = new OleDbCommand();

                try
                {
                    cmd = connection.CreateCommand();
                    cmd.CommandText = "DELETE FROM Proyectos WHERE IdProyecto = @IdProyecto";
                    cmd.Parameters.AddWithValue("@IdProyecto", idProyecto);

                    connection.Open();
                    cmd.ExecuteNonQuery();
                    connection.Close();
                }
                catch (OleDbException ex)
                {
                    string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                    ErrorUtilities.SetNewErrorMessage(ex, methodName + " Exception,ProyectoTesisSalasModel", "ControlTesis");
                }
                catch (Exception ex)
                {
                    string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                    ErrorUtilities.SetNewErrorMessage(ex, methodName + " Exception,ProyectoTesisSalasModel", "ControlTesis");
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        #endregion

        #region Tesis


        public void SetNewProyectoTesis(ProyectosTesis tesis)
        {
            OleDbConnection connection = new OleDbConnection(connectionString);

            string sSql;
            OleDbDataAdapter dataAdapter;

            DataSet dataSet = new DataSet();
            DataRow dr;

            try
            {
                tesis.IdTesis = AuxiliarModel.GetLastId("ProyectosTesis", "IdTesis");

                string sqlCadena = "SELECT * FROM ProyectosTesis WHERE IdProyecto = 0";

                dataAdapter = new OleDbDataAdapter();
                dataAdapter.SelectCommand = new OleDbCommand(sqlCadena, connection);

                dataAdapter.Fill(dataSet, "ProyectosTesis");

                dr = dataSet.Tables["ProyectosTesis"].NewRow();
                dr["IdTesis"] = tesis.IdTesis;
                dr["IdProyecto"] = proyectoSalas.IdProyecto;
                dr["IdTipoProyecto"] = proyectoSalas.IdTipoProyecto;
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
                dr["Rubro"] = tesis.Rubro;
                dr["Tatj"] = tesis.Tatj;
                dr["TipoJuris"] = tesis.IdTipoJuris;
                dr["NumPaginas"] = tesis.NumPaginas;
                dr["IdAbogado"] = tesis.IdAbogadoResponsable;
                dr["IdInstancia"] = tesis.IdInstancia;
                dr["IdSubInstancia"] = tesis.IdSubInstancia;
                dr["Aprobada"] = tesis.Aprobada;

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
                dr["YearTesis"] = tesis.YearTesis;
                dr["ClaveTesis"] = tesis.ClaveTesis;
                dr["EstadoTesis"] = 1;

                dataSet.Tables["ProyectosTesis"].Rows.Add(dr);

                dataAdapter.InsertCommand = connection.CreateCommand();

                sSql = "INSERT INTO ProyectosTesis (IdTesis,IdProyecto,IdTipoProyecto,OficioEnvio,FechaEnvioOficio,FechaEnvioOficioInt,OficioEnvioPathOrigen," +
                       "OficioEnvioPathConten,Rubro,Tatj,TipoJuris,NumPaginas,IdAbogado,IdInstancia,IdSubInstancia,Aprobada,FAprobacion,FAprobacionInt,NumTesis,YearTesis,ClaveTesis,EstadoTesis) " +
                       " VALUES (@IdTesis,@IdProyecto,@IdTipoProyecto,@OficioEnvio,@FechaEnvioOficio,@FechaEnvioOficioInt,@OficioEnvioPathOrigen," +
                       "@OficioEnvioPathConten,@Rubro,@Tatj,@TipoJuris,@NumPaginas,@IdAbogado,@IdInstancia,@IdSubInstancia,@Aprobada,@FAprobacion,@FAprobacionInt,@NumTesis,@YearTesis,@ClaveTesis,@EstadoTesis)";

                dataAdapter.InsertCommand.CommandText = sSql;

                dataAdapter.InsertCommand.Parameters.Add("@IdTesis", OleDbType.Numeric, 0, "IdTesis");
                dataAdapter.InsertCommand.Parameters.Add("@IdProyecto", OleDbType.Numeric, 0, "IdProyecto");
                dataAdapter.InsertCommand.Parameters.Add("@IdTipoProyecto", OleDbType.Numeric, 0, "IdTipoProyecto");
                dataAdapter.InsertCommand.Parameters.Add("@OficioEnvio", OleDbType.VarChar, 0, "OficioEnvio");
                dataAdapter.InsertCommand.Parameters.Add("@FechaEnvioOficio", OleDbType.Date, 0, "FechaEnvioOficio");
                dataAdapter.InsertCommand.Parameters.Add("@FechaEnvioOficioInt", OleDbType.Numeric, 0, "FechaEnvioOficioInt");
                dataAdapter.InsertCommand.Parameters.Add("@OficioEnvioPathOrigen", OleDbType.VarChar, 0, "OficioEnvioPathOrigen");
                dataAdapter.InsertCommand.Parameters.Add("@OficioEnvioPathConten", OleDbType.VarChar, 0, "OficioEnvioPathConten");
                dataAdapter.InsertCommand.Parameters.Add("@Rubro", OleDbType.VarChar, 0, "Rubro");
                dataAdapter.InsertCommand.Parameters.Add("@Tatj", OleDbType.Numeric, 0, "Tatj");
                dataAdapter.InsertCommand.Parameters.Add("@TipoJuris", OleDbType.Numeric, 0, "TipoJuris");
                dataAdapter.InsertCommand.Parameters.Add("@NumPaginas", OleDbType.Numeric, 0, "NumPaginas");
                dataAdapter.InsertCommand.Parameters.Add("@IdAbogado", OleDbType.Numeric, 0, "IdAbogado");
                dataAdapter.InsertCommand.Parameters.Add("@IdInstancia", OleDbType.Numeric, 0, "IdInstancia");
                dataAdapter.InsertCommand.Parameters.Add("@IdSubInstancia", OleDbType.Numeric, 0, "IdSubInstancia");
                dataAdapter.InsertCommand.Parameters.Add("@Aprobada", OleDbType.Numeric, 0, "Aprobada");
                dataAdapter.InsertCommand.Parameters.Add("@FAprobacion", OleDbType.Date, 0, "FAprobacion");
                dataAdapter.InsertCommand.Parameters.Add("@FAprobacionInt", OleDbType.Numeric, 0, "FAprobacionInt");
                dataAdapter.InsertCommand.Parameters.Add("@NumTesis", OleDbType.Numeric, 0, "NumTesis");
                dataAdapter.InsertCommand.Parameters.Add("@YearTesis", OleDbType.Numeric, 0, "YearTesis");
                dataAdapter.InsertCommand.Parameters.Add("@ClaveTesis", OleDbType.VarChar, 0, "ClaveTesis");
                dataAdapter.InsertCommand.Parameters.Add("@EstadoTesis", OleDbType.Numeric, 0, "EstadoTesis");

                dataAdapter.Update(dataSet, "ProyectosTesis");
                dataSet.Dispose();
                dataAdapter.Dispose();

                this.SetTesisCompara(tesis.ComparaTesis, tesis.IdTesis);
                this.SetPrecedentes(tesis.Precedente, tesis.IdTesis);

                AddNewProyectToList(tesis);
            }
            catch (OleDbException ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                ErrorUtilities.SetNewErrorMessage(ex, methodName + " Exception,ProyectoTesisSalasModel", "ControlTesis");
            }
            catch (Exception ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                ErrorUtilities.SetNewErrorMessage(ex, methodName + " Exception,ProyectoTesisSalasModel", "ControlTesis");
            }
            finally
            {
                connection.Close();
            }
        }

        private void AddNewProyectToList(ProyectosTesis proyecto)
        {
            TesisTurnadaPreview tesis = new TesisTurnadaPreview();
            tesis.IdTesis = proyecto.IdTesis;
            tesis.Idabogado = proyecto.IdAbogadoResponsable;
            tesis.ClaveTesis = proyecto.ClaveTesis;
            tesis.Rubro = proyecto.Rubro;
            tesis.IdTipoAsunto = proyecto.Precedente.TipoAsunto;
            tesis.NumAsunto = proyecto.Precedente.NumAsunto;
            tesis.YearAsunto = proyecto.Precedente.YearAsunto;
            tesis.IdInstancia = proyecto.IdInstancia;
            tesis.EstadoTesis = 4;

            Constants.ListadoDeTesis.Add(tesis);
        }

        /// <summary>
        /// Devueleve el proyecto de tesis completo de la tesis seleccionada
        /// </summary>
        /// <param name="idTesis"></param>
        /// <returns></returns>
        public ProyectosTesis GetProyectoTesis(int idTesis)
        {
            ProyectosTesis tesis = new ProyectosTesis();

            OleDbConnection connection = new OleDbConnection(connectionString);
            OleDbCommand cmd = null;
            OleDbDataReader reader = null;

            String sqlCadena = "SELECT * FROM ProyectosTesis WHERE IdTesis = @IdTesis";

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
                        tesis.IdProyecto = Convert.ToInt32(reader["IdProyecto"]);
                        tesis.IdTesis = idTesis;
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
                        tesis.Ejecutoria = new EjecutoriasModel().GetEjecutorias(tesis.IdTesis);
                        tesis.Precedente = this.GetPrecedenteTesis(tesis.IdTesis);
                        tesis.ComparaTesis = this.GetTesisCompara(idTesis);
                        tesis.Turno = new TurnoModel().GetTurno(tesis.IdTipoJuris + 1, tesis.IdTesis);
                        tesis.MesPublica = reader["MesPublica"] as int? ?? -1; 
                    }
                }
                cmd.Dispose();
                reader.Close();
            }
            catch (OleDbException ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                ErrorUtilities.SetNewErrorMessage(ex, methodName + " Exception,ProyectoTesisSalasModel", "ControlTesis");
            }
            catch (Exception ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                ErrorUtilities.SetNewErrorMessage(ex, methodName + " Exception,ProyectoTesisSalasModel", "ControlTesis");
            }
            finally
            {
                connection.Close();
            }

            return tesis;
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
                sqlCadena = "SELECT ProyectosTesis.*, Proyectos.FRecepcion, Proyectos.IdEmisor " +
                            " FROM Proyectos INNER JOIN ProyectosTesis ON Proyectos.IdProyecto = ProyectosTesis.IdProyecto WHERE FechaEnvioOficioInt LIKE '" + inicio + "%' AND idTipoProyecto = 1";
            else
                sqlCadena = "SELECT ProyectosTesis.*, Proyectos.FRecepcion, Proyectos.IdEmisor  " +
                            " FROM Proyectos INNER JOIN ProyectosTesis ON Proyectos.IdProyecto = ProyectosTesis.IdProyecto WHERE (FechaEnvioOficioInt Between " + inicio + " and " + fin + ") AND idTipoProyecto = 1";

            ProyectosTesis tesis = new ProyectosTesis();
            try
            {
                connection.Open();

                cmd = new OleDbCommand(sqlCadena, connection);
                reader = cmd.ExecuteReader();

                

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        tesis = new ProyectosTesis();

                        tesis.IdTesis = Convert.ToInt32(reader["IdTesis"]);
                        tesis.IdProyecto = Convert.ToInt32(reader["IdProyecto"]);
                        tesis.IdEmisor = Convert.ToInt32(reader["IdEmisor"]);
                        tesis.OficioEnvio = reader["OficioEnvio"].ToString();
                        tesis.FRecepcion = DateTimeUtilities.GetDateFromReader(reader, "FRecepcion");
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
                        tesis.Ejecutoria = new EjecutoriasModel().GetEjecutorias(tesis.IdTesis);
                        tesis.Precedente = this.GetPrecedenteTesis(tesis.IdTesis);
                        tesis.ComparaTesis = this.GetTesisCompara(tesis.IdTesis);

                        listaDeTesis.Add(tesis);
                    }
                }
                cmd.Dispose();
                reader.Close();
            }
            catch (OleDbException ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                ErrorUtilities.SetNewErrorMessage(ex, methodName + " Exception,ProyectoTesisSalasModel", "ControlTesis");
                MessageBox.Show(tesis.IdTesis.ToString());
            }
            catch (Exception ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                ErrorUtilities.SetNewErrorMessage(ex, methodName + " Exception,ProyectoTesisSalasModel", "ControlTesis");
                MessageBox.Show(tesis.IdTesis.ToString());
            }
            finally
            {
                connection.Close();
            }

            return listaDeTesis;
        }

        /// <summary>
        /// Obtiene las tesis a incluir en el reporte de plenos de Circuito que se esta solicitando
        /// </summary>
        /// <param name="inicio">Fecha de inicio para incluir tesis</param>
        /// <param name="fin">Último día válido para incluir tesis</param>
        /// <returns></returns>
        public ObservableCollection<ProyectosTesis> GetTesisReportePlenos(int inicio, int fin)
        {
            ObservableCollection<ProyectosTesis> listaDeTesis = new ObservableCollection<ProyectosTesis>();

            string year = inicio.ToString().Substring(0, 4);
            string month = inicio.ToString().Substring(4, 2);

            OleDbConnection connection = new OleDbConnection(connectionString);
            OleDbCommand cmd = null;
            OleDbDataReader reader = null;

            String sqlCadena = "SELECT ConsTesisPlenos.*, Proyectos.FRecepcion, Proyectos.IdEmisor " +
                            "FROM ConsTesisPlenos INNER JOIN Proyectos ON ConsTesisPlenos.IdProyecto = Proyectos.IdProyecto " +
                            " WHERE IdTipoProyecto = 1 AND " +
                            "((MesPublica = " + month + " AND YEAR(FAprobacion) = " + year + ") OR (MesPublica = 0 AND (MONTH(FechaEnvioOficio) <= " + month + " AND YEAR(FechaEnvioOficio) = " + year + " )))";

            ProyectosTesis tesis = new ProyectosTesis();
            try
            {
                connection.Open();

                cmd = new OleDbCommand(sqlCadena, connection);
                reader = cmd.ExecuteReader();



                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        tesis = new ProyectosTesis();

                        tesis.IdTesis = Convert.ToInt32(reader["IdTesis"]);
                        tesis.IdProyecto = Convert.ToInt32(reader["IdProyecto"]);
                        tesis.IdEmisor = Convert.ToInt32(reader["IdEmisor"]);
                        tesis.OficioEnvio = reader["OficioEnvio"].ToString();
                        tesis.FRecepcion = DateTimeUtilities.GetDateFromReader(reader, "FRecepcion");
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
                        tesis.Ejecutoria = new EjecutoriasModel().GetEjecutorias(tesis.IdTesis);
                        tesis.Precedente = this.GetPrecedenteTesis(tesis.IdTesis);
                        tesis.ComparaTesis = this.GetTesisCompara(tesis.IdTesis);

                        listaDeTesis.Add(tesis);
                    }
                }
                cmd.Dispose();
                reader.Close();
            }
            catch (OleDbException ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                ErrorUtilities.SetNewErrorMessage(ex, methodName + " Exception,ProyectoTesisSalasModel", "ControlTesis");
            }
            catch (Exception ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                ErrorUtilities.SetNewErrorMessage(ex, methodName + " Exception,ProyectoTesisSalasModel", "ControlTesis");
            }
            finally
            {
                connection.Close();
            }

            return listaDeTesis;
        }

        /// <summary>
        /// Obtiene los datos generales de llegada del proyecto correspondiente
        /// </summary>
        /// <param name="idProyecto"></param>
        /// <returns></returns>
        public ProyectosSalas GetDatosLlegada(int idTesis)
        {
            ProyectosSalas proyecto = null;

            OleDbConnection connection = new OleDbConnection(connectionString);
            OleDbCommand cmd = null;
            OleDbDataReader reader = null;

            String sqlCadena = "SELECT PC.*, PT.IdProyecto FROM Proyectos PC INNER JOIN ProyectosTesis PT ON PC.IdProyecto = PT.IdProyecto WHERE IdTesis = @IdTesis";

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
                        proyecto = new ProyectosSalas();
                        proyecto.IdProyecto = reader["PT.IdProyecto"] as int? ?? -1; 
                        proyecto.Referencia = reader["ReferenciaOficialia"].ToString();
                        proyecto.FRecepcion = DateTimeUtilities.GetDateFromReader(reader, "FRecepcion");
                        proyecto.OficioRecepcion = reader["OficioRecepcion"].ToString();
                        proyecto.IdEmisor = reader["IdEmisor"] as int? ?? -1;
                        proyecto.IdSignatario = reader["IdSignatario"] as int? ?? -1;
                        proyecto.OfRecepcionPathOrigen = reader["OficioPathOrigen"].ToString();
                        proyecto.OfRecepcionPathConten = reader["OficioPathConten"].ToString();
                    }
                }
                cmd.Dispose();
                reader.Close();
            }
            catch (OleDbException ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                ErrorUtilities.SetNewErrorMessage(ex, methodName + " Exception,ProyectoTesisSalasModel", "ControlTesis");
            }
            catch (Exception ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                ErrorUtilities.SetNewErrorMessage(ex, methodName + " Exception,ProyectoTesisSalasModel", "ControlTesis");
            }
            finally
            {
                connection.Close();
            }

            return proyecto;
        } 

        public void UpdateDatosLlegada(ProyectosSalas tesis)
        {
            OleDbConnection connection = new OleDbConnection(connectionString);

            string sSql;
            OleDbDataAdapter dataAdapter;

            DataSet dataSet = new DataSet();
            DataRow dr;

            try
            {
                string sqlCadena = "SELECT * FROM Proyectos WHERE IdProyecto = @IdProyecto";

                dataAdapter = new OleDbDataAdapter();
                dataAdapter.SelectCommand = new OleDbCommand(sqlCadena, connection);
                dataAdapter.SelectCommand.Parameters.AddWithValue("@IdProyecto", tesis.IdProyecto);

                dataAdapter.Fill(dataSet, "Proyectos");

                dr = dataSet.Tables["Proyectos"].Rows[0];
                dr.BeginEdit();
                dr["ReferenciaOficialia"] = tesis.Referencia;
                dr["OficioRecepcion"] = tesis.OficioRecepcion;

                if (tesis.FRecepcion != null)
                {
                    dr["FRecepcion"] = tesis.FRecepcion;
                    dr["FRecepcionInt"] = DateTimeUtilities.DateToInt(tesis.FRecepcion);
                }
                else
                {
                    dr["FRecepcion"] = DBNull.Value;
                    dr["FRecepcionInt"] = 0;
                }

                dr["OficioPathOrigen"] = tesis.OfRecepcionPathOrigen;
                dr["OficioPathConten"] = tesis.OfRecepcionPathConten;
                dr["IdSignatario"] = tesis.IdSignatario;
                dr["IdEmisor"] = tesis.IdEmisor;

                dr.EndEdit();

                dataAdapter.UpdateCommand = connection.CreateCommand();

                sSql = "UPDATE Proyectos SET ReferenciaOficialia = @ReferenciaOficialia, OficioRecepcion = @OficioRecepcion,FRecepcion = @FRecepcion," +
                       "FRecepcionInt = @FRecepcionInt,OficioPathOrigen = @OficioPathOrigen, " +
                       "OficioPathConten = @OficioPathConten, " +
                       "IdSignatario = @IdSignatario,IdEmisor = @IdEmisor " +
                       " WHERE IdProyecto = @IdProyecto";
                dataAdapter.UpdateCommand.CommandText = sSql;

                dataAdapter.UpdateCommand.Parameters.Add("@ReferenciaOficialia", OleDbType.VarChar, 0, "ReferenciaOficialia");
                dataAdapter.UpdateCommand.Parameters.Add("@OficioRecepcion", OleDbType.VarChar, 0, "OficioRecepcion");
                dataAdapter.UpdateCommand.Parameters.Add("@FRecepcion", OleDbType.Date, 0, "FRecepcion");
                dataAdapter.UpdateCommand.Parameters.Add("@FRecepcionInt", OleDbType.Numeric, 0, "FRecepcionInt");
                dataAdapter.UpdateCommand.Parameters.Add("@OficioPathOrigen", OleDbType.VarChar, 0, "OficioPathOrigen");
                dataAdapter.UpdateCommand.Parameters.Add("@OficioPathConten", OleDbType.VarChar, 0, "OficioPathConten");
                dataAdapter.UpdateCommand.Parameters.Add("@IdSignatario", OleDbType.Numeric, 0, "IdSignatario");
                dataAdapter.UpdateCommand.Parameters.Add("@IdEmisor", OleDbType.Numeric, 0, "IdEmisor");
                dataAdapter.UpdateCommand.Parameters.Add("@IdProyecto", OleDbType.Numeric, 0, "IdProyecto");

                dataAdapter.Update(dataSet, "Proyectos");
                dataSet.Dispose();
                dataAdapter.Dispose();

            }
            catch (OleDbException ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;

                MessageBox.Show("Error ({0}) : {1}" + ex.Source + ex.Message, methodName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                ErrorUtilities.SetNewErrorMessage(ex, methodName, 0);
            }
            catch (Exception ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;

                MessageBox.Show("Error ({0}) : {1}" + ex.Source + ex.Message, methodName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                ErrorUtilities.SetNewErrorMessage(ex, methodName, 0);
            }
            finally
            {
                connection.Close();
            }
        }

        #endregion


        #region Tesis compara

        /// <summary>
        /// Actualiza los datos comparados de la tesis seleccionada, los cuales incluyen la versión
        /// en texto plano, la versión en RTF, así como la ruta de acceso del archivo original
        /// </summary>
        /// <param name="tesis"></param>
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

                dr["TextoOriginal"] = tesis.TextoOriginal;
                dr["TOPlano"] = tesis.TOPlano;
                dr["ToFilePathOrigen"] = tesis.ToFilePathOrigen;
                dr["ToFilePathConten"] = tesis.ToFilePathConten;
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

                sSql = "UPDATE TesisCompara SET TextoOriginal = @TextoOriginal, TOPlano = @TOPlano, ToFilePathOrigen = @ToFilePathOrigen, ToFilePathConten = @TOFilePathConten," +
                    " TextoRevision1 = @TextoRevision1, TR1Plano = @TR1Plano,TObsFilePathOrigen = @TObsFilePathOrigen," +
                       "TObsFilePathConten = @TObsFilePathConten, TextoRevision2 = @TextoRevision2, TR2Plano = @TR2Plano,TAprobFilePathOrigen = @TAprobFilePathOrigen," +
                       "TAprobFilePathConten = @TAprobFilePathConten " +
                       " WHERE IdTesis = @IdTesis";
                dataAdapter.UpdateCommand.CommandText = sSql;

                dataAdapter.UpdateCommand.Parameters.Add("@TextoOriginal", OleDbType.VarChar, 0, "TextoOriginal");
                dataAdapter.UpdateCommand.Parameters.Add("@TOPlano", OleDbType.VarChar, 0, "TOPlano");
                dataAdapter.UpdateCommand.Parameters.Add("@ToFilePathOrigen", OleDbType.VarChar, 0, "ToFilePathOrigen");
                dataAdapter.UpdateCommand.Parameters.Add("@ToFilePathConten", OleDbType.VarChar, 0, "ToFilePathConten");
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
                ErrorUtilities.SetNewErrorMessage(ex, methodName + " Exception,ProyectoTesisSalasModel", "ControlTesis");
            }
            catch (Exception ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                ErrorUtilities.SetNewErrorMessage(ex, methodName + " Exception,ProyectoTesisSalasModel", "ControlTesis");
            }
            finally
            {
                connection.Close();
            }
        }

        

        #endregion
    }
}