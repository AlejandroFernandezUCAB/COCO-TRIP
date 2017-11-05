using System;
namespace ApiRest_COCO_TRIP.Models
{
  public class Evento
   {
    int ev_id;
    string ev_nombre;
    int id_categoria;
    string ev_descripcion;
    DateTime ev_fechaini;
    DateTime ev_fechafin;
    int ev_precio;
    int ev_localidad;

    public int Ev_id { get => ev_id; set => ev_id = value; }
    public string Ev_nombre { get => ev_nombre; set => ev_nombre = value; }
    public int Id_categoria { get => id_categoria; set => id_categoria = value; }
    public string Ev_descripcion { get => ev_descripcion; set => ev_descripcion = value; }
    public int Ev_Precio { get => ev_precio; set => ev_precio = value; }
    public DateTime Ev_fechaini { get => ev_fechaini; set => ev_fechaini = value; }
    public DateTime Ev_fechafin { get => ev_fechafin; set => ev_fechafin = value; }
    public int Ev_localidad { get => ev_localidad; set => ev_localidad = value; }

    public Evento(string nombre, int categoria, string descripcion, DateTime fechaini,DateTime fechafin, int precio, int localidad)
    {
      ev_nombre = nombre;
      ev_descripcion = descripcion;
      id_categoria = categoria;
      ev_fechaini = fechaini;
      ev_fechafin = fechafin;
      ev_precio = precio;
      ev_localidad = localidad;
    }

    public Evento(int id)
    {
      ev_id = id;
    }

  }



}
