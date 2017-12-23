using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ApiRest_COCO_TRIP.Datos.Entity;
using ApiRest_COCO_TRIP.Datos.Fabrica;
using ApiRest_COCO_TRIP.Datos.DAO;

namespace ApiRest_COCO_TRIP.Negocio.Command
{
  public class ComandoModificarItinerario : Comando
  {
    Itinerario itinerario;
    public ComandoModificarItinerario(int idItinerario,string nombre, DateTime fechaInicio,DateTime fechaFin,
      int idUsuario)
    {
      itinerario = FabricaEntidad.CrearEntidadItinerario();
      itinerario.Id = idItinerario;
      itinerario.Nombre = nombre;
      itinerario.FechaFin = fechaFin;
      itinerario.FechaInicio = fechaInicio;
      itinerario.IdUsuario = idUsuario;
    }

    public override void Ejecutar()
    {
      DAOItinerario dAOItinerario = FabricaDAO.CrearDAOItinerario();
      dAOItinerario.Actualizar(itinerario);
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
