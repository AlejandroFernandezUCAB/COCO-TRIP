using System.Collections.Generic;
using ApiRest_COCO_TRIP.Datos.Entity;
using ApiRest_COCO_TRIP.Datos.Fabrica;
using ApiRest_COCO_TRIP.Datos.DAO;

namespace ApiRest_COCO_TRIP.Negocio.Command
{
  /// <summary>
  /// Metodo para obtener el identificador del ultimo grupo agregado de un usuario
  /// </summary>
  public class ComandoConsultarUltimoGrupo : Comando
  {
    private Usuario usuario;
    private Grupo grupo;

    private DAOGrupo datos;

    public ComandoConsultarUltimoGrupo(int idUsuario)
    {
      usuario = FabricaEntidad.CrearEntidadUsuario();
      grupo = FabricaEntidad.CrearEntidadGrupo();

      usuario.Id = idUsuario;
    }

    public override void Ejecutar()
    {
      datos = FabricaDAO.CrearDAOGrupo();
      grupo = (Grupo) datos.ConsultarUltimoGrupo (usuario);
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
