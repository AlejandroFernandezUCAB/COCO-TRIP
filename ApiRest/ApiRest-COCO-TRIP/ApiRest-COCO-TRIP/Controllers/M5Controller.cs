using System;
using System.Web.Http;
using Npgsql;
using System.Data;
using ApiRest_COCO_TRIP.Models;
using ApiRest_COCO_TRIP.Models.M7.Dato;
using System.Collections.Generic;
using System.Web;
using System.Net;

namespace ApiRest_COCO_TRIP.Controllers
{
  public class M5Controller : ApiController
  {
    List<Itinerario> itinerarios = new List<Itinerario>();
    
    Itinerario itinerario = new Itinerario();

   
    [HttpPost]
    public Itinerario AgregarItinerario(Itinerario it)
    {
      return itinerario.AgregarItinerario(it);
    }

   
    [HttpDelete]
    public Boolean EliminarItinerario(Itinerario it)
    {
      return itinerario.EliminarItinerario(it);
    }

    [HttpPost]
    public Boolean ModificarItinerario(Itinerario it)
    {
      return itinerario.ModificarItinerario(it);
    }

 /* [HttpGet]
    public Boolean AgregarEvento_It(Itinerario it,Evento ev)
    {
      return itinerario.AgregarEvento_It(it,ev)
    }*/

   
   [HttpPost]
    public Boolean AgregarActividad_It(Itinerario it, Actividad ac)
    {
      return itinerario.AgregarActividad_It(it, ac);
    }

    
    [HttpPost]
    public Boolean AgregarLugar_It(Itinerario it, LugarTuristico lt)
    {
      return itinerario.AgregarLugar_It(it, lt);
    }

  /*[HttpDelete]
    public Boolean EliminarEvento_It(Itinerario it, Evento ev)
    {
      return itinerario.EliminarEvento_It(it, ev);
    }*/

   
    [HttpDelete]
    public Boolean EliminarActividad_It(Itinerario it, Actividad ac)
    {
      return itinerario.EliminarActividad_It(it, ac);
    }

    
    [HttpDelete]
    public Boolean EliminarLugar_It(Itinerario it, LugarTuristico lt)
    {

      return itinerario.EliminarLugar_It(it, lt);
    }
    


    [HttpGet]
    public List<Itinerario> ConsultarItinerarios(int id_usuario)
    {
        return itinerario.ConsultarItinerarios(id_usuario);
    }




  }
}
