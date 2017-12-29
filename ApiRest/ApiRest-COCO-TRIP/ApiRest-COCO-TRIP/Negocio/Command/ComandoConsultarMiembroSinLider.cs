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
  /// Metodo que devuelve los integrantes de un grupo sin el integrante lider
  /// </summary>
  public class ComandoConsultarMiembroSinLider : Comando
  {
    private Grupo grupo;
    private List<Entidad> lista;

    private DAOGrupo datos;

    private static Logger log = LogManager.GetCurrentClassLogger();

    public ComandoConsultarMiembroSinLider(int id)
    {
      grupo = FabricaEntidad.CrearEntidadGrupo();
      grupo.Id = id;
    }

    public override void Ejecutar()
    {
      try
      {
        datos = FabricaDAO.CrearDAOGrupo();
        lista = datos.ConsultarMiembrosExceptoLider(grupo);
        log.Info("Id: " + grupo.Id);
      }
      catch (BaseDeDatosExcepcion e)
      {
        e.DatosAsociados = "Id: " + grupo.Id;
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
      return lista;
    }
  }
  
}
