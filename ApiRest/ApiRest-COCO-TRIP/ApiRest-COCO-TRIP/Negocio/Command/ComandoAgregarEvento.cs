using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ApiRest_COCO_TRIP.Datos.Entity;
using ApiRest_COCO_TRIP.Datos.Fabrica;
using ApiRest_COCO_TRIP.Datos.DAO;
using ApiRest_COCO_TRIP.Comun.Excepcion;
using ApiRest_COCO_TRIP.Datos.Singleton;
using NLog;

namespace ApiRest_COCO_TRIP.Negocio.Command
{
    /// <summary>
    /// Comando que permite agregar un evento.
    /// </summary>
    public class ComandoAgregarEvento : Comando
    {
        private Entidad evento;
        private DAO dao;
        private static Logger log;

        /// <summary>
        /// Metodo Constructor.
        /// </summary>
        /// <param name="localidad">Entidad del evento a agregar</param>
        public ComandoAgregarEvento(Entidad evento)
        {
            this.evento = (Evento)evento;
            dao = FabricaDAO.CrearDAOEvento();
            log = LogManager.GetCurrentClassLogger();
        }

        /// <summary>
        /// Metodo Ejecutar, realiza la logica del negocio para agregar un evento.
        /// </summary>
        public override void Ejecutar()
        {
            try
            {
                dao.Insertar(evento);
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
            catch (OperacionInvalidaExcepcion e)
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
