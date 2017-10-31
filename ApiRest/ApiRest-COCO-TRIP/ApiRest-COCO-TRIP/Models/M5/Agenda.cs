
namespace ApiRest_COCO_TRIP.Models.M5
{
  public class Agenda
  {
    int ag_id;
    int ag_idItinerario;
    int ag_idLugarTuristico;
    int ag_idActividad;
    int ag_idEvento;

    public int Ag_idItinerario { get => ag_idItinerario; set => ag_idItinerario = value; }
    public int Ag_idLugarTuristico { get => ag_idLugarTuristico; set => ag_idLugarTuristico = value; }
    public int Ag_idActividad { get => ag_idActividad; set => ag_idActividad = value; }
    public int Ag_idEvento { get => ag_idEvento; set => ag_idEvento = value; }
    public int Ag_id { get => ag_id; set => ag_id = value; }
  }
}
