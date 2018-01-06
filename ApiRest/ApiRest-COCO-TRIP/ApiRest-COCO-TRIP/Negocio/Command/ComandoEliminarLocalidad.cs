using System;
using System.Collections.Generic;
using ApiRest_COCO_TRIP.Datos.Entity;
using ApiRest_COCO_TRIP.Datos.Fabrica;
using ApiRest_COCO_TRIP.Datos.DAO;
using ApiRest_COCO_TRIP.Comun.Excepcion;
using NLog;

namespace ApiRest_COCO_TRIP.Negocio.Command
{
    /// <summary>
    /// Comando que permite Eliminar una Localidad.
    /// </summary>
    public class ComandoEliminarLocalidad : Comando
    {
        private Entidad localidad;
        private DAO dao;
        private static Logger log;

        /// <summary>
        /// Metodo Constructor.
        /// </summary>
        /// <param name="id">id de la localidad que se desea eliminar.</param>
        public ComandoEliminarLocalidad(int id)
        {
            localidad = FabricaEntidad.CrearEntidadLocalidad();
            localidad.Id = id;
            dao = FabricaDAO.CrearDAOLocalidad();
            log = LogManager.GetCurrentClassLogger();
        }

        /// <summary>
        /// Metodo Ejecutar, realiza la logica del negocio para eliminar una localidad.
        /// </summary>
        public override void Ejecutar()
        {
            try
            {
                dao.Eliminar(localidad);
                log.Info("Ejecutado el comando");
            }
            catch (BaseDeDatosExcepcion e)
            {
                log.Error(e.Message);
                throw e;
            }
            catch (CasteoInvalidoExcepcion e)
            {
                log.Error(e.Message);
                throw e;
            }
            catch (Exception e)
            {
                log.Error(e.Message);
                throw e;
            }
        }

        /// <summary>
        /// Metodo Retornar, obtiene una entidad como resultado de la ejecucion del comando.
        /// </summary>
        public override Entidad Retornar()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Metodo Retornar Lista, obtiene una lista de entidades como resultado de la ejecucion del comando.
        /// </summary>
        public override List<Entidad> RetornarLista()
        {
            throw new NotImplementedException();
        }
    }
}
