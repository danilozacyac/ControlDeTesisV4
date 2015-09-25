using System;
using System.Linq;
using System.Windows;
using ControlDeTesisV4.Dao;
using ControlDeTesisV4.Models;
using ControlDeTesisV4.UtilitiesFolder;

namespace ControlDeTesisV4
{
    /// <summary>
    /// Interaction logic for AddNombre.xaml
    /// </summary>
    public partial class AddNombre
    {
        string funcionario;
        
        /// <summary>
        /// Indica si el nombre de la persona que se esta agregando pertenece a un 1. Ponente, 2. Signatario, 3. Abogado responsable
        /// </summary>
        int tipoFuncionario;

        /// <summary>
        /// Indica si el funcionario pertenece a la Suprema Corte o alguno de los Plenos de Circuito
        /// </summary>
        int tipoOrganismo;

        /// <summary>
        /// Almacena el nombre del funcionario separandolo para su catalogación
        /// </summary>
        string[] splitName;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="funcionario"></param>
        /// <param name="tipoFuncionario">1. Ponente, 2. Signatario, 3. Abogado responsable</param>
        /// <param name="tipoOrganismo">1. SCJN   2. Plenos</param>
        public AddNombre(string funcionario, int tipoFuncionario, int tipoOrganismo)
        {
            InitializeComponent();
            this.funcionario = funcionario;
            this.tipoFuncionario = tipoFuncionario;
            this.tipoOrganismo = tipoOrganismo;
        }


        private void RadWindow_Loaded(object sender, RoutedEventArgs e)
        {
            splitName = funcionario.Split(default(string[]), StringSplitOptions.RemoveEmptyEntries);

            if (splitName.Count() == 3)
            {
                TxtNombre.Text = splitName[0];
                TxtPaterno.Text = splitName[1];
                TxtMaterno.Text = splitName[2];
            }
            else 
            {
                TxtNombre.Text = splitName[0];
                TxtPaterno.Text = splitName[1];

                int index = 2;
                string tempString = String.Empty;
                while (index < splitName.Count())
                {
                    tempString += splitName[index] + " ";
                    index++;
                }
                TxtMaterno.Text = tempString;
            }
        }

        private void BtnCancelar_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            this.Close();
        }

        private void BtnAceptar_Click(object sender, RoutedEventArgs e)
        {
            Funcionarios nuevoFuncionario = new Funcionarios();
            nuevoFuncionario.Nombre = TxtNombre.Text;
            nuevoFuncionario.Paterno = TxtPaterno.Text;
            nuevoFuncionario.Materno = TxtMaterno.Text;
            nuevoFuncionario.NombreCompleto = nuevoFuncionario.Nombre + " " + nuevoFuncionario.Paterno + " " + nuevoFuncionario.Materno;
            nuevoFuncionario.IdTipoFuncionario = tipoOrganismo;
            nuevoFuncionario.Estado = 1;

            if (tipoFuncionario == 1)
                new FuncionariosModel().SetNewPonente(nuevoFuncionario, tipoOrganismo);
            else if (tipoFuncionario == 2)
                new FuncionariosModel().SetNewSignatario(nuevoFuncionario, tipoOrganismo);

            Constants.NuevoFuncionario = nuevoFuncionario;

            DialogResult = true;
            this.Close();
        }
    }
}
