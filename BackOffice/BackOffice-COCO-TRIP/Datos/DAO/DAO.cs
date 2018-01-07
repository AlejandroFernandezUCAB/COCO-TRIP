using BackOffice_COCO_TRIP.Datos.Entidades;
using Newtonsoft.Json.Linq;

namespace BackOffice_COCO_TRIP.Datos.DAO
{
  /// <summary>
  /// Clase abstracta base para realizar peticiones al servicio web
  /// </summary>
  /// <typeparam name="T1">Parametro que indica el tipo de dato que devolveran los metodos</typeparam>
  /// <typeparam name="T2">Parametro que indica el tipo de dato que recibiran los metodos Post, Put, Patch</typeparam>
  public abstract class DAO<T1, T2>
  {
    protected static readonly string BaseUri = Negocio.Registro.ApiRestBaseUri;

    /// <summary>
    /// Metodo Get, consulta dado el id de una entidad.
    /// </summary>
    /// <param name="id">Identificador unico de la entidad.</param>
    /// <returns>Json respuesta del servicio web.</returns>
    public abstract JObject Get(int id);

    /// <summary>
    /// Metodo Post, agrega una entidad.
    /// </summary>
    /// <param name="data">Entidad a agregar.</param>
    /// <returns>Json respuesta del servicio web.</returns>
    public abstract JObject Post(Entidad data);

    /// <summary>
    /// Metodo Put, actualiza/modifica una entidad.
    /// </summary>
    /// <param name="data">Entidad a actualizar/modificar.</param>
    /// <returns>Json respuesta del servicio web.</returns>
    public abstract JObject Put(Entidad data);

    /// <summary>
    /// Metodo Delete, elimina una entidad dado su id.
    /// </summary>
    /// <param name="id">Identificador unico de la entidad.</param>
    /// <returns>Json respuesta del servicio web.</returns>
    public abstract JObject Delete(int id);

    /// <summary>
    /// Metodo Patch.
    /// </summary>
    /// <param name="data">Entidad.</param>
    /// <returns>Json respuesta del servicio web.</returns>
    public abstract JObject Patch(Entidad data);

  }
}
