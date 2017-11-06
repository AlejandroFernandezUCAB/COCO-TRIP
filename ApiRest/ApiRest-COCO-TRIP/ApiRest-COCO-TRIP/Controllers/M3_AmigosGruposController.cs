using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using ApiRest_COCO_TRIP.Models;
using Npgsql;
using System.Data;
using Newtonsoft.Json;

namespace ApiRest_COCO_TRIP.Controllers
{
  public class M3_AmigosGruposController : ApiController
  {

    int cmkdf;

    Usuario usuario;
    PeticionAmigoGrupo peticion;

    // GET api/<controller>/<action>/id 
    [HttpGet]
    public string AgregarAmigo(String idUsuario1, String idUsuario2)
    {
      peticion = new PeticionAmigoGrupo();
      peticion.AgregarAmigosBD(Convert.ToInt32(idUsuario1), Convert.ToInt32(idUsuario2));
      return "1";
    }

    [HttpGet]
    public Usuario VisualizarPerfilAmigo(String nombreUsuario)
    {
      peticion = new PeticionAmigoGrupo();
      return peticion.VisualizarPerfilAmigoBD(nombreUsuario);
    }

    [HttpGet]
    public Usuario BuscarAmigo(string nombre)
    {
      peticion = new PeticionAmigoGrupo();
      return peticion.BuscarAmigo(nombre);

    }

    [HttpGet]
    public string AgregarGrupo(String nombre, String foto, String usuario)
    {
      peticion = new PeticionAmigoGrupo();

      if (foto != "null")

      { peticion.AgregarGrupoBD(nombre, Convert.ToByte(foto),Convert.ToInt32(usuario));
      }

      else
      {
        peticion.AgregarGrupoBD(nombre, Convert.ToInt32(usuario));
      }
      
      
      return "1";
    }
    
  }
}
