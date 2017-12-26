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
  public class ComandoConsultarEventosPorCategoria : Comando
  {
    private Entidad categoria;
    private DAO dao;
    private List<Entidad> eventos;

    public ComandoConsultarEventosPorCategoria(int id)
    {
      this.categoria = FabricaEntidad.CrearEntidadCategoria();
      this.categoria.Id = id;
      dao = FabricaDAO.CrearDAOEvento();
    }

    public override void Ejecutar()
    {
      try
      {
        eventos = dao.ConsultarLista(categoria);
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
      return eventos;
    }
  }
}