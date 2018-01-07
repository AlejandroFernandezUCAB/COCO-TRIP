using Newtonsoft.Json;

namespace BackOffice_COCO_TRIP.Datos.Entidades
{
  public abstract class Entidad
  {
    private long id;
    [JsonProperty(PropertyName = "id")]
    public long Id { get => id; set => id = value; }
  }
}
