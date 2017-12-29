using System.Collections.Generic;
using ApiRest_COCO_TRIP.Datos.Entity;
using ApiRest_COCO_TRIP.Datos.DAO;
using ApiRest_COCO_TRIP.Datos.Fabrica;
using System;

namespace ApiRest_COCO_TRIP.Negocio.Command
{
    public class ComandoEstadoCategoria : Comando
    {
        DAO dao = FabricaDAO.CrearDAOCategoria();
        private Entidad entidad = FabricaEntidad.CrearEntidadCategoria();

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="entidad">Entidad con la que se desea proceder</param>
        public ComandoEstadoCategoria(Entidad entidad)
        {
            this.entidad = entidad;
        }

        /// <summary>
        /// Ejecuta el comando al cual fue designado, actualizar estado de la categoria
        /// </summary>
        /// <exception cref="Exception"></exception>
        public override void Ejecutar()
        {
            try
            {
                ((DAOCategoria)dao).ActualizarEstado(entidad);
            }
            catch (Exception e)
            {
                //TERMINAR
                throw e;
            }
        }

        /// <summary>
        /// No implementado
        /// </summary>
        /// <returns>Entidad que fue utilizada en metodo ejecutar</returns>
        /// <exception cref="NotImplementedException"
        public override Entidad Retornar()
        {
            throw new System.NotImplementedException();
        }

        public override List<Entidad> RetornarLista()
        {
            throw new System.NotImplementedException();
        }


    }
}
