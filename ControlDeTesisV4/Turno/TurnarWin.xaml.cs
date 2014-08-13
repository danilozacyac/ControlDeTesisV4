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
        private Ejecutorias ejecutoria;
        private int idTipoDocumento;
        private int numPaginas = 0;

        public TurnarWin(ProyectosTesis proyecto, int idTipoDocumento)
        {
            InitializeComponent();
            this.proyecto = proyecto;
            this.idTipoDocumento = idTipoDocumento;
            this.Header = "Turnar tesis";
        }

        public TurnarWin(Ejecutorias ejecutoria)
        {
            InitializeComponent();
            this.ejecutoria = ejecutoria;
            this.idTipoDocumento = 3;
            this.Header = "Turnar ejecutoria";
        }

        private void RadWindow_Loaded(object sender, RoutedEventArgs e)
        {
            CbxAbogado.DataContext = (from n in FuncionariosSingleton.AbogResp
                                      where n.Nivel == 1
                                      orderby n.NombreCompleto
                                      select n);

            DtpFTurno.SelectedDate = DateTime.Now;
            DtpSugerida.SelectedDate = DateTime.Now.AddDays(5);

            if (idTipoDocumento == 1 || idTipoDocumento == 2)
            {
                numPaginas += proyecto.NumPaginas;
                numPaginas += (proyecto.Ejecutoria != null) ? proyecto.Ejecutoria.CcNumFojas : 0;

            }
            else if (idTipoDocumento == 3)
            {
                numPaginas += ejecutoria.CcNumFojas;
            }
            
            

        }

        private void BtnTurno_Click(object sender, RoutedEventArgs e)
        {
            TurnoDao turno = new TurnoDao();
            turno.IdAbogado = Convert.ToInt16(CbxAbogado.SelectedValue);
            turno.IdTipoDocto = idTipoDocumento;
            turno.NumPaginas = Convert.ToInt32(TxtNumPaginas.Text);
            turno.FTurno = DtpFTurno.SelectedDate;
            turno.FSugerida = DtpSugerida.SelectedDate;
            turno.FTurno = DateTime.Now;

            if (idTipoDocumento == 1 || idTipoDocumento == 2)
            {
                turno.IdDocto = proyecto.IdTesis;
                new ProyectoTesisCcstModel().UpdateEstadoTesis(proyecto.IdTesis, 5);
            }
            else if (idTipoDocumento == 3)
            {
                turno.IdDocto = ejecutoria.IdEjecutoria;
                new EjecutoriasModel().UpdateEstadoEjecutoria(ejecutoria.IdEjecutoria, 5);
            }
            else if (idTipoDocumento == 4)
            {
                //turno.IdDocto = votos.IdVoto
                //Actualizar estado del voto
            }

            new TurnoModel().SetNewTurno(turno);
            this.Close();
        }
    }
}