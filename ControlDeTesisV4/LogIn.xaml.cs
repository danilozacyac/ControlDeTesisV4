using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ControlDeTesisV4.Models;
using Telerik.Windows.Controls;

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