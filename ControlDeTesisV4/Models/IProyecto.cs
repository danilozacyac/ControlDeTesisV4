using System;
using System.Collections.ObjectModel;
using System.Linq;
using ControlDeTesisV4.Dao;

namespace ControlDeTesisV4.Models
{
    interface IProyecto
    {

        #region Proyectos

        /// <summary>
        /// Establece los datos generales del proyecto que se esta ingresando
        /// </summary>
        void SetNewProyecto();

        /// <summary>
        /// Actualiza la información de las tesis en proceso de publicación, los estados en los que se puede encontrar la tesis son
        /// los siguientes:
        /// 1. Recepcion
        /// 2. ENvio de Observaciones o envío del proyecto
        /// 3. Aprobación
        /// 4. Turno
        /// 5. Publicación
        /// </summary>
        /// <param name="tesis">Tesis que se va a actualizar</param>
        void UpdateProyectoTesis(ProyectosTesis tesis);

        /// <summary>
        /// Elimina los datos de recepción de un proyecto
        /// </summary>
        /// <param name="idProyecto">Identificador del Proyecto que se va a eliminar</param>
        void DeleteProyecto(int idProyecto);

        #endregion 

        void SetNewProyectoTesis(ObservableCollection<ProyectosTesis> listaProyectos);

        void SetTesisCompara(TesisCompara tesisCompara, int idTesis);



      

    }
}
