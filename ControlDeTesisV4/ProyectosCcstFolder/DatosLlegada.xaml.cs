using System;
using System.Collections.Generic;
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

namespace ControlDeTesisV4.ProyectosCcstFolder
{
    /// <summary>
    /// Interaction logic for DatosLlegada.xaml
    /// </summary>
    public partial class DatosLlegada
    {
        public DatosLlegada()
        {
            InitializeComponent();
        }

        private void RadWindow_Loaded(object sender, RoutedEventArgs e)
        {
            CbxInstancia.DataContext = (from n in OtrosDatosSingleton.Instancias
                                        where n.IdDato < 5
                                        select n);

           CbxDestinatario.DataContext = FuncionariosSingleton.Signatarios;
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

        private int idInstancia;
        private void CbxInstancia_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
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

        private void BtnLoadOficioRecibido_Click(object sender, RoutedEventArgs e)
        {
            TxtPathOficioAtn.Text = this.GetFilePath();
        }

        private void BtnViewOficioRecibido_Click(object sender, RoutedEventArgs e)
        {
            Process.Start(oficialiaCcst.FileOficioAtnOrigen);
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
            Funcionarios funcionario = new Funcionarios();
            funcionario.NombreCompleto = nombre;

            IEnumerable<Funcionarios> lista = FuncionariosSingleton.Signatarios.Where(func => func.NombreCompleto.Equals(nombre));

            if (lista.Count() == 0)
            {
                funcionario.IdFuncionario = new FuncionariosModel().SetNewSignatario(funcionario);
                FuncionariosSingleton.Signatarios.Add(funcionario);
                CbxDestinatario.SelectedItem = funcionario;
            }

        }

        private void BtnGuardar_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
