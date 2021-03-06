using BackOffice_COCO_TRIP.Datos.DAO;
using BackOffice_COCO_TRIP.Datos.Entidades;
using Newtonsoft.Json.Linq;

namespace BackOffice_COCO_TRIP.Negocio.Fabrica
{
  /// <summary>
  /// Clase que representa la fabrica de DAO, aqui se deben retornar todos los comandos del sistema.
  /// </summary>
  public static class FabricaDAO
  {
    /// <summary>
    /// Metodo que devuelve el DAOEvento
    /// </summary>
    /// <returns>DAOEvento</returns>
    public static DAOEvento GetDAOEvento()
    {

      return new DAOEvento();
    }

    /// <summary>
    /// Metodo que devuelve el DAOLocalidad
    /// </summary>
    /// <returns>DAOLocalidad</returns>
    public static DAOLocalidad GetDAOLocalidad()
    {

      return new DAOLocalidad();
    }

    public static DAOCategoria GetDAOCategoria()
    {
      return new DAOCategoria();
    }

    public static DAOLugar_Turistico GetDAOLugar_Turistico()
    {
      return new DAOLugar_Turistico();
    }

  }
}
