﻿using System;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using ControlDeTesisV4.Dao;
using ControlDeTesisV4.Models;
using ControlDeTesisV4.Singletons;
using ControlDeTesisV4.UtilitiesFolder;
using DocumentMgmtApi;
using ScjnUtilities;

namespace ControlDeTesisV4.ProyectosCcstFolder
{
    /// <summary>
    /// Interaction logic for TesisPublicar.xaml
    /// </summary>
    public partial class TesisPublicar
    {
        private ProyectosSalas proyecto;
        private readonly bool isUpdating = false;

        private SolidColorBrush textBoxDragColor = new SolidColorBrush(Color.FromRgb(133, 194, 255));
        private SolidColorBrush textBoxDropColor = new SolidColorBrush(Color.FromRgb(255, 255, 255));

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
            int filter = (idInstancia == 4) ? 2 : 1;

            AddNombre newFunc = new AddNombre(nombre, 2, filter);
            newFunc.Owner = this;
            newFunc.ShowDialog();

            if (newFunc.DialogResult == true)
            {
                Funcionarios nuevo = new Funcionarios(
                    Constants.NuevoFuncionario.IdFuncionario, Constants.NuevoFuncionario.Paterno, Constants.NuevoFuncionario.Materno,
                    Constants.NuevoFuncionario.Nombre, Constants.NuevoFuncionario.NombreCompleto);
                nuevo.IdTipoFuncionario = Constants.NuevoFuncionario.IdTipoFuncionario;

                FuncionariosSingleton.Signatarios.Add(nuevo);
                CbxSignatario.SelectedItem = nuevo;
                this.MuestraSignatarios();
            }
            else
                CbxSignatario.Text = String.Empty;

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
                listbox.Background = this.textBoxDragColor;
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
            listbox.Background = this.textBoxDropColor;

            e.Handled = true;
        }

        private void BtnGuardarTurnar_Click(object sender, RoutedEventArgs e)
        {
            

            proyecto.IdEmisor = Convert.ToInt16(CbxEmisores.SelectedValue);
            proyecto.Proyecto.IdInstancia = Convert.ToInt16(CbxInstancia.SelectedValue);
            proyecto.Proyecto.Precedente.IdPonente = Convert.ToInt16(CbxPonentes.SelectedValue);
            proyecto.Proyecto.Precedente.TipoAsunto = Convert.ToInt16(CbxTipoAsunto.SelectedValue);
            proyecto.Proyecto.IdAbogadoResponsable = Convert.ToInt16(CbxAbogado.SelectedValue);
            proyecto.IdSignatario = Convert.ToInt16(CbxSignatario.SelectedValue);
            proyecto.Proyecto.Tatj = (Radjuris.IsChecked == true) ? 1 : 0;
            proyecto.Proyecto.IdTipoJuris = (Radjuris.IsChecked == true) ? Convert.ToInt16(CbxTipoJuris.SelectedValue) : -1;

            new ProyectoTesisSalasModel(proyecto).SetNewProyecto();


            

            this.Close();
        }

        private void ChkEjecutoria_Checked(object sender, RoutedEventArgs e)
        {
            BtnEjecutoria.Visibility = Visibility.Visible;
        }

        private void ChkEjecutoria_Unchecked(object sender, RoutedEventArgs e)
        {
            
        }

        private void BtnEjecutoria_Click(object sender, RoutedEventArgs e)
        {
           
           

        }

        int idInstancia;
        private void CbxInstancia_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CbxEmisores.IsEnabled = true;
            CbxSignatario.IsEnabled = true;
            CbxPonentes.IsEnabled = true;

            idInstancia = ((OtrosDatos)CbxInstancia.SelectedItem).IdDato;

            if (idInstancia == 4)
                CbxEmisores.DataContext = OtrosDatosSingleton.AreasEmisorasPlenos;
            else
                CbxEmisores.DataContext = OtrosDatosSingleton.AreasEmisoras;

            this.MuestraSignatarios();
            this.MuestraPonentes();
        }

        public void MuestraSignatarios()
        {
            int filter = (idInstancia == 4) ? 2 : 1;

            CbxSignatario.DataContext = (from n in FuncionariosSingleton.Signatarios
                                         where n.IdTipoFuncionario == filter
                                         select n);
        }

        private void AddPonente(string nombre)
        {
            int filter = (idInstancia == 4) ? 2 : 1;

            AddNombre newFunc = new AddNombre(nombre, 1, filter);
            newFunc.Owner = this;
            newFunc.ShowDialog();

            if (newFunc.DialogResult == true)
            {
                Funcionarios nuevo = new Funcionarios(
                    Constants.NuevoFuncionario.IdFuncionario, Constants.NuevoFuncionario.Paterno, Constants.NuevoFuncionario.Materno,
                    Constants.NuevoFuncionario.Nombre, Constants.NuevoFuncionario.NombreCompleto);
                nuevo.IdTipoFuncionario = Constants.NuevoFuncionario.IdTipoFuncionario;
                nuevo.Estado = Constants.NuevoFuncionario.Estado;

                FuncionariosSingleton.Ponentes.Add(nuevo);
                this.MuestraPonentes();
                CbxPonentes.SelectedItem = nuevo;
                
            }
            else
                CbxPonentes.Text = String.Empty;

        }

        public void MuestraPonentes()
        {
            int filter = (idInstancia == 4) ? 2 : 1;

            CbxPonentes.DataContext = (from n in FuncionariosSingleton.Ponentes
                                         where n.IdTipoFuncionario == filter && n.Estado == 1
                                         select n);
        }

        private void CbxPonentes_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                this.AddPonente(CbxPonentes.Text);
            }
        }
    }
}
