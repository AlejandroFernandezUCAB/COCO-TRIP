using BackOffice_COCO_TRIP.Datos.Entidades;
using BackOffice_COCO_TRIP.Models.Peticion;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BackOffice_COCO_TRIP.Negocio.Componentes.Comandos
{
  
  public class ComandoConsultarLocalidad : Comando
  {
    private int id;
    private LocalidadEvento localidad;
    private String mensaje;
    public ComandoConsultarLocalidad(int id) {
      this.id = id;
    }

    public override void Execute()
    {
      PeticionM8_Localidad peticion = new PeticionM8_Localidad();
      JObject respuesta = peticion.Get(id);

      if (respuesta.Property("dato") == null)
      {


        mensaje= "Ocurrio un error durante la comunicacion, revise su conexion a internet";
        localidad = new LocalidadEvento();

      }
      else
      {
        mensaje= "Se hizo con exito";
        localidad = respuesta["dato"].ToObject<LocalidadEvento>();
      }
    }

    public override object GetResult()
    {
      return localidad;
    }
    /*
    public String GetMensaje() {
      return mensaje;
    }
    */
  }
}
