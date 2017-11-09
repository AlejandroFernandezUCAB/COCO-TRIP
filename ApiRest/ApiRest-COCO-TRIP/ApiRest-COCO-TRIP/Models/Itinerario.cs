using System;
using System.Collections.Generic;
using ApiRest_COCO_TRIP.Models.Dato;
using Npgsql;
using System.Data;

namespace ApiRest_COCO_TRIP.Models
{
   public class Itinerario
   {
     int id;
     int idUsuario;
     string nombre;
     DateTime fechaInicio;
     DateTime fechaFin;
     List<LugarTuristico> lugares= new List<LugarTuristico>();
     List<Actividad> actividades = new List<Actividad>();
    //List<Evento> eventos = new List<Evento>();
    public int Id { get => id; set => id = value; }
    public int IdUsuario { get => idUsuario; set => idUsuario = value; }
    public string Nombre { get => nombre; set => nombre = value; }
    public DateTime FechaInicio { get => fechaInicio; set => fechaInicio = value; }
    public DateTime FechaFin { get => fechaFin; set => fechaFin = value; }
    public List<LugarTuristico> Lugares { get => lugares; set => lugares = value; }
    public List<Actividad> Actividades { get => actividades; set => actividades = value; }
    
    public Itinerario()
    {
      id = new int();
      idUsuario = new int();
      nombre = "";
      fechaInicio = new DateTime();
      fechaFin = new DateTime();
    }
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
    public Itinerario(string nombre, int idusuario)
    {
      this.nombre = nombre;
      this.idUsuario = idusuario;
    }

    public Itinerario(int id)
    {
      this.id = id;
    }

  
   
  }
}
