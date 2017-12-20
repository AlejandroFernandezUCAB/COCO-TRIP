using System.Collections.Generic;
using ApiRest_COCO_TRIP.Datos.Entity;
using ApiRest_COCO_TRIP.Datos.DAO;
using ApiRest_COCO_TRIP.Datos.Fabrica;

namespace ApiRest_COCO_TRIP.Negocio.Comando
{
  /// <summary>
  /// Agrega una amistad pendiente
  /// </summary>
  public class ComandoAgregarAmigo : Comando
  {
    private Usuario usuario;
    private Amigo amigo;

    private DAO datos;

    /// <summary>
    /// Constructor del comando
    /// </summary>
    public ComandoAgregarAmigo (int id, string nombre)
    {
      usuario = FabricaEntidad.CrearEntidadUsuario();
      amigo = FabricaEntidad.CrearEntidadAmigo();

      usuario.NombreUsuario = nombre;
      amigo.Activo = id;
    }

    public override void Ejecutar()
    {
      datos = FabricaDAO.CrearDAOUsuario();
      usuario = (Usuario) datos.ConsultarId(usuario);

      datos = FabricaDAO.CrearDAOAmigo();
      amigo.Pasivo = usuario.Id;
      amigo = (Amigo) datos.ConsultarId(amigo);

      if(amigo.Id == 0)
      {
        datos.Insertar(amigo);
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
