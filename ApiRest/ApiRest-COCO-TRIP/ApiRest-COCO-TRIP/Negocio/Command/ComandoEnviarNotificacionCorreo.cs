using System;
using System.Collections.Generic;
using ApiRest_COCO_TRIP.Datos.Entity;
using ApiRest_COCO_TRIP.Datos.DAO;
using ApiRest_COCO_TRIP.Datos.Fabrica;
using ApiRest_COCO_TRIP.Datos.Singleton;

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
      datos = FabricaDAO.CrearDAOUsuario();
      remitente = (Usuario) datos.ConsultarPorId(remitente);

      servicio = Correo.ObtenerInstancia();
      servicio.RecomendarAplicacion(destinatario.Correo, destinatario.NombreUsuario, remitente.NombreUsuario);
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
