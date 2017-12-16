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
  
  public class ComandoConsultarLocalidad : Comando
  {
    private int id;
    private ArrayList resultado= new ArrayList();

    public override void Execute()
    {
      PeticionM8_Localidad peticion = new PeticionM8_Localidad();
      JObject respuesta = peticion.Get(id);

      if (respuesta.Property("dato") == null)
      {
        resultado.Add(new Localidad());
        resultado.Add("Ocurrio un error durante la comunicacion, revise su conexion a internet");
      }
      else
      {
        resultado.Add(respuesta["dato"].ToObject<Localidad>());
        resultado.Add("Se hizo con exito");
      }
    }

    public override ArrayList GetResult()
    {
      return resultado;
    }

    public override void SetPropiedad(object propiedad)
    {
      this.id = (int)propiedad;
    }
  }
}
