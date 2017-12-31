using ApiRest_COCO_TRIP.Datos.DAO;

namespace ApiRest_COCO_TRIP.Datos.Fabrica
{
  /// <summary>
  /// Fabrica que instancia los DAO
  /// </summary>
  public class FabricaDAO
  {
    /// <summary>
    /// Retorna la instancia de DAOUsuario
    /// </summary>
    /// <returns>Grupo</returns>
    public static DAOUsuario CrearDAOUsuario()
    {
      return new DAOUsuario();
    }

    /// <summary>
    /// Retorna la instancia de DAOGrupo
    /// </summary>
    /// <returns>Grupo</returns>
    public static DAOGrupo CrearDAOGrupo()
    {
      return new DAOGrupo();
    }

    /// <summary>
    /// Retorna la instancia de DAOAmigo
    /// </summary>
    /// <returns>Grupo</returns>
    public static DAOAmigo CrearDAOAmigo()
    {
      return new DAOAmigo();
    }

    /// <summary>
    /// Retorna la instancia de DAOCategoria
    /// </summary>
    /// <returns>Grupo</returns>
    public static DAOCategoria CrearDAOCategoria()
    {
      return new DAOCategoria();
    }

    /// Retorna la instancia de DAOItinerario
    /// </summary>
    /// <returns>DAOItinerario</returns>
    public static DAOItinerario CrearDAOItinerario()
    {
      return new DAOItinerario();
    }
    
    /// Retorna la instancia de DAOAgenda
    /// </summary>
    /// <returns>DAOAgenda</returns>
    public static DAOAgenda CrearDAOAgenda()
    {
      return new DAOAgenda();
    }

    /// Retorna una nueva instancia de DAOLocalidadEvento
    /// </summary>
    /// <returns>DAOLocalidadEvento</returns>
    public static DAOLocalidadEvento CrearDAOLocalidad()
    {
      return new DAOLocalidadEvento();
    }

    /// Retorna una nueva instancia de DAOEvento
    /// </summary>
    /// <returns>DAOEvento</returns>
    public static DAOEvento CrearDAOEvento()
    {
      return new DAOEvento();
    }

    /// Retorna la instancia de DAONotificacion
    /// </summary>
    /// <returns>DAONotificacion</returns>
    public static DAONotificacion CrearDAONotifiacacion()
    {
      return new DAONotificacion();
    }

	/// <summary>
	/// Retorna la instancia de DAOActividad
	/// </summary>
	/// <returns></returns>
	public static DAOActividad CrearDAOActividad()
	{
		return new DAOActividad();
	}

	/// <summary>
	/// Retorna la instancia de DAOHorario
	/// </summary>
	/// <returns></returns>
	public static DAOHorario CrearDAOHorario()
	{
		return new DAOHorario();
	}

	/// <summary>
	/// Retorna la instancia de DAOLugarTuristico
	/// </summary>
	/// <returns></returns>
	public static DAOLugarTuristico CrearDAOLugarTuristico()
	{
		return new DAOLugarTuristico();
	}
	
	/// <summary>
	/// Retorna la instancia de DAOFoto
	/// </summary>
	/// <returns></returns>
	public static DAOFoto CrearDAOFoto() 
	{
		return new DAOFoto();
	}

	/// <summary>
	/// Retorna la instancia de DAOLugarTuristico_Categoria
	/// </summary>
	/// <returns></returns>
	public static DAOLugarTuristico_Categoria CrearDAOLugarTuristico_Categoria()
	{
		return new DAOLugarTuristico_Categoria();
	}
  }
}
