using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlDeTesisV4.Dao
{
    public class ProyectosCcst 
    {
        private int idProyecto;
        private readonly int idTipoProyecto = 2;
        private string destinatario;
        private string oficioAtn;
        private DateTime? fOficioAtn;
        private int fOficioAtnInt;
        private string fileOficioAtnOrigen;
        private string fileOficioAtnConten;
        private ObservableCollection<ProyectosTesis> proyectos;
        

        

        

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

        public string Destinatario
        {
            get
            {
                return this.destinatario;
            }
            set
            {
                this.destinatario = value;
            }
        }

        public string OficioAtn
        {
            get
            {
                return this.oficioAtn;
            }
            set
            {
                this.oficioAtn = value;
            }
        }

        public DateTime? FOficioAtn
        {
            get
            {
                return this.fOficioAtn;
            }
            set
            {
                this.fOficioAtn = value;
            }
        }

        public int FOficioAtnInt
        {
            get
            {
                return this.fOficioAtnInt;
            }
            set
            {
                this.fOficioAtnInt = value;
            }
        }

        public string FileOficioAtnOrigen
        {
            get
            {
                return this.fileOficioAtnOrigen;
            }
            set
            {
                this.fileOficioAtnOrigen = value;
            }
        }

        public string FileOficioAtnConten
        {
            get
            {
                return this.fileOficioAtnConten;
            }
            set
            {
                this.fileOficioAtnConten = value;
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
            }
        }
        
    }
}
