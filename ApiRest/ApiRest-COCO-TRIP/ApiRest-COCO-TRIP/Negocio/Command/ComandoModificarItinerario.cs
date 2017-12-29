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
    private Usuario usuario;
    private Itinerario itinerario;
    private DAOItinerario dAOItinerario = FabricaDAO.CrearDAOItinerario();
    private List<Entidad> listaItinerarios;
    public ComandoModificarItinerario(int idItinerario,string nombre, DateTime fechaInicio,DateTime fechaFin,
      int idUsuario)
    {
      itinerario = FabricaEntidad.CrearEntidadItinerario();
      itinerario.Id = idItinerario;
      itinerario.Nombre = nombre;
      itinerario.FechaFin = fechaFin;
      itinerario.FechaInicio = fechaInicio;
      itinerario.IdUsuario = idUsuario;
      usuario = FabricaEntidad.CrearEntidadUsuario();
      usuario.Id = idUsuario;
    }

    public override void Ejecutar()
    {
      
      dAOItinerario.Actualizar(itinerario);
    }

    public override Entidad Retornar()
    {
      listaItinerarios = dAOItinerario.ConsultarLista(usuario);
      foreach (Entidad item in listaItinerarios)
      {
        Itinerario itinerarioNew = (Itinerario)item;
        if (itinerarioNew.Nombre == itinerario.Nombre) return itinerarioNew;
      }
      return null;
    }

    public override List<Entidad> RetornarLista()
    {
      throw new NotImplementedException();
    }
  }
}
