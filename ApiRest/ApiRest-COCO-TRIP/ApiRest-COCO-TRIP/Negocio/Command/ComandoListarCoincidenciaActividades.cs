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
    /// Comando que consulta las coincidencias de Actividades
    /// </summary>
    public class ComandoListarCoincidenciaActividades : Comando
  {
    Actividad actividad;
    DAOItinerario dAOItinerario;
    List<Entidad> list_actividades;
        /// <summary>
        /// Constructor de la clase
        /// </summary>
        /// <param name="busqueda">coincidencia a ser comparadaa</param>
        public ComandoListarCoincidenciaActividades(string busqueda)
    {
      actividad = FabricaEntidad.CrearEntidadActividad();
      actividad.Nombre = busqueda;
      dAOItinerario = FabricaDAO.CrearDAOItinerario();
    }

    public override void Ejecutar()
    {
      list_actividades = dAOItinerario.ConsultarActividades(actividad);
    }

    public override Entidad Retornar()
    {
      throw new NotImplementedException();
    }

    public override List<Entidad> RetornarLista()
    {
      return list_actividades;
    }
  }
}
