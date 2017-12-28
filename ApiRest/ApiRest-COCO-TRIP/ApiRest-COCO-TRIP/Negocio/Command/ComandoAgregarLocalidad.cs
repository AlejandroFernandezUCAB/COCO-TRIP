using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ApiRest_COCO_TRIP.Datos.Entity;
using Newtonsoft.Json.Linq;
using ApiRest_COCO_TRIP.Datos.DAO;
using ApiRest_COCO_TRIP.Datos.Fabrica;
using ApiRest_COCO_TRIP.Comun.Excepcion;

namespace ApiRest_COCO_TRIP.Negocio.Command
{
  public class ComandoAgregarLocalidad : Comando
  {
    private Entidad localidad;
    private DAO dao;

    public ComandoAgregarLocalidad(Entidad localidad) {
      this.localidad = (LocalidadEvento)localidad;
      dao = FabricaDAO.CrearDAOLocalidad();
    }
    public override void Ejecutar()
    {
      try
      {
        dao.Insertar(localidad);
      }
      catch (BaseDeDatosExcepcion e)
      {
        throw e;
      }
      catch (CasteoInvalidoExcepcion e)
      {
        throw e;
      }
      catch (Exception e)
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
