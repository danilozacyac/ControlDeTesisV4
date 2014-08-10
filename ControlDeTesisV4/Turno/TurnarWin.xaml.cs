using System;
using System.Linq;
using System.Windows;
using ControlDeTesisV4.Dao;
using ControlDeTesisV4.Models;
using ControlDeTesisV4.Singletons;

namespace ControlDeTesisV4.Turno
{
    /// <summary>
    /// Interaction logic for TurnarWin.xaml
    /// </summary>
    public partial class TurnarWin
    {
        private ProyectosTesis proyecto;
        private int idTipoDocumento;

        public TurnarWin(ProyectosTesis proyecto, int idTipoDocumento)
        {
            InitializeComponent();
            this.proyecto = proyecto;
            this.idTipoDocumento = idTipoDocumento;
        }

        private void RadWindow_Loaded(object sender, RoutedEventArgs e)
        {
            CbxAbogado.DataContext = (from n in FuncionariosSingleton.AbogResp
                                      where n.Nivel == 1
                                      orderby n.NombreCompleto
                                      select n);
        }

        private void BtnTurno_Click(object sender, RoutedEventArgs e)
        {
            TurnoDao turno = new TurnoDao();
            turno.IdAbogado = Convert.ToInt16(CbxAbogado.SelectedValue);
            turno.IdTipoDocto = idTipoDocumento;
            turno.NumPaginas = proyecto.NumPaginas;
            turno.FTurno = DateTime.Now;

            if (idTipoDocumento == 1 || idTipoDocumento == 2)
                new ProyectoTesisCcstModel().UpdateEstadoTesis(proyecto.IdTesis, 4);

            new TurnoModel().SetNewTurno(turno);
            this.Close();
        }
    }
}