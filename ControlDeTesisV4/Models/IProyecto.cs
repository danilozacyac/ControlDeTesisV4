using System;
using System.Collections.ObjectModel;
using System.Linq;
using ControlDeTesisV4.Dao;

namespace ControlDeTesisV4.Models
{
    interface IProyecto
    {
        /// <summary>
        /// Establece los datos generales del proyecto que se esta ingresando
        /// </summary>
        void SetNewProyecto();


        void SetNewProyectoTesis(ObservableCollection<ProyectosTesis> listaProyectos);

        void SetTesisCompara(TesisCompara tesisCompara, int idTesis);



      

    }
}
