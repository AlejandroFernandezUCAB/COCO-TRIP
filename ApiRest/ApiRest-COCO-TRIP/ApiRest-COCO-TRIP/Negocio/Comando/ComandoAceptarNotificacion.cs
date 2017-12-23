using System.Collections.Generic;
using ApiRest_COCO_TRIP.Datos.Entity;
using ApiRest_COCO_TRIP.Datos.DAO;
using ApiRest_COCO_TRIP.Datos.Fabrica;

namespace ApiRest_COCO_TRIP.Negocio.Comando
{
  /// <summary>
  /// Acepta la solicitud de amistad de un usuario
  /// </summary>
  public class ComandoAceptarNotificacion : Comando
  {
    private Amigo amigo;
    private Usuario usuario;

    private DAOUsuario baseUsuario;
    private DAOAmigo baseAmigo;

    public ComandoAceptarNotificacion(int id, string nombreAceptado)
    {
      amigo = FabricaEntidad.CrearEntidadAmigo();
      usuario = FabricaEntidad.CrearEntidadUsuario();

      amigo.Pasivo = id;
      usuario.NombreUsuario = nombreAceptado;
    }

    public override void Ejecutar()
    {
      baseUsuario = FabricaDAO.CrearDAOUsuario();
      usuario = (Usuario) baseUsuario.ConsultarPorNombre(usuario);

      baseAmigo = FabricaDAO.CrearDAOAmigo();
      amigo.Activo = usuario.Id;
      baseAmigo.AceptarNotificacion(amigo);
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
