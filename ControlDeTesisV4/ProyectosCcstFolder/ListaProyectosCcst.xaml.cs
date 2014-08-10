using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using ControlDeTesisV4.Dao;
using ControlDeTesisV4.Models;
using ControlDeTesisV4.ProyectosSalasFolder;
using ControlDeTesisV4.VisualComparition;

namespace ControlDeTesisV4.ProyectosCcstFolder
{
    /// <summary>
    /// Lógica de interacción para ListaProyectosCcst.xaml
    /// </summary>
    public partial class ListaProyectosCcst : UserControl
    {
        public ListaProyectosCcst()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            GListado.DataContext = new ProyectoPreviewModel().GetPreviewSalasSinTurnar(2);
        }

        private void ComparaButton_Click(object sender, RoutedEventArgs e)
        {
            Button but = sender as Button;
            idTesis = Convert.ToInt32(but.Uid);

            if (lastProyecto == null || idTesis != lastProyecto.IdTesis)
            {
                lastProyecto = new ProyectoTesisSalasModel().GetProyectoTesis(idTesis);
            }

            CcstCompare ccst = new CcstCompare(lastProyecto);
            ccst.ShowDialog();
        }

        private void BtnInfoRec_Click(object sender, RoutedEventArgs e)
        {
            Button but = sender as Button;
            idTesis = Convert.ToInt32(but.Uid);

            if (lastProyecto == null || idTesis != lastProyecto.IdTesis)
            {
                lastProyecto = new ProyectoTesisSalasModel().GetProyectoTesis(idTesis);
            }

            DatosProyectoCcst ccst = new DatosProyectoCcst(lastProyecto);
            ccst.ShowDialog();
        }

        private int idTesis = 0;
        private ProyectosTesis lastProyecto;
        private void BtnInfoApr_Click(object sender, RoutedEventArgs e)
        {
            Button but = sender as Button;
            idTesis = Convert.ToInt32(but.Uid);

            if (lastProyecto == null || idTesis != lastProyecto.IdTesis)
            {
                lastProyecto = new ProyectoTesisSalasModel().GetProyectoTesis(idTesis);
            }
            CapturaAprobacion aprob = new CapturaAprobacion(lastProyecto);
            aprob.ShowDialog();
        }
    }
}
