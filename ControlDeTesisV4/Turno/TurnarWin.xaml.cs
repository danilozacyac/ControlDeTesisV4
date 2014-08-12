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
            TxtNumPaginas.Text = (proyecto.NumPaginas + proyecto.Ejecutoria.CcNumFojas).ToString();

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
                new ProyectoTesisCcstModel().UpdateEstadoTesis(proyecto.IdTesis, 4);

            new TurnoModel().SetNewTurno(turno);
            this.Close();
        }
    }
}