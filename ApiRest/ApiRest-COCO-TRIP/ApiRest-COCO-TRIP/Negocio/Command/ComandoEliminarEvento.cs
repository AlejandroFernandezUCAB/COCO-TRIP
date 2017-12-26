using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ApiRest_COCO_TRIP.Datos.Entity;
using ApiRest_COCO_TRIP.Datos.DAO;
using ApiRest_COCO_TRIP.Datos.Fabrica;
using ApiRest_COCO_TRIP.Comun.Excepcion;

namespace ApiRest_COCO_TRIP.Negocio.Command
{
  public class ComandoEliminarEvento : Comando
  {
    private Entidad evento;
    private DAO dao;

    public ComandoEliminarEvento(int id)
    {
      this.evento = FabricaEntidad.CrearEntidadEvento();
      this.evento.Id=id;
      dao = FabricaDAO.CrearDAOEvento();
    }

    public override void Ejecutar()
    {
      try
      {
        dao.Eliminar(evento);
      }
      catch (BaseDeDatosExcepcion e)
      {
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
        throw e;
        //INSERTAR EN LOG
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
