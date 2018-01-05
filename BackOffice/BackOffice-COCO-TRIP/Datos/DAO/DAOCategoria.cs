using BackOffice_COCO_TRIP.Datos.Entidades;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using BackOffice_COCO_TRIP.Datos.DAO.Interfaces;
using System.Threading.Tasks;

namespace BackOffice_COCO_TRIP.Datos.DAO
{
  public class DAOCategoria: DAO<JObject,Categoria> , IDAOCategoria
  {
    private const string ControllerUri = "M9_Categorias";
    private JObject responseData;
    private JObject requestData;
    private HttpClient cliente = new HttpClient();
    private Task<HttpResponseMessage> responseTask;
    private HttpResponseMessage response;
    private Task<JObject> readTask;


    public override JObject Delete(int id)
    {
      throw new NotImplementedException();
    }

    /// <summary>
    /// Metodo para solicitar la lista de las categorias existentes
    /// </summary>
    public override JObject Get(int id)
    {
      try
      {
          cliente.BaseAddress = new Uri(BaseUri);
          cliente.DefaultRequestHeaders.Accept.Clear();
          responseTask = cliente.GetAsync($"{BaseUri}/{ControllerUri}/listarCategorias/{id}");
          responseTask.Wait();
          response = responseTask.Result;
          readTask = response.Content.ReadAsAsync<JObject>();
          readTask.Wait();
          responseData = readTask.Result;
      }
      catch (Exception ex)
      {
        responseData = new JObject
          {
            { "error", ex.Message }
          };
      }
      return responseData;
    }

    /// <summary>
    /// Clase abstracta base para realizar peticiones al servicio web
    /// </summary>
    public override JObject Patch(Entidad data)
    {
      throw new NotImplementedException();
    }

    /// <summary>
    /// Clase que permite agregar una nueva categoria mediante peticiones al servicio web 
    /// </summary>
    public override JObject Post(Entidad data)
    {
      try
      {
          cliente.BaseAddress = new Uri(BaseUri);
          cliente.DefaultRequestHeaders.Accept.Clear();
          requestData = new JObject
          {
            { "nombre", ((Categoria)data).Name },
            { "descripcion", ((Categoria)data).Description },
            { "nivel", ((Categoria)data).Nivel },
            { "categoriaSuperior", ((Categoria)data).UpperCategories }
          };
          responseTask = cliente.PostAsJsonAsync($"{BaseUri}/{ControllerUri}/AgregarCategoria", requestData);
          responseTask.Wait();
          response = responseTask.Result;
          readTask = response.Content.ReadAsAsync<JObject>();
          readTask.Wait();
          responseData = readTask.Result;
      }
      catch (Exception ex)
      {
        responseData = new JObject
          {
            { "error", ex.Message }
          };
      }
      return responseData;
    }


    /// <summary>
    /// Clase Para modificar una categoria mediante peticiones al servicio web
    /// </summary>
    public override JObject Put(Entidad data)
    {
      try
      {
          cliente.BaseAddress = new Uri(BaseUri);
          cliente.DefaultRequestHeaders.Accept.Clear();
          requestData = new JObject
          {
            { "id", data.Id },
            { "nombre", ((Categoria)data).Name },
            { "descripcion", ((Categoria)data).Description },
            { "categoriaSuperior", ((Categoria)data).UpperCategories},
            {"nivel", ((Categoria)data).Nivel }
          };
          responseTask = cliente.PutAsJsonAsync($"{BaseUri}/{ControllerUri}/ModificarCategoria", requestData);
          responseTask.Wait();
          response = responseTask.Result;
          readTask = response.Content.ReadAsAsync<JObject>();
          readTask.Wait();
          responseData = readTask.Result;
      }
      catch (Exception ex)
      {
        responseData = new JObject
          {
            { "error", ex.Message }
          };
      }
      return responseData;
    }

    /// <summary>
    /// Clase Para Modificar el estado de una categoria mediante peticiones al servicio web 
    /// </summary>
    public JObject PutEditarEstado(Entidad data)
    {
      try
      {
          cliente.BaseAddress = new Uri(BaseUri);
          cliente.DefaultRequestHeaders.Accept.Clear();
          requestData = new JObject
          {
            { "id", data.Id },
            { "estatus", ((Categoria)data).Status}
          };
          responseTask = cliente.PutAsJsonAsync($"{BaseUri}/{ControllerUri}/actualizarEstatus", requestData);
          responseTask.Wait();
          response = responseTask.Result;
          readTask = response.Content.ReadAsAsync<JObject>();
          readTask.Wait();
          responseData = readTask.Result;
      }
      catch (Exception ex)
      {
        responseData = new JObject
          {
            { "error", ex.Message }
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
          cliente.BaseAddress = new Uri(BaseUri);
          cliente.DefaultRequestHeaders.Accept.Clear();
          responseTask = cliente.GetAsync($"{BaseUri}/{ControllerUri}/CategoriasHabilitadas/");
          responseTask.Wait();
          response = responseTask.Result;
          readTask = response.Content.ReadAsAsync<JObject>();
          readTask.Wait();
          responseData = readTask.Result;
      }
      catch (Exception ex)
      {
        responseData = new JObject
          {
            { "error", ex.Message }
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
          cliente.BaseAddress = new Uri(BaseUri);
          cliente.DefaultRequestHeaders.Accept.Clear();
          responseTask = cliente.GetAsync($"{BaseUri}/{ControllerUri}/obtenerCategoriasPorId/{id}");
          responseTask.Wait();
          response = responseTask.Result;
          readTask = response.Content.ReadAsAsync<JObject>();
          readTask.Wait();
          responseData = readTask.Result;
      }
      catch (Exception ex)
      {
        responseData = new JObject
          {
            { "error", ex.Message }
          };
      }
      return responseData;
    }
  }
}
