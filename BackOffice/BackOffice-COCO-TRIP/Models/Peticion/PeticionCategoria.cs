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

  /// <summary>
  /// Clase  base para realizar peticiones al servicio web
  /// </summary>
  public class PeticionCategoria : BasePeticion<JObject, Categories>
  {

    private const string ControllerUri = "M9_Categorias";
    private JObject responseData;


   
    public override JObject Delete(int id)
    {
      throw new NotImplementedException();
    }

    /// <summary>
    /// Clase que le pide al Api la lista de las categorias existentes
    /// </summary>
    public override JObject Get(int id)
    {

      try
      {
        using (var cliente = new HttpClient())
        {
          cliente.BaseAddress = new Uri(BaseUri);
          cliente.DefaultRequestHeaders.Accept.Clear();
          var responseTask = cliente.GetAsync($"{BaseUri}/{ControllerUri}/listarCategorias/{id}");
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
    /// Clase abstracta base para realizar peticiones al servicio web
    /// </summary>
    public override JObject Patch(Categories data)
    {
      throw new NotImplementedException();
    }

    /// <summary>
    /// Clase que permite agregar una nueva categoria mediante peticiones al servicio web 
    /// </summary>
    public override JObject Post(Categories data)
    {
      try
      {
        using (var cliente = new HttpClient())
        {
          cliente.BaseAddress = new Uri(BaseUri);
          cliente.DefaultRequestHeaders.Accept.Clear();
          JObject jsonData = new JObject
          {
            { "nombre", data.Name },
            { "descripcion", data.Description },
            { "nivel", data.Nivel },
            { "categoriaSuperior", data.UpperCategories }
          };
          var responseTask = cliente.PostAsJsonAsync($"{BaseUri}/{ControllerUri}/AgregarCategoria", jsonData);
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
    /// Clase Para actualizar el Status de una categoria mediante peticiones al servicio web
    /// </summary>
    public override JObject Put(Categories data)
    {
      try
      {
        using (var cliente = new HttpClient())
        {
          cliente.BaseAddress = new Uri(BaseUri);
          cliente.DefaultRequestHeaders.Accept.Clear();
          JObject jsonData = new JObject
          {
            { "IdCategoria", data.Id },
            { "estatus", data.Status }
          };
          var responseTask = cliente.PutAsJsonAsync($"{BaseUri}/{ControllerUri}/actualizarEstatus", jsonData);
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
    /// Clase Para Modificar una categoria mediante peticiones al servicio web 
    /// </summary>
    public JObject PutEditarCategoria(Categories data)
    {
      try
      {
        using (var cliente = new HttpClient())
        {
          cliente.BaseAddress = new Uri(BaseUri);
          cliente.DefaultRequestHeaders.Accept.Clear();
          JObject jsonData = new JObject
          {
            { "IdCategoria", data.Id },
            { "nombre",data.Name },
            { "descripcion", data.Description },
            { "categoriaSuperior",data.UpperCategories },
            {"nivel", data.Nivel }
          };
          var responseTask = cliente.PutAsJsonAsync($"{BaseUri}/{ControllerUri}/ModificarCategoria", jsonData);
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
    /// Clase que permite obtener las categorias que estan Habilitadas mediante peticiones al servicio web 
    /// </summary>
    public JObject GetCategoriasHabilitadas()
    {

      try
      {
        using (var cliente = new HttpClient())
        {
          cliente.BaseAddress = new Uri(BaseUri);
          cliente.DefaultRequestHeaders.Accept.Clear();
          var responseTask = cliente.GetAsync($"{BaseUri}/{ControllerUri}/CategoriasHabilitadas/");
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
    /// Clase que permite poder obtener las categorias mediante un Id a traves de  peticiones al servicio web 
    /// </summary>
    public JObject GetPorId(int id)
    {

      try
      {
        using (var cliente = new HttpClient())
        {
          cliente.BaseAddress = new Uri(BaseUri);
          cliente.DefaultRequestHeaders.Accept.Clear();
          var responseTask = cliente.GetAsync($"{BaseUri}/{ControllerUri}/obtenerCategoriasPorId/{id}");
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
