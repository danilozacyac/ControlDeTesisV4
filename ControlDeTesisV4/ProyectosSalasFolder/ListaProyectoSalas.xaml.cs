using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using ControlDeTesisV4.Dao;
using ControlDeTesisV4.Models;
using ControlDeTesisV4.UtilitiesFolder;
using ControlDeTesisV4.VisualComparition;
using Telerik.Windows.Controls.GridView;

namespace ControlDeTesisV4.ProyectosSalasFolder
{
    /// <summary>
    /// Lógica de interacción para ListaProyectoSalas.xaml
    /// </summary>
    public partial class ListaProyectoSalas : UserControl
    {
        

        public ListaProyectoSalas()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            Constants.ProyectosSalas = new ProyectoPreviewModel().GetPreviewSalasSinTurnar(1);

            GListado.DataContext = Constants.ProyectosSalas;
        }

        private void ComparaButton_Click(object sender, RoutedEventArgs e)
        {
            Button but = sender as Button;
            idTesis = Convert.ToInt32(but.Uid);

            if (lastProyecto == null || idTesis != lastProyecto.IdTesis)
            {
                lastProyecto = new ProyectoTesisSalasModel().GetProyectoTesis(idTesis);
            }

            SalasCompare salas = new SalasCompare(lastProyecto);
            salas.ShowDialog();
        }

        private int idTesis = 0;
        private ProyectosTesis lastProyecto;
        private void BtnInfoRec_Click(object sender, RoutedEventArgs e)
        {
            Button but = sender as Button;
            idTesis = Convert.ToInt32(but.Uid);

            

            if (lastProyecto == null || idTesis != lastProyecto.IdTesis)
            {
                lastProyecto = new ProyectoTesisSalasModel().GetProyectoTesis(idTesis);
            }

            DatosProyectoSalas obs = new DatosProyectoSalas(lastProyecto);
            obs.ShowDialog();
        }

        private void BtnInfoObs_Click(object sender, RoutedEventArgs e)
        {
            Button but = sender as Button;
            idTesis = Convert.ToInt32(but.Uid);

            if (lastProyecto == null || idTesis != lastProyecto.IdTesis)
            {
                lastProyecto = new ProyectoTesisSalasModel().GetProyectoTesis(idTesis);
            }

            CapturObservaciones obs = new CapturObservaciones(lastProyecto);
            obs.ShowDialog();
        }

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

        private void SearchTextBox_Search(object sender, RoutedEventArgs e)
        {
            String tempString = ((TextBox)sender).Text.ToUpper();

            if (!String.IsNullOrEmpty(tempString))
                GListado.DataContext = (from n in Constants.ProyectosSalas
                                        where n.Asunto.ToUpper().Contains(tempString) || n.Rubro.ToUpper().Contains(tempString)
                                        select n).ToList();
            else
                GListado.DataContext = Constants.ProyectosSalas;
        }


        public ProyectoPreview SelectedTesis;
        private void GListado_SelectionChanged(object sender, Telerik.Windows.Controls.SelectionChangeEventArgs e)
        {
            SelectedTesis = GListado.SelectedItem as ProyectoPreview;
        }
    }
}
