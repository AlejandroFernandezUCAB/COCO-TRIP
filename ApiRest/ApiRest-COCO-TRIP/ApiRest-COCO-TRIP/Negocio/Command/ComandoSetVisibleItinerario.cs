using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ApiRest_COCO_TRIP.Datos.Entity;
using ApiRest_COCO_TRIP.Datos.DAO;
using ApiRest_COCO_TRIP.Datos.Fabrica;

namespace ApiRest_COCO_TRIP.Negocio.Command
{
  public class ComandoSetVisibleItinerario : Comando
  {
    private Itinerario itinerario;
    private DAOItinerario dAOItinerario;
    private Boolean visible;
    private Boolean retorno;
    public ComandoSetVisibleItinerario(int idItinerario, int idUsuario,
      Boolean visible)
    {
      itinerario = FabricaEntidad.CrearEntidadItinerario();
      itinerario.Id = idItinerario;
      itinerario.IdUsuario = idUsuario;
      this.visible = visible;
    }

    public override void Ejecutar()
    {
      dAOItinerario = FabricaDAO.CrearDAOItinerario();
      retorno = dAOItinerario.SetVisible(itinerario.IdUsuario, itinerario.Id, visible);
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
