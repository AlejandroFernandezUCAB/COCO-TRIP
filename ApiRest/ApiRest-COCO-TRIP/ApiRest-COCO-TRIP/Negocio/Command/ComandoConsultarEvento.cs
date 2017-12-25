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
  public class ComandoConsultarEvento : Comando
  {
    private Entidad evento;
    private DAO dao;

    public ComandoConsultarEvento(int id) {
      this.evento = FabricaEntidad.CrearEntidadEvento();
      this.evento.Id = id;
      dao = FabricaDAO.CrearDAOEvento();
    }

    public override void Ejecutar()
    {
      try
      {
        evento=dao.ConsultarPorId(evento);
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
      return evento;
    }

    public override List<Entidad> RetornarLista()
    {
      throw new NotImplementedException();
    }
  }
}
