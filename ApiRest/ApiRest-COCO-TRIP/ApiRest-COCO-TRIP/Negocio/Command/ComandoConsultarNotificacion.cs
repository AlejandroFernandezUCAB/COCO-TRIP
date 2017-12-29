using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ApiRest_COCO_TRIP.Datos.Entity;
using ApiRest_COCO_TRIP.Datos.Fabrica;
using ApiRest_COCO_TRIP.Datos.DAO;

namespace ApiRest_COCO_TRIP.Negocio.Command
{
  public class ComandoConsultarNotificacion : Comando
  {
    
    Notificacion notificacion;
    DAONotificacion dAONotificacion = FabricaDAO.CrearDAONotifiacacion();
    public ComandoConsultarNotificacion(int idUsuario)
    {
      notificacion = FabricaEntidad.CrearEntidadNotificacion();
        notificacion.IdUsuario = idUsuario;
    }

    public override void Ejecutar()
    {
     notificacion =(Notificacion) dAONotificacion.ConsultarPorId(notificacion);
    }

    public override Entidad Retornar()
    {
      return notificacion;
    }

    public override List<Entidad> RetornarLista()
    {
      throw new NotImplementedException();
    }
  }
}
