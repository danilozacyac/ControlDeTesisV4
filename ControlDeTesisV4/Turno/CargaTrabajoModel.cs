using System;
using System.Collections.ObjectModel;
using System.Linq;
using ControlDeTesisV4.Models;
using Telerik.Windows.Controls;

namespace ControlDeTesisV4.Turno
{
    public class CargaTrabajoModel : ViewModelBase
    {
        private ObservableCollection<CargaTrabajo> aisladas;
        private ObservableCollection<CargaTrabajo> jurisprudencias;
        private ObservableCollection<CargaTrabajo> ejecutorias;
        private ObservableCollection<CargaTrabajo> votos;

        public CargaTrabajoModel() { }


        public ObservableCollection<CargaTrabajo> Aisladas
        {
            get
            {
                if (this.aisladas == null)
                {
                    this.aisladas = CargasDeTrabajoModel.GetCargaPorTipoDocto(1, "Aisladas");
                }

                return this.aisladas;
            }
        }

        public ObservableCollection<CargaTrabajo> Jurisprudencias
        {
            get
            {
                if (this.jurisprudencias == null)
                {
                    this.jurisprudencias = CargasDeTrabajoModel.GetCargaPorTipoDocto(2, "Jurisprudencias");
                }

                return this.jurisprudencias;
            }
        }

        public ObservableCollection<CargaTrabajo> Ejecutorias
        {
            get
            {
                if (this.ejecutorias == null)
                {
                    this.ejecutorias = CargasDeTrabajoModel.GetCargaPorTipoDocto(3, "Ejecutorias");
                }

                return this.ejecutorias;
            }
        }

        public ObservableCollection<CargaTrabajo> Votos
        {
            get
            {
                if (this.votos == null)
                {
                    this.votos = CargasDeTrabajoModel.GetCargaPorTipoDocto(4, "Votos");
                }

                return this.votos;
            }
        }
    }
}
