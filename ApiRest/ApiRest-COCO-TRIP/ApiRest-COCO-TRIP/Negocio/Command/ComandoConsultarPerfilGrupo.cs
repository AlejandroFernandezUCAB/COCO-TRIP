using System.Collections.Generic;
using ApiRest_COCO_TRIP.Datos.Entity;
using ApiRest_COCO_TRIP.Datos.Fabrica;
using ApiRest_COCO_TRIP.Datos.DAO;

namespace ApiRest_COCO_TRIP.Negocio.Command
{
  /// <summary>
  /// Procedimiento para visualizar el perfil del grupo
  /// </summary>
  public class ComandoConsultarPerfilGrupo : Comando
  {
    private Grupo grupo;
    private DAOGrupo datos;

    public ComandoConsultarPerfilGrupo(int id)
    {
      grupo = FabricaEntidad.CrearEntidadGrupo();
      grupo.Id = id;
    }

    public override void Ejecutar()
    {
      datos = FabricaDAO.CrearDAOGrupo();
      grupo = (Grupo) datos.ConsultarPorId(grupo);
    }

    public override Entidad Retornar()
    {
      return grupo;
    }

    public override List<Entidad> RetornarLista()
    {
      throw new System.NotImplementedException();
    }
  }
}
