using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ApiRest_COCO_TRIP.Datos.Entity;
using ApiRest_COCO_TRIP.Datos.DAO;
using ApiRest_COCO_TRIP.Datos.Fabrica;
using ApiRest_COCO_TRIP.Comun.Excepcion;
using ApiRest_COCO_TRIP.Datos.Singleton;
using NLog;

namespace ApiRest_COCO_TRIP.Negocio.Command
{
  public class ComandoConsultarEvento : Comando
  {
    private Entidad evento;
    private DAO dao;
        private static Logger log;

        public ComandoConsultarEvento(int id) {
      this.evento = FabricaEntidad.CrearEntidadEvento();
      this.evento.Id = id;
      dao = FabricaDAO.CrearDAOEvento();
            log = LogManager.GetCurrentClassLogger();
        }

    public override void Ejecutar()
    {
      try
      {
        evento=dao.ConsultarPorId(evento);
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

    public override Entidad Retornar()
    {
      return evento;
    }

    public override List<Entidad> RetornarLista()
    {
      throw new NotImplementedException();
    }
  }
}
