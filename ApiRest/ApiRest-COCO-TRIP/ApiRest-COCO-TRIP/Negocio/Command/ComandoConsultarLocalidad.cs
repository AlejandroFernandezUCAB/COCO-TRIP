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
  public class ComandoConsultarLocalidad : Comando
  {
    private Entidad localidad;
    private DAO dao;
        private static Logger log;

        public ComandoConsultarLocalidad(int id) {
      localidad = FabricaEntidad.CrearEntidadLocalidad();
      localidad.Id = id;
      dao = FabricaDAO.CrearDAOLocalidad();
            log = LogManager.GetCurrentClassLogger();
        }

    public override void Ejecutar()
    {
      try
      {
        localidad = dao.ConsultarPorId(localidad);
                log.Info("Ejecutado el comando ");
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
      return localidad;
    }

    public override List<Entidad> RetornarLista()
    {
      throw new NotImplementedException();
    }
  }
}
