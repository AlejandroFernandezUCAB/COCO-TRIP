using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ApiRest_COCO_TRIP.Datos.Entity;
using ApiRest_COCO_TRIP.Datos.DAO;
using ApiRest_COCO_TRIP.Datos.Fabrica;
using ApiRest_COCO_TRIP.Comun.Excepcion;
using ApiRest_COCO_TRIP.Datos.Singleton;

namespace ApiRest_COCO_TRIP.Negocio.Command
{
  public class ComandoConsultarLocalidades : Comando
  {
    private List<Entidad> localidades;
    private DAO dao;
        private Log log;

    public ComandoConsultarLocalidades() {
      dao = FabricaDAO.CrearDAOLocalidad();
            log = Log.ObtenerInstancia();
    }

    public override void Ejecutar()
    {
      try
      {
        localidades = dao.ConsultarLista(null);
                log.ApiRestInfo("ComandoConsultarLocalidades","Ejecutado el comando");

      }
      catch (BaseDeDatosExcepcion e)
      {
                log.ApiRestError("ComandoConsultarLocalidades", e.Message);
                throw e;
      }
      catch (Exception e)
      {
                log.ApiRestError("ComandoConsultarLocalidades", e.Message);
                throw e;
      }
    }

    public override Entidad Retornar()
    {
      throw new NotImplementedException();
    }

    public override List<Entidad> RetornarLista()
    {
      return localidades;
    }
  }
}
