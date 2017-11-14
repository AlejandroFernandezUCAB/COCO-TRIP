using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace BackOffice_COCO_TRIP.Models.Peticion
{
  public class PeticionEvento : BasePeticion<JObject, Evento>
  {
    private const string ControllerUri = "M8_Eventos";
    private JObject responseData;

    public override JObject Delete(int id)
    {
      throw new NotImplementedException();
    }

    public override JObject Get(int id)
    {
      throw new NotImplementedException();
    }

    public override JObject Patch(Evento data)
    {
      throw new NotImplementedException();
    }

    public override JObject Post(LocalidadEvento data)
    {
      try
      {
        using (var cliente = new HttpClient())
        {
          cliente.BaseAddress = new Uri(BaseUri);
          cliente.DefaultRequestHeaders.Accept.Clear();
          JObject jsonData = new JObject
          {

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

    public override JObject Post(Evento data)
    {
      throw new NotImplementedException();
    }

    public override JObject Put(Evento data)
    {
      throw new NotImplementedException();
    }
  }
}
