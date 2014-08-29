using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using ControlDeTesisV4.Models;
using DocumentMgmtApi;

namespace ControlDeTesisV4.EjecutoriasVotos
{
    /// <summary>
    /// Interaction logic for DiceSugiere.xaml
    /// </summary>
    public partial class DiceSugiere
    {
        private ObservableCollection<Observaciones> listaObservaciones;
        private Observaciones observacion;
        private int idDocumento = -1;
        private readonly int tipoDocumento;

        public DiceSugiere(ObservableCollection<Observaciones> listaObservaciones)
        {
            InitializeComponent();
            this.listaObservaciones = listaObservaciones;
            observacion = new Observaciones();
            observacion.IdDocumento = idDocumento;
        }

        public DiceSugiere(Observaciones observacion, int idDocumento, int tipoDocumento)
        {
            InitializeComponent();
            this.observacion = observacion;
            this.idDocumento = idDocumento;
            this.tipoDocumento = tipoDocumento;
        }

        private void RadWindow_Loaded(object sender, RoutedEventArgs e)
        {
            this.DataContext = observacion;
        }

        private void RadGuardar_Click(object sender, RoutedEventArgs e)
        {
            if (idDocumento == -1) 
            {
                listaObservaciones.Add(observacion);
                this.Close();
            }
            else
            {
                listaObservaciones.Add(observacion);

                if (tipoDocumento == 3)
                    new EjecutoriasModel().SetNewObservacion(observacion, idDocumento);
                else if (tipoDocumento == 4)
                    new VotosModel().SetNewObservacion(observacion, idDocumento);
                
                this.Close();
            }
        }
    }
}
