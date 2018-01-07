using BackOffice_COCO_TRIP.Datos.Entidades;

namespace BackOffice_COCO_TRIP.Negocio.Fabrica
{
  /// <summary>
  /// Clase que representa la fabrica de entidades, aqui se debe retornar todos los comandos existentes en el sistema.
  /// </summary>
  public class FabricaEntidad
  {
    public static Categoria GetCategoria()
    {
      return new Categoria();
    }

    public static LugarTuristico GetLugarTuristico()
    {
      return new LugarTuristico();
    }

    public static Foto GetFoto()
    {
      return new Foto();
    }

    public static Horario GetHorario()
    {
      return new Horario();
    }

    public static Actividad GetActividad()
    {
      return new Actividad();
    }

    /// <summary>
    /// Metodo que retorna la entidad evento
    /// </summary>
    /// <returns>Evento</returns>
    public static Evento GetEvento()
    {
      return new Evento();
    }

    /// <summary>
    /// Metodo que retorna la entidad localidad
    /// </summary>
    /// <returns>Localidad</returns>
    public static Localidad GetLocalidad()
    {
      return new Localidad();
    }
  }

}
