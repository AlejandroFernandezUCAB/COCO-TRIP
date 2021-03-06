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
    /// Comando que permite modificar un evento.
    /// </summary>
    public class ComandoModificarEvento : Comando
    {
        private Entidad evento;
        private DAO dao;
        private static Logger log;

        /// <summary>
        /// Metodo Constructor.
        /// </summary>
        /// <param name="evento">instacia evento que se desea modificar.</param>
        public ComandoModificarEvento(Entidad evento)
        {
            this.evento = evento;
            dao = FabricaDAO.CrearDAOEvento();
            log = LogManager.GetCurrentClassLogger();

        }

        /// <summary>
        /// Metodo Ejecutar, realiza la logica del negocio para modificar un evento.
        /// </summary>
        public override void Ejecutar()
        {
            try
            {
                dao.Actualizar(evento);
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
        /// Metodo Retornar Lista, obtiene una lista de entidade como resultado de la ejecucion del comando.
        /// </summary>
        public override List<Entidad> RetornarLista()
        {
            throw new NotImplementedException();
        }
    }
}
