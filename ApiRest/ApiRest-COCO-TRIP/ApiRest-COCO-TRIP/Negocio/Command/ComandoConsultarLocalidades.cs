using System;
using System.Collections.Generic;
using ApiRest_COCO_TRIP.Datos.Entity;
using ApiRest_COCO_TRIP.Datos.DAO;
using ApiRest_COCO_TRIP.Datos.Fabrica;
using ApiRest_COCO_TRIP.Comun.Excepcion;
using NLog;

namespace ApiRest_COCO_TRIP.Negocio.Command
{
    /// <summary>
    /// Comando que permite consultar todas las localidades.
    /// </summary>
    public class ComandoConsultarLocalidades : Comando
    {
        private List<Entidad> localidades;
        private DAO dao;
        private static Logger log;

        /// <summary>
        /// Metodo Constructor.
        /// </summary>
        public ComandoConsultarLocalidades()
        {
            dao = FabricaDAO.CrearDAOLocalidad();
            log = LogManager.GetCurrentClassLogger();
        }

        /// <summary>
        /// Metodo Ejecutar, realiza la logica del negocio para consultar las localidades.
        /// </summary>
        public override void Ejecutar()
        {
            try
            {
                localidades = dao.ConsultarLista(null);
                log.Info("Comando Ejecutado");

            }
            catch (BaseDeDatosExcepcion e)
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
            return localidades;
        }
    }
}
