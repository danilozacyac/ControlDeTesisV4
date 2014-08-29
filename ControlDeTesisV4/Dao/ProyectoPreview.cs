using System;
using System.Linq;

namespace ControlDeTesisV4.Dao
{
    public class ProyectoPreview
    {
        private int idProyecto;
        private int idTesis;
        private string oficioRecepcion;
        private DateTime? fRecepcion;
        private string rubro;
        private int tatj;
        private string asunto;
        private DateTime? fResolucion;
        private int idPonente;
        private int estadoTesis;
        private int idAbogadoResponsable;
        private readonly string comparativoImage = "/ControlDeTesis2;component/Resources/compare.png";

        public string ComparativoImage
        {
            get
            {
                return this.comparativoImage;
            }
        }

        public int IdProyecto
        {
            get
            {
                return this.idProyecto;
            }
            set
            {
                this.idProyecto = value;
            }
        }

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

        public string OficioRecepcion
        {
            get
            {
                return this.oficioRecepcion;
            }
            set
            {
                this.oficioRecepcion = value;
            }
        }

        public DateTime? FRecepcion
        {
            get
            {
                return this.fRecepcion;
            }
            set
            {
                this.fRecepcion = value;
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

        public string Asunto
        {
            get
            {
                return this.asunto;
            }
            set
            {
                this.asunto = value;
            }
        }

        public DateTime? FResolucion
        {
            get
            {
                return this.fResolucion;
            }
            set
            {
                this.fResolucion = value;
            }
        }

        public int IdPonente
        {
            get
            {
                return this.idPonente;
            }
            set
            {
                this.idPonente = value;
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
    }
}
