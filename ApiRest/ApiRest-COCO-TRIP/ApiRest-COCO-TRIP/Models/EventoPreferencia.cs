using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiRest_COCO_TRIP.Models
{
  public class EventoPreferencia
  {
    private string nombreEvento;
    private DateTime fechaInicio;
    private DateTime fechaFin;
    private TimeSpan horaInicio;
    private TimeSpan horaFin;
    private Double precio;
    private string descripcion;
    private string nombreLocal;
    private string localFotoRuta;
    private string nombreCategoria;



    public string NombreEvento { get => nombreEvento; set => nombreEvento = value; }
    public DateTime FechaInicio { get => fechaInicio; set => fechaInicio = value; }
    public DateTime FechaFin { get => fechaFin; set => fechaFin = value; }
    public TimeSpan HoraInicio { get => horaInicio; set => horaInicio = value; }
    public TimeSpan HoraFin { get => horaFin; set => horaFin = value; }
    public Double Precio { get => precio; set => precio = value; }
    public string Descripcion { get => descripcion; set => descripcion = value; }
    public string NombreLocal { get => nombreLocal; set => nombreLocal = value; }
    public string LocalFotoRuta { get => localFotoRuta; set => localFotoRuta = value; }
    public string NombreCategoria { get => nombreCategoria; set => nombreCategoria = value; }

  }
}
