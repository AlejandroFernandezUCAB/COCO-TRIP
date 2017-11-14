using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.Cors;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ApiRest_COCO_TRIP.Models;
using ApiRest_COCO_TRIP.Models.Excepcion;
using System.Collections;
using ApiRest_COCO_TRIP.Validaciones;

namespace ApiRest_COCO_TRIP.Controllers
{
  [EnableCors(origins: "*", headers: "*", methods: "*")]
  public class M9_CategoriasController : ApiController
  {

    private PeticionCategoria Peticion;
    private IDictionary response = new Dictionary<string, object>();
    private const string Response_Data = "data";
    private const string Response_Error = "error";


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
      try
      {

        ValidacionWS.validarParametrosNotNull(data, new List<string> {
          "IdCategoria",
          "estatus"
        });

        Categoria categoria = data.ToObject<Categoria>();
        Peticion = new PeticionCategoria();
        Peticion.ActualizarEstatus(categoria);
        response.Add(Response_Data, "Se actualizo de forma exitosa");
      }

      catch (JsonSerializationException ex)
      {

        response.Add(Response_Error, ex.Message);
        /*
        response.Add(Response_Error, ex.Message);
        var resp = new HttpResponseMessage(HttpStatusCode.BadRequest)
        {
          Content = new StringContent(response.ToString()),
        };

        throw new HttpResponseException(resp);
        */
      }

      catch (JsonReaderException ex)
      {
        response.Add(Response_Error, ex.Message);
        /*
        response.Add(Response_Error, ex.Message);
        var resp = new HttpResponseMessage(HttpStatusCode.BadRequest)
        {
          Content = new StringContent(response.ToString()),
        };

        throw new HttpResponseException(resp);
        */
      }

      catch (BaseDeDatosExcepcion ex)
      {
        response.Add(Response_Error, ex.Message);
        /*
        response.Add(Response_Error, ex.Message);
        var resp = new HttpResponseMessage(HttpStatusCode.BadRequest)
        {
          Content = new StringContent(response.ToString()),
        };

        throw new HttpResponseException(resp);
        */
      }
      catch (ParametrosNullException ex)
      {
        response.Add(Response_Error, ex.Mensaje);
        /*
        response.Add(Response_Error, ex.Message);
        var resp = new HttpResponseMessage(HttpStatusCode.BadRequest)
        {
          Content = new StringContent(response.ToString()),
        };

        throw new HttpResponseException(resp);
        */
      }
      catch (Exception ex)
      {
        response.Add(Response_Error, "Ocurrio un error inesperado");
        /*
        response.Add(Response_Error, ex.Message);
        var resp = new HttpResponseMessage(HttpStatusCode.InternalServerError)
        {
          Content = new StringContent(response.ToString()),
        };

        throw new HttpResponseException(resp);
        */
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

        Categoria categoria = new Categoria(id);
        PeticionCategoria peticion = new PeticionCategoria();
        IList<Categoria> lista = peticion.ObtenerCategorias(categoria);

        response.Add(Response_Data, lista);

      }
      catch (BaseDeDatosExcepcion ex)
      {
        response.Add(Response_Error, ex.Message);
        /*
        response.Add(Response_Error, ex.Message);
        var resp = new HttpResponseMessage(HttpStatusCode.BadRequest)
        {
          Content = new StringContent(response.ToString()),
        };

        throw new HttpResponseException(resp);
        */
      }
      catch (ParametrosNullException ex)
      {
        response.Add(Response_Error, ex.Mensaje);
        /*
        response.Add(Response_Error, ex.Message);
        var resp = new HttpResponseMessage(HttpStatusCode.BadRequest)
        {
          Content = new StringContent(response.ToString()),
        };

        throw new HttpResponseException(resp);
        */
      }
      catch (Exception ex)
      {
        response.Add(Response_Error, "Ocurrio un error inesperado");
        /*
        response.Add(Response_Error, ex.Message);
        var resp = new HttpResponseMessage(HttpStatusCode.InternalServerError)
        {
          Content = new StringContent(response.ToString()),
        };

        throw new HttpResponseException(resp);
        */
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
      try
      {

        ValidacionWS.validarParametrosNotNull(data, new List<string> {
            "IdCategoria",
            "nombre",
            "descripcion",
            "categoriaSuperior",
            "nivel"
        });

        Categoria categoria = data.ToObject<Categoria>();
        Peticion = new PeticionCategoria();
        Peticion.ModificarCategoria(categoria);
        response.Add(Response_Data, "Se actualizo de forma exitosa");
      }


      catch (HijoConDePendenciaException ex)
      {

        response.Add(Response_Error, ex.Mensaje);
        /*
        response.Add(Response_Error, ex.Message);
        var resp = new HttpResponseMessage(HttpStatusCode.BadRequest)
        {
          Content = new StringContent(response.ToString()),
        };

        throw new HttpResponseException(resp);
        */
      }

      catch (NombreDuplicadoException ex)
      {

        response.Add(Response_Error, ex.Mensaje);
        response.Add("MensajeError", "Este nombre de categoria ya existe");
        /*
        response.Add(Response_Error, ex.Message);
        var resp = new HttpResponseMessage(HttpStatusCode.BadRequest)
        {
          Content = new StringContent(response.ToString()),
        };

        throw new HttpResponseException(resp);
        */
      }

      catch (JsonSerializationException ex)
      {

        response.Add(Response_Error, ex.Message);
        /*
        response.Add(Response_Error, ex.Message);
        var resp = new HttpResponseMessage(HttpStatusCode.BadRequest)
        {
          Content = new StringContent(response.ToString()),
        };

        throw new HttpResponseException(resp);
        */
      }

      catch (JsonReaderException ex)
      {
        response.Add(Response_Error, ex.Message);
        /*
        response.Add(Response_Error, ex.Message);
        var resp = new HttpResponseMessage(HttpStatusCode.BadRequest)
        {
          Content = new StringContent(response.ToString()),
        };

        throw new HttpResponseException(resp);
        */
      }

      catch (BaseDeDatosExcepcion ex)
      {
        response.Add(Response_Error, ex.Message);
        /*
        response.Add(Response_Error, ex.Message);
        var resp = new HttpResponseMessage(HttpStatusCode.BadRequest)
        {
          Content = new StringContent(response.ToString()),
        };

        throw new HttpResponseException(resp);
        */
      }
      catch (ParametrosNullException ex)
      {
        response.Add(Response_Error, ex.Mensaje);
        /*
        response.Add(Response_Error, ex.Message);
        var resp = new HttpResponseMessage(HttpStatusCode.BadRequest)
        {
          Content = new StringContent(response.ToString()),
        };

        throw new HttpResponseException(resp);
        */
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


        PeticionCategoria peticion = new PeticionCategoria();
        IList<Categoria> lista = peticion.ObtenerCategoriasHabilitadas();

        response.Add(Response_Data, lista);

      }
      catch (BaseDeDatosExcepcion ex)
      {
        response.Add(Response_Error, ex.Message);
        /*
        response.Add(Response_Error, ex.Message);
        var resp = new HttpResponseMessage(HttpStatusCode.BadRequest)
        {
          Content = new StringContent(response.ToString()),
        };

        throw new HttpResponseException(resp);
        */
      }
      catch (ParametrosNullException ex)
      {
        response.Add(Response_Error, ex.Mensaje);
        /*
        response.Add(Response_Error, ex.Message);
        var resp = new HttpResponseMessage(HttpStatusCode.BadRequest)
        {
          Content = new StringContent(response.ToString()),
        };

        throw new HttpResponseException(resp);
        */
      }
      catch (Exception ex)
      {
        response.Add(Response_Error, "Ocurrio un error inesperado");
        /*
        response.Add(Response_Error, ex.Message);
        var resp = new HttpResponseMessage(HttpStatusCode.InternalServerError)
        {
          Content = new StringContent(response.ToString()),
        };

        throw new HttpResponseException(resp);
        */
      }

      return response;
    }

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

                Categoria categoria = data.ToObject<Categoria>();
                Peticion = new PeticionCategoria();
                Peticion.AgregarCategoria(categoria);
                response.Add(Response_Data, "Se agrego la categoria de forma exitosa.");

            }

            catch (JsonSerializationException ex)
            {

                response.Add(Response_Error, ex.Message);
                /*
                response.Add(Response_Error, ex.Message);
                var resp = new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                  Content = new StringContent(response.ToString()),
                };

                throw new HttpResponseException(resp);
                */
            }

            catch (JsonReaderException ex)
            {
                response.Add(Response_Error, ex.Message);
                /*
                response.Add(Response_Error, ex.Message);
                var resp = new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                  Content = new StringContent(response.ToString()),
                };

                throw new HttpResponseException(resp);
                */
            }

            catch (BaseDeDatosExcepcion ex)
            {
                response.Add(Response_Error, ex.Message);
                /*
                response.Add(Response_Error, ex.Message);
                var resp = new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                  Content = new StringContent(response.ToString()),
                };

                throw new HttpResponseException(resp);
                */
            }
            catch (ParametrosNullException ex)
            {
                response.Add(Response_Error, ex.Mensaje);
                /*
                response.Add(Response_Error, ex.Message);
                var resp = new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                  Content = new StringContent(response.ToString()),
                };

                throw new HttpResponseException(resp);
                */
            }
            catch (Exception ex)
            {
                response.Add(Response_Error, "Ocurrio un error inesperado");
                /*
                response.Add(Response_Error, ex.Message);
                var resp = new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                  Content = new StringContent(response.ToString()),
                };

                throw new HttpResponseException(resp);
                */
            } 

            return response;
        }
        


    [ResponseType(typeof(IDictionary))]
    [HttpGet]
    [ActionName("obtenerCategoriasPorId")]
    public IDictionary ObtenerCategoriaPorId(int id)
    {
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
        /*
        response.Add(Response_Error, ex.Message);
        var resp = new HttpResponseMessage(HttpStatusCode.BadRequest)
        {
          Content = new StringContent(response.ToString()),
        };

        throw new HttpResponseException(resp);
        */
      }
      catch (ParametrosNullException ex)
      {
        response.Add(Response_Error, ex.Mensaje);
        /*
        response.Add(Response_Error, ex.Message);
        var resp = new HttpResponseMessage(HttpStatusCode.BadRequest)
        {
          Content = new StringContent(response.ToString()),
        };

        throw new HttpResponseException(resp);
        */
      }
      catch (Exception ex)
      {
        response.Add(Response_Error, "Ocurrio un error inesperado");
        /*
        response.Add(Response_Error, ex.Message);
        var resp = new HttpResponseMessage(HttpStatusCode.InternalServerError)
        {
          Content = new StringContent(response.ToString()),
        };

        throw new HttpResponseException(resp);
        */
      }

      return response;
    }



  }





}
