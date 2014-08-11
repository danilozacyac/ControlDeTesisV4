using System;
using System.Linq;

namespace DocumentMgmtApi
{
    public class Observaciones
    {
        private int idObservacion;
        private int idEjecutoria;
        private string foja;
        private string parrafo;
        private string renglon;
        private string dice;
        private string sugiere;
        private int isAcepted;

        
        public int IdObservacion
        {
            get
            {
                return this.idObservacion;
            }
            set
            {
                this.idObservacion = value;
            }
        }

        public int IdEjecutoria
        {
            get
            {
                return this.idEjecutoria;
            }
            set
            {
                this.idEjecutoria = value;
            }
        }

        public string Foja
        {
            get
            {
                return this.foja;
            }
            set
            {
                this.foja = value;
            }
        }

        public string Parrafo
        {
            get
            {
                return this.parrafo;
            }
            set
            {
                this.parrafo = value;
            }
        }

        public string Renglon
        {
            get
            {
                return this.renglon;
            }
            set
            {
                this.renglon = value;
            }
        }

        public string Dice
        {
            get
            {
                return this.dice;
            }
            set
            {
                this.dice = value;
            }
        }

        public string Sugiere
        {
            get
            {
                return this.sugiere;
            }
            set
            {
                this.sugiere = value;
            }
        }

        public int IsAcepted
        {
            get
            {
                return this.isAcepted;
            }
            set
            {
                this.isAcepted = value;
            }
        }

    }
}
