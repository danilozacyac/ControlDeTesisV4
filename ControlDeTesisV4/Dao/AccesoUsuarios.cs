using System;
using System.Collections.Generic;
using System.Linq;

namespace ControlDeTesisV4.Dao
{
    public static class AccesoUsuarios
    {
        private static string nombre;

        
        public static string Nombre
        {
            get
            {
                return nombre;
            }
            set
            {
                nombre = value;
            }
        }

        private static string usuario;

        public static string Usuario
        {
            get
            {
                return usuario;
            }
            set
            {
                usuario = value;
            }
        }

        private static string pwd;

        public static string Pwd
        {
            get
            {
                return pwd;
            }
            set
            {
                pwd = value;
            }
        }

        private static int llave;

        public static int Llave
        {
            get
            {
                return llave;
            }
            set
            {
                llave = value;
            }
        }

        private static int perfil;

        public static int Perfil
        {
            get
            {
                return perfil;
            }
            set
            {
                perfil = value;
            }
        }

        private static List<int> secciones;

        public static List<int> Secciones
        {
            get
            {
                return secciones;
            }
            set
            {
                secciones = value;
            }
        }

    }
}
