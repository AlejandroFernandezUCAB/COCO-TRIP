using BackOffice_COCO_TRIP.Datos.DAO;
using BackOffice_COCO_TRIP.Datos.DAO.Interfaces;
using BackOffice_COCO_TRIP.Datos.Entidades;
using BackOffice_COCO_TRIP.Negocio.Fabrica;
using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;

namespace BackOffice_COCO_TRIP.Negocio.Comandos
{
  public class ComandoConsultarEventosPorCategoria : Comando
  {
    private int id;
    private ArrayList resultado = new ArrayList();
    public override void Execute()
    {
      IDAOEvento peticionEvento = FabricaDAO.GetDAOEvento();
      JObject respuesta = peticionEvento.GetEventoPorCategoria(id);
      if (respuesta.Property("dato") == null)
      {
        resultado.Add(new List < Evento > ());
        resultado.Add("Ocurrio un error durante la comunicacion, revise su conexion a internet");
        
      }
      else
      {
        resultado.Add(respuesta["dato"].ToObject<List<Evento>>());
        resultado.Add( "Se hizo con exito");
      }
    }

    public override ArrayList GetResult()
    {
      return resultado;
    }

    public override void SetPropiedad(object propiedad)
    {
      id = (int)propiedad;
    }
  }
}
