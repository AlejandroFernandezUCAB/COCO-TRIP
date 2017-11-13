using System;
using System.Net.Http;
using System.Net.Http.Headers;
using BackOffice_COCO_TRIP.Models.Dato;
using System.Collections.Generic;

namespace BackOffice_COCO_TRIP.Models.Peticion
{
  /// <summary>
  /// Realiza las peticiones HTTP al servicio web de COCO-TRIP
  /// </summary>
  public class PeticionLugares
  {
    private HttpClient peticion;
    private HttpResponseMessage respuesta;

    public PeticionLugares ()
    {
      peticion = new HttpClient();
      peticion.BaseAddress = new Uri("http://192.168.0.101:8090/"); //Sujeto a cambios -> localhost:puerto que decidan en Slack
      peticion.DefaultRequestHeaders.Accept.Clear();
      peticion.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    }

    //GET

    /*public List<LugarTuristico> GetLista (int desde, int hasta)
    {

    }*/

  }
}
