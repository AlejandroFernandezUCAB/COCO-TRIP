using System.Collections.Generic;
using ApiRest_COCO_TRIP.Datos.Entity;
using ApiRest_COCO_TRIP.Datos.Fabrica;

namespace ApiRest_COCO_TRIP.Negocio.Comando
{
  /// <summary>
  /// Agrega una amistad pendiente
  /// </summary>
  public class ComandoAgregarAmigo : Comando
  {
    private Usuario data;

    /// <summary>
    /// Constructor del comando
    /// </summary>
    public ComandoAgregarAmigo(int id, string usuario)
    {
      data = FabricaEntidad.CrearEntidadUsuario();

      data.Id = id;
      data.NombreUsuario = usuario;
    }

    public override void Ejecutar()
    {
      
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
