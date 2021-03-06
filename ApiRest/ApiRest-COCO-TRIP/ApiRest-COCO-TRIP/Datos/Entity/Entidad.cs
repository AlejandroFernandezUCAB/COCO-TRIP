using Newtonsoft.Json;
namespace ApiRest_COCO_TRIP.Datos.Entity
{
  /// <summary>
  /// Superclase de entidades
  /// </summary>
  public class Entidad
  {
    private int id;

    /// <summary>
    /// Getters y setters del atributo id
    /// </summary>
    [JsonProperty(PropertyName = "id")]
    public int Id
    {
      get { return id; }
      set { id = value; }
    }
  }
}
