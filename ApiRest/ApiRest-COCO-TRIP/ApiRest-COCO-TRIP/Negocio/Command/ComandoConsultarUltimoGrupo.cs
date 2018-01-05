using System.Collections.Generic;
using ApiRest_COCO_TRIP.Datos.Entity;
using ApiRest_COCO_TRIP.Datos.Fabrica;
using ApiRest_COCO_TRIP.Datos.Singleton;
using ApiRest_COCO_TRIP.Datos.DAO;
using ApiRest_COCO_TRIP.Comun.Excepcion;
using System.Web.Http;
using System.Net;
using NLog;

namespace ApiRest_COCO_TRIP.Negocio.Command
{
  /// <summary>
  /// Metodo para obtener el identificador del ultimo grupo agregado de un usuario
  /// </summary>
  public class ComandoConsultarUltimoGrupo : Comando
  {
    private Usuario usuario;
    private Grupo grupo;

    private DAOGrupo datos;
    private Archivo archivo;

    private static Logger log = LogManager.GetCurrentClassLogger();

    public ComandoConsultarUltimoGrupo(int idUsuario)
    {
      usuario = FabricaEntidad.CrearEntidadUsuario();
      grupo = FabricaEntidad.CrearEntidadGrupo();

      usuario.Id = idUsuario;
    }

    public override void Ejecutar()
    {
      try
      {
        datos = FabricaDAO.CrearDAOGrupo();
        archivo = Archivo.ObtenerInstancia();
        grupo = (Grupo) datos.ConsultarUltimoGrupo(usuario);

        if (grupo.RutaFoto != null)
        {
          grupo.RutaFoto = Archivo.Ruta + grupo.RutaFoto + Archivo.Extension;
        }

        log.Info("Id: " + usuario.Id);
      }
      catch (BaseDeDatosExcepcion e)
      {
        e.DatosAsociados = "Id: " + usuario.Id;
        log.Error(e.Mensaje + "|" + e.DatosAsociados);
        throw new HttpResponseException(HttpStatusCode.InternalServerError);
      }
      catch (IOExcepcion e)
      {
        e.DatosAsociados = "Id: " + usuario.Id;
        log.Error(e.Mensaje + "|" + e.DatosAsociados);
        throw new HttpResponseException(HttpStatusCode.InternalServerError);
      }
    }

    public override Entidad Retornar()
    {
      return grupo;
    }

    public override List<Entidad> RetornarLista()
    {
      throw new System.NotImplementedException();
    }
  }
}
