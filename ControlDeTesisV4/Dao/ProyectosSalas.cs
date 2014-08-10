using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace ControlDeTesisV4.Dao
{
    public class ProyectosSalas : INotifyPropertyChanged
    {
        private int idProyecto;
        private readonly int idTipoProyecto = 1;
        private string referencia;
        private DateTime? fRecepcion;
        private int fRecepcionInt;
        private string oficioRecepcion;
        private int idEmisor;
        private int idSignatario;
        private string ofRecepcionPathOrigen;
        private string ofRecepcionPathConten;
        private int ejecutoria;
        private DateTime? fRecepcionPrograma;
        private int fRecepcionProgramaInt;
        private DateTime? fTentSesion;
        private int fTentSesionInt;
        private ObservableCollection<ProyectosTesis> proyectos;
        private int totalProyectos = 0;
        

        
        

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

        public int IdTipoProyecto
        {
            get
            {
                return this.idTipoProyecto;
            }
        }

        public string Referencia
        {
            get
            {
                return this.referencia;
            }
            set
            {
                this.referencia = value;
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

        public int IdEmisor
        {
            get
            {
                return this.idEmisor;
            }
            set
            {
                this.idEmisor = value;
            }
        }

        public int IdSignatario
        {
            get
            {
                return this.idSignatario;
            }
            set
            {
                this.idSignatario = value;
            }
        }

        public string OfRecepcionPathOrigen
        {
            get
            {
                return this.ofRecepcionPathOrigen;
            }
            set
            {
                this.ofRecepcionPathOrigen = value;
            }
        }

        public string OfRecepcionPathConten
        {
            get
            {
                return this.ofRecepcionPathConten;
            }
            set
            {
                this.ofRecepcionPathConten = value;
            }
        }

        public int Ejecutoria
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

        public DateTime? FRecepcionPrograma
        {
            get
            {
                return this.fRecepcionPrograma;
            }
            set
            {
                this.fRecepcionPrograma = value;
            }
        }

        public int FRecepcionProgramaInt
        {
            get
            {
                return this.fRecepcionProgramaInt;
            }
            set
            {
                this.fRecepcionProgramaInt = value;
            }
        }

        public DateTime? FTentSesion
        {
            get
            {
                return this.fTentSesion;
            }
            set
            {
                this.fTentSesion = value;
            }
        }

        public int FTentSesionInt
        {
            get
            {
                return this.fTentSesionInt;
            }
            set
            {
                this.fTentSesionInt = value;
            }
        }

        public ObservableCollection<ProyectosTesis> Proyectos
        {
            get
            {
                return this.proyectos;
            }
            set
            {
                this.proyectos = value;
                this.OnPropertyChanged("Proyectos");
            }
        }

        public int TotalProyectos
        {
            get
            {
                return this.totalProyectos;
            }
            set
            {
                this.totalProyectos = value;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}
