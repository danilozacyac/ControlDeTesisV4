using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using ControlDeTesisV4.Dao;
using ControlDeTesisV4.Models;
using ControlDeTesisV4.ProyectosSalasFolder;
using Telerik.Windows.Controls;

namespace ControlDeTesisV4.Reportes
{
    /// <summary>
    /// Interaction logic for TesisIncompletas.xaml
    /// </summary>
    public partial class TesisIncompletas
    {
        List<ProyectosTesis> listaIncompletas;

        public TesisIncompletas(List<ProyectosTesis> listaIncompletas)
        {
            InitializeComponent();
            this.listaIncompletas = listaIncompletas;
        }

        private void RadWindow_Loaded(object sender, RoutedEventArgs e)
        {
            GListado.DataContext = listaIncompletas;
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
    }
}
