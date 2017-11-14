using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiRest_COCO_TRIP.Models
{
  public class LugarTuristicoPreferencia
  {
    private string nombreLT;
    private Double costo;
    private string descripcion;
    private string direccion;
    private string lugarFotoRuta;
    private string nombreCategoria;

    public string NombreLT { get => nombreLT; set => nombreLT = value; }
    public Double Costo { get => costo; set => costo = value; }
    public string Descripcion { get => descripcion; set => descripcion = value; }
    public string Direccion { get => direccion; set => direccion = value; }
    public string LugarFotoRuta { get => lugarFotoRuta; set => lugarFotoRuta = value; }
    public string NombreCategoria { get => nombreCategoria; set => nombreCategoria = value; }

  }
}
