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
    /// Clase encargda de llamar al dao para agrear un item a la agenda
    /// </summary>
    public class ComandoAgregarAgenda : Comando
  {
    private DAOAgenda dAOAgenda;
    private Agenda agenda;
        /// <summary>
        /// Constructor de la clase
        /// </summary>
        /// <param name="tipo">Puede ser "Lugar Turistico","Evento" o "Actividad"</param>
        /// <param name="fechaFin">Fecha fin del itinerario</param>
        /// <param name="fechaInicio">Fecha inicio del itinerario</param>
        /// <param name="idItem">idItem a insertar en agenda</param>
        /// <param name="idItinerario">id del itinerario</param>
        public ComandoAgregarAgenda(string tipo,int idItinerario, int idItem, DateTime fechaInicio
      , DateTime fechaFin)
    {
      agenda = FabricaEntidad.CrearEntidadAgenda();
      agenda.IdItinerario = idItinerario;
      agenda.FechaInicio = fechaInicio;
      agenda.FechaFin = fechaFin;
      if (tipo == "Evento")
      {
        agenda.IdEvento = idItem;
        agenda.IdActividad = 0;
        agenda.IdLugarTuristico = 0;
      }else if (tipo == "Actividad")
      {
        agenda.IdEvento = 0;
        agenda.IdActividad = idItem;
        agenda.IdLugarTuristico = 0;
      }
      if (tipo == "Lugar Turistico")
      {
        agenda.IdEvento = 0;
        agenda.IdActividad = 0;
        agenda.IdLugarTuristico = idItem;
      }
    }

    public override void Ejecutar()
    {
      dAOAgenda = FabricaDAO.CrearDAOAgenda();
      dAOAgenda.Insertar(agenda);
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
