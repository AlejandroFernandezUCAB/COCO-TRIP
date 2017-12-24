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

    /// <summary>
    /// Retorna la instancia de la entidad Categoria
    /// </summary>
    /// <returns>Grupo</returns>
    public static Categoria CrearEntidadCategoria()
    {
      return new Categoria();
    }

    /// Retorna la instancia de la entidad Itinerario
    /// </summary>
    /// <returns>Itinerario</returns>
    public static Itinerario CrearEntidadItinerario()
    {
      return new Itinerario();
    }

    /// <summary>
    /// Retorna la instancia de la entidad Agenda
    /// </summary>
    /// <returns>Grupo</returns>
    public static Agenda CrearEntidadAgenda()
    {
      return new Agenda();
    }

    /// <summary>
    /// Retorna una nueva instancia de la entidad LocalidadEvento
    /// </summary>
    /// <returns>Grupo</returns>
    public static LocalidadEvento CrearEntidadLocalidad()
    {
      return new LocalidadEvento();
    }

    /// <summary>
    /// Retorna una nueva instancia de la entidad Evento
    /// </summary>
    /// <returns>Grupo</returns>
    public static Evento CrearEntidadEvento()
    {
      return new Evento();
    }

  }
}
