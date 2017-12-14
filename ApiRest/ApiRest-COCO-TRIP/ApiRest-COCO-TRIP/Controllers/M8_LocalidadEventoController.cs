using System.Web.Http;
using ApiRest_COCO_TRIP.Models;
using System.Web.Http.Cors;
using System.Collections.Generic;
using System;
using System.Collections;
using System.Web.Http.Description;
using Newtonsoft.Json.Linq;
using ApiRest_COCO_TRIP.Models.Excepcion;
using System.Data.SqlClient;
namespace ApiRest_COCO_TRIP.Controllers
{

  /**
   * <summary>Clase encargada de recibir todas las peticiones relacionadas a Localidades de los eventos</summary>
   * **/
  [EnableCors(origins: "*", headers: "*", methods: "*")]
  public class M8_LocalidadEventoController : ApiController
  {
    private IDictionary respuesta = new Dictionary<string, object>();
    /**
     * <summary>Metodo de controlador para Agregar un Localidad de Evento a la BBDD</summary>
     * <param name="lEvento">Objeto con infomarcion de la localidad evento a agregar</param>
     * <exception cref="BaseDeDatosExcepcion"></exception>
     * <exception cref="ParametrosNullException"></exception>
     * <returns>Retorna el ID de la localidad agregada</returns>
     * **/
    [ResponseType(typeof(IDictionary))]
    [ActionName("agregarLocalidadEvento")]
    [HttpPost]
    public IDictionary AgregarLocalidadEvento([FromBody] JObject data)
    {
      try
      {
        Validaciones.ValidacionWS.validarParametrosNotNull(data, new List<string>
        {
          "nombre","descripcion","coordenadas"
        });
        LocalidadEvento lEvento = (LocalidadEvento)data.ToObject<LocalidadEvento>();
        PeticionLocalidadEvento peticionLocalidadEvento = new PeticionLocalidadEvento();
        int idEvento = peticionLocalidadEvento.AgregarLocalidadEvento(lEvento);
        respuesta.Add("dato", "Se ha creado una localidad");

      }
      catch (BaseDeDatosExcepcion e)
      {
        respuesta.Add("Error", e.Message);
      }
      catch (ParametrosNullException e)
      {
        respuesta.Add("dato", e.Mensaje);

      }
      catch (System.InvalidCastException e)
      {
        Console.WriteLine(e.Message);
      }
      catch (Exception e)
      {
        respuesta.Add("Error", "Error noo esperado " + e.Message);

      }
     

      return respuesta;


    }
    /**
     * <summary>Metodo de controlador para Eliminar una Localidad de Evento de la BBDD</summary>
     * <param name="id">Id de la localidad a eliminar </param>
     * <returns>TRUE si se eliminó exitosamente false si no </returns>
     * **/
    [ResponseType(typeof(IDictionary))]
    [ActionName("EliminarLocalidadEventoPorId")]
    [HttpDelete]
    public IDictionary EliminarLocalidadEvento(int id)
    {
      try
      {
        PeticionLocalidadEvento peticionLocalidadEvento = new PeticionLocalidadEvento();
       String respuestaPeticion = peticionLocalidadEvento.EliminarLocalidadEvento(id).ToString();
        respuesta.Add("dato", respuestaPeticion);
      }
      catch (BaseDeDatosExcepcion e)
      {
        respuesta.Add("Error Eliminar Evento", e.Message);
      }
      catch (ParametrosNullException e)
      {
        respuesta.Add("Error Eliminar Localidad Evento", e.Mensaje);

      }
      catch (Exception e)
      {
        respuesta.Add("Error Eliminar Localidad Evento", "Error noo esperado ");

      }

       return respuesta;
    }
    /**
     * <summary>Metodo de controlador para Consultar una Localidad de evento dado su id todos los datos</summary>
     * <param name="id">Id del evento a consultar</param>
     * <returns>Objeto Localidad Evento </returns>
     * **/
    [ResponseType(typeof(IDictionary))]
    [ActionName("ConsultarLocalidadPorId")]
    [HttpGet]
    public IDictionary ConsultarLocalidadEvento(int id)
    {
      try
      {
        PeticionLocalidadEvento peticionLocalidadEvento = new PeticionLocalidadEvento();
        LocalidadEvento localidadEvento =  peticionLocalidadEvento.ConsultarLocalidadEvento(id);
        respuesta.Add("dato", localidadEvento);
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
     * <summary>Metodo de controlador para Listar todos las localidades de los eventos</summary>
     * <param name="id_categoria">id de la categoria a consultar sus eventos</param>
     * <returns>La lista de evento de dicha categoria</returns>
     * **/
    [ResponseType(typeof(IDictionary))]
    [ActionName("ListarLocalidades")]
    [HttpGet]
    public IDictionary ListaLocalidadEventos()
    {
      try
      {
        PeticionLocalidadEvento peticionLocalidadEvento = new PeticionLocalidadEvento();
        IList<LocalidadEvento> list = peticionLocalidadEvento.ListaLocalidadEventos();
        respuesta.Add("dato", list);
      }
      catch (BaseDeDatosExcepcion e)
      {
        respuesta.Add("Error ListaLocalidadEventos", e.Message);
      }
      catch (ParametrosNullException e)
      {
        respuesta.Add("Error ListaLocalidadEventos", e.Mensaje);

      }
      catch (Exception e)
      {
        respuesta.Add("Error ListaLocalidadEventos", "Error noo esperado ");

      }

      return respuesta;
    }


    /**
     * <summary>Metodo de controlador para Agregar un Localidad de Evento a la BBDD</summary>
     * <param name="lEvento">Objeto con infomarcion de la localidad evento a agregar</param>
     * <exception cref="BaseDeDatosExcepcion"></exception>
     * <exception cref="ParametrosNullException"></exception>
     * <returns>Retorna el ID de la localidad agregada</returns>
     * **/
    [ResponseType(typeof(IDictionary))]
    [ActionName("actualizarLocalidadEvento")]
    [HttpPut]
    public IDictionary ActualizarLocalidadEvento([FromBody] JObject data)
    {
      try
      {
        Validaciones.ValidacionWS.validarParametrosNotNull(data, new List<string>
        {
          "id","nombre","descripcion","coordenadas"
        });
        LocalidadEvento lEvento = (LocalidadEvento)data.ToObject<LocalidadEvento>();
        PeticionLocalidadEvento peticionLocalidadEvento = new PeticionLocalidadEvento();

        int idEvento = peticionLocalidadEvento.ActualizarLocalidadEvento(lEvento);
        respuesta.Add("dato", "Se ha actualizado una localidad");

      }
      catch (BaseDeDatosExcepcion e)
      {
        respuesta.Add("Error", e.Message);
      }
      catch (ParametrosNullException e)
      {
        respuesta.Add("dato", e.Mensaje);

      }
      catch (System.InvalidCastException e)
      {
        Console.WriteLine(e.Message);
      }
      catch (Exception e)
      {
        respuesta.Add("Error", "Error noo esperado " + e.Message);

      }

      return respuesta;


    }

  }
}
