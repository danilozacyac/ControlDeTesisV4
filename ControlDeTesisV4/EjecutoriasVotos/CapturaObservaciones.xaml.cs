using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using ControlDeTesisV4.Models;
using DocumentMgmtApi;
using ModuloInterconexionCommonApi;

namespace ControlDeTesisV4.EjecutoriasVotos
{
    /// <summary>
    /// Interaction logic for CapturaObservaciones.xaml
    /// </summary>
    public partial class CapturaObservaciones
    {
        ObservableCollection<Observaciones> observaciones;
        string observacionesFilePath;
        private readonly int idEjecutoria;

        public CapturaObservaciones(ObservableCollection<Observaciones> observaciones, string observacionesFilePath, int idEjecutoria)
        {
            InitializeComponent();
            this.observaciones = observaciones;
            this.observacionesFilePath = observacionesFilePath;
            this.idEjecutoria = idEjecutoria;
        }

        private void RadWindow_Loaded(object sender, RoutedEventArgs e)
        {
            GObserv.DataContext = observaciones;
        }

        private void TxtDrag_PreviewDragOver(object sender, DragEventArgs e)
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

        private void TxtDrop_PreviewDrop(object sender, DragEventArgs e)
        {
            TextBox txt = sender as TextBox;

            string[] filenames = (string[])e.Data.GetData(DataFormats.FileDrop, true);

            foreach (string filename in filenames)
                txt.Text = filename;

            var listbox = sender as TextBox;
            listbox.Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));

            WordDocReader.ReadFileContent(TxtObservaciones.Text, observaciones);

            e.Handled = true;
        }

        private void BtnLoadFilePath_Copy_Click(object sender, RoutedEventArgs e)
        {
            TxtObservaciones.Text = DocumentConversion.GetFilePath();
            WordDocReader.ReadFileContent(TxtObservaciones.Text, observaciones);
        }

        private void TxtFojasProv_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = StringUtilities.IsTextAllowed(e.Text);
        }

        private void BtnEliminar_Click(object sender, RoutedEventArgs e)
        {
            Observaciones observ = GObserv.SelectedItem as Observaciones;

            observaciones.Remove(observ);

            if (observ.IdEjecutoria != -1)
            {
                new EjecutoriasModel().DeleteObservaciones(observ);
            }
        }

        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            Observaciones observ = GObserv.SelectedItem as Observaciones;

            DiceSugiere sugiere = new DiceSugiere(observ, observ.IdEjecutoria);
            sugiere.ShowDialog();
        }

        private void BtnAgregar_Click(object sender, RoutedEventArgs e)
        {
            DiceSugiere sugiere = new DiceSugiere(observaciones);
            sugiere.ShowDialog();

        }
    }
}
