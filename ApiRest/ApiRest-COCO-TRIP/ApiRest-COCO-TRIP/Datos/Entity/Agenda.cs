
namespace ApiRest_COCO_TRIP.Datos.Entity
{
  public class Agenda : Entidad
  {
    int idItinerario;
    int idLugarTuristico;
    int idActividad;
    int idEvento;
    int fklugarturistico;

    public int IdItinerario { get => idItinerario; set => idItinerario = value; }
    public int IdLugarTuristico { get => idLugarTuristico; set => idLugarTuristico = value; }
    public int IdActividad { get => idActividad; set => idActividad = value; }
    public int IdEvento { get => idEvento; set => idEvento = value; }

    public Agenda()
    {
      Id = new int();
      idItinerario = new int();
      idLugarTuristico = new int();
      idEvento = new int();
      fklugarturistico = new int();
    }
  }
}
