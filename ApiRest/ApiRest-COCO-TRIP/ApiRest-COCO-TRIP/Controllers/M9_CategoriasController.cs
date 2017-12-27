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
using ApiRest_COCO_TRIP.Models;
using ApiRest_COCO_TRIP.Datos.DAO;
using ApiRest_COCO_TRIP.Datos.Fabrica;

namespace ApiRest_COCO_TRIP.Controllers
{
  [EnableCors(origins: "*", headers: "*", methods: "*")]
  public class M9_CategoriasController : ApiController
  {

    private PeticionCategoria Peticion;
    private IDictionary response = new Dictionary<string, object>();
    private const string Response_Data = "data";
    private const string Response_Error = "error";
    private Comando com;
    private ApiRest_COCO_TRIP.Datos.Entity.Entidad categoria = FabricaEntidad.CrearEntidadCategoria();
    

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
        
        categoria = data.ToObject<ApiRest_COCO_TRIP.Datos.Entity.Categoria>();
        com = FabricaComando.CrearComandoEstadoCategoria(categoria);
        com.Ejecutar();
        response.Add(Response_Data, "Se actualizo de forma exitosa");
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
        response.Add(Response_Error, "Ocurrio un error inesperado");

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

        categoria = new ApiRest_COCO_TRIP.Datos.Entity.Categoria(id);
        com = FabricaComando.CrearComandoObtenerCategorias(categoria);
        com.Ejecutar();
        IList<ApiRest_COCO_TRIP.Datos.Entity.Categoria> lista = ((ComandoObtenerCategorias)com).RetornarLista2(); 
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

        categoria = data.ToObject<ApiRest_COCO_TRIP.Datos.Entity.Categoria>();
        com = FabricaComando.CrearComandoModificarCategoria(categoria);
        com.Ejecutar();
        response.Add(Response_Data, "Se actualizo de forma exitosa");
      }


      catch (HijoConDePendenciaException ex)
      {

        response.Add(Response_Error, ex.Mensaje);
        response.Add("MensajeError", "No se puede mover porque tiene categorias asociadas");

      }

      catch (NombreDuplicadoException ex)
      {

        response.Add(Response_Error, ex.Mensaje);
        response.Add("MensajeError", "Este nombre de categoria ya existe");

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
        response.Add(Response_Error, "Ocurrio un error inesperado");
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


        PeticionCategoria peticion = new PeticionCategoria();
        IList<Categoria> lista = peticion.ObtenerCategoriasHabilitadas();

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

        Categoria categoria = data.ToObject<Categoria>();
        Peticion = new PeticionCategoria();
        Peticion.AgregarCategoria(categoria);
        response.Add(Response_Data, "Se agrego la categoria de forma exitosa.");

      }

      catch (JsonSerializationException ex)
      {

        response.Add(Response_Error, ex.Message);

      }

      catch (NombreDuplicadoException ex)
      {

        response.Add(Response_Error, ex.Mensaje);
        response.Add("MensajeError", "Este nombre de categoria ya existe");

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
        response.Add(Response_Error, "Ocurrio un error inesperado");

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
        PeticionCategoria peticion = new PeticionCategoria();
        IList<Categoria> lista = peticion.ObtenerCategoriaPorId(categoria);

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
