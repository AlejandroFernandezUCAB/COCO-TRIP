using BackOffice_COCO_TRIP.Datos.Entidades;
using BackOffice_COCO_TRIP.Models.Peticion;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BackOffice_COCO_TRIP.Negocio.Componentes.Comandos
{
  public class ComandoFiltrarEventoPorCategoria : Comando
  {
    private int id;
    private ArrayList resultado = new ArrayList();
    public override void Execute()
    {
      PeticionEvento peticionEvento = new PeticionEvento();
      JObject respuesta = peticionEvento.Get(id);
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
