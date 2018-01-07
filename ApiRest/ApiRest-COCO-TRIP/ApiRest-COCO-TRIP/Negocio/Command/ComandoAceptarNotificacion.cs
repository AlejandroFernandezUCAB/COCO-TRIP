using System.Collections.Generic;
using ApiRest_COCO_TRIP.Datos.Entity;
using ApiRest_COCO_TRIP.Datos.DAO;
using ApiRest_COCO_TRIP.Datos.Fabrica;
using ApiRest_COCO_TRIP.Comun.Excepcion;
using System.Net;
using System.Web.Http;
using NLog;

namespace ApiRest_COCO_TRIP.Negocio.Command
{
  /// <summary>
  /// Acepta la solicitud de amistad de un usuario
  /// </summary>
  public class ComandoAceptarNotificacion : Comando
  {
    private Amigo amigo;
    private Usuario usuario;

    private DAOUsuario baseUsuario;
    private DAOAmigo baseAmigo;

    private static Logger log = LogManager.GetCurrentClassLogger();

    public ComandoAceptarNotificacion(int id, string nombreAceptado)
    {
      amigo = FabricaEntidad.CrearEntidadAmigo();
      usuario = FabricaEntidad.CrearEntidadUsuario();

      amigo.Pasivo = id;
      usuario.NombreUsuario = nombreAceptado;
    }

    public override void Ejecutar()
    {
      try
      {
        baseUsuario = FabricaDAO.CrearDAOUsuario();
        usuario = (Usuario)baseUsuario.ConsultarPorNombre(usuario);

        baseAmigo = FabricaDAO.CrearDAOAmigo();
        amigo.Activo = usuario.Id;
        baseAmigo.AceptarNotificacion(amigo);

        log.Info("Id:" + amigo.Pasivo + " Nombre: " + usuario.NombreUsuario);
      }
      catch (BaseDeDatosExcepcion e)
      {
        e.DatosAsociados = "Id:" + amigo.Pasivo + " Nombre: " + usuario.NombreUsuario;
        log.Error(e.Mensaje + "|" + e.DatosAsociados);
        throw new HttpResponseException(HttpStatusCode.InternalServerError);
      }
      catch (CasteoInvalidoExcepcion e)
      {
        log.Warn(e.Mensaje);
        throw new HttpResponseException(HttpStatusCode.BadRequest);
      }
    }

    public override Entidad Retornar()
    {
      throw new System.NotImplementedException();
    }

    public override List<Entidad> RetornarLista()
    {
      throw new System.NotImplementedException();
    }
  }
  
}
