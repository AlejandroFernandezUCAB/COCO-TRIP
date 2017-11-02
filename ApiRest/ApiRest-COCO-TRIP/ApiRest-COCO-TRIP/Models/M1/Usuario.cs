using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiRest_COCO_TRIP.Models.M1
{
  public class Usuario
  {
    private string nombreUsuario;
    private string correo;

    public Usuario()
    {
    }

    public string Correo { get => correo; set => correo = value; }
    public string NombreUsuario { get => nombreUsuario; set => nombreUsuario = value; }
  }
}
