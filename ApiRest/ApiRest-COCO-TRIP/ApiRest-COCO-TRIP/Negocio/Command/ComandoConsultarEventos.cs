using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ApiRest_COCO_TRIP.Datos.Entity;
using ApiRest_COCO_TRIP.Datos.DAO.Interfaces;
using ApiRest_COCO_TRIP.Datos.DAO;
using NLog;
using ApiRest_COCO_TRIP.Datos.Fabrica;
using ApiRest_COCO_TRIP.Comun.Excepcion;

namespace ApiRest_COCO_TRIP.Negocio.Command
{
    public class ComandoConsultarEventos : Comando
    {
        private IDAOEvento daoEvento;
        private List<Entidad> eventos;
        private static Logger log;

        /// <summary>
        /// Metodo Constructor.
        /// </summary>
        public ComandoConsultarEventos()
        {
            daoEvento = FabricaDAO.CrearDAOEvento();
            log = LogManager.GetCurrentClassLogger();
        }

        /// <summary>
        /// Metodo Ejecutar, realiza la logica del negocio para consultar todos los eventos.
        /// </summary>
        public override void Ejecutar()
        {
            try
            {
                eventos = daoEvento.ConsultarLista(null);
                log.Info("Ejecutado el comando");
            }

            catch (BaseDeDatosExcepcion e)
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
            return eventos;
        }
    }
}