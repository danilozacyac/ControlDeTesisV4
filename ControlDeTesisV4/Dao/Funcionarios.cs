using System;
using System.Linq;

namespace ControlDeTesisV4.Dao
{
    public class Funcionarios
    {
        int idFuncionario;
        string paterno;
        string materno;
        string nombre;
        string nombreCompleto;
        int nivel;
        int perfil;

        public Funcionarios(int idFuncionario, string paterno, string materno, string nombre, string nombreCompleto, int nivel, int perfil)
        {
            this.idFuncionario = idFuncionario;
            this.paterno = paterno;
            this.materno = materno;
            this.nombre = nombre;
            this.nombreCompleto = nombreCompleto;
            this.nivel = nivel;
            this.perfil = perfil;
        }

        public Funcionarios()
        {
        }

        public Funcionarios(int idFuncionario, string paterno, string materno, string nombre, string nombreCompleto)
        {
            this.idFuncionario = idFuncionario;
            this.paterno = paterno;
            this.materno = materno;
            this.nombre = nombre;
            this.nombreCompleto = nombreCompleto;
        }

        

        public int IdFuncionario
        {
            get
            {
                return this.idFuncionario;
            }
            set
            {
                this.idFuncionario = value;
            }
        }

        public string Paterno
        {
            get
            {
                return this.paterno;
            }
            set
            {
                this.paterno = value;
            }
        }

        public string Materno
        {
            get
            {
                return this.materno;
            }
            set
            {
                this.materno = value;
            }
        }

        public string Nombre
        {
            get
            {
                return this.nombre;
            }
            set
            {
                this.nombre = value;
            }
        }

        public string NombreCompleto
        {
            get
            {
                return this.nombreCompleto;
            }
            set
            {
                this.nombreCompleto = value;
            }
        }

        public int Nivel
        {
            get
            {
                return this.nivel;
            }
            set
            {
                this.nivel = value;
            }
        }

        public int Perfil
        {
            get
            {
                return this.perfil;
            }
            set
            {
                this.perfil = value;
            }
        }
    }
}
