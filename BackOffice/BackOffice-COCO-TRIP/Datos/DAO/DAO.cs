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

    // no se si los parametros de salida sea mejor entidad o dejarlo en T1. Creo que entidad. att lele
    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public abstract JObject Get(int id);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    public abstract JObject Post(Entidad data);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    public abstract JObject Put(Entidad data);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public abstract JObject Delete(int id);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    public abstract JObject Patch(Entidad data);

  }
}
