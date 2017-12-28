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
  public class ComandoEliminarEvento : Comando
  {
        private Entidad evento;
        private DAO dao;
        private Log log;

    public ComandoEliminarEvento(int id)
    {
      this.evento = FabricaEntidad.CrearEntidadEvento();
      this.evento.Id=id;
      dao = FabricaDAO.CrearDAOEvento();
            log = Log.ObtenerInstancia();
    }

    public override void Ejecutar()
    {
      try
      {
        
        dao.Eliminar(evento);
                log.ApiRestInfo("ComandoEliminarEvento","Ejecutado el Comando");
      }
      catch (BaseDeDatosExcepcion e)
      {
                log.ApiRestError("ComandoEliminarEvento", e.Message);
                throw e;
        //INSERTAR EN LOG
      }
      catch (CasteoInvalidoExcepcion e)
      {
                log.ApiRestError("ComandoEliminarEvento", e.Message);
                throw e;
        //INSERTAR EN LOG
      }
      catch (OperacionInvalidaException e)
      {
                log.ApiRestError("ComandoEliminarEvento", e.Message);
                throw e;
        //INSERTAR EN LOG
      }
      catch (Exception e)
      {
                log.ApiRestError("ComandoEliminarEvento", e.Message);
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
