using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using ControlDeTesisV4.Dao;
using ControlDeTesisV4.Models;
using ControlDeTesisV4.VisualComparition;

namespace ControlDeTesisV4.ProyectosSalasFolder
{
    /// <summary>
    /// Lógica de interacción para ListaProyectoSalas.xaml
    /// </summary>
    public partial class ListaProyectoSalas : UserControl
    {
        private int idInstancia;

        private ObservableCollection<ProyectoPreview> listaProyectos;
        private ProyectoPreview selectedTesis;

        public ListaProyectoSalas(int idInstancia)
        {
            InitializeComponent();
            this.idInstancia = idInstancia;
            worker.DoWork += this.WorkerDoWork;
            worker.RunWorkerCompleted += WorkerRunWorkerCompleted;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            LaunchBusyIndicator();
            //listaProyectos = new ProyectoPreviewModel().GetPreviewSalasSinTurnar(1);
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

            SalasCompare salas = new SalasCompare(lastProyecto);
            salas.ShowDialog();
        }

        private int idTesis = 0;
        private ProyectosTesis lastProyecto;
        private void BtnInfoRec_Click(object sender, RoutedEventArgs e)
        {
            Button but = sender as Button;
            idTesis = Convert.ToInt32(but.Uid);
            lastProyecto = new ProyectoTesisSalasModel().GetProyectoTesis(idTesis);

            DatosProyectoSalas obs = new DatosProyectoSalas(lastProyecto);
            obs.ShowDialog();
        }

        private void BtnInfoObs_Click(object sender, RoutedEventArgs e)
        {
            Button but = sender as Button;
            idTesis = Convert.ToInt32(but.Uid);

            lastProyecto = new ProyectoTesisSalasModel().GetProyectoTesis(idTesis);

            CapturObservaciones obs = new CapturObservaciones(lastProyecto);
            obs.ShowDialog();
        }

        private void BtnInfoApr_Click(object sender, RoutedEventArgs e)
        {
            Button but = sender as Button;
            idTesis = Convert.ToInt32(but.Uid);

            lastProyecto = new ProyectoTesisSalasModel().GetProyectoTesis(idTesis);

            CapturaAprobacion aprob = new CapturaAprobacion(lastProyecto);
            aprob.ShowDialog();
        }

        private void SearchTextBox_Search(object sender, RoutedEventArgs e)
        {
            String tempString = ((TextBox)sender).Text.ToUpper();

            if (!String.IsNullOrEmpty(tempString))
                GListado.DataContext = (from n in listaProyectos
                                        where n.Asunto.ToUpper().Contains(tempString) || n.Rubro.ToUpper().Contains(tempString)
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
                ProyectoTesisSalasModel model = new ProyectoTesisSalasModel();
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
            listaProyectos = new ProyectoPreviewModel().GetPreviewSalasSinTurnar(1, idInstancia);
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
