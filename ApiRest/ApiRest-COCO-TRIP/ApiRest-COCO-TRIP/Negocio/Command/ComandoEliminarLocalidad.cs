using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ApiRest_COCO_TRIP.Datos.Entity;
using ApiRest_COCO_TRIP.Datos.Fabrica;
using ApiRest_COCO_TRIP.Datos.DAO;
using ApiRest_COCO_TRIP.Comun.Excepcion;
using ApiRest_COCO_TRIP.Negocio.Fabrica;

namespace ApiRest_COCO_TRIP.Negocio.Command
{
  public class ComandoEliminarLocalidad : Comando
  {
    private Entidad localidad;
    private DAO dao;
    private Comando comando;

    public ComandoEliminarLocalidad(int id) {
      localidad = FabricaEntidad.CrearEntidadLocalidad();
      localidad.Id = id;
      dao = FabricaDAO.CrearDAOLocalidad();
    }

    public override void Ejecutar()
    {
      try
      {
        comando = FabricaComando.CrearComandoConsultarLocalidad(localidad.Id);
        comando.Ejecutar();
        if (comando.Retornar().Id == localidad.Id)
          dao.Eliminar(localidad);
        else
          throw new  ItemNoEncontradoException("No se ha encontrado el la localidad seleccionada");
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
