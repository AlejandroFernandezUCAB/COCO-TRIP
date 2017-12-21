using System.Collections.Generic;
using ApiRest_COCO_TRIP.Datos.Entity;
using ApiRest_COCO_TRIP.Datos.DAO;
using ApiRest_COCO_TRIP.Datos.Fabrica;

namespace ApiRest_COCO_TRIP.Negocio.Comando
{
  /// <summary>
  /// Procemiento que se encarga de hacer la peticion para
  /// eliminar un amigo de la base de datos
  /// </summary>
  public class ComandoEliminarAmigo : Comando
  {
    private Usuario usuario;
    private Amigo amigo;

    private DAOUsuario baseUsuario;
    private DAOAmigo baseAmigo;

    public ComandoEliminarAmigo (int id, string nombreAmigo)
    {
      usuario = FabricaEntidad.CrearEntidadUsuario();
      amigo = FabricaEntidad.CrearEntidadAmigo();

      usuario.NombreUsuario = nombreAmigo;
      amigo.Activo = id;
    }

    public override void Ejecutar()
    {
      baseUsuario = FabricaDAO.CrearDAOUsuario();
      baseAmigo = FabricaDAO.CrearDAOAmigo();

      usuario = (Usuario) baseUsuario.ConsultarPorNombre(usuario);
      amigo.Pasivo = usuario.Id;

      baseAmigo.Eliminar(amigo);
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
