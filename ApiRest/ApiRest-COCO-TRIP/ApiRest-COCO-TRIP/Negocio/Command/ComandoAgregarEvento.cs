using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ApiRest_COCO_TRIP.Datos.Entity;
using ApiRest_COCO_TRIP.Datos.Fabrica;
using ApiRest_COCO_TRIP.Datos.DAO;
using ApiRest_COCO_TRIP.Comun.Excepcion;

namespace ApiRest_COCO_TRIP.Negocio.Command
{
  public class ComandoAgregarEvento : Comando
  {
    private Entidad evento;
    private DAO dao;

    public ComandoAgregarEvento(Entidad evento) {
      this.evento = (Evento)evento;
      dao = FabricaDAO.CrearDAOEvento();
    }
    public override void Ejecutar()
    {
      try
      {
        dao.Insertar(evento);
      }
      catch (BaseDeDatosExcepcion e)
      {
        throw e;
      }
      catch (CasteoInvalidoExcepcion e)
      {
        throw e;
      }
      catch (OperacionInvalidaException e)
      {
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
