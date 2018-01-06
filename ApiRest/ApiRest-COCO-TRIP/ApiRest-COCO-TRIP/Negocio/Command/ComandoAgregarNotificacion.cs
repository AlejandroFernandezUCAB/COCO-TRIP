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
    /// Clase que llama al dao para agregar una notificacion
    /// </summary>
    public class ComandoAgregarNotificacion : Comando
  {
    private Notificacion notificacion;
    private DAONotificacion dAONotificacion = FabricaDAO.CrearDAONotifiacacion();
        /// <summary>
        /// Constructor de la clase
        /// </summary>
        /// <param name="idUsuario">id del usuario</param>
        public ComandoAgregarNotificacion(int idUsuario)
    {
      notificacion = FabricaEntidad.CrearEntidadNotificacion();
      notificacion.IdUsuario = idUsuario;
    }

    public override void Ejecutar()
    {
      dAONotificacion.Insertar(notificacion);
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
