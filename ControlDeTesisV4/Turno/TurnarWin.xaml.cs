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
        private Votos votos;
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

        public TurnarWin(Votos votos)
        {
            InitializeComponent();
            this.votos = votos;
            this.idTipoDocumento = 4;
            this.Header = "Turnar Voto";
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
                if (proyecto.Ejecutoria != null)
                {
                    numPaginas += proyecto.Ejecutoria.CcNumFojas;

                    if (proyecto.Ejecutoria.Votos != null)
                    {
                        foreach (Votos voto in proyecto.Ejecutoria.Votos)
                            numPaginas += voto.CcNumFojas;
                    }
                }
                

            }
            else if (idTipoDocumento == 3)
            {
                numPaginas += ejecutoria.CcNumFojas;
            }
            else if (idTipoDocumento == 4)
            {
                numPaginas += votos.ProvNumFojas;
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
                new AuxiliarModel().UpdateEstadoDocumento(proyecto.IdTesis, 5,"ProyectosTesis","IdTesis","EstadoTesis");
            }
            else if (idTipoDocumento == 3)
            {
                turno.IdDocto = ejecutoria.IdEjecutoria;
                new AuxiliarModel().UpdateEstadoDocumento(ejecutoria.IdEjecutoria, 5, "Ejecutorias", "IdEjecutoria", "EstadoEjecutoria");
            }
            else if (idTipoDocumento == 4)
            {
                turno.IdDocto = votos.IdVoto;
                new AuxiliarModel().UpdateEstadoDocumento(votos.IdVoto, 5, "Votos", "IdVoto", "EstadoVoto");
            }

            new TurnoModel().SetNewTurno(turno);
            this.Close();
        }
    }
}