using BackOffice_COCO_TRIP.Datos.DAO;
using BackOffice_COCO_TRIP.Datos.Entidades;
using Newtonsoft.Json.Linq;

namespace BackOffice_COCO_TRIP.Negocio.Fabrica
{
  public static class FabricaDAO
  {
    public static DAOEvento GetDAOEvento()
    {

      return new DAOEvento();
    }
    public static DAOLocalidad GetDAOLocalidad()
    {

      return new DAOLocalidad();
    }

    public static DAOCategoria GetDAOCategoria()
    {
      return new DAOCategoria();
    }
    
  }
}
