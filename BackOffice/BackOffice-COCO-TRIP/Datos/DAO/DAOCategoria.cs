using System;
using System.Net.Http;
using System.Threading.Tasks;
using BackOffice_COCO_TRIP.Datos.DAO.Interfaces;
using BackOffice_COCO_TRIP.Datos.Entidades;
using Newtonsoft.Json.Linq;

/// <summary>
/// Autores - MODULO 9:
///      Marialette Arguelles, Michel Jraiche y Horacio Orrillo
/// DESCRIPCION: 
///     Data Access Object de la entidad Categoria. En esta clase se encapsula el acceso a la fuente de datos.
/// </summary>
namespace BackOffice_COCO_TRIP.Datos.DAO
{
  /// <summary>
  /// En esta clase se encapsula el acceso a la fuente de datos.
  /// </summary>
  public class DAOCategoria : DAO<JObject, Categoria>, IDAOCategoria
  {
    private const string ControllerUri = "M9_Categorias";
    private JObject responseData;
    private JObject requestData;
    private HttpClient cliente = new HttpClient();
    private Task<HttpResponseMessage> responseTask;
    private HttpResponseMessage response;
    private Task<JObject> readTask;

    /// <summary>
    /// Metodo Delete, elimina una Categoria dado su id.
    /// </summary>
    /// <param name="id">Identificador unico de la Categoria.</param>
    /// <returns>Json respuesta del servicio web.</returns>
    /// <exception cref="NotImplementedException">Metodo no implementado</exception>
    public override JObject Delete(int id)
    {
      throw new NotImplementedException();
    }

    /// <summary>
    /// Metodo Get, lista las subcategorias existentes dado el id superior,
    /// de no dar un id, lista todas las categorias superiores. 
    /// </summary>
    /// <param name="id">Identificador unico de la entidad.</param>
    /// <returns>Json respuesta del servicio web.</returns>
    /// <exception cref="Exception">Error al listar las categorias.</exception>
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
    /// Metodo Patch.
    /// </summary>
    /// <param name="data">Entidad</param>
    /// <returns>Json respuesta del servicio web.</returns>
    /// <exception cref="NotImplementedException">Metodo no implementado</exception>
    public override JObject Patch(Entidad data)
    {
      throw new NotImplementedException();
    }

    /// <summary>
    /// Metodo Post, agrega una nueva categoria mediante peticiones al servicio web.
    /// </summary>
    /// <param name="data">Categoria a agregar</param>
    /// <returns>Json respuesta del servicio web.</returns>
    /// <exception cref="Exception">Error al agregar la categorias.</exception>
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
    /// Metodo Put, modificar una categoria mediante peticiones al servicio web.
    /// </summary>
    /// <param name="data">Categoria a modificar</param>
    /// <returns>Json respuesta del servicio web.</returns>
    /// <exception cref="Exception">Error al modificar la categoria.</exception>
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
    /// Metodo Put, modifica el estado de una categoria mediante peticiones al servicio web.
    /// </summary>
    /// <param name="data">Categoria a modificar dado el id y estado.</param>
    /// <returns>Json respuesta del servicio web.</returns>
    /// <exception cref="Exception">Error al actualizar el estado de la categoria.</exception>
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
    /// Metodo Get, permite obtener las categorias que estan Habilitadas mediante peticiones al servicio web.
    /// </summary>
    /// <returns>Json respuesta del servicio web.</returns>
    /// <exception cref="Exception">Error al obtener las categorias Habilitadas.</exception>
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
    /// Metodo Get, permite obtener las categorias mediante un Id a traves de peticiones al servicio web.
    /// </summary>
    /// <param name="id">Identificador unico de la categoria.</param>
    /// <returns>Json respuesta del servicio web.</returns>
    /// <exception cref="Exception">Error al obtener la categoria.</exception>
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
