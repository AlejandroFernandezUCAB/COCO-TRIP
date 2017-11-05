using System;
using NUnit.Framework;
using ApiRest_COCO_TRIP.Models.Dato;

namespace ApiRestPruebas.M7.Dato
{
  /// <summary>
  /// Pruebas unitarias de la clase Actividad
  /// </summary>
  [TestFixture]
  public class PruebaActividad
  {
    //Objeto 1 de la clase que se esta testeando
    private Actividad actividad;
    //Objeto 2 de la clase que se esta testeando
    private Actividad pruebaActividad;

    /// <summary>
    /// Instancia los objetos
    /// </summary>
    [SetUp]
    public void SetActividad()
    {
      actividad = new Actividad();

      actividad.Id = 1;
      actividad.Nombre = "Escalar el Avila";
      actividad.Duracion = new TimeSpan(5, 0, 0);
      actividad.Descripcion = "Waraira Repano";
      actividad.Activar = true;
      actividad.Foto.Id = 1;

      pruebaActividad = new Actividad();

      pruebaActividad.Id = 1;
      pruebaActividad.Nombre = "Escalar el Avila";
      pruebaActividad.Duracion = new TimeSpan(5, 0, 0);
      pruebaActividad.Descripcion = "Waraira Repano";
      pruebaActividad.Activar = true;
      pruebaActividad.Foto.Id = 1;
    }

    /// <summary>
    /// Test del metodo Equal de la clase Actividad
    /// </summary>
    [Test]
    [Category("Equal")]
    public void TestEqualActividad()
    {
      //Prueba de null y un objeto distinto
      Assert.AreEqual(false, actividad.Equals(null));
      Assert.AreEqual(false, actividad.Equals(new LugarTuristico()));

      //Prueba ID
      pruebaActividad.Id = 2;
      Assert.AreEqual(false, actividad.Equals(pruebaActividad));
      pruebaActividad.Id = 1;
      Assert.AreEqual(true, actividad.Equals(pruebaActividad));

      //Prueba Duracion
      pruebaActividad.Duracion = new TimeSpan(4, 0, 0);
      Assert.AreEqual(false, actividad.Equals(pruebaActividad));
      pruebaActividad.Duracion = new TimeSpan(5, 0, 0);
      Assert.AreEqual(true, actividad.Equals(pruebaActividad));

      //Prueba ID de la Foto
      pruebaActividad.Foto.Id = 2;
      Assert.AreEqual(false, actividad.Equals(pruebaActividad));
      actividad.Foto.Id = 2;
      Assert.AreEqual(true, actividad.Equals(pruebaActividad));

      //Prueba Contenido de la Foto
      pruebaActividad.Foto.Contenido = new byte[5];
      Assert.AreEqual(false, actividad.Equals(pruebaActividad));
      actividad.Foto.Contenido = new byte[5];
      Assert.AreEqual(true, actividad.Equals(pruebaActividad));
      pruebaActividad.Foto.Contenido[0] = 1;
      Assert.AreEqual(false, actividad.Equals(pruebaActividad));
      actividad.Foto.Contenido[0] = 1;
      Assert.AreEqual(true, actividad.Equals(pruebaActividad));

      //Prueba
      Assert.AreEqual(true, actividad.Equals(actividad));
    }

  }
}
