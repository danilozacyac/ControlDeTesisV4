﻿using System;
using System.Collections.ObjectModel;
using System.Linq;
using ControlDeTesisV4.Dao;
using ControlDeTesisV4.Models;

namespace ControlDeTesisV4.Singletons
{
    public class OtrosDatosSingleton
    {
        private static ObservableCollection<OtrosDatos> tipoAsuntos;
        private static ObservableCollection<OtrosDatos> tipoJurisprudencia;
        private static ObservableCollection<OtrosDatos> areasEmisoras;
        private static ObservableCollection<OtrosDatos> areasEmisorasGrid;
        private static ObservableCollection<OtrosDatos> areasEmisorasPlenos;
        private static ObservableCollection<OtrosDatos> instancias;
        private static ObservableCollection<OtrosDatos> tipoVotos;

        private OtrosDatosSingleton()
        {
        }

        public static ObservableCollection<OtrosDatos> TipoAsuntos
        {
            get
            {
                if (tipoAsuntos == null)
                    tipoAsuntos = new OtrosDatosModel().GetTiposAsunto();

                return tipoAsuntos;
            }
        }

        public static ObservableCollection<OtrosDatos> TipoJurisprudencias
        {
            get
            {
                if (tipoJurisprudencia == null)
                    tipoJurisprudencia = new OtrosDatosModel().GetTiposJuris();

                return tipoJurisprudencia;
            }
        }

        public static ObservableCollection<OtrosDatos> AreasEmisorasGrid
        {
            get
            {
                if (areasEmisorasGrid == null)
                {
                    areasEmisorasGrid = AreasEmisoras;

                    areasEmisorasGrid = new ObservableCollection<OtrosDatos>(areasEmisorasGrid.Union(AreasEmisorasPlenos));
                }

                return areasEmisorasGrid;
            }
        }

        public static ObservableCollection<OtrosDatos> AreasEmisoras
        {
            get
            {
                if (areasEmisoras == null)
                    areasEmisoras = new OtrosDatosModel().GetAreasEmisoras();
                return areasEmisoras;
            }
        }

        public static ObservableCollection<OtrosDatos> AreasEmisorasPlenos
        {
            get
            {
                if (areasEmisorasPlenos == null)
                    areasEmisorasPlenos = new OtrosDatosModel().GetAreasEmisorasPlenos();

                return areasEmisorasPlenos;
            }
        }

        public static ObservableCollection<OtrosDatos> Instancias
        {
            get
            {
                if (instancias == null)
                    instancias = new OtrosDatosModel().GetInstancias();

                return instancias;
            }
        }

        public static ObservableCollection<OtrosDatos> TipoVotos
        {
            get
            {
                if (tipoVotos == null)
                    tipoVotos = new OtrosDatosModel().GetTipoDeVotos();

                return tipoVotos;
            }
        }
    }
}