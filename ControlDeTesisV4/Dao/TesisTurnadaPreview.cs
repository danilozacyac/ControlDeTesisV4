using System;
using System.Linq;
using ControlDeTesisV4.Singletons;

namespace ControlDeTesisV4.Dao
{
    public class TesisTurnadaPreview
    {
        private int idTesis;
        private int idabogado;
        private string claveTesis;
        private string rubro;
        private int idTipoAsunto;
        private int numAsunto;
        private int yearAsunto;
        private int idInstancia;
        private DateTime? fAprobacion;
        private DateTime? fTurno;
        private DateTime? fSugerida;
        private DateTime? fEntrega;
        private bool enTiempo;
        private int diasAtraso;
        private int semaforo;

        

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

        public int Idabogado
        {
            get
            {
                return this.idabogado;
            }
            set
            {
                this.idabogado = value;
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

        public int IdTipoAsunto
        {
            get
            {
                return this.idTipoAsunto;
            }
            set
            {
                this.idTipoAsunto = value;
            }
        }

        public int NumAsunto
        {
            get
            {
                return this.numAsunto;
            }
            set
            {
                this.numAsunto = value;
            }
        }

        public int YearAsunto
        {
            get
            {
                return this.yearAsunto;
            }
            set
            {
                this.yearAsunto = value;
            }
        }

        public string Asunto
        {
            get
            {
                return (from n in OtrosDatosSingleton.TipoAsuntos
                        where n.IdDato == this.IdTipoAsunto
                        select n.Descripcion).ToList()[0] +
                       " " + NumAsunto + "/" + YearAsunto;
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

        public DateTime? FTurno
        {
            get
            {
                return this.fTurno;
            }
            set
            {
                this.fTurno = value;
            }
        }

        public DateTime? FSugerida
        {
            get
            {
                return this.fSugerida;
            }
            set
            {
                this.fSugerida = value;
            }
        }

        public DateTime? FEntrega
        {
            get
            {
                return this.fEntrega;
            }
            set
            {
                this.fEntrega = value;
            }
        }

        public bool EnTiempo
        {
            get
            {
                return this.enTiempo;
            }
            set
            {
                this.enTiempo = value;
            }
        }

        public int DiasAtraso
        {
            get
            {
                return this.diasAtraso;
            }
            set
            {
                this.diasAtraso = value;
            }
        }

        public int Semaforo
        {
            get
            {
                return this.semaforo;
            }
            set
            {
                this.semaforo = value;
            }
        }
    }
}
