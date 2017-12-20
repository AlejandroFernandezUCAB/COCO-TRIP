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
  }
}
