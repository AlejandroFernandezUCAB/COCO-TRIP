using System.Collections.Generic;
using ApiRest_COCO_TRIP.Datos.Entity;
using ApiRest_COCO_TRIP.Datos.Fabrica;
using ApiRest_COCO_TRIP.Datos.DAO;

namespace ApiRest_COCO_TRIP.Negocio.Command
{
  /// <summary>
  /// Verifica si un usuario es lider de un grupo o solo un integrante. Si no es lider retorna una excepcion
  /// </summary>
  public class ComandoVerificarLider : Comando
  {
    private Usuario usuario;
    private Usuario lider;
    private Grupo grupo;

    private DAOGrupo datos;

    public ComandoVerificarLider(int idGrupo, int idUsuario)
    {
      usuario = FabricaEntidad.CrearEntidadUsuario();
      lider = FabricaEntidad.CrearEntidadUsuario();
      grupo = FabricaEntidad.CrearEntidadGrupo();

      grupo.Id = idGrupo;
      usuario.Id = idUsuario;
    }

    public override void Ejecutar()
    {
      datos = FabricaDAO.CrearDAOGrupo();
      lider = (Usuario) datos.ConsultarLider(grupo);

      if(lider.Id != usuario.Id) //Si no es el lider retorna una excepcion
      {
        //Excepcion
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
