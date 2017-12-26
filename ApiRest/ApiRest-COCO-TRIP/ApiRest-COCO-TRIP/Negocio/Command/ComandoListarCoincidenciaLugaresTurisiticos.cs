using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ApiRest_COCO_TRIP.Datos.Entity;
using ApiRest_COCO_TRIP.Datos.DAO;
using ApiRest_COCO_TRIP.Datos.Fabrica;

namespace ApiRest_COCO_TRIP.Negocio.Command
{
  public class ComandoListarCoincidenciaLugaresTurisiticos : Comando
  {
    LugarTuristico lugarTuristico;
    DAOItinerario dAOItinerario;
    List<Entidad> list_lugares;
    public ComandoListarCoincidenciaLugaresTurisiticos(string busqueda)
    {
      lugarTuristico = FabricaEntidad.CrearEntidadLugarTuristico();
      lugarTuristico.Nombre = busqueda;
    }

    public override void Ejecutar()
    {
      dAOItinerario = FabricaDAO.CrearDAOItinerario();
      list_lugares = dAOItinerario.ConsultarLugarTuristico(lugarTuristico);
    }

    public override Entidad Retornar()
    {
      throw new NotImplementedException();
    }

    public override List<Entidad> RetornarLista()
    {
      return list_lugares;
    }
  }
}
