using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiRest_COCO_TRIP.Models
{
  public class Grupo
  {
    private int id;
    private string nombre;
    private string foto;
    private Usuario lider;

    public int Id { get => id; set => id = value; }
    public string Nombre { get => nombre; set => nombre = value; }
    public string Foto { get => foto; set => foto = value; }
    public Usuario Lider { get => lider; set => lider = value; }

    public Grupo()
    {
      id = new int();
      nombre = "";
      foto = "";
      lider = new Usuario();
    }

    public Grupo(int id, string nombre, string foto, Usuario lider)
    {
      this.id = id;
      this.nombre = nombre;
      this.foto = foto;
      this.lider = lider;
    }
  }
}
