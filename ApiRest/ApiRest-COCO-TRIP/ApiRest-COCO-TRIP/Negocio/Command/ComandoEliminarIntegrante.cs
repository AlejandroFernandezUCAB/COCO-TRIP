using System.Collections.Generic;
using ApiRest_COCO_TRIP.Datos.DAO;
using ApiRest_COCO_TRIP.Datos.Entity;
using ApiRest_COCO_TRIP.Datos.Fabrica;
using ApiRest_COCO_TRIP.Comun.Excepcion;
using System.Web.Http;
using System.Net;
using NLog;

namespace ApiRest_COCO_TRIP.Negocio.Command
{
  /// <summary>
  /// Procedimiento para eliminar un integrante del grupo al modificar
  /// </summary>
  public class ComandoEliminarIntegrante : Comando
  {
    private Usuario usuario;
    private Grupo grupo;

    private DAOUsuario baseUsuario;
    private DAOGrupo baseGrupo;

    private static Logger log = LogManager.GetCurrentClassLogger();

    public ComandoEliminarIntegrante(int idGrupo, string nombreUsuario)
    {
      usuario = FabricaEntidad.CrearEntidadUsuario();
      grupo = FabricaEntidad.CrearEntidadGrupo();

      usuario.NombreUsuario = nombreUsuario;
      grupo.Id = idGrupo;
    }

    public override void Ejecutar()
    {
      try
      {
        baseUsuario = FabricaDAO.CrearDAOUsuario();
        baseGrupo = FabricaDAO.CrearDAOGrupo();

        usuario = (Usuario)baseUsuario.ConsultarPorNombre(usuario);
        baseGrupo.EliminarIntegrante(grupo, usuario);

        log.Info("IdGrupo: " + grupo.Id + " Nombre: " + usuario.NombreUsuario);
      }
      catch (BaseDeDatosExcepcion e)
      {
        e.DatosAsociados = "IdGrupo: " + grupo.Id + " Nombre: " + usuario.NombreUsuario;
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
