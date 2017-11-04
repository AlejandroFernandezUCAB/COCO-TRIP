using NUnit.Framework;
using ApiRest_COCO_TRIP.Models.M7.Dato;

namespace ApiRestPruebas.M7.Dato
{
  /// <summary>
  /// Pruebas unitarias de la clase Foto
  /// </summary>
  [TestFixture]
  public class PruebaFoto
  {
    //Objeto 1 de la clase que se esta testeando
    private Foto foto;
    //Objeto 2 de la clase que se esta testeando
    private Foto pruebaFoto;

    /// <summary>
    /// Instancia los objetos
    /// </summary>
    [SetUp]
    public void SetFoto()
    {
      foto = new Foto();
      foto.Id = 1;

      pruebaFoto = new Foto();
      pruebaFoto.Id = 1;
    }

    /// <summary>
    /// Test del metodo Equal de la clase Foto
    /// </summary>
    [Test]
    [Category("Equal")]
    public void TestEqualFoto()
    {
      //Prueba null y un objeto distinto
      Assert.AreEqual(false, foto.Equals(null));
      Assert.AreEqual(false, foto.Equals(new Horario()));

      //Prueba ID de la Foto
      pruebaFoto.Id = 2;
      Assert.AreEqual(false, foto.Equals(pruebaFoto));
      foto.Id = 2;
      Assert.AreEqual(true, foto.Equals(pruebaFoto));

      //Prueba Contenido de la Foto
      pruebaFoto.Contenido = new byte[5];
      Assert.AreEqual(false, foto.Equals(pruebaFoto));
      foto.Contenido = new byte[5];
      Assert.AreEqual(true, foto.Equals(pruebaFoto));
      pruebaFoto.Contenido[0] = 1;
      Assert.AreEqual(false, foto.Equals(pruebaFoto));
      foto.Contenido[0] = 1;
      Assert.AreEqual(true, foto.Equals(pruebaFoto));

    }
  }
}
