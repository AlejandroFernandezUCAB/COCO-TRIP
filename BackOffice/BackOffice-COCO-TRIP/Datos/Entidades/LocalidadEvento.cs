using BackOffice_COCO_TRIP.Datos.Entidades;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BackOffice_COCO_TRIP.Datos.Entidades
{
  public class LocalidadEvento : Entidad
  {
    private string nombre;
    private string descripcion;
    private string coordenadas;

    [JsonProperty(PropertyName = "nombre")]
    [Required(ErrorMessage = "Debe ingresar un nombre")]
    
    [MaxLength(20, ErrorMessage = "ha excedido el maximo de caracteres (20)")]
    public string Nombre { get => nombre; set => nombre = value; }

    [JsonProperty(PropertyName = "descripcion")]
    [Required(ErrorMessage = "Debe ingresar descripcion de la localidad")]
    [StringLength(100)]
    public string Descripcion { get => descripcion; set => descripcion = value; }

    [JsonProperty(PropertyName = "coordenadas")]
    [Required(ErrorMessage = "Debe seleccionar unas coordenadas")]
    [StringLength(60)]
    public string Coordenadas { get => coordenadas; set => coordenadas = value; }
    public LocalidadEvento(string _nombre, string _descripcion, string _coordenadas)
    {
      nombre = _nombre;
      descripcion = _descripcion;
      coordenadas = _coordenadas;

    }
    public LocalidadEvento(int id, string _nombre, string _descripcion, string _coordenadas)
    {
      this.Id = id;
      nombre = _nombre;
      descripcion = _descripcion;
      coordenadas = _coordenadas;

    }
    public LocalidadEvento(int id)
    {
      this.Id = id;
    }

    public LocalidadEvento()
    {
     
    }

  }
}
