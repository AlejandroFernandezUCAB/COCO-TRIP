using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ApiRest_COCO_TRIP.Datos.Entity;
using ApiRest_COCO_TRIP.Datos.Fabrica;
using ApiRest_COCO_TRIP.Datos.DAO;

namespace ApiRest_COCO_TRIP.Negocio.Command
{
  public class ComandoEliminarLocalidad : Comando
  {
    private Entidad localidad;

    public ComandoEliminarLocalidad(int id) {
      localidad = FabricaEntidad.CrearEntidadLocalidad();
      localidad.Id = id;
    }

    public override void Ejecutar()
    {
      DAO dao = FabricaDAO.CrearDAOLocalidad();
      dao.Eliminar(localidad);
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
