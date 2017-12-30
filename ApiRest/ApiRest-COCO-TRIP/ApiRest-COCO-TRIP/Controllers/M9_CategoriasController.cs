using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.Cors;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ApiRest_COCO_TRIP.Models.Excepcion;
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
    /// EndPoint para actualizar el estatus de una categoria a aprtir de el Id.
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
        response.Add(Response_Error, ex.Message);

      }
      catch (ParametrosNullException ex)
      {
        response.Add(Response_Error, ex.Mensaje);


      }
      catch (Exception ex)
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
      response = new Dictionary<string, object>();
      try
      {

        categoria = new Categoria(id);
        com = FabricaComando.CrearComandoObtenerCategorias(categoria);
        com.Ejecutar();
        IList<Categoria> lista = ((ComandoObtenerCategorias)com).RetornarLista2(); 
        response.Add(Response_Data, lista);

      }
      catch (BaseDeDatosExcepcion ex)
      {
        response.Add(Response_Error, ex.Message);

      }
      catch (ParametrosNullException ex)
      {
        response.Add(Response_Error, ex.Mensaje);

      }
      catch (Exception ex)
      {
        response.Add(Response_Error, mensaje.ErrorInesperado);

      }

      return response;
    }

    ///<summary>
    ///EndPoint para modificar los datos de la categoria
    ///</summary>
    ///




    /// <summary>
    /// EndPoint para actualizar el estatus de una categoria a aprtir de el Id.
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    [ResponseType(typeof(IDictionary))]
    [ActionName("ModificarCategoria")]
    [HttpPut]
    public IDictionary ModificarCategorias([FromBody] JObject data)
    {
      response = new Dictionary<string, object>();
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


      catch (HijoConDePendenciaException ex)
      {

        response.Add(Response_Error, ex.Mensaje);
        response.Add("MensajeError", mensaje.ErrorCategoriaAsociada);

      }

      catch (NombreDuplicadoException ex)
      {

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
        response.Add(Response_Error, ex.Message);

      }
      catch (ParametrosNullException ex)
      {
        response.Add(Response_Error, ex.Mensaje);

      }
      catch (Exception ex)
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
      response = new Dictionary<string, object>();
      try
      {


        com = FabricaComando.CrearComandoObtenerCategoriasHabilitadas();
        com.Ejecutar();
        IList<Categoria> lista = ((ComandoObtenerCategoriasHabilitadas)com).RetornarLista2();
        response.Add(Response_Data, lista);

      }
      catch (BaseDeDatosExcepcion ex)
      {
        response.Add(Response_Error, ex.Message);

      }
      catch (ParametrosNullException ex)
      {
        response.Add(Response_Error, ex.Mensaje);

      }
      catch (Exception ex)
      {
        response.Add(Response_Error, mensaje.ErrorInesperado);

      }

      return response;
    }

    [ResponseType(typeof(IDictionary))]
    [ActionName("AgregarCategoria")]
    [HttpPost]
    public IDictionary agregarCategoria([FromBody] JObject data)
    {
      response = new Dictionary<string, object>();
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

      catch (NombreDuplicadoException ex)
      {

        response.Add(Response_Error, ex.Mensaje);
        response.Add("MensajeError", mensaje.ErrorCategoriaDuplicada);

      }

      catch (JsonReaderException ex)
      {
        response.Add(Response_Error, ex.Message);

      }

      catch (BaseDeDatosExcepcion ex)
      {
        response.Add(Response_Error, ex.Message);

      }
      catch (ParametrosNullException ex)
      {
        response.Add(Response_Error, ex.Mensaje);

      }
      catch (Exception ex)
      {
        response.Add(Response_Error, mensaje.ErrorInesperado);

      }

      return response;
    }



    [ResponseType(typeof(IDictionary))]
    [HttpGet]
    [ActionName("obtenerCategoriasPorId")]
    public IDictionary ObtenerCategoriaPorId(int id)
    {
      response = new Dictionary<string, object>();
      try
      {


        Categoria categoria = new Categoria(id);
        com=FabricaComando.CrearComandoObtenerCategoriaPorId(categoria);
        com.Ejecutar();
        IList<Categoria> lista = ((ComandoObtenerCategoriaPorId)com).RetornarLista2();
        response.Add(Response_Data, lista);

      }
      catch (BaseDeDatosExcepcion ex)
      {
        response.Add(Response_Error, ex.Message);

      }
      catch (ParametrosNullException ex)
      {
        response.Add(Response_Error, ex.Mensaje);

      }
      catch (Exception ex)
      {
        response.Add(Response_Error, "Ocurrio un error inesperado");

      }

      return response;
    }

  }

}
