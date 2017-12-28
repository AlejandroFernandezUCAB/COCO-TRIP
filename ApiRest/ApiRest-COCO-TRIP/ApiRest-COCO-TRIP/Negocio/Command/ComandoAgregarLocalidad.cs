using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ApiRest_COCO_TRIP.Datos.Entity;
using Newtonsoft.Json.Linq;
using ApiRest_COCO_TRIP.Datos.DAO;
using ApiRest_COCO_TRIP.Datos.Fabrica;
using ApiRest_COCO_TRIP.Comun.Excepcion;
using ApiRest_COCO_TRIP.Datos.Singleton;

namespace ApiRest_COCO_TRIP.Negocio.Command
{
  public class ComandoAgregarLocalidad : Comando
  {
    private Entidad localidad;
    private DAO dao;
    private Log log;
    public ComandoAgregarLocalidad(Entidad localidad) {
      this.localidad = (LocalidadEvento)localidad;
      dao = FabricaDAO.CrearDAOLocalidad();
            log = Log.ObtenerInstancia(); 
    }
    public override void Ejecutar()
    {
      try
      {
        dao.Insertar(localidad);
                log.ApiRestInfo("ComandoAgregarLocalidad","Ejecutado el comando");
      }
      catch (BaseDeDatosExcepcion e)
      {
                log.ApiRestError("ComandoAgregarLocalidad",e.Message);
                throw e;
      }
      catch (CasteoInvalidoExcepcion e)
      {
                log.ApiRestError("ComandoAgregarLocalidad", e.Message);
                throw e;
      }
      catch (Exception e)
      {
                log.ApiRestError("ComandoAgregarLocalidad", e.Message);
                throw e;
      }

    }

    public override Entidad Retornar()
    {
      throw new NotImplementedException();
    }

    public override List<Entidad> RetornarLista()
    {
      throw new NotImplementedException();
    }
  }
}
