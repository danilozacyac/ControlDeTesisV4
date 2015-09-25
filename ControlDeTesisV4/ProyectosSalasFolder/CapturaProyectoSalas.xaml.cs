using System;
using System.Collections.Generic;
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
using DocumentMgmtApi;
using ScjnUtilities;

namespace ControlDeTesisV4.ProyectosSalasFolder
{
    /// <summary>
    /// Interaction logic for CapturaProyectoSalas.xaml
    /// </summary>
    public partial class CapturaProyectoSalas
    {
        private ProyectosSalas oficialiaSalas;

        public CapturaProyectoSalas()
        {
            InitializeComponent();

            this.oficialiaSalas = new ProyectosSalas();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.DataContext = oficialiaSalas;

            CbxSignatario.DataContext = FuncionariosSingleton.Signatarios;
            CbxInstancia.DataContext = from n in OtrosDatosSingleton.Instancias
                                       where n.IdDato < 5
                                       select n;



        }

        private void TxtTotalProyectos_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = StringUtilities.IsTextAllowed(e.Text);
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

        private void BtnAgregarProy_Click(object sender, RoutedEventArgs e)
        {
            if (oficialiaSalas.Proyectos == null)
                oficialiaSalas.Proyectos = new ObservableCollection<ProyectosTesis>();

            DatosProyectoSalas datos = new DatosProyectoSalas(oficialiaSalas.Proyectos, idInstancia);
            datos.Owner = this;
            datos.ShowDialog();
            GProyectos.DataContext = oficialiaSalas.Proyectos;
        }

        private void TxtArchivoPath_PreviewDragOver(object sender, DragEventArgs e)
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

        private void TxtArchivoPath_PreviewDrop(object sender, DragEventArgs e)
        {
            string[] filenames = (string[])e.Data.GetData(DataFormats.FileDrop, true);

            foreach (string filename in filenames)
                TxtArchivoPath.Text = filename;
            //TxtProyFilePath.Text +=  File.ReadAllText(filename);

            var listbox = sender as TextBox;
            listbox.Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));

            e.Handled = true;
        }

        private void BtnGuardar_Click(object sender, RoutedEventArgs e)
        {
            oficialiaSalas.FRecepcion = DtpFRecep.SelectedDate;
            oficialiaSalas.IdEmisor = ((OtrosDatos)CbxEmisores.SelectedItem).IdDato;
            oficialiaSalas.IdSignatario = ((Funcionarios)CbxSignatario.SelectedItem).IdFuncionario;
            oficialiaSalas.Ejecutoria = (RadProySiEje.IsChecked == true) ? 1 : 0;


            new ProyectoTesisSalasModel(oficialiaSalas).SetNewProyecto();

            this.Close();
        }

        private int idInstancia;
        private void CbxInstancia_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CbxEmisores.IsEnabled = true;
            CbxSignatario.IsEnabled = true;

            idInstancia = ((OtrosDatos)CbxInstancia.SelectedItem).IdDato;

            if (oficialiaSalas.Proyectos != null && oficialiaSalas.Proyectos.Count > 0)
            {
                foreach (ProyectosTesis tesis in oficialiaSalas.Proyectos)
                {
                    tesis.IdInstancia = idInstancia;
                }
            }

            if (idInstancia == 4)
                CbxEmisores.DataContext = OtrosDatosSingleton.AreasEmisorasPlenos;
            else
                CbxEmisores.DataContext = OtrosDatosSingleton.AreasEmisoras;

            this.MuestraSignatarios();
        }

        private void BtnLoadOficioRecibido_Click(object sender, RoutedEventArgs e)
        {
            TxtArchivoPath.Text = DocumentConversion.GetFilePath();
        }

        

        private void TxtArchivoPath_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (File.Exists(TxtArchivoPath.Text))
            {
                BtnViewOficioRecibido.IsEnabled = true;
                oficialiaSalas.OfRecepcionPathOrigen = TxtArchivoPath.Text;
            }
            else
            {
                BtnViewOficioRecibido.IsEnabled = false;
                MessageBox.Show("Verifica que la ruta del archivo del oficio sea correcta");
            }
        }

        private void BtnViewOficioRecibido_Click(object sender, RoutedEventArgs e)
        {
            Process.Start(oficialiaSalas.OfRecepcionPathOrigen);
        }


        public void MuestraSignatarios()
        {
            int filter = (idInstancia == 4) ? 2 : 1;

            CbxSignatario.DataContext = (from n in FuncionariosSingleton.Signatarios
                                         where n.IdTipoFuncionario == filter
                                         select n);
        }


    }
}
