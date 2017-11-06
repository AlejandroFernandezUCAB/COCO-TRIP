using Npgsql;
using System.Web.Http;
using ApiRest_COCO_TRIP.Models;
using System.Collections.Generic;
namespace ApiRest_COCO_TRIP.Controllers
{
  public class M2_PreferenciasController : ApiController
  {

    private PeticionPerfil peticion;

    /// <summary>
    /// Metodo Post para agregar una preferencia del usuario, hará una llamda a base de datos para buscar id de usuario
    /// y id de categoria para agregarlo en la tabla de preferencias
    /// </summary>
    /// <param name="id">Id de categoria</param>
    /// <param name="nombre">Nombre de la categoria</param>
    /// <param name="descripcion">Descripcion de la categoria</param>
    /// <param name="estatus">Estatus de la categoria</param>
    /// <param name="nivel">Nivel de la categoria</param>
    /// <param name="nombreUsuario">Nombre del usuario</param>
    /// <returns>Lista de categorias del usuario</returns>
    // GET api/<controller>/<action>/prefencia
    [HttpPost]
    public List<Categoria> AgregarPreferencia( int id, string nombre, string descripcion,
                                                bool estatus, int nivel, string nombreUsuario)
    {

      Usuario usuario = new Usuario();
      List<Categoria> preferencias = new List<Categoria>();
      Categoria categoria = new Categoria( id, nombre, descripcion, estatus, nivel);

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
  }

}
