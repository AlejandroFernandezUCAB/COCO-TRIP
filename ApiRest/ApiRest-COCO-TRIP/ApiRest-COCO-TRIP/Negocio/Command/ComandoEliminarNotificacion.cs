using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ApiRest_COCO_TRIP.Datos.Entity;
using ApiRest_COCO_TRIP.Datos.Fabrica;
using ApiRest_COCO_TRIP.Datos.DAO;

namespace ApiRest_COCO_TRIP.Negocio.Command
{
    /// <summary>
    /// Comando que elimina las notificaciones del usuario
    /// </summary>
    public class ComandoEliminarNotificacion : Comando
  {
    Notificacion notificacion = FabricaEntidad.CrearEntidadNotificacion();
    DAONotificacion dAONotificacion = FabricaDAO.CrearDAONotifiacacion();
        /// <summary>
        /// Constructor de la clase
        /// </summary>
        /// <param name="idUsuario">Id del usuario</param>
        public ComandoEliminarNotificacion(int idUsuario)
    {
      notificacion.IdUsuario = idUsuario;
    }

    public override void Ejecutar()
    {
      dAONotificacion.Eliminar(notificacion);
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
