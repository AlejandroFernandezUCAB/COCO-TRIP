using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ApiRest_COCO_TRIP.Datos.Entity;
using ApiRest_COCO_TRIP.Datos.DAO;
using ApiRest_COCO_TRIP.Datos.Fabrica;
using Newtonsoft.Json.Linq;
using ApiRest_COCO_TRIP.Comun.Excepcion;

namespace ApiRest_COCO_TRIP.Negocio.Command
{
  public class ComandoModificarLocalidad : Comando
  {
    private Entidad localidad;

    public ComandoModificarLocalidad(Entidad localidad) {
      this.localidad = (LocalidadEvento)localidad;
    }

    public override void Ejecutar()
    {
      DAO dao = FabricaDAO.CrearDAOLocalidad();
      try
      {
          validarEntidad();
          dao.Actualizar(localidad);
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
