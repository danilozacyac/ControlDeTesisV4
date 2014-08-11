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
using ControlDeTesisV4.Models;
using DocumentMgmtApi;
using Telerik.Windows.Controls;

namespace ControlDeTesisV4.EjecutoriasVotos
{
    /// <summary>
    /// Interaction logic for DiceSugiere.xaml
    /// </summary>
    public partial class DiceSugiere
    {
        private ObservableCollection<Observaciones> listaObservaciones;
        private Observaciones observacion;
        private readonly int idEjecutoria;

        public DiceSugiere(ObservableCollection<Observaciones> listaObservaciones)
        {
            InitializeComponent();
            this.listaObservaciones = listaObservaciones;
            observacion = new Observaciones();
            idEjecutoria = -1;
            observacion.IdEjecutoria = idEjecutoria;
        }

        public DiceSugiere(Observaciones observacion, int idEjecutoria)
        {
            InitializeComponent();
            this.observacion = observacion;
            this.idEjecutoria = idEjecutoria;
        }

        private void RadWindow_Loaded(object sender, RoutedEventArgs e)
        {
            this.DataContext = observacion;
        }

        private void RadGuardar_Click(object sender, RoutedEventArgs e)
        {
            if (idEjecutoria == -1) 
            {
                listaObservaciones.Add(observacion);
                this.Close();
            }
            else
            {
                listaObservaciones.Add(observacion);
                new EjecutoriasModel().SetNewObservacion(observacion, idEjecutoria);
                this.Close();
            }
        }
    }
}
