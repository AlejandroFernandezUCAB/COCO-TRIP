using System.Collections.Generic;
using ApiRest_COCO_TRIP.Datos.Entity;
using ApiRest_COCO_TRIP.Datos.Singleton;
using ApiRest_COCO_TRIP.Datos.DAO;
using ApiRest_COCO_TRIP.Datos.Fabrica;
using ApiRest_COCO_TRIP.Comun.Excepcion;
using Newtonsoft.Json;
using System.Web.Http;
using System.Net;
using NLog;
using System;

namespace ApiRest_COCO_TRIP.Negocio.Command
{
  /// <summary>
  /// Agrega un grupo
  /// </summary>
  public class ComandoAgregarGrupo : Comando
  {
    private Grupo grupo;

    private Archivo archivo;
    private DAOGrupo datos;

    private static Logger log = LogManager.GetCurrentClassLogger();

    public ComandoAgregarGrupo (Entidad _grupo)
    {
      grupo = (Grupo) _grupo;
    }

    public override void Ejecutar()
    {
      try
      {
        archivo = Archivo.ObtenerInstancia();
        datos = FabricaDAO.CrearDAOGrupo();

        grupo = (Grupo)datos.InsertarId(grupo);

        if (grupo.ContenidoFoto != null) //Valida si el grupo tiene foto
        {
          archivo.EscribirArchivo(Convert.FromBase64String(grupo.ContenidoFoto), Archivo.FotoGrupo + grupo.Id + Archivo.Extension);
        }

        log.Info(JsonConvert.SerializeObject(grupo));
      }
      catch (BaseDeDatosExcepcion e)
      {
        e.DatosAsociados = JsonConvert.SerializeObject(grupo);
        log.Error(e.Mensaje + "|" + e.DatosAsociados);
        throw new HttpResponseException(HttpStatusCode.InternalServerError);
      }
      catch (IOExcepcion e)
      {
        e.DatosAsociados = JsonConvert.SerializeObject(grupo);
        log.Error(e.Mensaje + "|" + e.DatosAsociados);
        throw new HttpResponseException(HttpStatusCode.InternalServerError);
      }
      catch (ReferenciaNulaExcepcion e)
      {
        log.Warn(e.Mensaje);
        throw new HttpResponseException(HttpStatusCode.BadRequest);
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
