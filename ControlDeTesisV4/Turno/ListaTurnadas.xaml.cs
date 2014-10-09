using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using ControlDeTesisV4.Dao;
using ControlDeTesisV4.Models;
using ControlDeTesisV4.Singletons;
using ControlDeTesisV4.UtilitiesFolder;
using ControlDeTesisV4.UtilitiesWin;

namespace ControlDeTesisV4.Turno
{
    /// <summary>
    /// Lógica de interacción para ListaTurnadas.xaml
    /// </summary>
    public partial class ListaTurnadas : UserControl
    {
        public static TesisTurnadaPreview TesisTurnada;
        private int estadoTesis;

        public ListaTurnadas()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            GListadoTurno.DataContext = Constants.ListadoDeTesis;
            RCbxAbogados.DataContext = FuncionariosSingleton.AbogResp;

            RCbxFiltro.Items.Add("Tesis Asignadas");
            RCbxFiltro.Items.Add("Tesis Atrazadas");
            RCbxFiltro.Items.Add("Mostrar todo");
            if(AccesoUsuarios.Perfil == 0 || AccesoUsuarios.Perfil == 3)
                RCbxFiltro.Items.Add("Abogados");
            

        }

        private void GListadoTurno_SelectionChanged(object sender, Telerik.Windows.Controls.SelectionChangeEventArgs e)
        {
            TesisTurnada = GListadoTurno.SelectedItem as TesisTurnadaPreview;

            Constants.TesisTurno = new ProyectoTesisSalasModel().GetProyectoTesis(TesisTurnada.IdTesis);
        }

        private void RCbxFiltro_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RCbxAbogados.Visibility = Visibility.Collapsed;
            switch (RCbxFiltro.SelectedIndex)
            {
                case 0:
                    if(AccesoUsuarios.Llave != 0)
                    GListadoTurno.DataContext = (from n in Constants.ListadoDeTesis
                                                 where n.Idabogado == AccesoUsuarios.Llave
                                                 select n);
                    break;
                case 1:
                    break;
                case 2: GListadoTurno.DataContext = Constants.ListadoDeTesis;
                    break;
                case 3: RCbxAbogados.Visibility = Visibility.Visible;
                    break;
            }
        }

        private void RCbxAbogados_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Funcionarios func = RCbxAbogados.SelectedItem as Funcionarios;

            GListadoTurno.DataContext = (from n in Constants.ListadoDeTesis
                                         where n.Idabogado == func.IdFuncionario
                                         select n);
        }

        private void IHelp_PreviewMouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            GuiaColores guia = new GuiaColores();
            guia.ShowDialog();
        }
    }
}