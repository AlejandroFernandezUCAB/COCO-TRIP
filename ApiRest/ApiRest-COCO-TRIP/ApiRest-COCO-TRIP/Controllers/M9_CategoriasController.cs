using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.Cors;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ApiRest_COCO_TRIP.Comun.Excepcion;
using System.Collections;
using ApiRest_COCO_TRIP.Validaciones;
using ApiRest_COCO_TRIP.Negocio.Command;
using ApiRest_COCO_TRIP.Negocio.Fabrica;
using ApiRest_COCO_TRIP.Datos.Entity;
using ApiRest_COCO_TRIP.Datos.Fabrica;
using ApiRest_COCO_TRIP.Datos.Singleton;

namespace ApiRest_COCO_TRIP.Controllers
{
  [EnableCors(origins: "*", headers: "*", methods: "*")]
  public class M9_CategoriasController : ApiController
  {
    private IDictionary response = new Dictionary<string, object>();
    private const string Response_Data = "data";
    private const string Response_Error = "error";
    private Comando com;
    private MensajeResultadoOperacion mensaje = MensajeResultadoOperacion.ObtenerInstancia();
    private Entidad categoria = FabricaEntidad.CrearEntidadCategoria();

    /// <summary>
    /// EndPoint para actualizar el estatus de una categoria a aprtir de el Id
    /// </summary>
    /// <param name="data"></param>     
    /// <returns></returns>
    [ResponseType(typeof(IDictionary))]
    [ActionName("actualizarEstatus")]
    [HttpPut]
    public IDictionary ActualizarEstatusCategoria([FromBody] JObject data)
    {
      response = new Dictionary<string, object>();
      try
      {
        ValidacionWS.validarParametrosNotNull(data, new List<string> {
          "id",
          "estatus"
        });
        categoria = data.ToObject<Categoria>();
        com = FabricaComando.CrearComandoEstadoCategoria(categoria);
        com.Ejecutar();
        response.Add(Response_Data, mensaje.ExitoModificar);
      }
      catch (JsonSerializationException ex)
      {
        response.Add(Response_Error, ex.Message);
      }
      catch (JsonReaderException ex)
      {
        response.Add(Response_Error, ex.Message);
      }
      catch (BaseDeDatosExcepcion ex)
      {
        response.Add(Response_Error, ex.Mensaje);
      }
      catch (ParametrosNullExcepcion ex)
      {
        response.Add(Response_Error, ex.Mensaje);
      }
      catch (Excepcion ex)
      {
        response.Add(Response_Error, mensaje.ErrorInesperado);
      }
      return response;

    }


    /// <summary>
    /// EndPoint para obtener las categorias hijas a partir de una categoria padre dado un ID,
    /// si el id no viene en la peticion se devuelve las categorias padres absolutas
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [ResponseType(typeof(IDictionary))]
    [ActionName("listarCategorias")]
    [HttpGet]
    public IDictionary ObtenerCategorias(int id = -1)
    {
      try
      {
        categoria = new Categoria(id);
        com = FabricaComando.CrearComandoObtenerCategorias(categoria);
        com.Ejecutar();
        response.Add(Response_Data, ((ComandoObtenerCategorias)com).RetornarLista());
      }
      catch (BaseDeDatosExcepcion ex)
      {
        response.Add(Response_Error, ex.Message);
      }
      catch (Excepcion ex)
      {
        response.Add(Response_Error, mensaje.ErrorInesperado);
      }
      return response;
    }


    ///<summary>
    ///EndPoint para modificar los datos de la categoria
    ///</summary>
    [ResponseType(typeof(IDictionary))]
    [ActionName("ModificarCategoria")]
    [HttpPut]
    public IDictionary ModificarCategorias([FromBody] JObject data)
    {
      try
      {
        ValidacionWS.validarParametrosNotNull(data, new List<string> {
            "id",
            "nombre",
            "descripcion",
            "categoriaSuperior",
            "nivel"
        });
        categoria = data.ToObject<Categoria>();
        com = FabricaComando.CrearComandoModificarCategoria(categoria);
        com.Ejecutar();
        response.Add(Response_Data, mensaje.ExitoModificar);
      }
      catch (HijoConDePendenciaExcepcion ex)
      {
        response.Add(Response_Error, ex.Mensaje);
        response.Add("MensajeError", mensaje.ErrorCategoriaAsociada);
      }
      catch (NombreDuplicadoExcepcion ex)
      {
                //AQUI SE MANDA EL MENSAJE DE ERROR
        response.Add(Response_Error, ex.Mensaje);
        response.Add("MensajeError", mensaje.ErrorCategoriaDuplicada);
      }
      catch (JsonSerializationException ex)
      {
        response.Add(Response_Error, ex.Message);
      }
      catch (JsonReaderException ex)
      {
        response.Add(Response_Error, ex.Message);
      }
      catch (BaseDeDatosExcepcion ex)
      {
        response.Add(Response_Error, ex.Mensaje);
      }
      catch (ParametrosNullExcepcion ex)
      {
        response.Add(Response_Error, ex.Mensaje);
      }
      catch (Excepcion ex)
      {
        response.Add(Response_Error, mensaje.ErrorInesperado);
      }
      return response;

    }


    /// <summary>
    /// EndPoint para obtener las categorias hijas a partir de una categoria padre dado un ID,
    /// si el id no viene en la peticion se devuelve las categorias padres absolutas
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [ResponseType(typeof(IDictionary))]
    [ActionName("CategoriasHabilitadas")]
    [HttpGet]
    public IDictionary ObtenerCategoriasHabilitadas()
    {
      try
      {
        com = FabricaComando.CrearComandoObtenerCategoriasHabilitadas();
        com.Ejecutar();
        response.Add(Response_Data, ((ComandoObtenerCategoriasHabilitadas)com).RetornarLista());
      }
      catch (BaseDeDatosExcepcion ex)
      {
        response.Add(Response_Error, ex.Message);
      }
      catch (Excepcion ex)
      {
        response.Add(Response_Error, mensaje.ErrorInesperado);
      }
      return response;
    }


    /// <summary>
    /// EndPoint para agregar categorias 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [ResponseType(typeof(IDictionary))]
    [ActionName("AgregarCategoria")]
    [HttpPost]
    public IDictionary agregarCategoria([FromBody] JObject data)
    {
      try
      {
        ValidacionWS.validarParametrosNotNull(data, new List<string> {
                    "nombre",
                    "descripcion",
                    "categoriaSuperior",
                    "nivel"
                 });
        categoria = data.ToObject<Categoria>();
        com = FabricaComando.CrearComandoAgregarCategoria(categoria);
        com.Ejecutar();
        response.Add(Response_Data, mensaje.ExitoInsertarCategoria);
      }
      catch (JsonSerializationException ex)
      {
        response.Add(Response_Error, ex.Message);
      }
      catch (NombreDuplicadoExcepcion ex)
      {
        response.Add(Response_Error, ex.Mensaje);
        response.Add("MensajeError", mensaje.ErrorCategoriaDuplicada);
      }
      catch (BaseDeDatosExcepcion ex)
      {
        response.Add(Response_Error, ex.Message);
      }
      catch (ParametrosNullExcepcion ex)
      {
        response.Add(Response_Error, ex.Mensaje);
      }
      catch (Excepcion ex)
      {
        response.Add(Response_Error, mensaje.ErrorInesperado);
      }
      return response;
    }


    /// <summary>
    /// EndPoint para consultar una categoria por ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [ResponseType(typeof(IDictionary))]
    [HttpGet]
    [ActionName("obtenerCategoriasPorId")]
    public IDictionary ObtenerCategoriaPorId(int id)
    {
      try
      {
        categoria.Id = id;
        com=FabricaComando.CrearComandoObtenerCategoriaPorId(categoria);
        com.Ejecutar();
        response.Add(Response_Data, ((ComandoObtenerCategoriaPorId)com).RetornarLista());
      }
      catch (BaseDeDatosExcepcion ex)
      {
        response.Add(Response_Error, ex.Mensaje);
      }
      catch (Excepcion ex)
      {
        response.Add(Response_Error, mensaje.ErrorInesperado);
      }
      return response;
    }
  }

}
