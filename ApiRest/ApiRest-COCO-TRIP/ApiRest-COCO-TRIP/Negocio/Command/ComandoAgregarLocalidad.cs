using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ApiRest_COCO_TRIP.Datos.Entity;
using Newtonsoft.Json.Linq;
using ApiRest_COCO_TRIP.Datos.DAO;
using ApiRest_COCO_TRIP.Datos.Fabrica;
using ApiRest_COCO_TRIP.Models.Excepcion;

namespace ApiRest_COCO_TRIP.Negocio.Command
{
  public class ComandoAgregarLocalidad : Comando
  {
    private Entidad localidad;
    private String mensaje=null;

    public ComandoAgregarLocalidad(Entidad localidad) {
      this.localidad = (LocalidadEvento)localidad;
    }
    public override void Ejecutar()
    {
      DAO dao = FabricaDAO.CrearDAOLocalidad();
      try
      {
        validarEntidad();
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

    }

    public override Entidad Retornar()
    {
      throw new NotImplementedException();
    }

    public override List<Entidad> RetornarLista()
    {
      throw new NotImplementedException();
    }

    private void validarEntidad()
    {
      if (((LocalidadEvento)localidad).Descripcion == null)
        ((LocalidadEvento)localidad).Descripcion = "";
    }
  }
}
