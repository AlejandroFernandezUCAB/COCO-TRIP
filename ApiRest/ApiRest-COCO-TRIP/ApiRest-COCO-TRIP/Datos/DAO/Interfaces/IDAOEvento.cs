using ApiRest_COCO_TRIP.Datos.Entity;
using System.Collections.Generic;


namespace ApiRest_COCO_TRIP.Datos.DAO.Interfaces
{
    public interface IDAOEvento
    {

         void Insertar(Entidad objeto);

        /// <summary>
        /// Metodo Read, consulta mediante un Id.
        /// </summary>
        /// <param name="objeto">Instacia tipo Entidad con Id con el que se desea consultar.</param>
        /// <returns>Entidad asociada al Id colocado por parametro.</returns>
        Entidad ConsultarPorId(Entidad objeto);

        /// <summary>
        /// Metodo Read.
        /// </summary>
        /// <param name="objeto">Instacia tipo Entidad que se desea consultar.</param>
        /// <returns>Lista de Entidades referenciadas a la consulta</returns>
         List<Entidad> ConsultarLista(Entidad objeto);

        /// <summary>
        /// Metodo Update, actualiza una Entidad enviada por parametro.
        /// </summary>
        /// <param name="objeto">Instancia tipo Entidad que se desea actualizar/modificar</param>
        void Actualizar(Entidad objeto);

        /// <summary>
        /// Metodo Delete, elimina una Entidad enviada por parametro.
        /// </summary>
        /// <param name="objeto">Instancia tipo Entidad que se desea eliminar</param>
        void Eliminar(Entidad objeto);

        List<Entidad> ConsultarListaPorCategoria(Entidad objeto);

    }
}
