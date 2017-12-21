using System.Net.Http;
using System.Web.Http;
using ApiRest_COCO_TRIP.Models;
using System.Reflection;
using Npgsql;
using System.Data;
using Newtonsoft.Json;
using System.Net.Mail;
using System.Web.Http.Cors;
using System.Collections.Generic;
using System;
using System.Web.Http.Description;
using System.Collections;
using ApiRest_COCO_TRIP.Models.Excepcion;
using Newtonsoft.Json.Linq;
using System.Net;
using ApiRest_COCO_TRIP.Models.Dato;
using ApiRest_COCO_TRIP.Models.BaseDeDatos;

namespace ApiRest_COCO_TRIP.Controllers
{

  /**
   * <summary>Clase encargada de recibir todas las peticiones relacionadas a Eventos</summary>
   * **/
  [EnableCors(origins: "*", headers: "*", methods: "*")]
  public class M8_EventosController : ApiController
  {
    private IDictionary respuesta = new Dictionary<string, object>();



    /**
     * <summary>Metodo de controlador para Agregar un Evento a la BBDD</summary>
     * **/

    [ResponseType(typeof(IDictionary))]
    [ActionName("agregarEvento")]
    [HttpPost]
    public IDictionary AgregarEvento([FromBody] JObject data)
    {
      try
      {
        Validaciones.ValidacionWS.validarParametrosNotNull(data, new List<string>
        {
          "nombre","descripcion","precio","fechaInicio","fechaFin","horaInicio","horaFin","foto",
          "idCategoria","idLocalidad"
        });
        Evento evento = data.ToObject<Evento>();

        PeticionEvento peticionEvento = new PeticionEvento();
        int idEvento = peticionEvento.AgregarEvento(evento);
        respuesta.Add("dato", "Se ha agregado");

      }
      catch (BaseDeDatosExcepcion e)
      {
        e.NombreMetodos.Add(this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name);
        throw new HttpResponseException(HttpStatusCode.InternalServerError);
        e.Mensaje = "Error en Evento Controlador Insertar";
        respuesta.Add("Error", e.Message);
      }
      catch (ParametrosNullException e)
      {
        respuesta.Add("Error", e.Mensaje);

      }
      catch (Exception e)
      {
        respuesta.Add("Error", "Error noo esperado ");

      }

      return respuesta;
    }



    /**
     * <summary>Metodo de controlador para Eliminar un Evento de la BBDD</summary>
     * **/
    [ResponseType(typeof(IDictionary))]
    [ActionName("EliminarEventoPorId")]
    [HttpPut]
    public IDictionary EliminarEvento(int id)
    {
      try
      {
        PeticionEvento peticionEvento = new PeticionEvento();
        String respuestaPeticion = peticionEvento.EliminarEventoId(id).ToString();
        respuesta.Add("dato", respuestaPeticion);
      }
      catch (BaseDeDatosExcepcion e)
      {
        respuesta.Add("Error Eliminar Evento", e.Message);
      }
      catch (ParametrosNullException e)
      {
        respuesta.Add("Error Eliminar Evento", e.Mensaje);

      }
      catch (Exception e)
      {
        respuesta.Add("Error Eliminar Evento", "Error noo esperado ");

      }


      return respuesta;
    }



    /**
     * <summary>Metodo de controlador para Consultar un evento dado su id todos los datos</summary>
     * <param name="id">Id del evento en la base de datos</param>
     * **/
    [ResponseType(typeof(IDictionary))]
    [ActionName("ConsultarEventoPorId")]
    [HttpGet]
    public IDictionary ConsultarEventoBO(int id)
    {
      try
      {
        PeticionEvento peticionEvento = new PeticionEvento();
        Evento evento = peticionEvento.ConsultarEvento(id);
        respuesta.Add("dato", evento);
      }
      catch (BaseDeDatosExcepcion e)
      {
        respuesta.Add("Error Consultat Evento por id", e.Message);
      }
      catch (ParametrosNullException e)
      {
        respuesta.Add("Error Consultat Evento por id", e.Mensaje);

      }
      catch (Exception e)
      {
        respuesta.Add("Error Consultat Evento por id", "Error noo esperado ");

      }
      return respuesta;
    }

    /**
     * <summary>Metodo de controlador para Listar todos los eventos de una Categoria</summary>
     * <param name="id_categoria">Id de la categoria en la base de datos</param>
     * **/
    [ResponseType(typeof(IDictionary))]
    [ActionName("ListarEventos")]
    [HttpGet]
    public IDictionary ListaEventosPorCategoria(int id)
    {
      try
      {

        PeticionEvento peticionEvento = new PeticionEvento();
        List<Evento> list = peticionEvento.ListaEventosPorCategoria(id);
        respuesta.Add("dato", list);
      }
      catch (BaseDeDatosExcepcion e)
      {
        respuesta.Add("Error Listar evento por categoria", e.Message);
      }
      catch (ParametrosNullException e)
      {
        respuesta.Add("Error Listar evento por categoria", e.Mensaje);

      }
      catch (Exception e)
      {
        respuesta.Add("Error Listar evento por categoria", "Error noo esperado ");

      }
      return respuesta;
    }

    /*
    [HttpGet]
    public Evento ConsultarEvento(int id)
    {
      try
      {
        PeticionEvento peticionEvento = new PeticionEvento();
        return peticionEvento.ConsultarEvento(id);
      }
      catch (BaseDeDatosExcepcion e)
      {
        throw e;
      }
      catch (ParametrosNullException e)
      {
        throw e;

      }
      catch (NpgsqlException e)
      {
        throw e;
      }
      catch (FormatException e)
      {
        throw e;
      }
      catch (Exception e)
      {
        throw e;

      }

    }


    [HttpGet]
    public List<Evento> ListarEventosPorFecha(DateTime date)
    {
      try
      {
        PeticionEvento peticionEvento = new PeticionEvento();
        return peticionEvento.ListaEventosPorFecha(date);
      }
      catch (BaseDeDatosExcepcion e)
      {
        throw e;
      }
      catch (ParametrosNullException e)
      {
        throw e;

      }
      catch (NpgsqlException e)
      {
        throw e;
      }
      catch (FormatException e)
      {
        throw e;
      }
      catch (Exception e)
      {
        throw e;

      }
    }


    [HttpGet]
    public List<Evento> listaTodosEventos()
    {
      try
      {
        PeticionEvento peticionEvento = new PeticionEvento();
        return peticionEvento.ListaEventos();
      }
      catch (BaseDeDatosExcepcion e)
      {
        throw e;
      }
      catch (ParametrosNullException e)
      {
        throw e;

      }
      catch (NpgsqlException e)
      {
        throw e;
      }
      catch (FormatException e)
      {
        throw e;
      }
      catch (Exception e)
      {
        throw e;

      }
    }


    [HttpGet]
    public List<Evento> ListarEventosPorCategoriaNombre(string nombreCategoria)
    {
      try
      {
        PeticionEvento peticionEvento = new PeticionEvento();
        return peticionEvento.ListaEventosPorCategoriaNombre(nombreCategoria);
      }
      catch (BaseDeDatosExcepcion e)
      {
        throw e;
      }
      catch (ParametrosNullException e)
      {
        throw e;

      }
      catch (NpgsqlException e)
      {
        throw e;
      }
      catch (FormatException e)
      {
        throw e;
      }
      catch (Exception e)
      {
        throw e;

      }
    }

    [HttpGet]
    public Evento ConsultarEventoNombre(string nombreEvento)
    {
      try
      {
        PeticionEvento peticionEvento = new PeticionEvento();
        return peticionEvento.ConsultarEventoNombre(nombreEvento);
      }
      catch (BaseDeDatosExcepcion e)
      {
        throw e;
      }
      catch (ParametrosNullException e)
      {
        throw e;

      }
      catch (NpgsqlException e)
      {
        throw e;
      }
      catch (FormatException e)
      {
        throw e;
      }
      catch (Exception e)
      {
        throw e;

      }

    }

    [HttpPut]
    public bool EliminarEventoNombre(string nombreEvento)
    {
      try
      {
        PeticionEvento peticionEvento = new PeticionEvento();
        return peticionEvento.EliminarEventoNombre(nombreEvento);
      }
      catch (BaseDeDatosExcepcion e)
      {
        throw e;
      }
      catch (ParametrosNullException e)
      {
        throw e;

      }
      catch (NpgsqlException e)
      {
        throw e;
      }
      catch (FormatException e)
      {
        throw e;
      }
      catch (Exception e)
      {
        throw e;

      }

    
  */
  }
}
