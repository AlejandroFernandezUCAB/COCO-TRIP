using System.Collections.Generic;
using ApiRest_COCO_TRIP.Datos.Entity;
using ApiRest_COCO_TRIP.Datos.DAO;
using ApiRest_COCO_TRIP.Datos.Fabrica;

namespace ApiRest_COCO_TRIP.Negocio.Comando
{
  /// <summary>
  /// Procedimiento que se encarga de recoger los datos de
  /// la base de datos para visualizar la lista de amigos
  /// </summary>
  public class ComandoVisualizarListaAmigos : Comando
  {
    private List<Entidad> lista;
    private Usuario usuario;

    private DAO datos;

    public ComandoVisualizarListaAmigos(int id)
    {
      usuario = FabricaEntidad.CrearEntidadUsuario();
      usuario.Id = id;
    }

    public override void Ejecutar()
    {
      datos = FabricaDAO.CrearDAOAmigo();
      lista = datos.ConsultarLista(usuario);
    }

    public override Entidad Retornar()
    {
      throw new System.NotImplementedException();
    }

    public override List<Entidad> RetornarLista()
    {
      return lista;
    }
  }
}
