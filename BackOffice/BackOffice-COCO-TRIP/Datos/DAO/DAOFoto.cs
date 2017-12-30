using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BackOffice_COCO_TRIP.Datos.Entidades;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Net;
using System.Net.Sockets;
using Newtonsoft.Json;

namespace BackOffice_COCO_TRIP.Datos.DAO
{
  public class DAOFoto : DAO<JObject, Entidad>
  {
    private const string ControllerUri = "M7_LugaresTuristicosController";
    private JObject responseData;

    public override JObject Delete(int id)
    {
      throw new NotImplementedException();
    }

    /// <summary>
    /// Metodo para obtener lista de fotos de un lugar turistico
    /// </summary>
    /// <param name="id"></param>
    /// <returns>JObject</returns>
    public override JObject Get(int id)
    {
      try
      {
        using (HttpClient cliente = new HttpClient())
        {
          cliente.BaseAddress = new Uri(BaseUri);
          cliente.DefaultRequestHeaders.Accept.Clear();
          //necesito un metodo del api rest que me traiga las fotos de un lugar.
          // 'ConsultarFotos' aun no existe.
          // store procedure que me ayudara: ConsultarFotos

          var responseTask = cliente.GetAsync($"{BaseUri}/{ControllerUri}/ConsultarFotos/{id}");
          responseTask.Wait();
          var response = responseTask.Result;
          var readTask = response.Content.ReadAsAsync<JObject>();
          readTask.Wait();
          responseData = readTask.Result;
        }
      }
      catch (HttpRequestException ex)
      {
        responseData = new JObject
          {
            { "error", ex.Message }

          };
      }

      catch (WebException ex)
      {

        responseData = new JObject
          {
            { "error", ex.Message }

          };
      }
      catch (SocketException ex)
      {

        responseData = new JObject
          {
            { "error", ex.Message }

          };
      }
      catch (AggregateException ex)
      {

        responseData = new JObject
          {
            { "error", ex.Message }

          };
      }
      catch (JsonSerializationException ex)
      {

        responseData = new JObject
          {
            { "error", ex.Message }

          };
      }
      catch (JsonReaderException ex)
      {

        responseData = new JObject
          {
            { "error", ex.Message }

          };
      }
      catch (Exception ex)
      {

        responseData = new JObject
          {
            { "error", $"Ocurrio un error inesperado: {ex.Message}" }

          };
      }

      return responseData;
    }
    // Fin Get

    public override JObject Patch(Entidad data)
    {
      throw new NotImplementedException();
    }

    public override JObject Post(Entidad data)
    {
      throw new NotImplementedException();
    }

    public override JObject Put(Entidad data)
    {
      throw new NotImplementedException();
    }
  }
}
