using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BackOffice_COCO_TRIP.Models
{
  public class M8_Localidad
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


  }
}
