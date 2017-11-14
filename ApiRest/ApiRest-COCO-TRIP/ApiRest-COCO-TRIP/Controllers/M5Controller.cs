using System;
using System.Web.Http;
using System.Net;
using ApiRest_COCO_TRIP.Models;
using ApiRest_COCO_TRIP.Models.Dato;
using System.Collections.Generic;
using System.Web.Http.Cors;
using Npgsql;

namespace ApiRest_COCO_TRIP.Controllers
{
  [EnableCors(origins: "*", headers: "*", methods: "*")]
  public class M5Controller : ApiController
  {

    List<Itinerario> itinerarios = new List<Itinerario>();
    private PeticionItinerario peti = new PeticionItinerario(); 
   
    [HttpPut]
    public Itinerario AgregarItinerario(Itinerario it)
    {
      try
      {
        return peti.AgregarItinerario(it);
      }
      catch (NpgsqlException e)
      { 
        throw new HttpResponseException(HttpStatusCode.InternalServerError);
      }
      catch (InvalidCastException)
      {
        throw new HttpResponseException(HttpStatusCode.BadRequest);
      }
      catch (NullReferenceException)
      {
        throw new HttpResponseException(HttpStatusCode.BadRequest);
      }
    }

   
    [HttpDelete]
    public Boolean EliminarItinerario(int idit)
    {
      try
      {
        return peti.EliminarItinerario(idit);
      }
      catch(NpgsqlException)
      {
        return false;
        throw new HttpResponseException(HttpStatusCode.InternalServerError);
      }
    }



    [HttpPost]
    public Itinerario ModificarItinerario(Itinerario it)
    {
      try
      {
        return peti.ModificarItinerario(it);
      }
      catch (NpgsqlException)
      {
        throw new HttpResponseException(HttpStatusCode.InternalServerError);
      }
      catch (InvalidCastException)
      {
        throw new HttpResponseException(HttpStatusCode.BadRequest);
      }
    }
    
    [HttpPut]
    public Boolean AgregarItem_It(string tipo,int idit, int iditem,DateTime fechaini,DateTime fechafin)
    {
      try
      {
        return peti.AgregarItem_It(tipo,idit, iditem, fechaini, fechafin);
      }
      catch (NpgsqlException)
      {
        throw new HttpResponseException(HttpStatusCode.InternalServerError);
      }
    }

    [HttpDelete]
    public Boolean EliminarItem_It(string tipo,int idit, int iditem)
    {
      try
      {
        return peti.EliminarItem_It(tipo, idit, iditem);
      }
      catch (NpgsqlException e)
      {
        throw new HttpResponseException(HttpStatusCode.InternalServerError);
      }
      catch (InvalidCastException)
      {
        throw new HttpResponseException(HttpStatusCode.BadRequest);
      }
    }

    
    [HttpGet]
    public List<Itinerario> ConsultarItinerarios(int id_usuario)
    {
      try
      {
        return peti.ConsultarItinerarios(id_usuario);
      }
      catch (NpgsqlException e)
      {
        throw new HttpResponseException(HttpStatusCode.InternalServerError);
      }
      catch (ArgumentException)
      {
        throw new HttpResponseException(HttpStatusCode.InternalServerError);
      }
      catch (InvalidCastException e)
      {
        throw e;
        throw new HttpResponseException(HttpStatusCode.BadRequest);
      }

    }

     [HttpGet]
    public List<Evento> ConsultarEventos(string busqueda, DateTime fechainicio, DateTime fechafin)
    {
      return peti.ConsultarEventos(busqueda, fechainicio, fechafin);
    }

    [HttpGet]
    public List<LugarTuristico> ConsultarLugaresTuristicos(string busqueda)
    {
      return peti.ConsultarLugarTuristico(busqueda);
    }

    [HttpGet]
    public List<Actividad> ConsultarActividad(string busqueda)
    {
      return peti.ConsultarActividades(busqueda);
    }

    [HttpGet]
    public string NotificacionCorreo(int id_usuario)
    {
      return peti.EnviarCorreo(id_usuario);
    }


    [HttpGet]
    public Boolean SetVisible(int idusuario, int iditinerario, Boolean visible)
    {
      return peti.SetVisible(idusuario, iditinerario, visible);
    }


  }
}
