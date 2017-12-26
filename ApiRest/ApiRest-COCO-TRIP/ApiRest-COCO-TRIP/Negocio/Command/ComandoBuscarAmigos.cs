using System.Collections.Generic;
using ApiRest_COCO_TRIP.Datos.Entity;
using ApiRest_COCO_TRIP.Datos.DAO;
using ApiRest_COCO_TRIP.Datos.Fabrica;
using ApiRest_COCO_TRIP.Comun.Excepcion;
using System.Net;
using System.Web.Http;

namespace ApiRest_COCO_TRIP.Negocio.Command
{
  /// <summary>
  /// Busca usuarios que coincidan con la busqueda y no pertenezcan a la lista de amigos del usuario
  /// </summary>
  public class ComandoBuscarAmigos : Comando
  {
    private Usuario usuario;
    private List<Entidad> lista;

    private DAOAmigo datos;

    public ComandoBuscarAmigos(int id, string nombre)
    {
      usuario = FabricaEntidad.CrearEntidadUsuario();
      lista = new List<Entidad>();

      usuario.Id = id;
      usuario.Nombre = nombre;
    }

    public override void Ejecutar()
    {
      try
      {
        datos = FabricaDAO.CrearDAOAmigo();
        lista = datos.BuscarAmigos(usuario);
      }
      catch (BaseDeDatosExcepcion e)
      {
        e.DatosAsociados = "Id" + usuario.Id + " Nombre:" + usuario.Nombre;
        e.NombreMetodos = this.GetType().FullName;
        throw new HttpResponseException(HttpStatusCode.InternalServerError);
      }
      catch (CasteoInvalidoExcepcion e)
      {
        e.NombreMetodos = this.GetType().FullName;
        throw new HttpResponseException(HttpStatusCode.BadRequest);
      }
    }

    public override Entidad Retornar()
    {
      throw new System.NotImplementedException();
    }

    public override List<Entidad> RetornarLista()
    {
      return lista;
    }
  }
}
