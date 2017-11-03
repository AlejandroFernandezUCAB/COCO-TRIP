using System.Collections.Generic;


namespace ApiRest_COCO_TRIP.Models
{
   public class LugarTuristico
   {
    int id;
    string nombre;
    string descripcion;
    string direccion;
    string correo;
    int telefono;
    float precio;
    //hora int o string?
    //coordenadas int?
    //fotos?
    List<string> categorias;
    List<string> subcategorias;

    public string Nombre { get => nombre; set => nombre = value; }
    public string Descripcion { get => descripcion; set => descripcion = value; }
    public string Direccion { get => direccion; set => direccion = value; }
    public string Correo { get => correo; set => correo = value; }
    public int Telefono { get => telefono; set => telefono = value; }
    public float Precio { get => precio; set => precio = value; }
    public List<string> Categorias { get => categorias; set => categorias = value; }
    public List<string> Subcategorias { get => subcategorias; set => subcategorias = value; }
    public int Id { get => id; set => id = value; }

    public LugarTuristico(int id)
    {
      this.id = id;
    }
  }
}
