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

    /// <summary>
    /// Procedimiento que se encarga de recoger los datos de
    /// la base de datos para visualizar la lista de amigos
    /// </summary>
    /// <param name="nombreUsuario"></param>
    /// <returns></returns>
    [HttpGet]
    public List<Usuario> VisualizarListaAmigos(string nombreUsuario)
    {
      peticion = new PeticionAmigoGrupo();
      return peticion.VisualizarListaAmigoBD(nombreUsuario);
    }

    /// <summary>
    /// Procemiento que se encarga de hacer la peticion para
    /// eliminar un amigo de la base de datos
    /// </summary>
    /// <param name="nombreAmigo"></param>
    /// <param name="nombreUsuario"></param>
    /// <returns></returns>
    [HttpGet]
    public int EliminarAmigo(string nombreAmigo, string nombreUsuario)
    {
      peticion = new PeticionAmigoGrupo();
      return peticion.EliminarAmigoBD(nombreAmigo, nombreUsuario);
    }

    /// <summary>
    /// Procedimiento que se encarga de hacer la peticion para
    /// eliminar un grupo de la base de datos
    /// </summary>
    /// <param name="nombreUsuario"></param>
    /// <param name="idGrupo"></param>
    /// <returns></returns>
    [HttpGet]
    public int EliminarGrupo(string nombreUsuario, int idGrupo)
    {
      peticion = new PeticionAmigoGrupo();
      return peticion.EliminarGrupoBD(nombreUsuario, idGrupo);
    }
    /// <summary>
    /// Procedimiento que se encarga de hacer la peticion para
    /// modificar los datos de un grupo
    /// </summary>
    /// <param name="nombreGrupo">Nombre del grupo</param>
    /// <param name="nombreUsuario">Nombre del usuario que esta modificando</param>
    /// <param name="foto">Foto del grupo</param>
    /// <param name="idGrupo">El identificador del grupo</param>
    /// <returns></returns>
    [HttpGet]
    public int ModificarGrupo(string nombreGrupo, string nombreUsuario, /*byte foto,*/ int idGrupo)
    {
      peticion = new PeticionAmigoGrupo();
      return peticion.ModificarGrupoBD(nombreGrupo,nombreUsuario, /*foto, */idGrupo);
    }
  }


}
