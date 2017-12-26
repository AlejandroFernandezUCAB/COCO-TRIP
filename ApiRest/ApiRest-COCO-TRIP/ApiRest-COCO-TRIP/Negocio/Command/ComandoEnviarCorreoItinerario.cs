using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ApiRest_COCO_TRIP.Datos.Entity;
using ApiRest_COCO_TRIP.Datos.Fabrica;
using ApiRest_COCO_TRIP.Datos.DAO;

namespace ApiRest_COCO_TRIP.Negocio.Command
{
  public class ComandoEnviarCorreoItinerario : Comando
  {
    Usuario usuario;
    DAONotificacion dAONotificacion;
    Boolean respuesta;
    public ComandoEnviarCorreoItinerario(int idUsuario)
    {
      usuario = FabricaEntidad.CrearEntidadUsuario();
      dAONotificacion = FabricaDAO.CrearDAONotifiacacion();
      usuario.Id = idUsuario;
    }

    public bool Respuesta { get => respuesta; set => respuesta = value; }

    public override void Ejecutar()
    {
      respuesta = dAONotificacion.EnviarCorreo(usuario);
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
