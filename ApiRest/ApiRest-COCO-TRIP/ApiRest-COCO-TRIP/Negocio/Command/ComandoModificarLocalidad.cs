using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ApiRest_COCO_TRIP.Datos.Entity;
using ApiRest_COCO_TRIP.Datos.DAO;
using ApiRest_COCO_TRIP.Datos.Fabrica;
using Newtonsoft.Json.Linq;

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
      dao.Actualizar(localidad);
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
