using BackOffice_COCO_TRIP.Datos.Entidades;

namespace BackOffice_COCO_TRIP.Negocio.Fabrica
{
  /// <summary>
  /// Clase para instanciar entidades
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

  }

}
