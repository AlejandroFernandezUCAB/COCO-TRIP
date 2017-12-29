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
  public class ComandoModificarEvento : Comando
  {
    private Entidad evento;
    private DAO dao;
        private Log log;

    public  ComandoModificarEvento(Entidad evento)
    {
      this.evento = evento;
      dao = FabricaDAO.CrearDAOEvento();
            log = Log.ObtenerInstancia();

    }

    public override void Ejecutar()
    {
      try {
        dao.Actualizar(evento);
                log.ApiRestInfo("ComandoModificarEvento","Ejecutado el comando");
      }
      catch (BaseDeDatosExcepcion e)
      {
        throw e;
        //INSERTAR EN LOG
      }
      catch (CasteoInvalidoExcepcion e)
      {
                log.ApiRestError("ComandoModificarEvento",e.Message);
        throw e;
        //INSERTAR EN LOG
      }
      catch (OperacionInvalidaException e)
      {
                log.ApiRestError("ComandoModificarEvento", e.Message);
                throw e;
        //INSERTAR EN LOG
      }
      catch (Exception e)
      {
                log.ApiRestError("ComandoModificarEvento", e.Message);
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
