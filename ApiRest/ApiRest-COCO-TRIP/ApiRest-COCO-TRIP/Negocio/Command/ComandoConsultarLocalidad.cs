using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ApiRest_COCO_TRIP.Datos.Entity;
using ApiRest_COCO_TRIP.Datos.Fabrica;
using ApiRest_COCO_TRIP.Datos.DAO;
using ApiRest_COCO_TRIP.Comun.Excepcion;
using ApiRest_COCO_TRIP.Datos.Singleton;

namespace ApiRest_COCO_TRIP.Negocio.Command
{
  public class ComandoConsultarLocalidad : Comando
  {
    private Entidad localidad;
    private DAO dao;
    private Log log;

    public ComandoConsultarLocalidad(int id) {
      localidad = FabricaEntidad.CrearEntidadLocalidad();
      localidad.Id = id;
      dao = FabricaDAO.CrearDAOLocalidad();
            log = Log.ObtenerInstancia();
    }

    public override void Ejecutar()
    {
      try
      {
        localidad = dao.ConsultarPorId(localidad);
                log.ApiRestInfo("ComandoConsultarLocalidad","Ejecutado el comando ");
      }
      catch (BaseDeDatosExcepcion e)
      {
                log.ApiRestError("ComandoConsultarLocalidad",e.Message);
                throw e;
        //INSERTAR EN LOG
      }
      catch (CasteoInvalidoExcepcion e)
      {
        throw e;
        //INSERTAR EN LOG
      }

      catch (OperacionInvalidaException e)
      {
                log.ApiRestError("ComandoConsultarLocalidad", e.Message);
                throw e;
        //INSERTAR EN LOG
      }
      catch (Exception e)
      {
                log.ApiRestError("ComandoConsultarLocalidad", e.Message);
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
