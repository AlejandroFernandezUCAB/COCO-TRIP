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
          respuesta.Add("dato", idEvento); 
       
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
        String respuestaPeticion = peticionEvento.EliminarEvento(id).ToString();
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
    public IDictionary ListaEventosPorCategoria(int id_categoria)
    {
      try
      {

        PeticionEvento peticionEvento = new PeticionEvento();
        List<Evento> list = peticionEvento.ListaEventosPorCategoria(id_categoria);
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
    [HttpGet]
    public Evento ConsultarEvento(int id)
    {
      PeticionEvento peticionEvento = new PeticionEvento();
      return peticionEvento.ConsultarEvento(id);
    }

    [HttpGet]
    public List<Evento> ListarEventosPorFecha(DateTime date)
    {
      PeticionEvento peticionEvento = new PeticionEvento();
      return peticionEvento.ListaEventosPorFecha(date);
    }

    /**
     * <summary>Metodo de controlador para listar todas las categorias desde la fecha danda</summary>
     * <param name="date">Fecha desde donde se buscaran los eventos</param>
     * */
    /**
   [ResponseType(typeof(IDictionary))]
   [ActionName("listarEventosPorFecha")]
   [HttpGet]
   public IDictionary ListarEventosPorFecha([FromBody] JObject data)
   {
     Validaciones.ValidacionWS.validarParametrosNotNull(data, new List<string>
       {
         "dia","mes","ano"
       });
     DateTime date =new DateTime()
     PeticionEvento peticionEvento = new PeticionEvento();
     peticionEvento.ListaEventosPorFecha(date);

     return
   }
 **/
  }
}
