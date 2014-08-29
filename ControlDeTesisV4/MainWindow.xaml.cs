﻿using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using ControlDeTesisV4.Dao;
using ControlDeTesisV4.EjecutoriasVotos;
using ControlDeTesisV4.Models;
using ControlDeTesisV4.ProyectosCcstFolder;
using ControlDeTesisV4.ProyectosSalasFolder;
using ControlDeTesisV4.Reportes;
using ControlDeTesisV4.Turno;
using ControlDeTesisV4.UtilitiesFolder;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.Docking;

namespace ControlDeTesisV4
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            StyleManager.ApplicationTheme = new Windows8Theme();
            InitializeComponent();
            
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LogIn log = new LogIn();
            log.ShowDialog();

            if (AccesoUsuarios.Llave == 0 || AccesoUsuarios.Llave == -1)
            {
                Close();
            }

            if (AccesoUsuarios.Perfil != 5 && AccesoUsuarios.Perfil != 0)
            {
                RibbonHeader.SelectedIndex = 1;
                Proyectos.IsEnabled = false;
            }

            Constants.ListadoDeTesis = new ObservableCollection<TesisTurnadaPreview>();
            Constants.ListadoDeEjecutorias = new ObservableCollection<Ejecutorias>();
            Constants.ListadoDeVotos = new ObservableCollection<Votos>();
                
            new VotosModel().GetVoto();
            new EjecutoriasModel().GetEjecutorias();
            new TesisTurnadasModel().GetPreviewTesisTurnadas();
        }

        private void BtnNuevoPS_Click(object sender, RoutedEventArgs e)
        {
            CapturaProyectoSalas salas = new CapturaProyectoSalas();
            salas.Show();
        }

        private void BtnNuevoPC_Click(object sender, RoutedEventArgs e)
        {
            CapturaProyectoCcst ccst = new CapturaProyectoCcst();
            ccst.Show();
        }

        private bool isCargasVisible = false;
        private void BtnCargas_Click(object sender, RoutedEventArgs e)
        {
            if (!isCargasVisible)
            {
                RadSplitContainer leftContainer = new RadSplitContainer() { InitialPosition = DockState.DockedBottom };
                RadPaneGroup group = new RadPaneGroup();
                RadPane pane = new RadPane();
                pane.Header = "Carga de Trabajo por Abogado";
                pane.Content = new StatTurno();
                RadPane pane2 = new RadPane();
                pane2.Header = "Total";
                pane2.Content = new CargaPorabogado();

                group.AddItem(pane, DockPosition.Center);
                group.AddItem(pane2, DockPosition.Center);
                leftContainer.Items.Add(group);
                Docking.Items.Add(leftContainer);
                isCargasVisible = true;
            }
        }



        private void Docking_PreviewClose(object sender, StateChangeEventArgs e)
        {
            if (e.Panes != null && e.Panes.Count() > 0 && e.Panes.ToList()[0].GetType() == typeof(RadPane))
            {
                if (e.Panes.ToList()[0].Header.Equals("Carga de Trabajo por Abogado"))
                {
                    isCargasVisible = false;
                }
            }
        }

        private void BtnNuevaEjecProv_Click(object sender, RoutedEventArgs e)
        {
            CapturaEjecutoria ejec = new CapturaEjecutoria(1);
            ejec.ShowDialog();
        }

        private void BtnListaTurnadas_Click(object sender, RoutedEventArgs e)
        {
            RadPane pane = new RadPane();
            pane.Header = "Tesis turnadas";
            pane.Content = new ListaTurnadas();

            PanelCentral.AddItem(pane, DockPosition.Center);
        }

        private void BtnEjecPublica_Click(object sender, RoutedEventArgs e)
        {
            CapturaEjecutoria ejec = new CapturaEjecutoria(0);
            ejec.ShowDialog();
        }

        private void BtnTesisPublica_Click(object sender, RoutedEventArgs e)
        {
            TesisPublicar publicar = new TesisPublicar();
            publicar.ShowDialog();
        }

        private void TurnaTesis_Click(object sender, RoutedEventArgs e)
        {
            TurnarWin turnar = new TurnarWin(Constants.TesisTurno, Constants.TesisTurno.Tatj + 1);
            turnar.ShowDialog();
        }

        private void BtnNuevoVotoProv_Click(object sender, RoutedEventArgs e)
        {
            CapturaVotos votos = new CapturaVotos();
            votos.ShowDialog();
        }

        private void BtnPorTurnar_Click(object sender, RoutedEventArgs e)
        {
            RadPane pane = new RadPane();
            pane.Header = "Tesis turnadas";
            pane.Content = new ListaTurnadas();

            PanelCentral.AddItem(pane, DockPosition.Center);
        }

        private void BtnListaEjecutoria_Click(object sender, RoutedEventArgs e)
        {
            RadPane pane = new RadPane();
            pane.Header = "Listado de ejecutorias";
            pane.Content = new ListadoEjecutorias();

            PanelCentral.AddItem(pane, DockPosition.Center);
        }

        private void BtnTurnaEjecutoria_Click(object sender, RoutedEventArgs e)
        {
            if (Constants.EjecutoriaTurno != null)
            {
                TurnarWin turnar = new TurnarWin(Constants.EjecutoriaTurno);
                turnar.ShowDialog();

                Constants.EjecutoriaTurno = null;
            }
        }

        private void BtnVotoPublica_Click(object sender, RoutedEventArgs e)
        {
            VotoSencillo voto = new VotoSencillo();
            voto.ShowDialog();
        }

        private void BtnListaVotos_Click(object sender, RoutedEventArgs e)
        {
            RadPane pane = new RadPane();
            pane.Header = "Listado de votos";
            pane.Content = new ListadoVotos();

            PanelCentral.AddItem(pane, DockPosition.Center);
        }

        
        private void BtnEntregarTesis_Click(object sender, RoutedEventArgs e)
        {
            AuxiliarModel auxiliar = new AuxiliarModel();
            auxiliar.SetFechaEntrega(Constants.TesisTurno.Turno.IdTurno);
            auxiliar.UpdateEstadoDocumento(Constants.TesisTurno.IdTesis, 6, "ProyectosTesis", "IdTesis", "EstadoTesis");
            Constants.TesisTurno.EstadoTesis = 6;
        }

        private void BtnRecibirTesis_Click(object sender, RoutedEventArgs e)
        {
            AuxiliarModel auxiliar = new AuxiliarModel();
            auxiliar.SetEntregaValida(Constants.TesisTurno.Turno);
            auxiliar.UpdateEstadoDocumento(Constants.TesisTurno.IdTesis, 7, "ProyectosTesis", "IdTesis", "EstadoTesis");
            Constants.TesisTurno.EstadoTesis = 7;
        }

        private void BtnEntregarEjecutoria_Click(object sender, RoutedEventArgs e)
        {
            AuxiliarModel auxiliar = new AuxiliarModel();
            auxiliar.SetFechaEntrega(Constants.EjecutoriaTurno.Turno.IdTurno);
            auxiliar.UpdateEstadoDocumento(Constants.EjecutoriaTurno.IdTesis, 6, "Ejecutorias", "IdEjecutoria", "EstadoEjecutoria");
            Constants.EjecutoriaTurno.EstadoEjecutoria = 6;
        }

        private void BtnRecibirEjecutoria_Click(object sender, RoutedEventArgs e)
        {
            AuxiliarModel auxiliar = new AuxiliarModel();
            auxiliar.SetEntregaValida(Constants.EjecutoriaTurno.Turno);
            auxiliar.UpdateEstadoDocumento(Constants.EjecutoriaTurno.IdEjecutoria, 7, "Ejecutorias", "IdEjecutoria", "EstadoEjecutoria");
            Constants.EjecutoriaTurno.EstadoEjecutoria = 7;
        }

        private void BtnEntregarVoto_Click(object sender, RoutedEventArgs e)
        {
            AuxiliarModel auxiliar = new AuxiliarModel();
            auxiliar.SetFechaEntrega(Constants.VotoTurno.Turno.IdTurno);
            auxiliar.UpdateEstadoDocumento(Constants.VotoTurno.IdVoto, 6, "Votos", "IdVoto", "EstadoVoto");
            Constants.VotoTurno.EstadoVoto = 6;
        }

        private void BtnRecibirVoto_Click(object sender, RoutedEventArgs e)
        {
            AuxiliarModel auxiliar = new AuxiliarModel();
            auxiliar.SetFechaEntrega(Constants.VotoTurno.Turno.IdTurno);
            auxiliar.UpdateEstadoDocumento(Constants.VotoTurno.IdVoto, 7, "Votos", "IdVoto", "EstadoVoto");
            Constants.VotoTurno.EstadoVoto = 7;
        }

        private void BtnProyectosSalas_Click(object sender, RoutedEventArgs e)
        {
            SeleccionPeriodo periodo = new SeleccionPeriodo(1);
            periodo.ShowDialog();
        }

        private void BtnProyectosCcst_Click(object sender, RoutedEventArgs e)
        {
            SeleccionPeriodo periodo = new SeleccionPeriodo(2);
            periodo.ShowDialog();
        }
    }
}
