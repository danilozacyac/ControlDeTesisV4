using System;
using System.Collections.ObjectModel;
using System.Linq;
using ControlDeTesisV4.Dao;

namespace ControlDeTesisV4.UtilitiesFolder
{
    public class Constants
    {

    public static Funcionarios NuevoFuncionario;

        #region Variables UserControls

        public static ProyectosTesis TesisTurno = null;
        public static Ejecutorias EjecutoriaTurno = null;
        public static Votos VotoTurno = null;


        public static ObservableCollection<TesisTurnadaPreview> ListadoDeTesis;
        public static ObservableCollection<Ejecutorias> ListadoDeEjecutorias;
        public static ObservableCollection<Votos> ListadoDeVotos;

        #endregion

    }
}
