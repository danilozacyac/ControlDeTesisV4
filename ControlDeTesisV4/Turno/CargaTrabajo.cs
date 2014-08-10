using System;
using System.Linq;

namespace ControlDeTesisV4.Turno
{
    public class CargaTrabajo
    {
        private string abogado;
        private string tipoDocto;
        private double total;

        public CargaTrabajo(string abogado, string tipoDocto, double total)
        {
            this.abogado = abogado;
            this.tipoDocto = tipoDocto;
            this.total = total;
        }
        public string Abogado
        {
            get
            {
                return this.abogado;
            }
            set
            {
                this.abogado = value;
            }
        }

        public string TipoDocto
        {
            get
            {
                return this.tipoDocto;
            }
            set
            {
                this.tipoDocto = value;
            }
        }

        public double Total
        {
            get
            {
                return this.total;
            }
            set
            {
                this.total = value;
            }
        }
    }
}
