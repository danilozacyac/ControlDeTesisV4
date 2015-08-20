using System;
using System.Collections.ObjectModel;
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
        private ObservableCollection<ProyectoPreview> listaProyectos;

        public ListaProyectosCcst()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            listaProyectos = new ProyectoPreviewModel().GetPreviewSalasSinTurnar(2);
            GListado.DataContext = listaProyectos;
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
            CapturaAprobacionCcst aprob = new CapturaAprobacionCcst(lastProyecto);
            aprob.ShowDialog();
        }

        private void SearchTextBox_Search(object sender, RoutedEventArgs e)
        {
            String tempString = ((TextBox)sender).Text.ToUpper();

            if (!String.IsNullOrEmpty(tempString))
                GListado.DataContext = (from n in listaProyectos
                                          where n.Asunto.Contains(tempString) || n.Rubro.Contains(tempString)
                                          select n).ToList();
            else
                GListado.DataContext = listaProyectos;
        }
    }
}
