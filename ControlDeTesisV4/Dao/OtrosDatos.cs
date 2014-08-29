using System;
using System.Linq;

namespace ControlDeTesisV4.Dao
{
    public class OtrosDatos
    {
        private int idDato;
        private int idAuxiliar;
        private string descripcion;

        public OtrosDatos(int idDato, int idAuxiliar, string descripcion)
        {
            this.idDato = idDato;
            this.idAuxiliar = idAuxiliar;
            this.descripcion = descripcion;
        }

        public OtrosDatos(int idDato, string descripcion)
        {
            this.idDato = idDato;
            this.descripcion = descripcion;
        }

        public OtrosDatos(string descripcion)
        {
            this.descripcion = descripcion;
        }

        
        public int IdDato
        {
            get
            {
                return this.idDato;
            }
            set
            {
                this.idDato = value;
            }
        }
        
        public int IdAuxiliar
        {
            get
            {
                return this.idAuxiliar;
            }
            set
            {
                this.idAuxiliar = value;
            }
        }

        public string Descripcion
        {
            get
            {
                return this.descripcion;
            }
            set
            {
                this.descripcion = value;
            }
        }
    }
}
