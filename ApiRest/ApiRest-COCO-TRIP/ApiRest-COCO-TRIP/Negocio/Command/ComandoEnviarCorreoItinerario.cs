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
    /// Comando que agrega el enviar correos al usuario
    /// </summary>
    /// <param name="idUsuario">Id del usuario</param>
    public class ComandoEnviarCorreoItinerario : Comando
  {
    Usuario usuario;
    DAONotificacion dAONotificacion;
    Boolean respuesta;
        /// <summary>
        /// Constructor de la clase
        /// </summary>
        /// <param name="idUsuario">Id del usuario</param>
        public ComandoEnviarCorreoItinerario(int idUsuario)
    {
      usuario = FabricaEntidad.CrearEntidadUsuario();
      dAONotificacion = FabricaDAO.CrearDAONotifiacacion();
      usuario.Id = idUsuario;
    }

    public override void Ejecutar()
    {
      respuesta = dAONotificacion.EnviarCorreo(usuario);
    }

    public override Entidad Retornar()
    {
      usuario.Valido = respuesta;
      return usuario;
    }

    public override List<Entidad> RetornarLista()
    {
      throw new NotImplementedException();
    }
  }
}
