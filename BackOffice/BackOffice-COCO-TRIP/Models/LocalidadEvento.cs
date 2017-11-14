using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BackOffice_COCO_TRIP.Models
{
  public class LocalidadEvento
  {
    private int id;
    private string nombre;
    private string descripcion;
    private string coordenadas;

    [JsonProperty(PropertyName = "nombre")]
    public string Nombre { get => nombre; set => nombre = value; }
    [JsonProperty(PropertyName = "descripcion")]
    public string Descripcion { get => descripcion; set => descripcion = value; }
    [JsonProperty(PropertyName = "coordenadas")]
    public string Coordenadas { get => coordenadas; set => coordenadas = value; }
    [JsonProperty(PropertyName = "id")]
    public int Id { get => id; set => id = value; }

    public LocalidadEvento(string _nombre, string _descripcion, string _coordenadas)
    {
      nombre = _nombre;
      descripcion = _descripcion;
      coordenadas = _coordenadas;

    }
    public LocalidadEvento(int id, string _nombre, string _descripcion, string _coordenadas)
    {
      this.id = id;
      nombre = _nombre;
      descripcion = _descripcion;
      coordenadas = _coordenadas;

    }
    public LocalidadEvento(int id)
    {
      this.id = id;
    }

    public LocalidadEvento()
    {
     
    }
  }
}
