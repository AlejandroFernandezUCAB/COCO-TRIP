using Npgsql;
using System.Web.Http;
using ApiRest_COCO_TRIP.Models;
using System.Collections.Generic;
namespace ApiRest_COCO_TRIP.Controllers
{
  public class M2_PreferenciasController : ApiController
  {

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

      //Si todo funciona como debe ser el enviará la lista de preferencias del usuario, en caso contrario retornará Null

      try
      {

        usuario.AgregarPreferencia(categoria);
        return usuario.Preferencias;

      }
      catch (NpgsqlException e)
      {

        return null;

      }

    }
  }

}
