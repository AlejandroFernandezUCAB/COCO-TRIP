using System;
using System.Web.Http;
using ApiRest_COCO_TRIP.Models;
using ApiRest_COCO_TRIP.Models.Dato;
using System.Collections.Generic;
using System.Web.Http.Cors;

namespace ApiRest_COCO_TRIP.Controllers
{
  [EnableCors(origins: "*", headers: "*", methods: "*")]
  public class M5Controller : ApiController
  {

    List<Itinerario> itinerarios = new List<Itinerario>();
    private PeticionItinerario peti = new PeticionItinerario(); //preguntar
   
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

    [HttpDelete]
    public Boolean EliminarItem_It(Itinerario it, Agenda ag)
    {
      return peti.EliminarItem_It(it, ag);
    }

    
    [HttpGet]
    public List<Itinerario> ConsultarItinerarios(int id_usuario)
    { 
        return peti.ConsultarItinerarios(id_usuario);
    }

    [HttpGet]
    public List<Evento> ConsultarEventos(string busqueda)
    {
        return peti.ConsultarEventos(busqueda);
    }

  }
}
