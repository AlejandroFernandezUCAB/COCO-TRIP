using Newtonsoft.Json.Linq;
using BackOffice_COCO_TRIP.Datos.Entidades;
using BackOffice_COCO_TRIP.Models.Peticion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BackOffice_COCO_TRIP.Negocio.Componentes.Comandos
{
  public class ComandoInsertarLocalidad : Comando
  {
    private LocalidadEvento localidad;
    private String resultado;
    public ComandoInsertarLocalidad(LocalidadEvento localidad) {
      this.localidad = localidad;
    }
    public override void Execute()
    {
      PeticionM8_Localidad peticion = new PeticionM8_Localidad();
      JObject respuesta = peticion.Post(localidad);
      if (respuesta.Property("dato") == null)
      {


        resultado= "Ocurrio un error durante la comunicacion, revise su conexion a internet";

      }
      else
      {
        resultado= "Se hizo con exito";
      }
    }

    public override object GetResult()
    {
      throw new NotImplementedException();
    }
  }
}
