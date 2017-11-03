using System;
namespace ApiRest_COCO_TRIP
{
  public class Evento
   {
    int ev_id;
    string ev_nombre;
    string ev_categoria;
    string ev_descripcion;
    DateTime fecha;
    int precio;
    string direccion;

    public int Ev_id { get => ev_id; set => ev_id = value; }
    public string Ev_nombre { get => ev_nombre; set => ev_nombre = value; }
    public string Ev_categoria { get => ev_categoria; set => ev_categoria = value; }
    public string Ev_descripcion { get => ev_descripcion; set => ev_descripcion = value; }
    public DateTime Fecha { get => fecha; set => fecha = value; }
    public int Precio { get => precio; set => precio = value; }
    public string Direccion { get => direccion; set => direccion = value; }

    public Evento(int id, string nombre, string categoria, string descripcion, DateTime fecha, int precio, string direccion)
    {

    }
  }



}
