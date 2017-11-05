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
    //private List<Categoria> preferencias;

    public Usuario()
    {
    }

    public string Correo { get => correo; set => correo = value; }
    public string NombreUsuario { get => nombreUsuario; set => nombreUsuario = value; }
    //public List<Categoria> Preferencias { get => preferencias ; set => preferencias = value; }

    /*
    public List<Categoria> agregarPreferencia ( Categoria nuevaCategoria ){
      
      preferencias.add( nuevaCategoria );
    }
     */

    /*

    public List<Categoria> eliminarPreferencia(){

    }
     */
  }
}
