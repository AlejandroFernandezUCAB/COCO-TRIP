using System.Collections.Generic;
using ApiRest_COCO_TRIP.Datos.Entity;
using ApiRest_COCO_TRIP.Datos.DAO;
using ApiRest_COCO_TRIP.Datos.Fabrica;

namespace ApiRest_COCO_TRIP.Negocio.Command
{
  /// <summary>
  /// Busca usuarios que coincidan con la busqueda y no pertenezcan a la lista de amigos del usuario
  /// </summary>
  public class ComandoBuscarAmigos : Comando
  {
    private Usuario usuario;
    private List<Entidad> lista;

    private DAOAmigo datos;

    public ComandoBuscarAmigos(int id, string nombre)
    {
      usuario = FabricaEntidad.CrearEntidadUsuario();
      lista = new List<Entidad>();

      usuario.Id = id;
      usuario.Nombre = nombre;
    }

    public override void Ejecutar()
    {
      datos = FabricaDAO.CrearDAOAmigo();
      lista = datos.BuscarAmigos(usuario);
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
