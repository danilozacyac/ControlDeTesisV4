using System;
using System.Linq;

namespace ControlDeTesisV4.Dao
{
    public class TesisCompara
    {
        private int idRegistro;
        private int idTesis;
        private string textoOriginal;
        private string tOPlano;
        private string tOrigenAlfab;
        private string toFilePathOrigen;
        private string toFilePathConten;
        private string tObservaciones;
        private string tObservacionesPlano;
        private string tObsFilePathOrigen;
        private string tObsFilePathConten;
        private string tAprobada;
        private string tAprobadaPlano;
        private string tAprobFilePathOrigen;
        private string tAprobFilePathConten;

        public int IdRegistro
        {
            get
            {
                return this.idRegistro;
            }
            set
            {
                this.idRegistro = value;
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

        public string TextoOriginal
        {
            get
            {
                return this.textoOriginal;
            }
            set
            {
                this.textoOriginal = value;
            }
        }

        public string TOPlano
        {
            get
            {
                return this.tOPlano;
            }
            set
            {
                this.tOPlano = value;
            }
        }

        public string TOrigenAlfab
        {
            get
            {
                return this.tOrigenAlfab;
            }
            set
            {
                this.tOrigenAlfab = value;
            }
        }

        public string ToFilePathOrigen
        {
            get
            {
                return this.toFilePathOrigen;
            }
            set
            {
                this.toFilePathOrigen = value;
            }
        }

        public string ToFilePathConten
        {
            get
            {
                return this.toFilePathConten;
            }
            set
            {
                this.toFilePathConten = value;
            }
        }

        public string TObservaciones
        {
            get
            {
                return this.tObservaciones;
            }
            set
            {
                this.tObservaciones = value;
            }
        }

        public string TObservacionesPlano
        {
            get
            {
                return this.tObservacionesPlano;
            }
            set
            {
                this.tObservacionesPlano = value;
            }
        }

        public string TObsFilePathOrigen
        {
            get
            {
                return this.tObsFilePathOrigen;
            }
            set
            {
                this.tObsFilePathOrigen = value;
            }
        }

        public string TObsFilePathConten
        {
            get
            {
                return this.tObsFilePathConten;
            }
            set
            {
                this.tObsFilePathConten = value;
            }
        }

        public string TAprobada
        {
            get
            {
                return this.tAprobada;
            }
            set
            {
                this.tAprobada = value;
            }
        }

        public string TAprobadaPlano
        {
            get
            {
                return this.tAprobadaPlano;
            }
            set
            {
                this.tAprobadaPlano = value;
            }
        }

        public string TAprobFilePathOrigen
        {
            get
            {
                return this.tAprobFilePathOrigen;
            }
            set
            {
                this.tAprobFilePathOrigen = value;
            }
        }

        public string TAprobFilePathConten
        {
            get
            {
                return this.tAprobFilePathConten;
            }
            set
            {
                this.tAprobFilePathConten = value;
            }
        }
    }
}
