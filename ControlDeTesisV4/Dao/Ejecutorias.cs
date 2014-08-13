using System;
using System.Collections.ObjectModel;
using System.Linq;
using DocumentMgmtApi;

namespace ControlDeTesisV4.Dao
{
    public class Ejecutorias
    {
        int idEjecutoria;
        int idTesis;
        int forObservaciones;
        string oficioRecepcion;
        string provFilePathOrigen;
        string provFilePathConten;
        int provNumFojas;
        int numObservaciones;
        string obsFilePathOrigen;
        string obsFilePathConten;
        DateTime? fRecepcion;
        int fRecepcionInt;
        DateTime? fEnvioObs;
        int fEnvioObsInt;
        DateTime? fDevolucion;
        int fDevolucionInt;
        string ccFilePathOrigen;
        string ccFilePathConten;
        int ccNumFojas;
        string vpFilePathOrigen;
        string vpFilePathConten;
        int vpNumFojas;
        ObservableCollection<Observaciones> observaciones;
        ObservableCollection<Votos> votos;
        PrecedentesTesis precedente;
        int estadoEjecutoria;

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

        public int ForObservaciones
        {
            get
            {
                return this.forObservaciones;
            }
            set
            {
                this.forObservaciones = value;
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

        public string ProvFilePathOrigen
        {
            get
            {
                return this.provFilePathOrigen;
            }
            set
            {
                this.provFilePathOrigen = value;
            }
        }

        public string ProvFilePathConten
        {
            get
            {
                return this.provFilePathConten;
            }
            set
            {
                this.provFilePathConten = value;
            }
        }

        public int ProvNumFojas
        {
            get
            {
                return this.provNumFojas;
            }
            set
            {
                this.provNumFojas = value;
            }
        }

        public int NumObservaciones
        {
            get
            {
                return this.numObservaciones;
            }
            set
            {
                this.numObservaciones = value;
            }
        }

        public string ObsFilePathOrigen
        {
            get
            {
                return this.obsFilePathOrigen;
            }
            set
            {
                this.obsFilePathOrigen = value;
            }
        }

        public string ObsFilePathConten
        {
            get
            {
                return this.obsFilePathConten;
            }
            set
            {
                this.obsFilePathConten = value;
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

        public int FRecepcionInt
        {
            get
            {
                return this.fRecepcionInt;
            }
            set
            {
                this.fRecepcionInt = value;
            }
        }

        public DateTime? FEnvioObs
        {
            get
            {
                return this.fEnvioObs;
            }
            set
            {
                this.fEnvioObs = value;
            }
        }

        public int FEnvioObsInt
        {
            get
            {
                return this.fEnvioObsInt;
            }
            set
            {
                this.fEnvioObsInt = value;
            }
        }

        public DateTime? FDevolucion
        {
            get
            {
                return this.fDevolucion;
            }
            set
            {
                this.fDevolucion = value;
            }
        }

        public int FDevolucionInt
        {
            get
            {
                return this.fDevolucionInt;
            }
            set
            {
                this.fDevolucionInt = value;
            }
        }

        public string CcFilePathOrigen
        {
            get
            {
                return this.ccFilePathOrigen;
            }
            set
            {
                this.ccFilePathOrigen = value;
            }
        }

        public string CcFilePathConten
        {
            get
            {
                return this.ccFilePathConten;
            }
            set
            {
                this.ccFilePathConten = value;
            }
        }

        public int CcNumFojas
        {
            get
            {
                return this.ccNumFojas;
            }
            set
            {
                this.ccNumFojas = value;
            }
        }

        public string VpFilePathOrigen
        {
            get
            {
                return this.vpFilePathOrigen;
            }
            set
            {
                this.vpFilePathOrigen = value;
            }
        }

        public string VpFilePathConten
        {
            get
            {
                return this.vpFilePathConten;
            }
            set
            {
                this.vpFilePathConten = value;
            }
        }

        public int VpNumFojas
        {
            get
            {
                return this.vpNumFojas;
            }
            set
            {
                this.vpNumFojas = value;
            }
        }

        public ObservableCollection<Observaciones> Observaciones
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

        public ObservableCollection<Votos> Votos
        {
            get
            {
                return this.votos;
            }
            set
            {
                this.votos = value;
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

        public int EstadoEjecutoria
        {
            get
            {
                return this.estadoEjecutoria;
            }
            set
            {
                this.estadoEjecutoria = value;
            }
        }
    }
}