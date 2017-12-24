
using System;

namespace ApiRest_COCO_TRIP.Datos.Entity
{
  public class Agenda : Entidad
  {
    int idItinerario;
    int idLugarTuristico;
    int idActividad;
    int idEvento;
    int fklugarturistico;
    DateTime fechaInicio;
    DateTime fechaFin;

    public int IdItinerario { get => idItinerario; set => idItinerario = value; }
    public int IdLugarTuristico { get => idLugarTuristico; set => idLugarTuristico = value; }
    public int IdActividad { get => idActividad; set => idActividad = value; }
    public int IdEvento { get => idEvento; set => idEvento = value; }
    public DateTime FechaInicio { get => fechaInicio; set => fechaInicio = value; }
    public DateTime FechaFin { get => fechaFin; set => fechaFin = value; }

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
