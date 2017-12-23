using System.Collections.Generic;
using ApiRest_COCO_TRIP.Datos.Entity;
using ApiRest_COCO_TRIP.Datos.DAO;
using ApiRest_COCO_TRIP.Datos.Fabrica;

namespace ApiRest_COCO_TRIP.Negocio.Command
{
  /// <summary>
  /// Agrega una amistad pendiente
  /// </summary>
  public class ComandoAgregarAmigo : Comando
  {
    private Usuario usuario;
    private Amigo amigo;

    private DAOUsuario baseUsuario;
    private DAOAmigo baseAmigo;

    /// <summary>
    /// Constructor del comando
    /// </summary>
    public ComandoAgregarAmigo (int id, string nombre)
    {
      usuario = FabricaEntidad.CrearEntidadUsuario();
      amigo = FabricaEntidad.CrearEntidadAmigo();

      usuario.NombreUsuario = nombre;
      amigo.Activo = id;
    }

    public override void Ejecutar()
    {
      baseUsuario = FabricaDAO.CrearDAOUsuario();
      usuario = (Usuario) baseUsuario.ConsultarPorNombre(usuario);

      baseAmigo = FabricaDAO.CrearDAOAmigo();
      amigo.Pasivo = usuario.Id;
      amigo = (Amigo) baseAmigo.ConsultarId(amigo);

      if(amigo.Id == 0)
      {
        baseAmigo.Insertar(amigo);
      }
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
