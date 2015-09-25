using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using ControlDeTesisV4.Dao;
using ControlDeTesisV4.Models;
using ControlDeTesisV4.Singletons;
using ControlDeTesisV4.UtilitiesFolder;
using DocumentMgmtApi;

namespace ControlDeTesisV4.ProyectosSalasFolder
{
    /// <summary>
    /// Interaction logic for DatosLlegadaSalas.xaml
    /// </summary>
    public partial class DatosLlegadaSalas
    {

        private ProyectosTesis tesis;
        private ProyectosSalas proyecto;

        public DatosLlegadaSalas(ProyectosTesis tesis)
        {
            InitializeComponent();
            this.tesis = tesis;
        }

        private void RadWindow_Loaded(object sender, RoutedEventArgs e)
        {
            proyecto = new ProyectoTesisSalasModel().GetDatosLlegada(tesis.IdTesis);

            this.DataContext = proyecto;

            CbxSignatario.DataContext = FuncionariosSingleton.Signatarios;
            CbxEmisores.DataContext = OtrosDatosSingleton.AreasEmisoras;
            


            CbxSignatario.SelectedValue = proyecto.IdSignatario;
            CbxEmisores.SelectedValue = proyecto.IdEmisor;
            
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

        private void TxtArchivoPath_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (File.Exists(TxtArchivoPath.Text))
            {
                BtnViewOficioRecibido.IsEnabled = true;
                proyecto.OfRecepcionPathOrigen = TxtArchivoPath.Text;
            }
            else
            {
                BtnViewOficioRecibido.IsEnabled = false;
                MessageBox.Show("Verifica que la ruta del archivo del oficio sea correcta");
            }
        }

        

        private void BtnLoadOficioRecibido_Click(object sender, RoutedEventArgs e)
        {
            TxtArchivoPath.Text = DocumentConversion.GetFilePath();
        }

        private void BtnViewOficioRecibido_Click(object sender, RoutedEventArgs e)
        {
            Process.Start(proyecto.OfRecepcionPathOrigen);
        }

        private void BtnGuardar_Click(object sender, RoutedEventArgs e)
        {
            proyecto.IdSignatario = Convert.ToInt32(CbxSignatario.SelectedValue);
            proyecto.IdEmisor = Convert.ToInt32(CbxEmisores.SelectedValue);
            new ProyectoTesisSalasModel().UpdateDatosLlegada(proyecto);
            this.Close();
        }

        private void BtnSalir_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


        //private void CbxSignatario_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.Key == Key.Enter)
        //    {
        //        this.AddSignatario(CbxSignatario.Text);
        //    }
        //}

        //int idInstancia;
        //private void AddSignatario(string nombre)
        //{
        //    int filter = (idInstancia == 4) ? 2 : 1;

        //    AddNombre newFunc = new AddNombre(nombre, 2, filter);
        //    newFunc.Owner = this;
        //    newFunc.ShowDialog();

        //    if (newFunc.DialogResult == true)
        //    {
        //        Funcionarios nuevo = new Funcionarios(
        //            Constants.NuevoFuncionario.IdFuncionario, Constants.NuevoFuncionario.Paterno, Constants.NuevoFuncionario.Materno,
        //            Constants.NuevoFuncionario.Nombre, Constants.NuevoFuncionario.NombreCompleto);
        //        nuevo.IdTipoFuncionario = Constants.NuevoFuncionario.IdTipoFuncionario;

        //        FuncionariosSingleton.Signatarios.Add(nuevo);
        //        CbxSignatario.SelectedItem = nuevo;
        //        this.MuestraSignatarios();
        //    }
        //    else
        //        CbxSignatario.Text = String.Empty;


        //}

        //public void MuestraSignatarios()
        //{
        //    int filter = (idInstancia == 4) ? 2 : 1;

        //    CbxSignatario.DataContext = (from n in FuncionariosSingleton.Signatarios
        //                                 where n.IdTipoFuncionario == filter
        //                                 select n);
        //}

    }
}
