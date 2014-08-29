using System;
using System.Linq;
using System.Windows;
using ControlDeTesisV4.Models;

namespace ControlDeTesisV4
{
    /// <summary>
    /// Interaction logic for LogIn.xaml
    /// </summary>
    public partial class LogIn
    {
        public LogIn()
        {
            InitializeComponent();
        }

        private void BtnSalir_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BtnIngresar_Click(object sender, RoutedEventArgs e)
        {
            if (TxtUsuario.Text.Length == 0)
            {
                MessageBox.Show("  Falta capturar Usuario");
                return;
            }

            if (Passtext.Password.Length == 0)
            {
                MessageBox.Show("  Falta capturar Contraseña");
                return;
            }

            AccesoUsuariosModel accesoModel = new AccesoUsuariosModel();

            if (accesoModel.ObtenerUsuarioContraseña(TxtUsuario.Text.ToUpper(), Passtext.Password.ToUpper().ToString()) == false)
            {
                MessageBox.Show("  No existe usuario y/o contraseña  ");
            }
            else

                this.Close();
        }
    }
}