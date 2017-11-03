using NUnit.Framework;
using ApiRest_COCO_TRIP.Models.M7.Base;
using ApiRest_COCO_TRIP.Models.M7.Dato;

namespace ApiRest_COCO_TRIP.Tests.M7.Base
{
  /// <summary>
  /// Pruebas Unitarias de la clase Peticion
  /// </summary>
  [TestFixture]
  public class PruebaPeticion
  {
    private Peticion peticion;
    /*private LugarTuristico lugar;
    private Actividad actividad;
    private Horario horario;
    private Foto foto;*/

    /// <summary>
    /// Instancia los objetos
    /// </summary>
    [SetUp]
    public void SetPeticion()
    {
      peticion = new Peticion();

      //Continuo mañana
    }

  }
}
