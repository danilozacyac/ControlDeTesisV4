using System;
using System.Configuration;
using System.Data.Common;
using System.Data.OleDb;
using System.Linq;
using System.Windows.Forms;
using ControlDeTesisV4.Dao;
using ModuloInterconexionCommonApi;

namespace ControlDeTesisV4.Models
{
    public class AccesoUsuariosModel
    {
        private static readonly string connectionString = ConfigurationManager.ConnectionStrings["Modulo"].ToString();

        public bool ObtenerUsuarioContraseña(string sUsuario, string sPwd)
        {
            bool bExisteUsuario = false;
            string sSql;

            OleDbConnection connection = new OleDbConnection(connectionString);

            OleDbCommand cmd;
            OleDbDataReader reader;
            

            try
            {
                connection.Open();

                sSql = "SELECT * FROM Abogados WHERE usuario = @usuario AND Pass = @Pass";
                cmd = new OleDbCommand(sSql, connection);
                cmd.Parameters.AddWithValue("@usuario", sUsuario);
                cmd.Parameters.AddWithValue("@Pass", sPwd);
                reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    AccesoUsuarios.Usuario = reader["usuario"].ToString();
                    AccesoUsuarios.Llave = Convert.ToInt16(reader["IdAbogado"].ToString());
                    AccesoUsuarios.Perfil = Convert.ToInt16(reader["Perfil"].ToString());
                    AccesoUsuarios.Nombre = reader["nombre"].ToString();
                    this.GetPerfilAuths(AccesoUsuarios.Perfil);
                    bExisteUsuario = true;
                }
                else
                {
                    AccesoUsuarios.Llave = -1;
                }
            }
            catch (DbException ex)
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

            return bExisteUsuario;
        }


        public void GetPerfilAuths(int perfil)
        {
            string sSql;

            OleDbConnection connection = new OleDbConnection(connectionString);

            OleDbCommand cmd;
            OleDbDataReader reader;

            if(AccesoUsuarios.Secciones == null)
                AccesoUsuarios.Secciones = new System.Collections.Generic.List<int>();

            try
            {
                connection.Open();

                sSql = "SELECT * FROM PerfilSeccion WHERE perfil = @perfil ";
                cmd = new OleDbCommand(sSql, connection);
                cmd.Parameters.AddWithValue("@perfil", perfil);
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    AccesoUsuarios.Secciones.Add(reader["Seccion"] as int? ?? -1);
                }
               
            }
            catch (DbException ex)
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
    }
}
