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
    /// Comando que busca las coincidencia de enventos
    /// </summary>
    
    public class ComandoListarCoincidenciaEventos : Comando
  {
    Evento evento;
    DAOItinerario dAOItinerario;
    List<Entidad> list_eventos;
        /// <summary>
        /// Constructor de la clase
        /// </summary>
        /// <param name="busqueda">coincidencia a ser comparada de evento</param>
        /// <param name="fechaFin">fecha fin</param>
        /// <param name="fechaInicio">fecha inicio</param>
        public ComandoListarCoincidenciaEventos(string busqueda,DateTime fechaInicio
      ,DateTime fechaFin)
    {
      evento = FabricaEntidad.CrearEntidadEvento();
      evento.Nombre = busqueda;
      evento.FechaInicio = fechaInicio;
      evento.FechaFin = fechaFin;
      dAOItinerario = FabricaDAO.CrearDAOItinerario();
    }

    public override void Ejecutar()
    {
      list_eventos = dAOItinerario.ConsultarEventos(evento);
    }

    public override Entidad Retornar()
    {
      throw new NotImplementedException();
    }

    public override List<Entidad> RetornarLista()
    {
      return list_eventos;
    }
  }
}
