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
      /// Retorna la instancia de DAOItinerario
      /// </summary>
      /// <returns>Grupo</returns>
    }
    public static DAOItinerario CrearDAOItinerario()
    {
      return new DAOItinerario();
    }
  }
}
