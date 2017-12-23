using System.Collections.Generic;
using ApiRest_COCO_TRIP.Datos.DAO;
using ApiRest_COCO_TRIP.Datos.Entity;
using ApiRest_COCO_TRIP.Datos.Fabrica;

namespace ApiRest_COCO_TRIP.Negocio.Command
{
  /// <summary>
  /// Procedimiento para agregar un integrante al modificar el grupo
  /// </summary>
  public class ComandoAgregarIntegrante : Comando
  {
    private Usuario usuario;
    private Grupo grupo;

    private DAOUsuario baseUsuario;
    private DAOGrupo baseGrupo;

    public ComandoAgregarIntegrante(int idGrupo, string nombreUsuario)
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
      baseGrupo.AgregarIntegrante(grupo, usuario);
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
