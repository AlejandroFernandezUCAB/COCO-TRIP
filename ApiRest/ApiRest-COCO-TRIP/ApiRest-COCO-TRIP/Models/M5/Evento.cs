using System;


namespace Service1.Models
{
   public class Evento
   {
    string nombre;
    string categoria;
    string descripcion;
    DateTime fecha;
    int precio;
    string direccion;

    public string Nombre { get => nombre; set => nombre = value; }
    public string Categoria { get => categoria; set => categoria = value; }
    public string Descripcion { get => descripcion; set => descripcion = value; }
    public DateTime Fecha { get => fecha; set => fecha = value; }
    public int Precio { get => precio; set => precio = value; }
    public string Direccion { get => direccion; set => direccion = value; }
  }



}
