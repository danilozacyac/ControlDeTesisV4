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
using Microsoft.Win32;

namespace ControlDeTesisV4.ProyectosCcstFolder
{
    /// <summary>
    /// Interaction logic for DatosLlegada.xaml
    /// </summary>
    public partial class DatosLlegada
    {
        private ProyectosTesis preview;
        private ProyectosCcst proyecto;

        public DatosLlegada(ProyectosTesis preview)
        {
            InitializeComponent();
            this.preview = preview;
        }

        private void RadWindow_Loaded(object sender, RoutedEventArgs e)
        {
            proyecto = new ProyectoTesisCcstModel().GetDatosLlegada(preview.IdTesis);

           CbxDestinatario.DataContext = FuncionariosSingleton.Signatarios;

           this.DataContext = proyecto;
           CbxDestinatario.SelectedValue = proyecto.Destinatario;
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
                proyecto.FileOficioAtnOrigen = TxtPathOficioAtn.Text;
            }
            else
            {
                BtnViewOficioRecibido.IsEnabled = false;
                MessageBox.Show("Verifica que la ruta del archivo del oficio sea correcta");
            }
        }


        private void BtnLoadOficioRecibido_Click(object sender, RoutedEventArgs e)
        {
            TxtPathOficioAtn.Text = this.GetFilePath();
        }

        private void BtnViewOficioRecibido_Click(object sender, RoutedEventArgs e)
        {
            Process.Start(proyecto.FileOficioAtnOrigen);
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
                new FuncionariosModel().SetNewSignatario(funcionario);
                FuncionariosSingleton.Signatarios.Add(funcionario);
                CbxDestinatario.SelectedItem = funcionario;
            }

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

        private void BtnGuardar_Click(object sender, RoutedEventArgs e)
        {
            proyecto.Destinatario = Convert.ToInt16(CbxDestinatario.SelectedValue);
            new ProyectoTesisCcstModel().UpdateDatosLlegada(proyecto);
            this.Close();
        }
    }
}
