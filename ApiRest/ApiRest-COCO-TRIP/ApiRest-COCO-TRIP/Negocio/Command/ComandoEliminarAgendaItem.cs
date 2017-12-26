using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ApiRest_COCO_TRIP.Datos.Entity;
using ApiRest_COCO_TRIP.Datos.Fabrica;
using ApiRest_COCO_TRIP.Datos.DAO;

namespace ApiRest_COCO_TRIP.Negocio.Command
{
  public class ComandoEliminarAgendaItem : Comando
  {
    private Agenda agenda;
    private DAOAgenda dAOAgenda;
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
