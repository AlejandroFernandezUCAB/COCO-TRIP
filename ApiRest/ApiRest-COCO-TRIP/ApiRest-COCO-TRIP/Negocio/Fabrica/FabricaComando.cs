using ApiRest_COCO_TRIP.Negocio.Comando;

namespace ApiRest_COCO_TRIP.Negocio.Fabrica
{
  /// <summary>
  /// Fabrica que instancia los comandos
  /// </summary>
  public class FabricaComando
  {
    /// <summary>
    /// Retorna la instancia de la entidad ComandoAgregarAmigo
    /// </summary>
    /// <param name="id">ID del usuario</param>
    /// <param name="nombre">Nombre de usuario destino</param>
    /// <returns></returns>
    public static ComandoAgregarAmigo CrearComandoAgregarAmigo (int id, string nombre)
    {
      return new ComandoAgregarAmigo(id, nombre);
    }
  }
}
