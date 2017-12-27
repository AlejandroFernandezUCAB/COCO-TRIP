using BackOffice_COCO_TRIP.Datos.DAO.Interfaces;
using BackOffice_COCO_TRIP.Datos.Entidades;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;

namespace BackOffice_COCO_TRIP.Datos.DAO
{
  public class DAOEvento : DAO<JObject, Entidad>, IDAOEvento
  {
    private const string ControllerUri = "M8_Eventos";
    private JObject responseData;

    public override JObject Delete(int id)
    {
      try
      {
        using (HttpClient cliente = new HttpClient())
        {
          cliente.BaseAddress = new Uri(BaseUri);
          cliente.DefaultRequestHeaders.Accept.Clear();
          var responseTask = cliente.DeleteAsync($"{BaseUri}/{ControllerUri}/EliminarEventoPorId/{id}");
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

    public override JObject Get(int id)
    {
      try
      {
        using (HttpClient cliente = new HttpClient())
        {
          cliente.BaseAddress = new Uri(BaseUri);
          cliente.DefaultRequestHeaders.Accept.Clear();
          var responseTask = cliente.GetAsync($"{BaseUri}/{ControllerUri}/ListarEventos/{id}");


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

    public JObject GetEvento(int id)
    {

      try
      {
        using (HttpClient cliente = new HttpClient())
        {
          cliente.BaseAddress = new Uri(BaseUri);
          cliente.DefaultRequestHeaders.Accept.Clear();
          var responseTask = cliente.GetAsync($"{BaseUri}/{ControllerUri}/ConsultarEventoPorId/{id}");


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

    public override JObject Patch(Entidad data)
    {
      throw new NotImplementedException();
    }

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

            { "nombre", ((Evento)data).Nombre },
            { "descripcion", ((Evento)data).Descripcion },
             { "precio", ((Evento)data).Precio },
            { "fechaInicio",((Evento)data).FechaInicio.ToString()},
            {"fechaFin",((Evento)data).FechaFin.ToString() },
            {"horaInicio",((Evento)data).HoraInicio.Hour+":"+((Evento)data).FechaInicio.Minute+":00" },
            {"horaFin",((Evento)data).HoraInicio.Hour+":"+((Evento)data).FechaInicio.Minute+":00" },
            {"foto",((Evento)data).Foto },
            { "idCategoria",((Evento)data).IdCategoria},
            {"idLocalidad",((Evento)data).IdLocalidad }

          };
          var responseTask = cliente.PostAsJsonAsync($"{BaseUri}/{ControllerUri}/agregarEvento", jsonData);
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

    public override JObject Put(Entidad data)
    {
      throw new NotImplementedException();
    }
  }
}
