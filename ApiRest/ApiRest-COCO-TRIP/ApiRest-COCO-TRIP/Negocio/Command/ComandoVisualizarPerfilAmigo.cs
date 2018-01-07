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
  /// Solicita a la base de datos informacion del usuario que se desea visualizar
  /// </summary>
  public class ComandoVisualizarPerfilAmigo : Comando
  {
    private Usuario usuario;
    private DAOUsuario datos;

    private static Logger log = LogManager.GetCurrentClassLogger();

    public ComandoVisualizarPerfilAmigo (string nombre)
    {
      usuario = FabricaEntidad.CrearEntidadUsuario();
      usuario.NombreUsuario = nombre;
    }
    public override void Ejecutar()
    {
      try
      {
        datos = FabricaDAO.CrearDAOUsuario();
        usuario = (Usuario)datos.ConsultarPorNombre(usuario);

        log.Info("NombreUsuario: " + usuario.NombreUsuario);
      }
      catch (BaseDeDatosExcepcion e)
      {
        e.DatosAsociados = "NombreUsuario: " + usuario.NombreUsuario;
        log.Error(e.Mensaje + "|" + e.DatosAsociados);
        throw new HttpResponseException(HttpStatusCode.InternalServerError);
      }
      catch (CasteoInvalidoExcepcion e)
      {
        log.Error(e.Mensaje);
        throw new HttpResponseException(HttpStatusCode.BadRequest);
      }
    }

    public override Entidad Retornar()
    {
      return usuario;
    }

    public override List<Entidad> RetornarLista()
    {
      throw new System.NotImplementedException();
    }
  }
}
