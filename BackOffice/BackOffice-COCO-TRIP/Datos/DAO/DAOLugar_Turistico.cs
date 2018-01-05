using System;
using BackOffice_COCO_TRIP.Datos.Entidades;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Net;
using System.Net.Sockets;
using Newtonsoft.Json;
using BackOffice_COCO_TRIP.Datos.DAO.Interfaces;

namespace BackOffice_COCO_TRIP.Datos.DAO
{
  public class DAOLugar_Turistico : DAO<JObject, LugarTuristico> , IDAOLugar_Turistico
  {
    private const string ControllerUri = "M7_LugaresTuristicos";
    private JObject responseData;

    public override JObject Delete(int id)
    {
      throw new NotImplementedException();
    }

    /// <summary>
    /// Metodo Get para obtener los lugares turisticos
    /// </summary>
    /// <param name="cantidad"></param>
    /// <returns>JObject</returns>
    public override JObject Get(int cantidad)
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

          var responseTask = cliente.GetAsync($"{BaseUri}/{ControllerUri}/GetLista?desde=1&hasta={cantidad}");
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

    public JObject GetAll()
    {
      using (HttpClient cliente = new HttpClient())
      {
        cliente.BaseAddress = new Uri(BaseUri);
        cliente.DefaultRequestHeaders.Accept.Clear();
        var responseTask = cliente.GetAsync($"{BaseUri}/{ControllerUri}/ListaLugaresTuristicos");
        responseTask.Wait();
        var response = responseTask.Result;
        var readTask = response.Content.ReadAsAsync<JObject>();
        readTask.Wait();

        responseData = readTask.Result;
        return responseData;
      }
    }

    public override JObject Patch(Entidad data)
    {
      throw new NotImplementedException();
    }

    /// <summary>
    /// Sirve para agregar un lugar turistico a la bdd con acceso a la API
    /// </summary>
    /// <param name="data">Entidad lugar turistico</param>
    /// <returns></returns>
    public override JObject Post(Entidad data)
    {
      using (HttpClient cliente = new HttpClient())
      {
        cliente.BaseAddress = new Uri(BaseUri);
        cliente.DefaultRequestHeaders.Accept.Clear();
        var responseTask = cliente.PostAsJsonAsync($"{BaseUri}/{ControllerUri}/AgregarLugarTuristico", (LugarTuristico)data);
        responseTask.Wait();
        var response = responseTask.Result;
        var readTask = response.Content.ReadAsAsync<JObject>();
        readTask.Wait();

        responseData = readTask.Result;
        return responseData;
      }

    }

    public override JObject Put(Entidad data)
    {
      throw new NotImplementedException();
    }
  }
}
