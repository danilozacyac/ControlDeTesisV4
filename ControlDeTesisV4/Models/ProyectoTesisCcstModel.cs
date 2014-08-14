using System;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Windows.Forms;
using ControlDeTesisV4.Dao;
using ModuloInterconexionCommonApi;

namespace ControlDeTesisV4.Models
{
    public class ProyectoTesisCcstModel : IProyecto
    {
        ProyectosCcst tesisCcst;
        readonly string connectionString = ConfigurationManager.ConnectionStrings["Modulo"].ToString();

        public ProyectoTesisCcstModel() { }

        public ProyectoTesisCcstModel(ProyectosCcst tesisCcst)
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
                    dr["FechaOficioAtnInt"] = StringUtilities.DateToInt(tesisCcst.FOficioAtn); 
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
                dataAdapter.InsertCommand.Parameters.Add("@Destinatario", OleDbType.VarChar, 0, "Destinatario");
                dataAdapter.InsertCommand.Parameters.Add("@OficioAtn", OleDbType.VarChar, 0, "OficioAtn");
                dataAdapter.InsertCommand.Parameters.Add("@FechaOficioAtn", OleDbType.Date, 0, "FechaOficioAtn");
                dataAdapter.InsertCommand.Parameters.Add("@FechaOficioAtnInt", OleDbType.Numeric, 0, "FechaOficioAtnInt");
                dataAdapter.InsertCommand.Parameters.Add("@FileOficioAtnOrigen", OleDbType.VarChar, 0, "FileOficioAtnOrigen");
                dataAdapter.InsertCommand.Parameters.Add("@FileOficioAtnConten", OleDbType.VarChar, 0, "FileOficioAtnConten");

                dataAdapter.Update(dataSet, "ProyectosCcst");
                dataSet.Dispose();
                dataAdapter.Dispose();

                this.SetNewProyectoTesis(tesisCcst.Proyectos);
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

        #region Tesis

        public void SetNewProyectoTesis(ObservableCollection<ProyectosTesis> listaProyectos)
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
                    dr["IdProyecto"] = tesisCcst.IdProyecto;
                    dr["IdTipoProyecto"] = tesisCcst.IdTipoProyecto;
                    dr["OficioEnvio"] = tesis.OficioEnvio;
                    

                    if (tesis.FEnvio != null)
                    {
                        dr["FechaEnvioOficio"] = tesis.FEnvio;
                        dr["FechaEnvioOficioInt"] = StringUtilities.DateToInt(tesis.FEnvio);
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
                        dr["FAprobacionInt"] = StringUtilities.DateToInt(tesis.FAprobacion);
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

                    new PrecedentesModel().SetPrecedentes(tesis.Precedente, tesis.IdTesis);
                    this.SetTesisCompara(tesis.ComparaTesis, tesis.IdTesis);
                }
                
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

        public void SetTesisCompara(TesisCompara tesisCompara,int idTesis)
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

                //Los siguientes campos se omiten porque en los proyectos que formula la CCST solo existe el texto original y la tesis aprobada
                    //dr["TextoRevision1"] = tesisCompara.TObservaciones;
                    //dr["TR1Plano"] = tesisCompara.TObservacionesPlano;
                    //dr["TObsFilePathOrigen"] = tesisCompara.TObsFilePathOrigen;
                    //dr["TObsFilePathConten"] = tesisCompara.TObsFilePathConten;
                
                dr["TextoRevision2"] = tesisCompara.TAprobada;
                    dr["TR2Plano"] = tesisCompara.TAprobadaPlano;
                    dr["TAprobFilePathOrigen"] = tesisCompara.TAprobFilePathOrigen;
                    dr["TAprobFilePathConten"] = tesisCompara.TAprobFilePathConten;

                    dataSet.Tables["TesisCompara"].Rows.Add(dr);

                    dataAdapter.InsertCommand = connection.CreateCommand();

                    sSql = "INSERT INTO TesisCompara (IdTesis,TextoOriginal,TOPlano,TOrigenAlfab,ToFilePathOrigen,ToFilePathConten," +
                        "TextoRevision2,TR2Plano,TAprobFilePathOrigen,TAprobFilePathConten) " +
                           " VALUES (@IdTesis,@TextoOriginal,@TOPlano,@TOrigenAlfab,@ToFilePathOrigen,@ToFilePathConten," +
                        "@TAprobada,@TAprobadaPlano,@TAprobFilePathOrigen,@TAprobFilePathConten)";

                    dataAdapter.InsertCommand.CommandText = sSql;

                    dataAdapter.InsertCommand.Parameters.Add("@IdTesis", OleDbType.Numeric, 0, "IdTesis");
                    dataAdapter.InsertCommand.Parameters.Add("@TextoOriginal", OleDbType.VarChar, 0, "TextoOriginal");
                    dataAdapter.InsertCommand.Parameters.Add("@TOPlano", OleDbType.VarChar, 0, "TOPlano");
                    dataAdapter.InsertCommand.Parameters.Add("@TOrigenAlfab", OleDbType.VarChar, 0, "TOrigenAlfab");
                    dataAdapter.InsertCommand.Parameters.Add("@ToFilePathOrigen", OleDbType.VarChar, 0, "ToFilePathOrigen");
                    dataAdapter.InsertCommand.Parameters.Add("@ToFilePathConten", OleDbType.VarChar, 0, "ToFilePathConten");
                    //dataAdapter.InsertCommand.Parameters.Add("@TextoRevision1", OleDbType.VarChar, 0, "TextoRevision1");
                    //dataAdapter.InsertCommand.Parameters.Add("@TR1Plano", OleDbType.VarChar, 0, "TR1Plano");
                    //dataAdapter.InsertCommand.Parameters.Add("@TObsFilePathOrigen", OleDbType.VarChar, 0, "TObsFilePathOrigen");
                    //dataAdapter.InsertCommand.Parameters.Add("@TObsFilePathConten", OleDbType.VarChar, 0, "TObsFilePathConten");
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
                    dr["FechaEnvioOficioInt"] = StringUtilities.DateToInt(tesis.FEnvio);
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
                    dr["FAprobacionInt"] = StringUtilities.DateToInt(tesis.FAprobacion);
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

        

        #endregion


        
    }
}