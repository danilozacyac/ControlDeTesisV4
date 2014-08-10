using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using ControlDeTesisV4.Dao;
using ControlDeTesisV4.Models;

namespace ControlDeTesisV4.Turno
{
    /// <summary>
    /// Lógica de interacción para ListadoTesisTurnadas.xaml
    /// </summary>
    public partial class ListadoTesisTurnadas : UserControl
    {

    
        public ListadoTesisTurnadas()
        {
        }

        
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        private void UserControl_Loaded_1(object sender, RoutedEventArgs e)
        {
            GListadoTurno.DataContext = new TesisTurnadasModel().GetPreviewTesisTurnadas();
        }

        //private void BtnTurno_Click(object sender, RoutedEventArgs e)
        //{
        //    TurnoDao turno = new TurnoDao();
        //    turno.IdAbogado = Convert.ToInt16(CbxAbogado.SelectedValue);
        //    turno.IdTipoDocto = idTipoDocumento;
        //    turno.NumPaginas = proyecto.NumPaginas;
        //    turno.FTurno = DateTime.Now;
        //}

        

    }
}
