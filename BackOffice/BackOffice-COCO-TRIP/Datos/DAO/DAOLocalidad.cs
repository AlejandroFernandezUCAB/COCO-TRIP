using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using BackOffice_COCO_TRIP.Datos.Entidades;
using BackOffice_COCO_TRIP.Datos.DAO.Interfaces;

namespace BackOffice_COCO_TRIP.Datos.DAO
{

  /// <summary>
  /// Clase que realiza el CRUD de la entidad Localidad
  /// </summary>
  public class DAOLocalidad : DAO<JObject, Entidad>, IDAOLocalidad
  {
    private const string ControllerUri = "M8_LocalidadEvento";
    private JObject responseData;

    /// <summary>
    /// Método Delete, elimina una localidad dado su id.
    /// </summary>
    /// <param name="id">Identificador único de la localidad</param>
    /// <returns>JSON de la respuesta del WS</returns>
    public override JObject Delete(int id)
    {
      try
      {
        using (HttpClient cliente = new HttpClient())
        {
          cliente.BaseAddress = new Uri(BaseUri);
          cliente.DefaultRequestHeaders.Accept.Clear();
          var responseTask = cliente.DeleteAsync($"{BaseUri}/{ControllerUri}/EliminarLocalidadEventoPorId/{id}");
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

    /// <summary>
    /// Método Get, consulta una localidad dado su id.
    /// </summary>
    /// <param name="id">Identificador único de la localidad</param>
    /// <returns>JSON de la respuesta del WS</returns>
    public override JObject Get(int id)
    {
      try
      {
        using (HttpClient cliente = new HttpClient())
        {
          cliente.BaseAddress = new Uri(BaseUri);
          cliente.DefaultRequestHeaders.Accept.Clear();
          var responseTask = cliente.GetAsync($"{BaseUri}/{ControllerUri}/ConsultarLocalidadPorId/{id}");
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

    /// <summary>
    /// Método GetAll, consulta todas las localidades.
    /// </summary>
    /// <returns>JSON de la respuesta del WS</returns>
    public JObject GetAll()
    {
      try
      {
        using (HttpClient cliente = new HttpClient())
        {
          cliente.BaseAddress = new Uri(BaseUri);
          cliente.DefaultRequestHeaders.Accept.Clear();
          var responseTask = cliente.GetAsync($"{BaseUri}/{ControllerUri}/ListarLocalidades");
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

    /// <summary>
    /// Método Patch.
    /// </summary>
    /// <param name="data">Entidad</param>
    /// <returns>JSON de la respuesta del WS</returns>
    public override JObject Patch(Entidad data)
    {
      throw new NotImplementedException();
    }

    /// <summary>
    /// Método Post, agrega una Localidad.
    /// </summary>
    /// <param name="data">Entidad localidad a agregar</param>
    /// <returns>JSON de la respuesta del WS</returns>
    public override JObject Post(Entidad data)
    {
      try
      {
        using (HttpClient cliente = new HttpClient())
        {
          cliente.BaseAddress = new Uri(BaseUri);
          cliente.DefaultRequestHeaders.Accept.Clear();
          JObject jsonData = new JObject
          {

            { "nombre", ((Localidad)data).Nombre },
            { "descripcion", ((Localidad)data).Descripcion },
             { "coordenadas", ((Localidad)data).Coordenadas }


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

    /// <summary>
    /// Método Put, actualiza una Localidad.
    /// </summary>
    /// <param name="data">Entidad localidad a actualizar</param>
    /// <returns>JSON de la respuesta del WS</returns>
    public override JObject Put(Entidad data)
    {
      try
      {
        using (HttpClient cliente = new HttpClient())
        {
          cliente.BaseAddress = new Uri(BaseUri);
          cliente.DefaultRequestHeaders.Accept.Clear();
          JObject jsonData = new JObject
          {
             { "id", data.Id },
            { "nombre", ((Localidad)data).Nombre },
            { "descripcion", ((Localidad)data).Descripcion },
             { "coordenadas", ((Localidad)data).Coordenadas }


          };
          var responseTask = cliente.PutAsJsonAsync($"{BaseUri}/{ControllerUri}/actualizarLocalidadEvento", jsonData);
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
  }
}
