﻿using System;
using System.Configuration;
using System.Data.OleDb;
using System.Linq;
using ControlDeTesisV4.Dao;
using ControlDeTesisV4.UtilitiesFolder;
using ScjnUtilities;

namespace ControlDeTesisV4.Models
{
    public class TesisTurnadasModel
    {
        readonly string connectionString = ConfigurationManager.ConnectionStrings["Modulo"].ToString();

        /// <summary>
        /// Devuelve una colección de tesis, la colección puede ser de las tesis que estan en espera de ser turnadas
        /// o de aquellas que ya fueron turnadas
        /// </summary>
        /// <param name="estadoTesis">Estado del proceso de publicación en el que se encuentra la tesis</param>
        /// <returns></returns>
        public void GetPreviewTesisTurnadas()
        {

            OleDbConnection connection = new OleDbConnection(connectionString);
            OleDbCommand cmd = null;
            OleDbDataReader reader = null;

            String sqlCadena = "SELECT T.IdDocto, T.IdAbogado, PT.ClaveTesis, PT.Rubro, PrT.IdTipoAsunto, PrT.NumAsunto, PrT.YearAsunto, PT.IdInstancia, PrT.FResolucion, T.FTurno, T.FSugerida, T.FEntrega, PT.EstadoTesis,T.EnTiempo,T.DiasAtraso " +
                               " FROM (PrecedentesTesis PrT INNER JOIN ProyectosTesis PT ON PrT.IdTesis = PT.IdTesis) INNER JOIN Turno T ON PT.IdTesis = T.IdDocto " +
                               " WHERE (PT.EstadoTesis >= 4 ); ";

            try
            {
                connection.Open();

                cmd = new OleDbCommand(sqlCadena, connection);
                reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        TesisTurnadaPreview tesis = new TesisTurnadaPreview();
                        tesis.IdTesis = reader["IdDocto"] as int? ?? -1;
                        tesis.Idabogado = reader["IdAbogado"] as int? ?? -1;
                        tesis.ClaveTesis = reader["ClaveTesis"].ToString();
                        tesis.Rubro = reader["Rubro"].ToString();
                        tesis.IdTipoAsunto = reader["IdTipoAsunto"] as int? ?? -1;
                        tesis.NumAsunto = reader["NumAsunto"] as int? ?? -1;
                        tesis.YearAsunto = reader["YearAsunto"] as int? ?? -1;
                        tesis.IdInstancia = reader["IdInstancia"] as int? ?? -1;
                        //tesis.FAprobacion = StringUtilities.GetDateFromReader(reader, "FAprobacion");
                        tesis.FTurno = DateTimeUtilities.GetDateFromReader(reader, "FTurno");
                        tesis.FSugerida = DateTimeUtilities.GetDateFromReader(reader, "FSugerida");
                        tesis.FEntrega = DateTimeUtilities.GetDateFromReader(reader, "FEntrega");
                        tesis.EnTiempo = Convert.ToBoolean(reader["EnTiempo"] as int? ?? -1);
                        tesis.DiasAtraso = reader["DiasAtraso"] as int? ?? -1;
                        tesis.EstadoTesis = reader["EstadoTesis"] as int? ?? -1;

                        TimeSpan? ts = tesis.FSugerida - DateTime.Now;
                        int diferenciaEnDias = ts.Value.Days;

                        tesis.Semaforo = diferenciaEnDias;
                        
                        Constants.ListadoDeTesis.Add(tesis);
                    }
                }
                cmd.Dispose();
                reader.Close();
            }
            catch (OleDbException ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                ErrorUtilities.SetNewErrorMessage(ex, methodName + " Exception,TesisTurnadasModel", "ControlTesis");
            }
            catch (Exception ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                ErrorUtilities.SetNewErrorMessage(ex, methodName + " Exception,TesisTurnadasModel", "ControlTesis");
            }
            finally
            {
                connection.Close();
            }

        }


        public TesisTurnadaPreview GetPreviewTesisTurnada(int idDocumento)
        {
            TesisTurnadaPreview tesis = null;

            OleDbConnection connection = new OleDbConnection(connectionString);
            OleDbCommand cmd = null;
            OleDbDataReader reader = null;

            String sqlCadena = "SELECT T.IdDocto, T.IdAbogado, PT.ClaveTesis, PT.Rubro, PrT.IdTipoAsunto, PrT.NumAsunto, PrT.YearAsunto, PT.IdInstancia, PrT.FResolucion, T.FTurno, T.FSugerida, T.FEntrega, PT.EstadoTesis,T.EnTiempo,T.DiasAtraso " +
                               " FROM (PrecedentesTesis PrT INNER JOIN ProyectosTesis PT ON PrT.IdTesis = PT.IdTesis) INNER JOIN Turno T ON PT.IdTesis = T.IdDocto " +
                               " WHERE (((T.IdDocto) = @IdDocumento)); ";

            try
            {
                connection.Open();

                cmd = new OleDbCommand(sqlCadena, connection);
                cmd.Parameters.AddWithValue("@IdDocumento", idDocumento);
                reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        tesis = new TesisTurnadaPreview();
                        tesis.IdTesis = reader["IdDocto"] as int? ?? -1;
                        tesis.Idabogado = reader["IdAbogado"] as int? ?? -1;
                        tesis.ClaveTesis = reader["ClaveTesis"].ToString();
                        tesis.Rubro = reader["Rubro"].ToString();
                        tesis.IdTipoAsunto = reader["IdTipoAsunto"] as int? ?? -1;
                        tesis.NumAsunto = reader["NumAsunto"] as int? ?? -1;
                        tesis.YearAsunto = reader["YearAsunto"] as int? ?? -1;
                        tesis.IdInstancia = reader["IdInstancia"] as int? ?? -1;
                        //tesis.FAprobacion = StringUtilities.GetDateFromReader(reader, "FAprobacion");
                        tesis.FTurno = DateTimeUtilities.GetDateFromReader(reader, "FTurno");
                        tesis.FSugerida = DateTimeUtilities.GetDateFromReader(reader, "FSugerida");
                        tesis.FEntrega = DateTimeUtilities.GetDateFromReader(reader, "FEntrega");
                        tesis.EnTiempo = Convert.ToBoolean(reader["EnTiempo"] as int? ?? -1);
                        tesis.DiasAtraso = reader["DiasAtraso"] as int? ?? -1;

                        TimeSpan? ts = tesis.FSugerida - DateTime.Now;
                        int diferenciaEnDias = ts.Value.Days;

                        tesis.Semaforo = diferenciaEnDias;

                    }
                }
                cmd.Dispose();
                reader.Close();
            }
            catch (OleDbException ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                ErrorUtilities.SetNewErrorMessage(ex, methodName + " Exception,TesisTurnadasModel", "ControlTesis");
            }
            catch (Exception ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                ErrorUtilities.SetNewErrorMessage(ex, methodName + " Exception,TesisTurnadasModel", "ControlTesis");
            }
            finally
            {
                connection.Close();
            }

            return tesis;
        }
    }
}