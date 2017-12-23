using System.Collections.Generic;
using ApiRest_COCO_TRIP.Datos.DAO;
using ApiRest_COCO_TRIP.Datos.Entity;
using ApiRest_COCO_TRIP.Datos.Fabrica;

namespace ApiRest_COCO_TRIP.Negocio.Comando
{
  /// <summary>
  /// Procedimiento para eliminar un integrante del grupo al modificar
  /// </summary>
  public class ComandoEliminarIntegrante : Comando
  {
    private Usuario usuario;
    private Grupo grupo;

    private DAOUsuario baseUsuario;
    private DAOGrupo baseGrupo;

    public ComandoEliminarIntegrante(int idGrupo, string nombreUsuario)
    {
      usuario = FabricaEntidad.CrearEntidadUsuario();
      grupo = FabricaEntidad.CrearEntidadGrupo();

      usuario.NombreUsuario = nombreUsuario;
      grupo.Id = idGrupo;
    }

    public override void Ejecutar()
    {
      baseUsuario = FabricaDAO.CrearDAOUsuario();
      baseGrupo = FabricaDAO.CrearDAOGrupo();

      usuario = (Usuario) baseUsuario.ConsultarPorNombre(usuario);
      baseGrupo.EliminarIntegrante(grupo, usuario);
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