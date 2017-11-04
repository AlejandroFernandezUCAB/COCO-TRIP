using System;
using System.Collections.Generic;


namespace ApiRest_COCO_TRIP.Models
{
   public class Itinerario
   {
     int id;
     int idUsuario;
     string nombre;
     DateTime fechaInicio;
     DateTime fechaFin;

    public int Id { get => id; set => id = value; }
    public int IdUsuario { get => idUsuario; set => idUsuario = value; }
    public string Nombre { get => nombre; set => nombre = value; }
    public DateTime FechaInicio { get => fechaInicio; set => fechaInicio = value; }
    public DateTime FechaFin { get => fechaFin; set => fechaFin = value; }

    public Itinerario(int id, string nombre, DateTime fechainicio, DateTime fechafin, int idusuario)
    {
      this.id = id;
      this.nombre = nombre;
      fechaInicio = fechainicio;
      fechaFin = fechafin;
      idUsuario = idusuario;
    }

    public Itinerario(string nombre, DateTime fechainicio, DateTime fechafin, int idusuario)
    {
      this.nombre = nombre;
      fechaInicio = fechainicio;
      fechaFin = fechafin;
      idUsuario = idusuario;
    }
    public Itinerario(int id)
    {
      this.id = id;
    }
  }

}
