using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ControlDeTesisV4.Dao;
using ControlDeTesisV4.EjecutoriasVotos;
using ControlDeTesisV4.Models;
using ControlDeTesisV4.Singletons;
using ControlDeTesisV4.Turno;
using ControlDeTesisV4.UtilitiesFolder;
using DocumentMgmtApi;
using ModuloInterconexionCommonApi;

namespace ControlDeTesisV4.ProyectosCcstFolder
{
    /// <summary>
    /// Interaction logic for TesisPublicar.xaml
    /// </summary>
    public partial class TesisPublicar
    {
        private ProyectosSalas proyecto;
        private readonly bool isUpdating = false;

        public TesisPublicar()
        {
            InitializeComponent();
            this.proyecto = new ProyectosSalas();
            proyecto.Proyecto = new ProyectosTesis();
            proyecto.Proyecto.Precedente = new PrecedentesTesis();
            proyecto.Proyecto.ComparaTesis = new TesisCompara();
        }

        public TesisPublicar(ProyectosSalas proyecto)
        {
            InitializeComponent();
            this.proyecto = proyecto;
            this.isUpdating = true;
        }


        private void RadWindow_Loaded(object sender, RoutedEventArgs e)
        {

            CbxEmisores.DataContext = OtrosDatosSingleton.AreasEmisoras;
            CbxInstancia.DataContext = OtrosDatosSingleton.Instancias;
            CbxPonentes.DataContext = FuncionariosSingleton.Ponentes;
            CbxTipoAsunto.DataContext = OtrosDatosSingleton.TipoAsuntos;
            CbxAbogado.DataContext = FuncionariosSingleton.AbogResp;
            CbxSignatario.DataContext = FuncionariosSingleton.Signatarios;
            CbxTipoJuris.DataContext = OtrosDatosSingleton.TipoJurisprudencias;

            this.DataContext = proyecto;


        }

        private void CbxSignatario_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                this.AddSignatario(CbxSignatario.Text);
            }
        }

        private void AddSignatario(string nombre)
        {
            Funcionarios funcionario = new Funcionarios();
            funcionario.NombreCompleto = nombre;

            IEnumerable<Funcionarios> lista = FuncionariosSingleton.Signatarios.Where(func => func.NombreCompleto.Equals(nombre));

            if (lista.Count() == 0)
            {
                funcionario.IdFuncionario = new FuncionariosModel().SetNewSignatario(funcionario);
                FuncionariosSingleton.Signatarios.Add(funcionario);
                CbxSignatario.SelectedItem = funcionario;
            }

        }


        private void BtnViewOficioRecibido_Click(object sender, RoutedEventArgs e)
        {
            Process.Start(TxtArchivoPath.Text);
        }

        private void BtnLoadOficioRecibido_Click(object sender, RoutedEventArgs e)
        {
            TxtArchivoPath.Text = DocumentConversion.GetFilePath();
        }

        private void RadAislada_Checked(object sender, RoutedEventArgs e)
        {
            LblJuris.Visibility = System.Windows.Visibility.Hidden;
            CbxTipoJuris.Visibility = System.Windows.Visibility.Hidden;
        }

        private void Radjuris_Checked(object sender, RoutedEventArgs e)
        {
            LblJuris.Visibility = System.Windows.Visibility.Visible;
            CbxTipoJuris.Visibility = System.Windows.Visibility.Visible;
        }

        private void Numeric_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = StringUtilities.IsTextAllowed(e.Text);
        }

        private void BtnLoadPath_Click(object sender, RoutedEventArgs e)
        {
            TxtProyFilePath.Text = DocumentConversion.GetFilePath();
        }

        

        private void TextBox_PreviewDragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effects = DragDropEffects.Copy;
                var listbox = sender as TextBox;
                listbox.Background = Constants.TextBoxDragColor;
            }
            else
            {
                e.Effects = DragDropEffects.None;
            }

            e.Handled = true;
        }

        private void TextBox_PreviewDrop(object sender, DragEventArgs e)
        {
            string[] filenames = (string[])e.Data.GetData(DataFormats.FileDrop, true);

            foreach (string filename in filenames)
                TxtProyFilePath.Text = filename;
            //TxtProyFilePath.Text +=  File.ReadAllText(filename);

            var listbox = sender as TextBox;
            listbox.Background = Constants.TextBoxDropColor;

            e.Handled = true;
        }

        private void BtnGuardarTurnar_Click(object sender, RoutedEventArgs e)
        {
            if (ChkEjecutoria.IsChecked == true && proyecto.Proyecto.Ejecutoria == null)
            {
                MessageBox.Show("Seleccionaste la casilla de ejecutoria pero no hay ninguna ejecutoria asociada");
                return;
            }

            proyecto.IdEmisor = Convert.ToInt16(CbxEmisores.SelectedValue);
            proyecto.Proyecto.IdInstancia = Convert.ToInt16(CbxInstancia.SelectedValue);
            proyecto.Proyecto.Precedente.IdPonente = Convert.ToInt16(CbxPonentes.SelectedValue);
            proyecto.Proyecto.Precedente.TipoAsunto = Convert.ToInt16(CbxTipoAsunto.SelectedValue);
            proyecto.Proyecto.IdAbogadoResponsable = Convert.ToInt16(CbxAbogado.SelectedValue);
            proyecto.IdSignatario = Convert.ToInt16(CbxSignatario.SelectedValue);
            proyecto.Proyecto.Tatj = (Radjuris.IsChecked == true) ? 1 : 0;
            proyecto.Proyecto.IdTipoJuris = (Radjuris.IsChecked == true) ? Convert.ToInt16(CbxTipoJuris.SelectedValue) : -1;

            new ProyectoTesisSalasModel(proyecto).SetNewProyecto();


            TurnarWin turnar = new TurnarWin(proyecto.Proyecto, proyecto.Proyecto.Tatj + 1);
            turnar.ShowDialog();

            this.Close();
        }

        private void ChkEjecutoria_Checked(object sender, RoutedEventArgs e)
        {
            BtnEjecutoria.Visibility = Visibility.Visible;
        }

        private void ChkEjecutoria_Unchecked(object sender, RoutedEventArgs e)
        {
            BtnEjecutoria.Visibility = Visibility.Collapsed;

            MessageBoxResult result = MessageBox.Show("¿Estas seguro de eliminar la ejecutoria asociada con todos sus datos",
                                                        "ATENCION", MessageBoxButton.YesNo, MessageBoxImage.Question);
            
            if(result == MessageBoxResult.Yes)
                proyecto.Proyecto.Ejecutoria = null;
        }

        private void BtnEjecutoria_Click(object sender, RoutedEventArgs e)
        {
            proyecto.Proyecto.Ejecutoria = new Ejecutorias();
            proyecto.Proyecto.Ejecutoria.ForObservaciones = 0;
            proyecto.Proyecto.Ejecutoria.Precedente = proyecto.Proyecto.Precedente;
            CapturaEjecutoria ejecutoriaCapt = new CapturaEjecutoria(proyecto.Proyecto.Ejecutoria);
            ejecutoriaCapt.ShowDialog();

        }
    }
}
