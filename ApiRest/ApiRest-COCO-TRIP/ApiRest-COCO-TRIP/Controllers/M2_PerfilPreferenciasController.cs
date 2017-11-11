using Npgsql;
using System.Web.Http;
using ApiRest_COCO_TRIP.Models;
using System.Collections.Generic;
using System.Web.Http.Cors;

namespace ApiRest_COCO_TRIP.Controllers
{
  [EnableCors(origins: "*", headers: "*", methods: "*")]
  public class M2_PerfilPreferenciasController : ApiController
  {

    protected PeticionPerfil peticion;

    [HttpGet]
    public List<Categoria> AgregarPreferencia( )
    {
      return new List<Categoria>();
    }
    /// <summary>
    /// Metodo Post para agregar una preferencia del usuario, hará una llamda a base de datos para buscar id de usuario
    /// y id de categoria para agregarlo en la tabla de preferencias
    /// </summary>
    /// <param name="nombreUsuario">Nombre del usuario</param>
    /// <param name="nombrePreferencia">Nombre de la preferencia a agregar</param>
    /// <returns>Lista de preferencias del usuario</returns>
    // POST api/<controller>/<action>/prefencia
    [HttpPost]
    public List<Categoria> AgregarPreferencias ( string nombreUsuario , string nombrePreferencia)
    {

      Usuario usuario = new Usuario();
      List<Categoria> preferencias = new List<Categoria>();
      Categoria categoria = new Categoria();
      PeticionPerfil peticion;

      usuario.NombreUsuario = nombreUsuario;
      usuario.Preferencias = preferencias;

      //Si todo funciona como debe ser el enviará la lista de preferencias del usuario,
      //en caso contrario retornará Null

      try
      {

        peticion = new PeticionPerfil();
        //Busco el nombre de usuario
        usuario.Id = peticion.ConsultarIdDelUsuario( usuario.NombreUsuario );

        if (usuario.Id == -1) 
        {

          return null; //No hay usuario en la bdd con -1

        }

        else //Se agrega al array de objetos y se busca el id en la BDD.
        {

          categoria.Id = peticion.ConsultarIdDeCategoria(categoria.Nombre);
          usuario.AgregarPreferencia(categoria);
          peticion.AgregarPreferencia(usuario.Id, categoria.Id);
          return usuario.Preferencias;

        }
        

      }
      catch (NpgsqlException e)
      {

        return null;

      }

    }

    // GET api/<controller>/<action>/prefencia
    [HttpGet]
    public string Hola() {

      return "Hola";

    }
  }

}
