using BackOffice_COCO_TRIP.Datos.Entidades;
using BackOffice_COCO_TRIP.Models;
using BackOffice_COCO_TRIP.Models.Peticion;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;

namespace BackOffice_COCO_TRIP.Negocio.Componentes.Comandos
{
  public class ComandoConsultarEventos : Comando
  {
    private int id;
    private ArrayList resultado = new ArrayList();
    public override void Execute()
    {
      try
      {
        PeticionCategoria peticionCategoria = new PeticionCategoria();
        PeticionM8_Localidad peticionLocalidad = new PeticionM8_Localidad();
        JObject respuestaCategoria = peticionCategoria.Get(id);
        JObject respuestaLocalidad = peticionLocalidad.GetAll();
        if (respuestaCategoria.Property("data") != null)
        {
          resultado.Add(respuestaCategoria["data"].ToObject<List<Categories>>());
          resultado.Add("Exito");
        }

        else
        {
          resultado.Add(new List<Categories>());
          resultado.Add("Error en la comunicacion o No existen Categorias");
        }

        if (respuestaLocalidad.Property("dato") != null)
        {
          resultado.Add(respuestaLocalidad["dato"].ToObject<List<Localidad>>());
        }

        else
        {
          resultado.Add(new List<Localidad>());
          resultado[1]+=" Error en la comunicacion o No existen localidades";
        }
      }
      catch (Exception e)
      {
        //TERMINAR
        throw e;
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
