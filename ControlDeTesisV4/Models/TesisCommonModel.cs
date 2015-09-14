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
    /// <summary>
    /// Contiene los métodos comunes a los proyectos de las Salas y de la CCST
    /// </summary>
    public class TesisCommonModel
    {
        readonly string connectionString = ConfigurationManager.ConnectionStrings["Modulo"].ToString();
        private readonly int idTipoProyecto;

        public TesisCommonModel(int idTipoProyecto)
        {
            this.idTipoProyecto = idTipoProyecto;
        }



        #region Tesis


        /// <summary>
        /// Inserta cada una de las tesis que pertenecen a un mismo proyecto
        /// </summary>
        /// <param name="listaProyectos">Colección de tesis pertenecientes a un proyecto</param>
        /// <param name="idProyecto">Identificador del proyecto que contiene las tesis</param>
        public void SetNewTesisProyecto(ObservableCollection<ProyectosTesis> listaProyectos, int idProyecto)
        {
            OleDbConnection connection = new OleDbConnection(connectionString);

            string sSql;
            OleDbDataAdapter dataAdapter;

            DataSet dataSet = new DataSet();
            DataRow dr;

            try
            {
                foreach (ProyectosTesis tesis in listaProyectos)
                {
                    tesis.IdTesis = AuxiliarModel.GetLastId("ProyectosTesis", "IdTesis");

                    string sqlCadena = "SELECT * FROM ProyectosTesis WHERE IdProyecto = 0";

                    dataAdapter = new OleDbDataAdapter();
                    dataAdapter.SelectCommand = new OleDbCommand(sqlCadena, connection);

                    dataAdapter.Fill(dataSet, "ProyectosTesis");

                    dr = dataSet.Tables["ProyectosTesis"].NewRow();
                    dr["IdTesis"] = tesis.IdTesis;
                    dr["IdProyecto"] = idProyecto;
                    dr["IdTipoProyecto"] = idTipoProyecto;
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
                    dr["EstadoTesis"] = 2;

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

                    this.SetPrecedentes(tesis.Precedente, tesis.IdTesis);
                    this.SetTesisCompara(tesis.ComparaTesis, tesis.IdTesis);
                }

            }
            catch (OleDbException ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                ErrorUtilities.SetNewErrorMessage(ex, methodName + " Exception,TesisCommonModel", "ControlTesis");
            }
            catch (Exception ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                ErrorUtilities.SetNewErrorMessage(ex, methodName + " Exception,TesisCommonModel", "ControlTesis");
            }
            finally
            {
                connection.Close();
            }
        }


        /// <summary>
        /// Elimina el detalle de la tesis seleccionada
        /// </summary>
        /// <param name="idTesis">Identificador de la tesis que se va a eliminar</param>
        public void DeleteTesisProyecto(int idTesis)
        {
            OleDbConnection connection = new OleDbConnection(connectionString);
            OleDbCommand cmd = new OleDbCommand();

            try
            {
                cmd = connection.CreateCommand();
                cmd.CommandText = "DELETE FROM ProyectosTesis WHERE IdTesis = @IdTesis";
                cmd.Parameters.AddWithValue("@IdTesis", idTesis);

                connection.Open();
                cmd.ExecuteNonQuery();
                connection.Close();
            }
            catch (OleDbException ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                ErrorUtilities.SetNewErrorMessage(ex, methodName + " Exception,TesisCommonModel", "ControlTesis");
            }
            catch (Exception ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                ErrorUtilities.SetNewErrorMessage(ex, methodName + " Exception,TesisCommonModel", "ControlTesis");
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
                sqlCadena = "SELECT * FROM ProyectosTesis WHERE FechaEnvioOficioInt LIKE '" + inicio + "%' AND idTipoProyecto = " + idTipoProyecto;
            else
                sqlCadena = "SELECT * FROM ProyectosTesis WHERE (FechaEnvioOficioInt Between " + inicio + " and " + fin + ") AND idTipoProyecto = " + idTipoProyecto;

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

                        tesis.IdTesis = reader["IdTesis"] as int? ?? -1;
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
                ErrorUtilities.SetNewErrorMessage(ex, methodName + " Exception,TesisCommonModel", "ControlTesis");
            }
            catch (Exception ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                ErrorUtilities.SetNewErrorMessage(ex, methodName + " Exception,TesisCommonModel", "ControlTesis");
            }
            finally
            {
                connection.Close();
            }

            return listaDeTesis;
        }

        /// <summary>
        /// Devuelve el número de tesis que tiene el proyecto seleccionado
        /// </summary>
        /// <param name="idProyecto"></param>
        /// <returns></returns>
        public int GetTesisNumberByProyecto(int idProyecto)
        {
            int total = 0;
            OleDbConnection connection = new OleDbConnection(connectionString);
            OleDbCommand cmd = null;
            OleDbDataReader reader = null;

            String sqlCadena = "SELECT IdProyecto,COUNT(Idproyecto) AS Total FROM ProyectosTesis WHERE IdProyecto = @IdProyecto AND IdTipoProyecto = @IdTipoProyecto GROUP BY idproyecto";

            try
            {
                connection.Open();

                cmd = new OleDbCommand(sqlCadena, connection);
                cmd.Parameters.AddWithValue("@IdProyecto", idProyecto);
                cmd.Parameters.AddWithValue("@IdTipoProyecto", idTipoProyecto);
                reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        total = Convert.ToInt32(reader["Total"]);
                    }
                }
                cmd.Dispose();
                reader.Close();
            }
            catch (OleDbException ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                ErrorUtilities.SetNewErrorMessage(ex, methodName + " Exception,TesisCommonModel", "ControlTesis");
            }
            catch (Exception ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                ErrorUtilities.SetNewErrorMessage(ex, methodName + " Exception,TesisCommonModel", "ControlTesis");
            }
            finally
            {
                connection.Close();
            }

            return total;
        }



        #endregion

        #region TesisCompara

        /// <summary>
        /// Ingresa los datos de comparación de la tesis en primera instancia, es decir,
        /// los datos de la propuesta generada por las salas o la enviada por la CCST a 
        /// las mismas para su aprobación
        /// </summary>
        /// <param name="tesisCompara">Tesis que se esta guardando</param>
        /// <param name="idTesis">Identificador de la tesis</param>
        public void SetTesisCompara(TesisCompara tesisCompara, int idTesis)
        {
            OleDbConnection connection = new OleDbConnection(connectionString);

            string sSql;
            OleDbDataAdapter dataAdapter;

            DataSet dataSet = new DataSet();
            DataRow dr;

            try
            {
                string sqlCadena = "SELECT * FROM TesisCompara WHERE Id = 0";

                dataAdapter = new OleDbDataAdapter();
                dataAdapter.SelectCommand = new OleDbCommand(sqlCadena, connection);

                dataAdapter.Fill(dataSet, "TesisCompara");

                dr = dataSet.Tables["TesisCompara"].NewRow();
                dr["IdTesis"] = idTesis;
                dr["TextoOriginal"] = tesisCompara.TextoOriginal;
                dr["TOPlano"] = tesisCompara.TOPlano;
                dr["TOrigenAlfab"] = tesisCompara.TOrigenAlfab;
                dr["ToFilePathOrigen"] = tesisCompara.ToFilePathOrigen;
                dr["ToFilePathConten"] = tesisCompara.ToFilePathConten;
                dr["TextoRevision1"] = (idTipoProyecto == 1) ?  tesisCompara.TObservaciones : String.Empty;
                dr["TR1Plano"] = (idTipoProyecto == 1) ?  tesisCompara.TObservacionesPlano : String.Empty;
                dr["TObsFilePathOrigen"] = (idTipoProyecto == 1) ?  tesisCompara.TObsFilePathOrigen : String.Empty;
                dr["TObsFilePathConten"] = (idTipoProyecto == 1) ? tesisCompara.TObsFilePathConten : String.Empty;
                dr["TextoRevision2"] = tesisCompara.TAprobada;
                dr["TR2Plano"] = tesisCompara.TAprobadaPlano;
                dr["TAprobFilePathOrigen"] = tesisCompara.TAprobFilePathOrigen;
                dr["TAprobFilePathConten"] = tesisCompara.TAprobFilePathConten;

                dataSet.Tables["TesisCompara"].Rows.Add(dr);

                dataAdapter.InsertCommand = connection.CreateCommand();

                sSql = "INSERT INTO TesisCompara (IdTesis,TextoOriginal,TOPlano,TOrigenAlfab,ToFilePathOrigen,ToFilePathConten,TextoRevision1," +
                       "TR1Plano,TObsFilePathOrigen,TObsFilePathConten,TextoRevision2,TR2Plano,TAprobFilePathOrigen,TAprobFilePathConten) " +
                       " VALUES (@IdTesis,@TextoOriginal,@TOPlano,@TOrigenAlfab,@ToFilePathOrigen,@ToFilePathConten,@TextoRevision1," +
                       "@TR1Plano,@TObsFilePathOrigen,@TObsFilePathConten,@TextoRevision2,@TR2Plano,@TAprobFilePathOrigen,@TAprobFilePathConten)";

                dataAdapter.InsertCommand.CommandText = sSql;

                dataAdapter.InsertCommand.Parameters.Add("@IdTesis", OleDbType.Numeric, 0, "IdTesis");
                dataAdapter.InsertCommand.Parameters.Add("@TextoOriginal", OleDbType.VarChar, 0, "TextoOriginal");
                dataAdapter.InsertCommand.Parameters.Add("@TOPlano", OleDbType.VarChar, 0, "TOPlano");
                dataAdapter.InsertCommand.Parameters.Add("@TOrigenAlfab", OleDbType.VarChar, 0, "TOrigenAlfab");
                dataAdapter.InsertCommand.Parameters.Add("@ToFilePathOrigen", OleDbType.VarChar, 0, "ToFilePathOrigen");
                dataAdapter.InsertCommand.Parameters.Add("@ToFilePathConten", OleDbType.VarChar, 0, "ToFilePathConten");
                dataAdapter.InsertCommand.Parameters.Add("@TextoRevision1", OleDbType.VarChar, 0, "TextoRevision1");
                dataAdapter.InsertCommand.Parameters.Add("@TR1Plano", OleDbType.VarChar, 0, "TR1Plano");
                dataAdapter.InsertCommand.Parameters.Add("@TObsFilePathOrigen", OleDbType.VarChar, 0, "TObsFilePathOrigen");
                dataAdapter.InsertCommand.Parameters.Add("@TObsFilePathConten", OleDbType.VarChar, 0, "TObsFilePathConten");
                dataAdapter.InsertCommand.Parameters.Add("@TextoRevision2", OleDbType.VarChar, 0, "TextoRevision2");
                dataAdapter.InsertCommand.Parameters.Add("@TR2Plano", OleDbType.VarChar, 0, "TR2Plano");
                dataAdapter.InsertCommand.Parameters.Add("@TAprobFilePathOrigen", OleDbType.VarChar, 0, "TAprobFilePathOrigen");
                dataAdapter.InsertCommand.Parameters.Add("@TAprobFilePathConten", OleDbType.VarChar, 0, "TAprobFilePathConten");

                dataAdapter.Update(dataSet, "TesisCompara");
                dataSet.Dispose();
                dataAdapter.Dispose();
            }
            catch (OleDbException ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                ErrorUtilities.SetNewErrorMessage(ex, methodName + " Exception,TesisCommonModel", "ControlTesis");
            }
            catch (Exception ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                ErrorUtilities.SetNewErrorMessage(ex, methodName + " Exception,TesisCommonModel", "ControlTesis");
            }
            finally
            {
                connection.Close();
            }
        }


        /// <summary>
        /// Obtiene el texto de la tesis en sus diferentes estados para poder mostrar el comparativo de los
        /// mismos
        /// </summary>
        /// <param name="idTesis">Identificador de la tesis</param>
        /// <returns></returns>
        public TesisCompara GetTesisCompara(int idTesis)
        {
            TesisCompara tesis = new TesisCompara();

            OleDbConnection connection = new OleDbConnection(connectionString);
            OleDbCommand cmd = null;
            OleDbDataReader reader = null;

            String sqlCadena = "SELECT * FROM TesisCompara WHERE IdTesis = @IdTesis";

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

                        tesis.IdTesis = idTesis;
                        tesis.TextoOriginal = reader["TextoOriginal"].ToString();
                        tesis.TOPlano = reader["TOPlano"].ToString();
                        tesis.TObservaciones = reader["TextoRevision1"].ToString();
                        tesis.TObservacionesPlano = reader["TR1Plano"].ToString();
                        tesis.TAprobada = reader["TextoRevision2"].ToString();
                        tesis.TAprobadaPlano = reader["TR2Plano"].ToString();
                        tesis.ToFilePathOrigen = reader["ToFilePathOrigen"].ToString();
                        tesis.TObsFilePathOrigen = reader["TobsFilePathOrigen"].ToString();
                        tesis.TAprobFilePathOrigen = reader["TAprobFilePathOrigen"].ToString();

                    }
                }
                cmd.Dispose();
                reader.Close();
            }
            catch (OleDbException ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                ErrorUtilities.SetNewErrorMessage(ex, methodName + " Exception,TesisCommonModel", "ControlTesis");
            }
            catch (Exception ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                ErrorUtilities.SetNewErrorMessage(ex, methodName + " Exception,TesisCommonModel", "ControlTesis");
            }
            finally
            {
                connection.Close();
            }

            return tesis;
        }

        /// <summary>
        /// Actualiza el texto de cada una de las etapas de las tesis cuando estas sufren modificaciones
        /// dentro de la ventana de Salas Compare o CcstCompare, estos cambios serán principalmente en el 
        /// formato
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
                dr["TextoRevision1"] = tesis.TObservaciones;
                dr["TR1Plano"] = tesis.TObservacionesPlano;
                dr["TextoRevision2"] = tesis.TAprobada;
                dr["TR2Plano"] = tesis.TAprobadaPlano;

                dr.EndEdit();

                dataAdapter.UpdateCommand = connection.CreateCommand();

                sSql = "UPDATE TesisCompara SET TextoOriginal = @TextoOriginal, TOPlano = @TOPlano, TextoRevision1 = @TextoRevision1, TR1Plano = @TR1Plano," +
                       "TextoRevision2 = @TextoRevision2, TR2Plano = @TR2Plano" +
                       " WHERE IdTesis = @IdTesis";
                dataAdapter.UpdateCommand.CommandText = sSql;

                dataAdapter.UpdateCommand.Parameters.Add("@TextoOriginal", OleDbType.VarChar, 0, "TextoOriginal");
                dataAdapter.UpdateCommand.Parameters.Add("@TOPlano", OleDbType.VarChar, 0, "TOPlano");
                dataAdapter.UpdateCommand.Parameters.Add("@TextoRevision1", OleDbType.VarChar, 0, "TextoRevision1");
                dataAdapter.UpdateCommand.Parameters.Add("@TR1Plano", OleDbType.VarChar, 0, "TR1Plano");
                dataAdapter.UpdateCommand.Parameters.Add("@TextoRevision2", OleDbType.VarChar, 0, "TextoRevision2");
                dataAdapter.UpdateCommand.Parameters.Add("@TR2Plano", OleDbType.VarChar, 0, "TR2Plano");
                dataAdapter.UpdateCommand.Parameters.Add("@IdTesis", OleDbType.Numeric, 0, "IdTesis");

                dataAdapter.Update(dataSet, "TesisCompara");
                dataSet.Dispose();
                dataAdapter.Dispose();
            }
            catch (OleDbException ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                ErrorUtilities.SetNewErrorMessage(ex, methodName + " Exception,TesisCommonModel", "ControlTesis");
            }
            catch (Exception ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                ErrorUtilities.SetNewErrorMessage(ex, methodName + " Exception,TesisCommonModel", "ControlTesis");
            }
            finally
            {
                connection.Close();
            }
        }

        /// <summary>
        /// Elimina la información de comparación de la tesis seleccionada
        /// </summary>
        /// <param name="idTesis"></param>
        public void DeleteTesisCompara(int idTesis)
        {
            OleDbConnection connection = new OleDbConnection(connectionString);
            OleDbCommand cmd = new OleDbCommand();

            try
            {
                cmd = connection.CreateCommand();
                cmd.CommandText = "DELETE FROM TesisCompara WHERE IdTesis = @IdTesis";
                cmd.Parameters.AddWithValue("@IdTesis", idTesis);

                connection.Open();
                cmd.ExecuteNonQuery();
                connection.Close();
            }
            catch (OleDbException ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                ErrorUtilities.SetNewErrorMessage(ex, methodName + " Exception,TesisCommonModel", "ControlTesis");
            }
            catch (Exception ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                ErrorUtilities.SetNewErrorMessage(ex, methodName + " Exception,TesisCommonModel", "ControlTesis");
            }
            finally
            {
                connection.Close();
            }
        }

        #endregion
        
        #region Precedentes

        /// <summary>
        /// Obtiene el precedente de la tesis que se esta solicitando
        /// </summary>
        /// <param name="idTesis">Identificador de la tesis</param>
        /// <returns></returns>
        public PrecedentesTesis GetPrecedenteTesis(int idTesis)
        {
            PrecedentesTesis precedente = new PrecedentesTesis();

            OleDbConnection connection = new OleDbConnection(connectionString);
            OleDbCommand cmd = null;
            OleDbDataReader reader = null;

            String sqlCadena = "SELECT * FROM PrecedentesTesis WHERE IdTesis = @IdTesis";

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
                        precedente.IdPrecedente = Convert.ToInt32(reader["IdPrecedente"]);
                        precedente.IdTesis = idTesis;
                        precedente.TipoAsunto = Convert.ToInt32(reader["IdTipoAsunto"]);
                        precedente.NumAsunto = Convert.ToInt32(reader["NumAsunto"]);
                        precedente.YearAsunto = Convert.ToInt32(reader["YearAsunto"]);
                        precedente.FResolucion = DateTimeUtilities.GetDateFromReader(reader, "FResolucion");
                        precedente.IdPonente = Convert.ToInt32(reader["IdPonente"]);
                        precedente.Promovente = reader["Promovente"].ToString();
                    }
                }
                cmd.Dispose();
                reader.Close();
            }
            catch (OleDbException ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                ErrorUtilities.SetNewErrorMessage(ex, methodName + " Exception,TesisCommonModel", "ControlTesis");
            }
            catch (Exception ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                ErrorUtilities.SetNewErrorMessage(ex, methodName + " Exception,TesisCommonModel", "ControlTesis");
            }
            finally
            {
                connection.Close();
            }

            return precedente;
        }

        public void SetPrecedentes(PrecedentesTesis precedente, int idTesis)
        {
            OleDbConnection connection = new OleDbConnection(connectionString);

            string sSql;
            OleDbDataAdapter dataAdapter;

            DataSet dataSet = new DataSet();
            DataRow dr;

            try
            {
                string sqlCadena = "SELECT * FROM PrecedentesTesis WHERE IdPrecedente = 0";
                precedente.IdPrecedente = DataBaseUtilities.GetNextIdForUse("PrecedentesTesis", "IdPrecedente", connection);

                dataAdapter = new OleDbDataAdapter();
                dataAdapter.SelectCommand = new OleDbCommand(sqlCadena, connection);

                dataAdapter.Fill(dataSet, "PrecedentesTesis");

                dr = dataSet.Tables["PrecedentesTesis"].NewRow();
                dr["IdPrecedente"] = precedente.IdPrecedente;
                dr["IdTesis"] = idTesis;
                dr["IdTipoAsunto"] = precedente.TipoAsunto;
                dr["NumAsunto"] = precedente.NumAsunto;
                dr["YearAsunto"] = precedente.YearAsunto;

                if (precedente.FResolucion != null)
                {
                    dr["FResolucion"] = precedente.FResolucion;
                    dr["FResolucionInt"] = DateTimeUtilities.DateToInt(precedente.FResolucion);
                }
                else
                {
                    dr["FResolucion"] = DBNull.Value;
                    dr["FResolucionInt"] = 0;
                }

                dr["IdPonente"] = precedente.IdPonente;
                dr["Promovente"] = precedente.Promovente;

                dataSet.Tables["PrecedentesTesis"].Rows.Add(dr);

                dataAdapter.InsertCommand = connection.CreateCommand();

                sSql = "INSERT INTO PrecedentesTesis (IdPrecedente,IdTesis,IdTipoAsunto,NumAsunto,YearAsunto,FResolucion,FResolucionInt,IdPonente,Promovente) " +
                       " VALUES (@IdPrecedente,@IdTesis,@IdTipoAsunto,@NumAsunto,@YearAsunto,@FResolucion,@FResolucionInt,@IdPonente,@Promovente)";

                dataAdapter.InsertCommand.CommandText = sSql;

                dataAdapter.InsertCommand.Parameters.Add("@IdPrecedente", OleDbType.Numeric, 0, "IdPrecedente");
                dataAdapter.InsertCommand.Parameters.Add("@IdTesis", OleDbType.Numeric, 0, "IdTesis");
                dataAdapter.InsertCommand.Parameters.Add("@IdTipoAsunto", OleDbType.Numeric, 0, "IdTipoAsunto");
                dataAdapter.InsertCommand.Parameters.Add("@NumAsunto", OleDbType.Numeric, 0, "NumAsunto");
                dataAdapter.InsertCommand.Parameters.Add("@YearAsunto", OleDbType.Numeric, 0, "YearAsunto");
                dataAdapter.InsertCommand.Parameters.Add("@FResolucion", OleDbType.Date, 0, "FResolucion");
                dataAdapter.InsertCommand.Parameters.Add("@FResolucionInt", OleDbType.Numeric, 0, "FResolucionInt");
                dataAdapter.InsertCommand.Parameters.Add("@IdPonente", OleDbType.Numeric, 0, "IdPonente");
                dataAdapter.InsertCommand.Parameters.Add("@Promovente", OleDbType.VarChar, 0, "Promovente");

                dataAdapter.Update(dataSet, "PrecedentesTesis");
                dataSet.Dispose();
                dataAdapter.Dispose();
            }
            catch (OleDbException ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                ErrorUtilities.SetNewErrorMessage(ex, methodName + " Exception,TesisCommonModel", "ControlTesis");
            }
            catch (Exception ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                ErrorUtilities.SetNewErrorMessage(ex, methodName + " Exception,TesisCommonModel", "ControlTesis");
            }
            finally
            {
                connection.Close();
            }
        }

        public void UpdatePrecedentes(PrecedentesTesis precedente, int idTesis)
        {
            OleDbConnection connection = new OleDbConnection(connectionString);

            string sSql;
            OleDbDataAdapter dataAdapter;

            DataSet dataSet = new DataSet();
            DataRow dr;

            try
            {
                string sqlCadena = "SELECT * FROM PrecedentesTesis WHERE IdPrecedente = " + precedente.IdPrecedente;

                dataAdapter = new OleDbDataAdapter();
                dataAdapter.SelectCommand = new OleDbCommand(sqlCadena, connection);

                dataAdapter.Fill(dataSet, "PrecedentesTesis");

                dr = dataSet.Tables["PrecedentesTesis"].Rows[0];
                dr.BeginEdit();
                dr["IdPrecedente"] = precedente.IdPrecedente;
                dr["IdTesis"] = idTesis;
                dr["IdTipoAsunto"] = precedente.TipoAsunto;
                dr["NumAsunto"] = precedente.NumAsunto;
                dr["YearAsunto"] = precedente.YearAsunto;

                if (precedente.FResolucion != null)
                {
                    dr["FResolucion"] = precedente.FResolucion;
                    dr["FResolucionInt"] = DateTimeUtilities.DateToInt(precedente.FResolucion);
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

                sSql = "Update PrecedentesTesis SET IdTipoAsunto = @IdTipoAsunto, NumAsunto = @NumAsunto, YearAsunto = @YearAsunto, " +
                            "FResolucion = @FResolucion,FResolucionInt = @FResolucionInt,IdPonente = @IdPonente,Promovente = @Promovente " +
                            "WHERE IdPrecedente = @IdPrecedente ";

                dataAdapter.UpdateCommand.CommandText = sSql;

                dataAdapter.UpdateCommand.Parameters.Add("@IdTipoAsunto", OleDbType.Numeric, 0, "IdTipoAsunto");
                dataAdapter.UpdateCommand.Parameters.Add("@NumAsunto", OleDbType.Numeric, 0, "NumAsunto");
                dataAdapter.UpdateCommand.Parameters.Add("@YearAsunto", OleDbType.Numeric, 0, "YearAsunto");
                dataAdapter.UpdateCommand.Parameters.Add("@FResolucion", OleDbType.Date, 0, "FResolucion");
                dataAdapter.UpdateCommand.Parameters.Add("@FResolucionInt", OleDbType.Numeric, 0, "FResolucionInt");
                dataAdapter.UpdateCommand.Parameters.Add("@IdPonente", OleDbType.Numeric, 0, "IdPonente");
                dataAdapter.UpdateCommand.Parameters.Add("@Promovente", OleDbType.VarChar, 0, "Promovente");
                dataAdapter.UpdateCommand.Parameters.Add("@IdPrecedente", OleDbType.Numeric, 0, "IdPrecedente");

                dataAdapter.Update(dataSet, "PrecedentesTesis");
                dataSet.Dispose();
                dataAdapter.Dispose();
            }
            catch (OleDbException ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                ErrorUtilities.SetNewErrorMessage(ex, methodName + " Exception,TesisCommonModel", "ControlTesis");
            }
            catch (Exception ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                ErrorUtilities.SetNewErrorMessage(ex, methodName + " Exception,TesisCommonModel", "ControlTesis");
            }
            finally
            {
                connection.Close();
            }
        }

        /// <summary>
        /// Elimina los precedentes asociados a la tesis que se esta eliminando
        /// </summary>
        /// <param name="idTesis">Identificador de la tesis que se esta eliminando</param>
        public void DeletePrecedentes(int idTesis)
        {
            OleDbConnection connection = new OleDbConnection(connectionString);
            OleDbCommand cmd = new OleDbCommand();

            try
            {
                cmd = connection.CreateCommand();
                cmd.CommandText = "DELETE FROM PrecedentesTesis WHERE IdTesis = @IdTesis";
                cmd.Parameters.AddWithValue("@IdTesis", idTesis);

                connection.Open();
                cmd.ExecuteNonQuery();
                connection.Close();
            }
            catch (OleDbException ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                ErrorUtilities.SetNewErrorMessage(ex, methodName + " Exception,TesisCommonModel", "ControlTesis");
            }
            catch (Exception ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                ErrorUtilities.SetNewErrorMessage(ex, methodName + " Exception,TesisCommonModel", "ControlTesis");
            }
            finally
            {
                connection.Close();
            }
        }

        #endregion


    }
}
