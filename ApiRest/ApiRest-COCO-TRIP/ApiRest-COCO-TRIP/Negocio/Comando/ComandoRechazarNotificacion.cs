using System.Collections.Generic;
using ApiRest_COCO_TRIP.Datos.Entity;
using ApiRest_COCO_TRIP.Datos.DAO;
using ApiRest_COCO_TRIP.Datos.Fabrica;

namespace ApiRest_COCO_TRIP.Negocio.Comando
{
  /// <summary>
  /// Rechaza la solicitud de amistad
  /// </summary>
  public class ComandoRechazarNotificacion : Comando
  {
    private Amigo amigo;
    private Usuario usuario;

    private DAOUsuario baseUsuario;
    private DAOAmigo baseAmigo;

    public ComandoRechazarNotificacion (int id, string nombreRechazado)
    {
      amigo = FabricaEntidad.CrearEntidadAmigo();
      usuario = FabricaEntidad.CrearEntidadUsuario();

      amigo.Pasivo = id;
      usuario.NombreUsuario = nombreRechazado;
    }

    public override void Ejecutar()
    {
      baseUsuario = FabricaDAO.CrearDAOUsuario();
      usuario = (Usuario) baseUsuario.ConsultarId(usuario);

      baseAmigo = FabricaDAO.CrearDAOAmigo();
      amigo.Activo = usuario.Id;
      baseAmigo.RechazarNotificacion(amigo);
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
