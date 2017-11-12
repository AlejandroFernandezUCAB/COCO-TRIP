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
    private JObject response = new JObject();
    private const string Response_Data = "data";
    private const string Response_Error = "error";


    /// <summary>
    /// EndPoint para actualizar el estatus de una categoria a aprtir de el Id.
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    [ResponseType(typeof(JObject))]
    [ActionName("actualizarEstatus")]
    [HttpPost]
    public JObject ActualizarEstatusCategoria([FromBody] JObject data)
    {
      try
      {
        
        ValidacionWS.validarParametrosNotNull(data ,new List<string> {
          "IdCategoria",
          "estatus"
        });
        
        Categoria categoria = data.ToObject<Categoria>();
        Peticion = new PeticionCategoria();
        Peticion.ActualizarEstatus(categoria);
        response.Add(Response_Data, "Se actualizo de forma exitosa");
      }

      catch(JsonSerializationException ex)
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

      catch(JsonReaderException ex)
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

      catch(BaseDeDatosExcepcion ex)
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
      catch(ParametrosNullException ex)
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
