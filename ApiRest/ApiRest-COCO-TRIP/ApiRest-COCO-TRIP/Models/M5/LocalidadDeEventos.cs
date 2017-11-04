

namespace ApiRest_COCO_TRIP.Models
{
  public class LocalidadDeEventos
  {
    int lo_id;
    string lo_nombre;
    string lo_descripcion;
    string lo_lugar;

    public string Nombre { get => lo_nombre; set => lo_nombre = value; }
    public string Descripcion { get => lo_descripcion; set => lo_descripcion = value; }
    public string Ubicacion { get => lo_lugar; set => lo_lugar = value; }
    public int Lo_id { get => lo_id; set => lo_id = value; }
  }
}
