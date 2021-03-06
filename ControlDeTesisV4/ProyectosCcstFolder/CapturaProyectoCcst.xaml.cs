﻿using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using ControlDeTesisV4.Dao;
using ControlDeTesisV4.Models;
using ControlDeTesisV4.Singletons;
using ControlDeTesisV4.UtilitiesFolder;
using Microsoft.Win32;

namespace ControlDeTesisV4.ProyectosCcstFolder
{
    /// <summary>o
    /// Interaction logic for CapturaProyectoCcst.xaml
    /// </summary>
    public partial class CapturaProyectoCcst
    {
        private ProyectosCcst oficialiaCcst;

        public CapturaProyectoCcst()
        {
            InitializeComponent();
            this.oficialiaCcst = new ProyectosCcst();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.DataContext = oficialiaCcst;

            CbxInstancia.DataContext = (from n in OtrosDatosSingleton.Instancias
                                        where n.IdDato < 5
                                        select n);
        }


        private void CbxDestinatario_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                this.AddSignatario(CbxDestinatario.Text);
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
                CbxDestinatario.SelectedItem = nuevo;
                this.MuestraSignatarios();
            }
            else
                CbxDestinatario.Text = String.Empty;

        }

        private void TxtPathOficioAtn_PreviewDragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effects = DragDropEffects.Copy;
                var listbox = sender as TextBox;
                listbox.Background = new SolidColorBrush(Color.FromRgb(155, 155, 155));
            }
            else
            {
                e.Effects = DragDropEffects.None;
            }

            e.Handled = true;
        }

        private void TxtPathOficioAtn_PreviewDrop(object sender, DragEventArgs e)
        {
            string[] filenames = (string[])e.Data.GetData(DataFormats.FileDrop, true);

            foreach (string filename in filenames)
                TxtPathOficioAtn.Text = filename;
            //TxtProyFilePath.Text +=  File.ReadAllText(filename);

            var listbox = sender as TextBox;
            listbox.Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));

            e.Handled = true;
        }

        private int idInstancia;
        private void CbxInstancia_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CbxDestinatario.IsEnabled = true;

            idInstancia = ((OtrosDatos)CbxInstancia.SelectedItem).IdDato;

            CbxPlenosDeCircuito.Visibility = (idInstancia < 4) ? Visibility.Hidden : Visibility.Visible;
            LblPlenos.Visibility = (idInstancia < 4) ? Visibility.Hidden : Visibility.Visible;

            if (oficialiaCcst.Proyectos != null && oficialiaCcst.Proyectos.Count > 0)
            {
                foreach (ProyectosTesis tesis in oficialiaCcst.Proyectos)
                {
                    tesis.IdInstancia = idInstancia;

                    if (idInstancia != 4)
                        tesis.IdSubInstancia = 0;
                }
            }

            if (idInstancia == 4)
                CbxPlenosDeCircuito.DataContext = OtrosDatosSingleton.AreasEmisorasPlenos;

        }

        private int subInstancia;
        private void CbxPlenosDeCircuito_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            subInstancia = ((OtrosDatos)CbxPlenosDeCircuito.SelectedItem).IdDato;

            if (oficialiaCcst.Proyectos != null && oficialiaCcst.Proyectos.Count > 0)
            {
                foreach (ProyectosTesis tesis in oficialiaCcst.Proyectos)
                {
                    tesis.IdSubInstancia = subInstancia;
                }
            }
        }

        private void BtnAgregarProy_Click(object sender, RoutedEventArgs e)
        {
            if (oficialiaCcst.Proyectos == null)
                oficialiaCcst.Proyectos = new ObservableCollection<ProyectosTesis>();

            DatosProyectoCcst datos = new DatosProyectoCcst(oficialiaCcst.Proyectos, idInstancia);
            datos.ShowDialog();
            GProyectos.DataContext = oficialiaCcst.Proyectos;

        }

        private void BtnGuardar_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(oficialiaCcst.OficioAtn) || String.IsNullOrWhiteSpace(oficialiaCcst.OficioAtn))
            {
                MessageBox.Show("Ingresa el número de oficio de solicitud de proyectos");
                return;
            }

            if (oficialiaCcst.FOficioAtn == null)
            {
                MessageBox.Show("Selecciona la fecha en que se recibio el oficio de solicitud");
                return;
            }

            //if (String.IsNullOrEmpty(oficialiaCcst.FileOficioAtnOrigen) || String.IsNullOrWhiteSpace(oficialiaCcst.FileOficioAtnOrigen))
            //{
            //    MessageBox.Show("Ingresa la ruta del archivo del oficio recibido");
            //    return;
            //}

            if (CbxInstancia.SelectedIndex == -1)
            {
                MessageBox.Show("Selecciona la instancia que realiza la solicitud");
                return;
            }

            //if (CbxDestinatario.SelectedIndex == -1)
            //{
            //    MessageBox.Show("Selecciona el destinatario de los archivos de respuesta");
            //    return;
            //}

            oficialiaCcst.Destinatario = CbxDestinatario.SelectedValue  as int? ?? -1;

            new ProyectoTesisCcstModel(oficialiaCcst).SetNewProyecto();


            this.Close();
        }

        private void TxtPathOficioAtn_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (File.Exists(TxtPathOficioAtn.Text))
            {
                BtnViewOficioRecibido.IsEnabled = true;
                oficialiaCcst.FileOficioAtnOrigen = TxtPathOficioAtn.Text;
            }
            else
            {
                BtnViewOficioRecibido.IsEnabled = false;
                MessageBox.Show("Verifica que la ruta del archivo del oficio sea correcta");
            }
        }

        private void BtnViewOficioRecibido_Click(object sender, RoutedEventArgs e)
        {
            Process.Start(oficialiaCcst.FileOficioAtnOrigen);
        }

        private String GetFilePath()
        {
            OpenFileDialog dialog = new OpenFileDialog();

            dialog.Filter = "Office Documents|*.doc;*.docx| RichTextFiles |*.rtf";

            dialog.InitialDirectory = @"C:\Users\" + Environment.UserName + @"\Documents";
            dialog.Title = "Selecciona el archivo del proyecto";
            dialog.ShowDialog();

            return dialog.FileName;
        }

        private void BtnLoadOficioRecibido_Click(object sender, RoutedEventArgs e)
        {
            TxtPathOficioAtn.Text = this.GetFilePath();
        }

        public void MuestraSignatarios()
        {
            int filter = (idInstancia == 4) ? 2 : 1;

            CbxDestinatario.DataContext = (from n in FuncionariosSingleton.Signatarios
                                         where n.IdTipoFuncionario == filter
                                         select n);
        }

    }
}
