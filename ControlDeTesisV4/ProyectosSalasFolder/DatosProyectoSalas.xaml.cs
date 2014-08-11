﻿using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using ControlDeTesisV4.Dao;
using ControlDeTesisV4.Singletons;
using DocumentMgmtApi;
using Microsoft.Win32;
using ModuloInterconexionCommonApi;

namespace ControlDeTesisV4.ProyectosSalasFolder
{
    /// <summary>
    /// Interaction logic for DatosProyectoSalas.xaml
    /// </summary>
    public partial class DatosProyectoSalas
    {
        private ObservableCollection<ProyectosTesis> proyectosSalas;
        ProyectosTesis proyecto;
        private readonly int idInstancia;
        private readonly bool isUpdating;

        public DatosProyectoSalas(ObservableCollection<ProyectosTesis> proyectosSalas, int idInstancia)
        {
            InitializeComponent();

            if (proyectosSalas == null)
                proyectosSalas = new ObservableCollection<ProyectosTesis>();


            this.proyectosSalas = proyectosSalas;
            this.idInstancia = idInstancia;


        }

        public DatosProyectoSalas(ProyectosTesis proyecto)
        {
            InitializeComponent();
            this.proyecto = proyecto;
            this.isUpdating = true;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            if (proyecto == null)
            {
                proyecto = new ProyectosTesis();
                proyecto.Precedente = new PrecedentesTesis();
                proyecto.ComparaTesis = new TesisCompara();
                proyecto.IdInstancia = idInstancia;
            }

            this.DataContext = proyecto;

            CbxAbogado.DataContext = FuncionariosSingleton.AbogResp;
            CbxPonentes.DataContext = FuncionariosSingleton.Ponentes;
            CbxTipoJuris.DataContext = OtrosDatosSingleton.TipoJurisprudencias;
            CbxTipoAsunto.DataContext = OtrosDatosSingleton.TipoAsuntos;

            if (isUpdating)
                LoadOnUpdating();

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

        private void BtnGuardar_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(proyecto.ComparaTesis.ToFilePathOrigen) || String.IsNullOrWhiteSpace(proyecto.ComparaTesis.ToFilePathOrigen))
            {
                MessageBox.Show("Agrega el archivo del proyecto");
                return;
            }
            else if (!File.Exists(proyecto.ComparaTesis.ToFilePathOrigen))
            {
                MessageBox.Show("La ruta del archivo que ingreso es incorrecta, favor de verificar");
                return;
            }

            proyecto.Tatj = (RadAislada.IsChecked == true) ? 0 : 1;
            proyecto.IdTipoJuris = (RadAislada.IsChecked == true) ? 0 : ((OtrosDatos)CbxTipoJuris.SelectedItem).IdDato;
            proyecto.ComparaTesis.TOrigenAlfab = StringUtilities.PrepareToAlphabeticalOrder(proyecto.Rubro.ToUpper());


            proyecto.Precedente.TipoAsunto = ((OtrosDatos)CbxTipoAsunto.SelectedItem).IdDato;
            proyecto.Precedente.IdPonente = ((Funcionarios)CbxPonentes.SelectedItem).IdFuncionario;

            TextRange range = new TextRange(TxtVistaPrevia.Document.ContentStart, TxtVistaPrevia.Document.ContentEnd);
            proyecto.ComparaTesis.TOPlano = range.Text;
            proyecto.ComparaTesis.TextoOriginal = DocumentComparer.GetRtfString(TxtVistaPrevia);

            proyectosSalas.Add(proyecto);
            this.Close();

        }

        private void TxtProyFilePath_PreviewDragOver(object sender, DragEventArgs e)
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

        private void TxtProyFilePath_PreviewDrop(object sender, DragEventArgs e)
        {
            string[] filenames = (string[])e.Data.GetData(DataFormats.FileDrop, true);

            foreach (string filename in filenames)
                TxtProyFilePath.Text = filename;
            //TxtProyFilePath.Text +=  File.ReadAllText(filename);

            var listbox = sender as TextBox;
            listbox.Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));

            e.Handled = true;
        }

        private void TxtProyFilePath_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (File.Exists(TxtProyFilePath.Text))
            {
                this.Cursor = Cursors.Wait;
                TxtVistaPrevia.Document = DocumentComparer.LoadDocumentContent(TxtProyFilePath.Text);
                proyecto.ComparaTesis.ToFilePathOrigen = TxtProyFilePath.Text;
                this.Cursor = Cursors.Arrow;
            }
        }

        private void BtnLoadPath_Click(object sender, RoutedEventArgs e)
        {
            TxtProyFilePath.Text = this.GetFilePath();
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

        private void LoadOnUpdating()
        {
            CbxAbogado.SelectedValue = proyecto.IdAbogadoResponsable;
            CbxTipoAsunto.SelectedValue = proyecto.Precedente.TipoAsunto;
            CbxPonentes.SelectedValue = proyecto.Precedente.IdPonente;

            if (proyecto.Tatj == 0)
            {
                RadAislada.IsChecked = true;
            }
            else
            {
                Radjuris.IsChecked = true;
                CbxTipoJuris.SelectedValue = proyecto.IdTipoJuris;
            }

        }
    }
}