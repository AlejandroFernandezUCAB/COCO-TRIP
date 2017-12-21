using System.Collections.Generic;
using ApiRest_COCO_TRIP.Datos.Entity;
using ApiRest_COCO_TRIP.Datos.DAO;
using ApiRest_COCO_TRIP.Datos.Fabrica;

namespace ApiRest_COCO_TRIP.Negocio.Comando
{
  /// <summary>
  /// Solicita a la base de datos informacion del usuario que se desea visualizar
  /// </summary>
  public class ComandoVisualizarPerfilAmigo : Comando
  {
    private Usuario usuario;
    private DAOUsuario datos;

    public ComandoVisualizarPerfilAmigo (string nombre)
    {
      usuario = FabricaEntidad.CrearEntidadUsuario();
      usuario.NombreUsuario = nombre;
    }
    public override void Ejecutar()
    {
      datos = FabricaDAO.CrearDAOUsuario();
      usuario = (Usuario) datos.ConsultarPorNombre(usuario);
    }

    public override Entidad Retornar()
    {
      return usuario;
    }

    public override List<Entidad> RetornarLista()
    {
      throw new System.NotImplementedException();
    }
  }
}
