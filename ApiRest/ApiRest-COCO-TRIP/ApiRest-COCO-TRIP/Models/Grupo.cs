using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiRest_COCO_TRIP.Models
{
  public class Grupo
  {
    private string nombre;
    private byte[] foto;
    private Usuario lider;

    public string Nombre { get => nombre; set => nombre = value; }
    public byte[] Foto { get => foto; set => foto = value; }
    public Usuario Lider { get => lider; set => lider = value; }

    public Grupo()
    {

      nombre = "";
      foto = new byte[0];
      lider = new Usuario();
    }

    public Grupo(string nombre, byte[] foto, Usuario lider)
    {
      this.nombre = nombre;
      this.foto = foto;
      this.lider = lider;
    }
  }
}
