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
        if (validarEntidad())
        dao.Insertar(localidad);
      }
      catch (BaseDeDatosExcepcion e)
      {
        mensaje = e.Mensaje;
      }
      catch (CasteoInvalidoExcepcion e)
      {
        mensaje = e.Message;
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

    private bool validarEntidad()
    {
      if (((LocalidadEvento)localidad).Descripcion == null)
        ((LocalidadEvento)localidad).Descripcion = "";
      if (((LocalidadEvento)localidad).Nombre != null && ((LocalidadEvento)localidad).Coordenadas != null)
        return true;
      return false;
    }
  }
}
