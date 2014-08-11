﻿using System;
using System.Linq;
using System.Windows;
using ControlDeTesisV4.EjecutoriasVotos;
using ControlDeTesisV4.ProyectosCcstFolder;
using ControlDeTesisV4.ProyectosSalasFolder;
using ControlDeTesisV4.Turno;
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

            //CapturaEjecutorias ejecuto = new CapturaEjecutorias();
            //ejecuto.ShowDialog();

            //Turnar turno = new Turnar();
            //turno.ShowDialog();
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
    }
}