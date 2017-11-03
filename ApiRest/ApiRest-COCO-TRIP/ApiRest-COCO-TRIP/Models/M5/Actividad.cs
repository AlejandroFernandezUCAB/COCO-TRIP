

namespace ApiRest_COCO_TRIP.Models
{
   public class Actividad
   {
    int id;
    string nombre;
    string descripcion;
    int id_Lugar;

    public string Nombre { get => nombre; set => nombre = value; }
    public string Descripcion { get => descripcion; set => descripcion = value; }
    public int Id_Lugar { get => id_Lugar; set => id_Lugar = value; }
    public int Id { get => id; set => id = value; }
    //foto?

    public Actividad(int id)
    {
      this.id = id;
    }

  }
}
