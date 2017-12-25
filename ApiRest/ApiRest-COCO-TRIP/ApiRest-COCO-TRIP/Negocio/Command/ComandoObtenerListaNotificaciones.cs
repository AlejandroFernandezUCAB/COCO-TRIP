using System.Collections.Generic;
using ApiRest_COCO_TRIP.Datos.Entity;
using ApiRest_COCO_TRIP.Datos.DAO;
using ApiRest_COCO_TRIP.Datos.Fabrica;
using System.Web.Http;
using System.Net;
using ApiRest_COCO_TRIP.Comun.Excepcion;

namespace ApiRest_COCO_TRIP.Negocio.Command
{
  /// <summary>
  /// Obtiene la lista de notificaciones pendientes de un usuario
  /// </summary>
  public class ComandoObtenerListaNotificaciones : Comando
  {
    private Usuario usuario;
    private List<Entidad> lista;

    private DAOAmigo datos;

    public ComandoObtenerListaNotificaciones (int id)
    {
      usuario = FabricaEntidad.CrearEntidadUsuario();
      lista = new List<Entidad>();

      usuario.Id = id;
    }

    public override void Ejecutar()
    {
      try
      {
        datos = FabricaDAO.CrearDAOAmigo();
        lista = datos.ConsultarListaNotificaciones(usuario);
      }
      catch (BaseDeDatosExcepcion e)
      {
        e.DatosAsociados = "Id:" + usuario.Id;
        e.NombreMetodos = this.GetType().FullName;
        throw new HttpResponseException(HttpStatusCode.InternalServerError);
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
