using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ApiRest_COCO_TRIP.Datos.Entity;
using ApiRest_COCO_TRIP.Datos.DAO;
using ApiRest_COCO_TRIP.Datos.Fabrica;
namespace ApiRest_COCO_TRIP.Negocio.Command
{
  public class ComandoModificarNotificacion : Comando
  {
    Notificacion notificacion = FabricaEntidad.CrearEntidadNotificacion();
    DAONotificacion dAONotificacion = FabricaDAO.CrearDAONotifiacacion();
    public ComandoModificarNotificacion(int idUsuario, Boolean push, Boolean correo)
    {
      notificacion.Correo = correo;
      notificacion.Push = push;
      notificacion.IdUsuario = idUsuario;
    }

    public override void Ejecutar()
    {
      dAONotificacion.Actualizar(notificacion);
    }

    public override Entidad Retornar()
    {
      return dAONotificacion.ConsultarPorId(notificacion);
    }

    public override List<Entidad> RetornarLista()
    {
      throw new NotImplementedException();
    }
  }
}
