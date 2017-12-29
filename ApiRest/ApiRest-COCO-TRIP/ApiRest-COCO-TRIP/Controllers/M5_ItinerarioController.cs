using System;
using System.Web.Http;
using System.Net;
using ApiRest_COCO_TRIP.Models.BaseDeDatos;
using System.Collections.Generic;
using System.Web.Http.Cors;
using Npgsql;
using Newtonsoft.Json;
using ApiRest_COCO_TRIP.Negocio.Command;
using ApiRest_COCO_TRIP.Negocio.Fabrica;
using ApiRest_COCO_TRIP.Datos.Entity;

namespace ApiRest_COCO_TRIP.Controllers
{
  [EnableCors(origins: "*", headers: "*", methods: "*")]
  public class M5_ItinerarioController : ApiController
  {

    List<Itinerario> itinerarios = new List<Itinerario>();
   // private PeticionItinerario peti = new PeticionItinerario();
    Comando comando;
    Itinerario itinerario;
   
    [HttpPut]
    public Itinerario AgregarItinerario(Itinerario it)
    {
      try
      {
        comando = FabricaComando.CrearComandoAgregarItinerario(it.IdUsuario,it.Nombre);
        comando.Ejecutar();
        Itinerario itNew = (Itinerario)comando.Retornar();
        return itNew;
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
      catch (Exception)
      {
        throw new HttpResponseException(HttpStatusCode.Ambiguous);
      }
    }

   
    [HttpDelete]
    public Boolean EliminarItinerario(int idit)
    {
      try
      {
        comando = FabricaComando.CrearComandoEliminarItinerario(idit);
        comando.Ejecutar();
        return true;
      }
      catch(NpgsqlException)
      {
        return false;
        throw new HttpResponseException(HttpStatusCode.InternalServerError);
      }
      catch (Exception)
      {
        throw new HttpResponseException(HttpStatusCode.Ambiguous);
      }
    }



    [HttpPost]
    public Itinerario ModificarItinerario(Itinerario it)
    {
      try
      {
        comando = FabricaComando.CrearComandoModificarItinerario(it.Id,it.Nombre,it.FechaInicio,it.FechaFin,it.IdUsuario);
        comando.Ejecutar();
        return (Itinerario)comando.Retornar();
      }
      catch (NpgsqlException)
      {
        throw new HttpResponseException(HttpStatusCode.InternalServerError);
      }
      catch (InvalidCastException)
      {
        throw new HttpResponseException(HttpStatusCode.BadRequest);
      }
      catch (Exception)
      {
        throw new HttpResponseException(HttpStatusCode.Ambiguous);
      }
    }
    
    [HttpGet]
    public Boolean AgregarItem_It(string tipo,int idit, int iditem,DateTime fechaini,DateTime fechafin)
    {
      try
      {
        return false;//peti.AgregarItem_It(tipo,idit, iditem, fechaini, fechafin);
      }
      catch (NpgsqlException)
      {
        throw new HttpResponseException(HttpStatusCode.InternalServerError);
      }
      catch (Exception)
      {
        throw new HttpResponseException(HttpStatusCode.Ambiguous);
      }
    }

    [HttpDelete]
    public Boolean EliminarItem_It(string tipo,int idit, int iditem)
    {
      try
      {
        return false; // peti.EliminarItem_It(tipo, idit, iditem);
      }
      catch (NpgsqlException e)
      {
        throw new HttpResponseException(HttpStatusCode.InternalServerError);
      }
      catch (InvalidCastException)
      {
        throw new HttpResponseException(HttpStatusCode.BadRequest);
      }
      catch (Exception)
      {
        throw new HttpResponseException(HttpStatusCode.Ambiguous);
      }
    }

    
    [HttpGet]
    public List<Itinerario> ConsultarItinerarios(int id_usuario)
    {
      try
      {

        comando = FabricaComando.CrearComandoConsultarItinerarios(id_usuario);
        comando.Ejecutar();
        List<Itinerario> listaItinerarios = new List<Itinerario>();
        foreach (Entidad item in comando.RetornarLista())
        {
          Itinerario itinerarioNew = (Itinerario)item;
          listaItinerarios.Add(itinerarioNew);
        }
        return listaItinerarios;
      }
      catch (NpgsqlException )
      {
        throw new HttpResponseException(HttpStatusCode.InternalServerError);
      }
      catch (ArgumentException)
      {
        throw new HttpResponseException(HttpStatusCode.InternalServerError);
      }
      catch (InvalidCastException )
      {
        throw new HttpResponseException(HttpStatusCode.BadRequest);
      }
      catch (Exception)
      {
        throw new HttpResponseException(HttpStatusCode.Ambiguous);
      }
    }

     [HttpGet]
    public List<Evento> ConsultarEventos(string busqueda, DateTime fechainicio, DateTime fechafin)
    {
      comando = FabricaComando.CrearComandoListarCoincidenciaEventos(busqueda,fechainicio,fechafin);
      comando.Ejecutar();
      List<Evento> listaEventos = new List<Evento>();
      foreach (Entidad item in comando.RetornarLista())
      {
        Evento eventoNew = (Evento)item;
        listaEventos.Add(eventoNew);
      }
      return listaEventos;
    }

    [HttpGet]
    public List<LugarTuristico> ConsultarLugaresTuristicos(string busqueda)
    {
      comando = FabricaComando.CrearComandoListarCoincidenciaLugaresTurisiticos(busqueda);
      comando.Ejecutar();
      List<LugarTuristico> listaLugar = new List<LugarTuristico>();
      foreach (Entidad item in comando.RetornarLista())
      {
        LugarTuristico lugarNew = (LugarTuristico)item;
        listaLugar.Add(lugarNew);
      }
      return listaLugar;
    }

    [HttpGet]
    public List<Actividad> ConsultarActividad(string busqueda)
    {
      comando = FabricaComando.CrearComandoListarCoincidenciaActividades(busqueda);
      comando.Ejecutar();
      List<Actividad> listaActividades = new List<Actividad>();
      foreach (Entidad item in comando.RetornarLista())
      {
        Actividad actividadNew = (Actividad)item;
        listaActividades.Add(actividadNew);
      }
      return listaActividades;
    }

    [HttpGet]
    public string NotificacionCorreo(int id_usuario)
    {
      comando = FabricaComando.CrearComandoEnviarCorreoItinerario(id_usuario);
      comando.Ejecutar();
      Usuario usuario = (Usuario)comando.Retornar();
      if (usuario.Valido)
      {
        return "Exitoso";
      }else return "failure";
    }


    [HttpGet]
    public Boolean SetVisible(int idusuario, int iditinerario, Boolean visible)
    {
      comando = FabricaComando.CrearComandoSetVisibleItinerario(visible,idusuario,iditinerario);
      comando.Ejecutar();
      Itinerario itinerario = (Itinerario)comando.Retornar();
      return itinerario.Visible;
    }

    //-------------------------------------------------------------------------------
    [HttpGet]
    public bool AgregarNotificacionConfiguracion(int id_usuario)
    {
      try
      { //int dato = JsonConvert.DeserializeObject<int>(id_usuario);
        comando = FabricaComando.CrearComandoAgregarNotificacion(id_usuario);
        comando.Ejecutar();
        return true;
      }
      catch (NpgsqlException e)
      {
        return false;
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
      catch (Exception)
      {
        throw new HttpResponseException(HttpStatusCode.Ambiguous);
      }
    }


    [HttpDelete]
    public bool EliminarNotificacionConfiguracion(int id_usuario)
    {
      try
      {
        comando = FabricaComando.CrearComandoEliminarNotificacion(id_usuario);
        comando.Ejecutar();
        return true;
      }
      catch (NpgsqlException)
      {
        return false;
        throw new HttpResponseException(HttpStatusCode.InternalServerError);
      }
      catch (Exception)
      {
        throw new HttpResponseException(HttpStatusCode.Ambiguous);
      }
    }



    [HttpGet]
    public bool ModificarNotificacionConfiguracion(int id_usuario, bool correo)
    {
      try
      {// dynamic req = new System.Dynamic.ExpandoObject();
        //req = JsonConvert.DeserializeObject<dynamic>(datos);
        comando = FabricaComando.CrearComandoModificarNotificacion(id_usuario,false,correo);
        comando.Ejecutar();
        return true;
      }
      catch (NpgsqlException)
      {
        return false;
        throw new HttpResponseException(HttpStatusCode.InternalServerError);
      }
      catch (InvalidCastException)
      {
        throw new HttpResponseException(HttpStatusCode.BadRequest);
      }
      catch (Exception)
      {
        throw new HttpResponseException(HttpStatusCode.Ambiguous);
      }
    }

    [HttpGet]
    public bool ConsultarNotificacion(int id_usuario)
    {
      comando = FabricaComando.CrearComandoConsultarNotificacion(id_usuario);
      comando.Ejecutar();
      Notificacion notificacion =(Notificacion) comando.Retornar();
      return notificacion.Correo;
    }
    //----------------------------------------------------------

  }
}
