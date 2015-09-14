using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using ControlDeTesisV4.Dao;
using ControlDeTesisV4.Models;
using ControlDeTesisV4.VisualComparition;

namespace ControlDeTesisV4.ProyectosCcstFolder
{
    /// <summary>
    /// Lógica de interacción para ListaProyectosCcst.xaml
    /// </summary>
    public partial class ListaProyectosCcst : UserControl
    {
        private ObservableCollection<ProyectoPreview> listaProyectos;
        private ProyectoPreview selectedTesis;

        public ListaProyectosCcst()
        {
            InitializeComponent();
            worker.DoWork += this.WorkerDoWork;
            worker.RunWorkerCompleted += WorkerRunWorkerCompleted;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            LaunchBusyIndicator();
            //listaProyectos = new ProyectoPreviewModel().GetPreviewSalasSinTurnar(2);
            //GListado.DataContext = listaProyectos;
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

        private void GListado_SelectionChanged(object sender, Telerik.Windows.Controls.SelectionChangeEventArgs e)
        {
            selectedTesis = GListado.SelectedItem as ProyectoPreview;
        }


        public void EliminarTesis()
        {
            if (selectedTesis == null)
            {
                MessageBox.Show("Selecciona la tesis que deseas eliminar");
                return;
            }

            MessageBoxResult result = MessageBox.Show("¿Estas seguro de eliminar esta tesis?", "Atención", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                ProyectoTesisCcstModel model = new ProyectoTesisCcstModel();
                model.DeleteTesisCompara(selectedTesis.IdTesis);
                model.DeleteTesisProyecto(selectedTesis.IdTesis);
                model.DeleteProyecto(selectedTesis.IdProyecto);

                model.DeletePrecedentes(selectedTesis.IdTesis);

                listaProyectos.Remove(selectedTesis);
            }
        }

        #region Background Worker

        private BackgroundWorker worker = new BackgroundWorker();
        private void WorkerDoWork(object sender, DoWorkEventArgs e)
        {
            listaProyectos = new ProyectoPreviewModel().GetPreviewSalasSinTurnar(2);
        }

        void WorkerRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            GListado.DataContext = listaProyectos;

            //Dispatcher.BeginInvoke(new Action<ObservableCollection<Organismos>>(this.UpdateGridDataSource), e.Result);
            this.BusyIndicator.IsBusy = false;
        }

        private void LaunchBusyIndicator()
        {
            if (!worker.IsBusy)
            {
                this.BusyIndicator.IsBusy = true;
                worker.RunWorkerAsync();

            }
        }

        #endregion
    }
}
