using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiRest_COCO_TRIP.Datos.Entity
{
  public class Notificacion :Entidad
  {
   
    private int idUsuario;
    private Boolean correo;
    private Boolean push;

    public int IdUsuario { get => idUsuario; set => idUsuario = value; }
    public Boolean Correo { get => correo; set => correo = value; }
    public bool Push { get => push; set => push = value; }

    public Notificacion(int idUsuario, Boolean correo, bool push)
    {
      this.idUsuario = idUsuario;
      this.correo = correo;
      this.push = push;
    }

    public Notificacion(int idUsuario, bool push)
    {
      this.idUsuario = idUsuario;
      this.push = push;
    }

    public Notificacion()
    {
    }

    public Notificacion(int id, int idUsuario, Boolean correo, bool push)
    {
      this.idUsuario = idUsuario;
      this.correo = correo;
      this.push = push;
    }
  }
}
