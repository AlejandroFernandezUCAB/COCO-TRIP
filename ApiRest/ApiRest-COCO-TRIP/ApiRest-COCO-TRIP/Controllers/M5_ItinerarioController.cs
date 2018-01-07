using System;
using System.Web.Http;
using System.Net;
using System.Collections.Generic;
using System.Web.Http.Cors;
using Npgsql;
using ApiRest_COCO_TRIP.Negocio.Command;
using ApiRest_COCO_TRIP.Negocio.Fabrica;
using ApiRest_COCO_TRIP.Datos.Entity;

namespace ApiRest_COCO_TRIP.Controllers
{
  [EnableCors(origins: "*", headers: "*", methods: "*")]
  public class M5_ItinerarioController : ApiController
  {

    List<Itinerario> itinerarios = new List<Itinerario>();
    Comando comando;
    Itinerario itinerario;
        /// <summary>
        /// Agrega una itinerario
        /// </summary>
        /// <param name="itinerario">con el ID y el nombre del ITINERARIO</param>
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

        /// <summary>
        /// Elimina un itinerario
        /// </summary>
        /// <param name="idit">ID del itinerario que se va a eliminar</param>
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


        /// <summary>
        /// Modifica los datos de un itineario, envia el id y los datos nuevos
        /// </summary>
        /// <param name="itineraario">con el mismo id y los datos modificados</param>
        [HttpPut]
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

        /// <summary>
        /// Agrega un item al itinerario
        /// </summary>
        /// <param name="idit">ID del itinerario</param>
        /// <param name="iditem">id del item</param>
        /// <param name="fechafin">fecha de inicio del item </param>
        /// <param name="fechaini">fecha fin del item</param>
        /// <param name="tipo">tipo de item "Evento","Actividad","Lugar Turistico"</param>
        [HttpGet]
    public Boolean AgregarItem_It(string tipo,int idit, int iditem,DateTime fechaini,DateTime fechafin)
    {
      try
      {
                comando = FabricaComando.CrearComandoAgregarAgenda(tipo,idit,iditem,fechaini,fechafin);
                comando.Ejecutar();
        return true;
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

        /// <summary>
        /// Elimina un item del itinerario
        /// </summary>
        /// <param name="idit">ID del itinerario a eliminar el item</param>
        /// <param name="iditem">ID del item a eliminar</param>
        /// <param name="tipo">tipo de item "Evento","Actividad","Lugar Turistico"</param>
        [HttpDelete]
    public Boolean EliminarItem_It(string tipo,int idit, int iditem)
    {
      try
      {
                comando = FabricaComando.CrearComandoEliminarAgendaItem(tipo,idit,iditem);
                comando.Ejecutar();

        return true; 
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

        /// <summary>
        /// consulta toodos los itinerarios de un usuario
        /// </summary>
        /// <param name="id_usuario">ID del usuario</param>
        /// <returns>List de items></returns>
        [HttpGet]
    public List<Entidad> ConsultarItinerarios(int id_usuario)
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
        return comando.RetornarLista();
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
        /// <summary>
        /// Consulta las coincidencias en el nombre y el rango de fechas
        /// </summary>
        /// <param name="busqueda">coincidencia a buscar en bbdd</param>
        /// <param name="fechafin">fecha de inicio del item </param>
        /// <param name="fechaini">fecha fin del item</param>
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
        /// <summary>
        /// Busca todas las coincidencias de dicho nombre en los lugares turisticos
        /// </summary>
        /// <param name="busqueda">coincidencias de lugar turistico</param>
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
        /// <summary>
        /// Busca todas las coincidencias de dicho nombre en los eventos
        /// </summary>
        /// <param name="busqueda">coincidencias de eventos</param>
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
        /// <summary>
        /// Activa las notificaciones de correo para dicho usuario
        /// </summary>
        /// <param name="id_usuario">id usuario a activar correo</param>
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

        /// <summary>
        /// Hace visible o invisible el itinerario seleccionado para el usuario determinado
        /// </summary>
        /// <param name="idusuario">id del usuario</param>
        /// <param name="iditinerario">id del itinerario a camibar visibility</param>
        /// <param name="visible">booleano que decide que visibilidad tendra</param>
        [HttpGet]
    public Boolean SetVisible(int idusuario, int iditinerario, Boolean visible)
    {
      comando = FabricaComando.CrearComandoSetVisibleItinerario(visible,idusuario,iditinerario);
      comando.Ejecutar();
      Itinerario itinerario = (Itinerario)comando.Retornar();
      return itinerario.Visible;
    }

        /// <summary>
        /// agrega una nueva notificacion al usuario
        /// </summary>
        /// <param name="id_usario">id del usuario</param>
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

        /// <summary>
        /// Elimina la cnfiguracion actual de la notificacion del usuario
        /// </summary>
        /// <param name="id_usuario">id del usuario</param>
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


        /// <summary>
        /// modifica si se desea recibir o no correos
        /// </summary>
        /// <param name="id_usuario">id del usuario</param>
        /// <param name="correo">boolean que decide si se envan correos</param>
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
        /// <summary>
        /// consulta el estad de la notificacion del usuario
        /// </summary>
        /// <param name="id_usuario">id del usuario</param>
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
