using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ApiRest_COCO_TRIP.Datos.Entity;
using ApiRest_COCO_TRIP.Datos.DAO;
using ApiRest_COCO_TRIP.Datos.Fabrica;

namespace ApiRest_COCO_TRIP.Negocio.Command
{
  public class ComandoAgregarItinerario : Comando
  {
    private DAOItinerario DAOitinerario;
    private Itinerario itinerario;

    public ComandoAgregarItinerario(int idUsuario,string nombreItinerario)
    {
      itinerario = FabricaEntidad.CrearEntidadItinerario();
      itinerario.Nombre = nombreItinerario;
      itinerario.IdUsuario = idUsuario;
      
    }

    public override void Ejecutar()
    {
      DAOitinerario = FabricaDAO.CrearDAOItinerario();
      DAOitinerario.Insertar(itinerario);
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
