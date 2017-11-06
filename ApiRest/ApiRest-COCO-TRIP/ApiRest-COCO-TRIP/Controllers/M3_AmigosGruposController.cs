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


    /// <summary>
    /// Buscar amigo en la aplicacion
    /// </summary>
    /// <param name="nombre">nombre del amigo a buscar</param>
    /// <returns></returns>
    [HttpGet]
    public List<Usuario> BuscarAmigo(string nombre)
    {
      peticion = new PeticionAmigoGrupo();
      return peticion.BuscarAmigo(nombre);

    }

    /// <summary>
    /// Procedimiento para agregar un grupo
    /// </summary>
    /// <param name="nombre">Nombre del grupo</param>
    /// <param name="foto">Foto del grupo</param>
    /// <param name="usuario">Lider del grupo(creador)</param>
    /// <returns></returns>
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
    /// Consultar lista de grupo del usuario
    /// </summary>
    /// <param name="id">id del usuario</param>
    /// <returns></returns>
    [HttpGet]
    public List<Grupo> ConsultarListaGrupos(string id)
    {
      peticion = new PeticionAmigoGrupo();
      return peticion.Listagrupo(Convert.ToInt32(id));

    }
    

    /// <summary>
    /// Procedimiento para visualizar el perfil del grupo
    /// </summary>
    /// <param name="id">Es el de id del grupo por el cual se buscara</param>
    /// <returns></returns>
    [HttpGet]
    public Grupo ConsultarPerfilGrupos(string id)
    {
      peticion = new PeticionAmigoGrupo();
      return peticion.ConsultarPerfilGrupo(Convert.ToInt32(id));

    }

  }
}
