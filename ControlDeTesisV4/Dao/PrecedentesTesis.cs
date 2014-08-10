using System;
using System.Linq;
using ControlDeTesisV4.Singletons;

namespace ControlDeTesisV4.Dao
{
    public class PrecedentesTesis
    {
        private int idTesis;
        private int idPrecedente;
        private int tipoAsunto;
        private int numAsunto;
        private int yearAsunto = DateTime.Now.Year;
        private DateTime? fResolucion;
        private int fResolucionInt;
        private int idPonente;
        private string promovente;

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

        public int IdPrecedente
        {
            get
            {
                return this.idPrecedente;
            }
            set
            {
                this.idPrecedente = value;
            }
        }

        public int TipoAsunto
        {
            get
            {
                return this.tipoAsunto;
            }
            set
            {
                this.tipoAsunto = value;
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

        public int FResolucionInt
        {
            get
            {
                return this.fResolucionInt;
            }
            set
            {
                this.fResolucionInt = value;
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

        public string Promovente
        {
            get
            {
                return this.promovente;
            }
            set
            {
                this.promovente = value;
            }
        }

        public string Asunto
        {
            get
            {
                return (from n in OtrosDatosSingleton.TipoAsuntos
                        where n.IdDato == this.TipoAsunto
                        select n.Descripcion).ToList()[0] +
                       " " + NumAsunto + "/" + YearAsunto;
            }
        }
    }
}