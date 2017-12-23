using System;
using System.Collections.Generic;
using ApiRest_COCO_TRIP.Models.Dato;

using Npgsql;
using System.Data;

namespace ApiRest_COCO_TRIP.Datos.Entity
{
   public class Itinerario : Entidad
   {
    
    int idUsuario;
    string nombre;
    DateTime fechaInicio;
    DateTime fechaFin;
    Boolean visible;
    List<dynamic> items_agenda = new List<dynamic>();
    public int IdUsuario { get => idUsuario; set => idUsuario = value; }
    public string Nombre { get => nombre; set => nombre = value; }
    public DateTime FechaInicio { get => fechaInicio; set => fechaInicio = value; }
    public DateTime FechaFin { get => fechaFin; set => fechaFin = value; }
    public List<dynamic> Items_agenda { get => items_agenda; set => items_agenda = value; }
    public bool Visible { get => visible; set => visible = value; }

    public Itinerario()
    {
      Id = new int();
      idUsuario = new int();
      nombre = "";
      fechaInicio = new DateTime();
      fechaFin = new DateTime();
    }
    public Itinerario(int id, string nombre, DateTime fechainicio, DateTime fechafin, int idusuario)
    {
      Id = id;
      this.nombre = nombre;
      fechaInicio = fechainicio;
      fechaFin = fechafin;
      idUsuario = idusuario;
    }

    public Itinerario(int id, string nombre, DateTime fechainicio, DateTime fechafin, int idusuario, bool visible)
    {
      Id = id;
      this.nombre = nombre;
      fechaInicio = fechainicio;
      fechaFin = fechafin;
      idUsuario = idusuario;
      this.visible = visible;
    }

    public Itinerario(int id, string nombre, int idusuario, bool visible)
    {
      Id = id;
      this.nombre = nombre;
      idUsuario = idusuario;
      this.visible = visible;
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
      Id = id;
    }
  }
}
