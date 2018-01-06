using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ApiRest_COCO_TRIP.Datos.Entity;
using ApiRest_COCO_TRIP.Datos.Fabrica;
using ApiRest_COCO_TRIP.Datos.DAO;

namespace ApiRest_COCO_TRIP.Negocio.Command
{
    /// <summary>
    /// Clase que que elimina un item de la agenda
    /// </summary>
    public class ComandoEliminarAgendaItem : Comando
  {
    private Agenda agenda;
    private DAOAgenda dAOAgenda;
        /// <summary>
        /// Constructor de la clase
        /// </summary>
        /// <param name="tipo">"Lugar Turistico", "Actividad" o "Evento"</param>
        /// <param name="idItem">Id del item</param>
        /// <param name="idItinerario">id del itinerario</param>
        public ComandoEliminarAgendaItem(string tipo,int idItem, int idItinerario)
    {
      agenda = FabricaEntidad.CrearEntidadAgenda();
      agenda.IdItinerario = idItinerario;
      if (tipo.Equals("Evento"))
      {
        agenda.IdActividad = 0;
        agenda.IdEvento = idItem;
        agenda.IdLugarTuristico = 0;
      }else if (tipo.Equals("Actividad"))
      {
        agenda.IdActividad = idItem;
        agenda.IdEvento = 0;
        agenda.IdLugarTuristico = 0;
      } else if (tipo.Equals("Lugar Turistico"))
      {
        agenda.IdActividad = 0;
        agenda.IdEvento = 0;
        agenda.IdLugarTuristico = idItem;
      }
    }

    public override void Ejecutar()
    {
      dAOAgenda = FabricaDAO.CrearDAOAgenda();
      dAOAgenda.Eliminar(agenda);
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
