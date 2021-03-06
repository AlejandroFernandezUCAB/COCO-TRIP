using System.Collections.Generic;
using ApiRest_COCO_TRIP.Datos.Entity;
using ApiRest_COCO_TRIP.Datos.Fabrica;
using ApiRest_COCO_TRIP.Datos.DAO;
using System.Net;
using System.Web.Http;
using ApiRest_COCO_TRIP.Comun.Excepcion;
using NLog;

namespace ApiRest_COCO_TRIP.Negocio.Command
{
  /// <summary>
  /// Verifica si un usuario es lider de un grupo o solo un integrante. Si no es lider retorna una excepcion
  /// </summary>
  public class ComandoVerificarLider : Comando
  {
    private Usuario usuario;
    private Usuario lider;
    private Grupo grupo;

    private DAOGrupo datos;

    private static Logger log = LogManager.GetCurrentClassLogger();

    public ComandoVerificarLider(int idGrupo, int idUsuario)
    {
      usuario = FabricaEntidad.CrearEntidadUsuario();
      lider = FabricaEntidad.CrearEntidadUsuario();
      grupo = FabricaEntidad.CrearEntidadGrupo();

      grupo.Id = idGrupo;
      usuario.Id = idUsuario;
    }

    public override void Ejecutar()
    {
      try
      {
        datos = FabricaDAO.CrearDAOGrupo();
        lider = (Usuario)datos.ConsultarLider(grupo);

        if (lider.Id != usuario.Id) //Si no es el lider retorna una excepcion
        {
          log.Info("No es lider|IdGrupo: " + grupo.Id + " IdUsuario: " + usuario.Id);
          throw new HttpResponseException(HttpStatusCode.Unauthorized);
        }
        else
        {
          log.Info("Lider|IdGrupo: " + grupo.Id + " IdUsuario: " + usuario.Id);
        }
      }
      catch (BaseDeDatosExcepcion e)
      {
        e.DatosAsociados = "IdGrupo: " + grupo.Id + " IdUsuario: " + usuario.Id;
        log.Error(e.Mensaje + "|" + e.DatosAsociados);
        throw new HttpResponseException(HttpStatusCode.InternalServerError);
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
