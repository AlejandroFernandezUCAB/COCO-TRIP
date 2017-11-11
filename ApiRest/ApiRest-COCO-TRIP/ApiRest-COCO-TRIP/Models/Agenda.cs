
namespace ApiRest_COCO_TRIP.Models
{
  public class Agenda
  {
    int id;
    int idItinerario;
    int idLugarTuristico;
    int idActividad;
    int idEvento;
    int fklugarturistico;

    public int IdItinerario { get => idItinerario; set => idItinerario = value; }
    public int IdLugarTuristico { get => idLugarTuristico; set => idLugarTuristico = value; }
    public int IdActividad { get => idActividad; set => idActividad = value; }
    public int IdEvento { get => idEvento; set => idEvento = value; }
    public int Id { get => id; set => id = value; }
  }
}
