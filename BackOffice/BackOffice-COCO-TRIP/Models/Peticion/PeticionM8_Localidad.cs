using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Web;

namespace BackOffice_COCO_TRIP.Models.Peticion
{
  public class PeticionM8_Localidad : BasePeticion<JObject, M8_Localidad>
  {
    private const string ControllerUri = "M8_Localidad";
    private JObject responseData;

    public override JObject Delete(int id)
    {
      throw new NotImplementedException();
    }

    public override JObject Get(int id)
    {
      throw new NotImplementedException();
    }

    public override JObject Patch(M8_Localidad data)
    {
      throw new NotImplementedException();
    }

    public override JObject Post(M8_Localidad data)
    {
      try
      {
        using (var cliente = new HttpClient())
        {
          cliente.BaseAddress = new Uri(BaseUri);
          cliente.DefaultRequestHeaders.Accept.Clear();
          JObject jsonData = new JObject
          {
            { "id", data.Id },
            { "nombre", data.Nombre },
            { "descripcion", data.Descripcion },
             { "coordenadas", data.Coordenadas }
              
              
          };
          var responseTask = cliente.PostAsJsonAsync($"{BaseUri}/{ControllerUri}/AgregarLocalidadEvento", jsonData);
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

    public override JObject Put(M8_Localidad data)
    {
      throw new NotImplementedException();
    }
  }
}
