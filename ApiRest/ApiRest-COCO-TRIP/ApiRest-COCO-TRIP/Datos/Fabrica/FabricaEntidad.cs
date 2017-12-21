using ApiRest_COCO_TRIP.Datos.Entity;

namespace ApiRest_COCO_TRIP.Datos.Fabrica
{
  /// <summary>
  /// Fabrica que instancia las entidades
  /// </summary>
  public class FabricaEntidad
  {
    /// <summary>
    /// Retorna la instancia de la entidad Grupo
    /// </summary>
    /// <returns>Grupo</returns>
    public static Grupo CrearEntidadGrupo ()
    {
      return new Grupo();
    }

    /// <summary>
    /// Retorna la instancia de la entidad Usuario
    /// </summary>
    /// <returns>Grupo</returns>
    public static Usuario CrearEntidadUsuario()
    {
      return new Usuario();
    }

    /// <summary>
    /// Retorna la instancia de la entidad Foto
    /// </summary>
    /// <returns>Grupo</returns>
    public static Foto CrearEntidadFoto()
    {
      return new Foto();
    }

    /// <summary>
    /// Retorna la instancia de la entidad Amigo
    /// </summary>
    /// <returns>Grupo</returns>
    public static Amigo CrearEntidadAmigo()
    {
      return new Amigo();
    }
  }
}
