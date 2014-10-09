using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using ControlDeTesisV4.Dao;
using ControlDeTesisV4.Models;
using ControlDeTesisV4.Singletons;
using ScjnUtilities;

namespace ControlDeTesisV4.EjecutoriasVotos
{
    /// <summary>
    /// Interaction logic for CapturaVotos.xaml
    /// </summary>
    public partial class CapturaVotos
    {
        private PrecedentesTesis precedente;
        private ObservableCollection<Votos> listaVotos;

        public CapturaVotos()
        {
            InitializeComponent();
            precedente = new PrecedentesTesis();
            listaVotos = new ObservableCollection<Votos>();
        }

        private void RadWindow_Loaded(object sender, RoutedEventArgs e)
        {
            this.DataContext = precedente;
            CbxPonentes.DataContext = FuncionariosSingleton.Ponentes;
            CbxTipoAsunto.DataContext = OtrosDatosSingleton.TipoAsuntos;
        }

        private void Numeric_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = StringUtilities.IsTextAllowed(e.Text);
        }

        private void BtnGuardar_Click(object sender, RoutedEventArgs e)
        {
            precedente.TipoAsunto = Convert.ToInt32(CbxTipoAsunto.SelectedValue);
            precedente.IdPonente = Convert.ToInt32(CbxPonentes.SelectedValue);
            
            

            foreach (Votos voto in listaVotos)
            {
                voto.EstadoVoto = 1;
                voto.CcFilePathConten = FilesUtilities.CopyToLocalResource(voto.CcFilePathOrigen);
                voto.VpFilePathConten = FilesUtilities.CopyToLocalResource(voto.VpFilePathOrigen);
                     
                new VotosModel().SetNewProyectoVoto(voto, precedente);
            }

            this.Close();
        }

        private void BtnAgregar_Click(object sender, RoutedEventArgs e)
        {
            DetalleVotos detalle = new DetalleVotos(listaVotos);
            detalle.ShowDialog();
            GListaVotos.DataContext = listaVotos;
        }
    }
}
