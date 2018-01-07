using System;
using System.Collections.Generic;
using ApiRest_COCO_TRIP.Datos.Entity;
using ApiRest_COCO_TRIP.Datos.DAO;
using ApiRest_COCO_TRIP.Datos.Fabrica;
using ApiRest_COCO_TRIP.Datos.Singleton;
using ApiRest_COCO_TRIP.Comun.Excepcion;
using System.Net;
using System.Web.Http;
using NLog;

namespace ApiRest_COCO_TRIP.Negocio.Command
{
  /// <summary>
  /// Envia un correo recomendando la aplicacion a un usuario
  /// </summary>
  public class ComandoEnviarNotificacionCorreo : Comando
  {
    private Usuario remitente;
    private Usuario destinatario;

    private DAOUsuario datos;
    private Correo servicio;

    private static Logger log = LogManager.GetCurrentClassLogger();

    public ComandoEnviarNotificacionCorreo (string correo, int id, string nombreDestino)
    {
      remitente = FabricaEntidad.CrearEntidadUsuario();
      destinatario = FabricaEntidad.CrearEntidadUsuario();

      remitente.Id = id;
      destinatario.NombreUsuario = nombreDestino;
      destinatario.Correo = correo;
    }

    public override void Ejecutar()
    {
      try
      {
        datos = FabricaDAO.CrearDAOUsuario();
        remitente = (Usuario)datos.ConsultarPorId(remitente);

        servicio = Correo.ObtenerInstancia();
        servicio.RecomendarAplicacion(destinatario.Correo, destinatario.NombreUsuario, remitente.NombreUsuario);
      
        log.Info("Correo: " + destinatario.Correo + " Id: " + remitente.Id +
            " NombreDestino: " + destinatario.NombreUsuario);
      }
      catch (BaseDeDatosExcepcion e)
      {
        e.DatosAsociados = "Correo: " + destinatario.Correo + " Id: " + remitente.Id +
                    " NombreDestino: " + destinatario.NombreUsuario;
        log.Error(e.Mensaje + "|" + e.DatosAsociados);
        throw new HttpResponseException(HttpStatusCode.InternalServerError);
      }
      catch (SmtpExcepcion e)
      {
        e.DatosAsociados = "Correo: " + destinatario.Correo + " Id: " + remitente.Id +
                    " NombreDestino: " + destinatario.NombreUsuario;
        log.Error(e.Mensaje + "|" + e.DatosAsociados);
        throw new HttpResponseException(HttpStatusCode.InternalServerError);
      }
      catch (ArgumentoNuloExcepcion e)
      {
        log.Warn(e.Mensaje);
        throw new HttpResponseException(HttpStatusCode.BadRequest);
      }
    }

    public override Entidad Retornar()
    {
      throw new NotImplementedException();
    }

    public override List<Entidad> RetornarLista()
    {
      throw new NotImplementedException();
    }
  }
}
