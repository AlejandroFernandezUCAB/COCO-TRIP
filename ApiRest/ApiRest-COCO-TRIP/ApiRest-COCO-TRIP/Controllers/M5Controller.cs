using System;
using System.Web.Http;
using Npgsql;
using System.Data;
using ApiRest_COCO_TRIP.Models;
using ApiRest_COCO_TRIP.Models.Dato;
using System.Collections.Generic;
using System.Web;
using System.Net;
using System.Web.Http.Cors;
using Newtonsoft.Json;

namespace ApiRest_COCO_TRIP.Controllers
{
  [EnableCors(origins: "*", headers: "*", methods: "*")]
  public class M5Controller : ApiController
  {

    List<Itinerario> itinerarios = new List<Itinerario>();
    private PeticionItinerario peti = new PeticionItinerario(); //preguntar
    private PeticionBusqueda petiBusq = new PeticionBusqueda(); 
   
    [HttpPut]
    public Itinerario AgregarItinerario(Itinerario it)
    {
      return peti.AgregarItinerario(it);
    }

   
    [HttpDelete]
    public Boolean EliminarItinerario(int idit)
    {
      return peti.EliminarItinerario(idit);
    }

    [HttpPost]
    public Boolean ModificarItinerario(Itinerario it)
    {
      return peti.ModificarItinerario(it);
    }

 /* [HttpGet]
    public Boolean AgregarEvento_It(Itinerario it,Evento ev)
    {
      return itinerario.AgregarEvento_It(it,ev)
    }*/

   
   [HttpPut]
    public Boolean AgregarActividad_It(Itinerario it, Actividad ac)
    {
      return peti.AgregarActividad_It(it, ac);
    }

    
    [HttpPut]
    public Boolean AgregarLugar_It(Itinerario it, LugarTuristico lt)
    {
      return peti.AgregarLugar_It(it, lt);
    }

  /*[HttpDelete]
    public Boolean EliminarEvento_It(Itinerario it, Evento ev)
    {
      return itinerario.EliminarEvento_It(it, ev);
    }*/

   
    [HttpDelete]
    public Boolean EliminarActividad_It(Itinerario it, Actividad ac)
    {
      return peti.EliminarActividad_It(it, ac);
    }

    
    [HttpDelete]
    public Boolean EliminarLugar_It(Itinerario it, LugarTuristico lt)
    {
      return peti.EliminarLugar_It(it, lt);
    }


    [HttpGet]
    public List<Itinerario> ConsultarItinerarios(int id_usuario)
    { 
        return peti.ConsultarItinerarios(id_usuario);
    }

    [HttpGet]
    public List<Evento> ConsultarEventos(string busqueda)
    {
        return petiBusq.ConsultarEventos(busqueda);
    }

  }
}
