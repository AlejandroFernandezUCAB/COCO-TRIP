using System.Collections.Generic;
using ApiRest_COCO_TRIP.Datos.Entity;
using ApiRest_COCO_TRIP.Datos.DAO;
using ApiRest_COCO_TRIP.Datos.Fabrica;

namespace ApiRest_COCO_TRIP.Negocio.Comando
{
  public class ComandoVisualizarPerfilAmigo : Comando
  {
    private Usuario usuario;
    private DAO datos;

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
