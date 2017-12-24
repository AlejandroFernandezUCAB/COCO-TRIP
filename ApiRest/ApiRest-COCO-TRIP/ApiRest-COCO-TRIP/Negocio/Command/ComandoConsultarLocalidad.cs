using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ApiRest_COCO_TRIP.Datos.Entity;
using ApiRest_COCO_TRIP.Datos.Fabrica;
using ApiRest_COCO_TRIP.Datos.DAO;
using ApiRest_COCO_TRIP.Models.Excepcion;

namespace ApiRest_COCO_TRIP.Negocio.Command
{
  public class ComandoConsultarLocalidad : Comando
  {
    private Entidad localidad;

    public ComandoConsultarLocalidad(int id) {
      localidad = FabricaEntidad.CrearEntidadLocalidad();
      localidad.Id = id;
    }

    public override void Ejecutar()
    {
      DAO dao = FabricaDAO.CrearDAOLocalidad();
      try
      {
        localidad = dao.ConsultarPorId(localidad);
      }
      catch (BaseDeDatosExcepcion e)
      {
        //INSERTAR EN LOG
      }
      catch (CasteoInvalidoExcepcion e)
      {
        //INSERTAR EN LOG
      }

      catch (OperacionInvalidaException e)
      {
        //INSERTAR EN LOG
      }
    }

    public override Entidad Retornar()
    {
      return localidad;
    }

    public override List<Entidad> RetornarLista()
    {
      throw new NotImplementedException();
    }
  }
}
