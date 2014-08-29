using System;
using System.ComponentModel;
using System.Linq;

namespace ControlDeTesisV4.Dao
{
    public class TurnoDao :  INotifyPropertyChanged
    {
        private int idTurno;
        private int idAbogado;
        private int idTipoDocto;
        private int idDocto;
        private int numPaginas;
        private DateTime? fTurno;
        private DateTime? fSugerida;
        private DateTime? fEntrega;
        private int enTiempo;
        private int diasAtraso;
        private int isReturn;
        private string observaciones;

        public int IdTurno
        {
            get
            {
                return this.idTurno;
            }
            set
            {
                this.idTurno = value;
            }
        }

        public int IdAbogado
        {
            get
            {
                return this.idAbogado;
            }
            set
            {
                this.idAbogado = value;
                this.OnPropertyChanged("IdAbogado");
            }
        }

        public int IdTipoDocto
        {
            get
            {
                return this.idTipoDocto;
            }
            set
            {
                this.idTipoDocto = value;
            }
        }

        public int IdDocto
        {
            get
            {
                return this.idDocto;
            }
            set
            {
                this.idDocto = value;
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

        public int EnTiempo
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

        public int IsReturn
        {
            get
            {
                return this.isReturn;
            }
            set
            {
                this.isReturn = value;
            }
        }

        public string Observaciones
        {
            get
            {
                return this.observaciones;
            }
            set
            {
                this.observaciones = value;
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
