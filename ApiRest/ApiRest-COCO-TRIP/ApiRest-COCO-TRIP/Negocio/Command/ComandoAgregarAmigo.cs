using System.Collections.Generic;
using ApiRest_COCO_TRIP.Datos.Entity;
using ApiRest_COCO_TRIP.Datos.DAO;
using ApiRest_COCO_TRIP.Datos.Fabrica;
using ApiRest_COCO_TRIP.Comun.Excepcion;
using System.Web.Http;
using System.Net;
using NLog;

namespace ApiRest_COCO_TRIP.Negocio.Command
{
  /// <summary>
  /// Agrega una amistad pendiente
  /// </summary>
  public class ComandoAgregarAmigo : Comando
  {
    private Usuario usuario;
    private Amigo amigo;

    private DAOUsuario baseUsuario;
    private DAOAmigo baseAmigo;

    private static Logger log = LogManager.GetCurrentClassLogger();

    /// <summary>
    /// Constructor del comando
    /// </summary>
    public ComandoAgregarAmigo (int id, string nombre)
    {
      usuario = FabricaEntidad.CrearEntidadUsuario();
      amigo = FabricaEntidad.CrearEntidadAmigo();

      usuario.NombreUsuario = nombre;
      amigo.Activo = id;
    }

    public override void Ejecutar()
    {
      try
      {
        baseUsuario = FabricaDAO.CrearDAOUsuario();
        usuario = (Usuario)baseUsuario.ConsultarPorNombre(usuario);

        baseAmigo = FabricaDAO.CrearDAOAmigo();
        amigo.Pasivo = usuario.Id;
        amigo = (Amigo)baseAmigo.ConsultarPorId(amigo);

        if (amigo.Id == 0)
        {
          baseAmigo.Insertar(amigo);
          log.Info("Id:" + amigo.Activo + " Nombre: " + usuario.NombreUsuario);
        }
        else
        {
          log.Warn("Ya existe la peticion de amistad|" +
          "Id:" + amigo.Activo + " Nombre: " + usuario.NombreUsuario);
        }
      }
      catch (BaseDeDatosExcepcion e)
      {
        e.DatosAsociados = "Id:" + amigo.Activo + " Nombre: " + usuario.NombreUsuario;
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
