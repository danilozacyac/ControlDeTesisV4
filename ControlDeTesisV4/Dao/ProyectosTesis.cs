using System;
using System.ComponentModel;
using System.Linq;

namespace ControlDeTesisV4.Dao
{
    public class ProyectosTesis : INotifyPropertyChanged
    {
        private int idTesis;
        private string oficioEnvio;
        private DateTime? fEnvio;
        private int fEnvioInt;
        private string oficioEnvioPathOrigen;
        private string oficioEnvioPathConten;
        private string rubro;
        private int tatj;
        private int idTipoJuris;
        private int numPaginas = 1;
        private int aprobada;
        private DateTime? fAprobacion;
        private int fAprobacionInt;
        private String numTesis;
        private int numTesisInt;
        private int yearTesis = System.DateTime.Now.Year;
        private string claveTesis;
        private int idAbogadoResponsable;
        private int idInstancia;
        private int idSubInstancia;
        private TesisCompara comparaTesis;
        private PrecedentesTesis precedente;
        private Ejecutorias ejecutoria;
        private TurnoDao turno;
        private int estadoTesis;

        public int IdTesis
        {
            get
            {
                return this.idTesis;
            }
            set
            {
                this.idTesis = value;
            }
        }

        public string OficioEnvio
        {
            get
            {
                return this.oficioEnvio;
            }
            set
            {
                this.oficioEnvio = value;
            }
        }

        public DateTime? FEnvio
        {
            get
            {
                return this.fEnvio;
            }
            set
            {
                this.fEnvio = value;
            }
        }

        public int FEnvioInt
        {
            get
            {
                return this.fEnvioInt;
            }
            set
            {
                this.fEnvioInt = value;
            }
        }

        public string OficioEnvioPathOrigen
        {
            get
            {
                return this.oficioEnvioPathOrigen;
            }
            set
            {
                this.oficioEnvioPathOrigen = value;
            }
        }

        public string OficioEnvioPathConten
        {
            get
            {
                return this.oficioEnvioPathConten;
            }
            set
            {
                this.oficioEnvioPathConten = value;
            }
        }

        public string Rubro
        {
            get
            {
                return this.rubro;
            }
            set
            {
                this.rubro = value;
            }
        }

        public int Tatj
        {
            get
            {
                return this.tatj;
            }
            set
            {
                this.tatj = value;
            }
        }

        public int IdTipoJuris
        {
            get
            {
                return this.idTipoJuris;
            }
            set
            {
                this.idTipoJuris = value;
            }
        }

        public int NumPaginas
        {
            get
            {
                return this.numPaginas;
            }
            set
            {
                this.numPaginas = value;
            }
        }

        public int Aprobada
        {
            get
            {
                return this.aprobada;
            }
            set
            {
                this.aprobada = value;
            }
        }

        public DateTime? FAprobacion
        {
            get
            {
                return this.fAprobacion;
            }
            set
            {
                this.fAprobacion = value;
            }
        }

        public int FAprobacionInt
        {
            get
            {
                return this.fAprobacionInt;
            }
            set
            {
                this.fAprobacionInt = value;
            }
        }

        public string NumTesis
        {
            get
            {
                return this.numTesis;
            }
            set
            {
                this.numTesis = value;
            }
        }

        public int NumTesisInt
        {
            get
            {
                return this.numTesisInt;
            }
            set
            {
                this.numTesisInt = value;
            }
        }

        public int YearTesis
        {
            get
            {
                return this.yearTesis;
            }
            set
            {
                this.yearTesis = value;
            }
        }

        public string ClaveTesis
        {
            get
            {
                return this.claveTesis;
            }
            set
            {
                this.claveTesis = value;
            }
        }

        public int IdAbogadoResponsable
        {
            get
            {
                return this.idAbogadoResponsable;
            }
            set
            {
                this.idAbogadoResponsable = value;
            }
        }

        public int IdInstancia
        {
            get
            {
                return this.idInstancia;
            }
            set
            {
                this.idInstancia = value;
            }
        }

        public int IdSubInstancia
        {
            get
            {
                return this.idSubInstancia;
            }
            set
            {
                this.idSubInstancia = value;
            }
        }

        public TesisCompara ComparaTesis
        {
            get
            {
                return this.comparaTesis;
            }
            set
            {
                this.comparaTesis = value;
            }
        }

        public PrecedentesTesis Precedente
        {
            get
            {
                return this.precedente;
            }
            set
            {
                this.precedente = value;
            }
        }

        public Ejecutorias Ejecutoria
        {
            get
            {
                return this.ejecutoria;
            }
            set
            {
                this.ejecutoria = value;
            }
        }

        public TurnoDao Turno
        {
            get
            {
                return this.turno;
            }
            set
            {
                this.turno = value;
                this.OnPropertyChanged("Turno");
            }
        }

        public int EstadoTesis
        {
            get
            {
                return this.estadoTesis;
            }
            set
            {
                this.estadoTesis = value;
                this.OnPropertyChanged("EstadoTesis");
            }
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion // INotifyPropertyChanged Members
    }
}
