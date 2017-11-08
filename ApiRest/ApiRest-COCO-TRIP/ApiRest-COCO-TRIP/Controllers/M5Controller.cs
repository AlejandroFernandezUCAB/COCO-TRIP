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
    Itinerario itinerario = new Itinerario();

   
    [HttpPut]
    public Itinerario AgregarItinerario(Itinerario it)
    {
      //Itinerario itinerarios = JsonConvert.DeserializeObject<Itinerario>(it);
      return itinerario.AgregarItinerario(it);
    }

   
    [HttpDelete]
    public Boolean EliminarItinerario(int id)
    {
      return itinerario.EliminarItinerario(id);
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
