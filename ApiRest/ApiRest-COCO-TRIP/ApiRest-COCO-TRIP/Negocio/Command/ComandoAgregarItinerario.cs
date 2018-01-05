using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ApiRest_COCO_TRIP.Datos.Entity;
using ApiRest_COCO_TRIP.Datos.DAO;
using ApiRest_COCO_TRIP.Datos.Fabrica;

namespace ApiRest_COCO_TRIP.Negocio.Command
{
    /// <summary>
    /// Clase que llama al dao para agregar un itinerario
    /// </summary>
    public class ComandoAgregarItinerario : Comando
  {
    private DAOItinerario DAOitinerario = FabricaDAO.CrearDAOItinerario();
    private Itinerario itinerario;
    private Usuario usuario;
    private List<Entidad> listaItinerarios;
        /// <summary>
        /// Constructr de la clase
        /// </summary>
        /// <param name="idUsuario">id del usuario</param>
        /// <param name="nombreItinerario">Nombre del nuevo itinerario</param>
        public ComandoAgregarItinerario(int idUsuario,string nombreItinerario)
    {
      itinerario = FabricaEntidad.CrearEntidadItinerario();
      usuario = FabricaEntidad.CrearEntidadUsuario();
      usuario.Id = idUsuario;
      itinerario.Nombre = nombreItinerario;
      itinerario.IdUsuario = idUsuario;
      
    }

    public override void Ejecutar()
    {
      DAOitinerario.Insertar(itinerario);
    }

    public override Entidad Retornar()
    {
      listaItinerarios =  DAOitinerario.ConsultarLista(usuario);
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
