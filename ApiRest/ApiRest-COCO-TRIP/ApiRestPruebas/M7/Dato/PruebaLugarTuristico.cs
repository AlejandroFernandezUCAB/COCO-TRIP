using NUnit.Framework;
using ApiRest_COCO_TRIP.Models.Dato;

namespace ApiRestPruebas.M7.Dato
{
  /// <summary>
  /// Pruebas unitarias de la clase LugarTuristico
  /// </summary>
  [TestFixture]
  public class PruebaLugarTuristico
  {
    //Objeto 1 de la clase a testear
    private LugarTuristico lugar;
    //Objeto 2 de la clase a testear
    private LugarTuristico pruebaLugar;

    /// <summary>
    /// Instancia los objetos
    /// </summary>
    [SetUp]
    public void SetLugarTuristico()
    {
      lugar = new LugarTuristico();

      lugar.Id = 1;
      lugar.Nombre = "Parque del Este";
      lugar.Costo = 0;
      lugar.Descripcion = "Lugar al aire libre";
      lugar.Direccion = "Frente a la estacion Parque del Este, Caracas";
      lugar.Correo = "parquemiranda@gob.ve";
      lugar.Telefono = 02122732867;
      lugar.Latitud = 20.0;
      lugar.Longitud = 10.0;
      lugar.Activar = true;

      lugar.Foto.Add(new Foto());
      lugar.Horario.Add(new Horario());
      lugar.Actividad.Add(new Actividad());

      pruebaLugar = new LugarTuristico();

      pruebaLugar.Id = 1;
      pruebaLugar.Nombre = "Parque del Este";
      pruebaLugar.Costo = 0;
      pruebaLugar.Descripcion = "Lugar al aire libre";
      pruebaLugar.Direccion = "Frente a la estacion Parque del Este, Caracas";
      pruebaLugar.Correo = "parquemiranda@gob.ve";
      pruebaLugar.Telefono = 02122732867;
      pruebaLugar.Latitud = 20.0;
      pruebaLugar.Longitud = 10.0;
      pruebaLugar.Activar = true;
    }

    /// <summary>
    /// Test del metodo Equal de la clase LugarTuristico
    /// </summary>
    [Test]
    [Category("Equal")]
    public void TestEqualLugarTuristico()
    {
      //Prueba null y un objeto distinto
      Assert.AreEqual(false, lugar.Equals(null));
      Assert.AreEqual(false, lugar.Equals(new Horario()));

      //Prueba elementos en las listas
      Assert.AreEqual(false, lugar.Equals(pruebaLugar));

      pruebaLugar.Foto.Add(new Foto());
      Assert.AreEqual(false, lugar.Equals(pruebaLugar));

      pruebaLugar.Horario.Add(new Horario());
      Assert.AreEqual(false, lugar.Equals(pruebaLugar));

      pruebaLugar.Actividad.Add(new Actividad());
      Assert.AreEqual(true, lugar.Equals(pruebaLugar));

      //Prueba ID de Lugar Turistico
      pruebaLugar.Id = 2;
      Assert.AreEqual(false, lugar.Equals(pruebaLugar));
      lugar.Id = 2;
      Assert.AreEqual(true, lugar.Equals(pruebaLugar));

      //Prueba Nombre de Lugar Turistico
      pruebaLugar.Nombre = "Parque Generalisimo de Miranda";
      Assert.AreEqual(false, lugar.Equals(pruebaLugar));
      lugar.Nombre = "Parque Generalisimo de Miranda";
      Assert.AreEqual(true, lugar.Equals(pruebaLugar));

      //Prueba Costo de Lugar Turistico
      pruebaLugar.Costo = 500;
      Assert.AreEqual(false, lugar.Equals(pruebaLugar));
      lugar.Costo = 500;
      Assert.AreEqual(true, lugar.Equals(pruebaLugar));

      //Prueba Descripcion de Lugar Turistico
      pruebaLugar.Descripcion = "Probando";
      Assert.AreEqual(false, lugar.Equals(pruebaLugar));
      lugar.Descripcion = "Probando";
      Assert.AreEqual(true, lugar.Equals(pruebaLugar));

      //Prueba Direccion de Lugar Turistico
      pruebaLugar.Direccion = "Frente a la estacion Miranda, Caracas";
      Assert.AreEqual(false, lugar.Equals(pruebaLugar));
      lugar.Direccion = "Frente a la estacion Miranda, Caracas";
      Assert.AreEqual(true, lugar.Equals(pruebaLugar));

      //Prueba Correo de Lugar Turistico
      pruebaLugar.Correo = "parquedeleste@gob.ve";
      Assert.AreEqual(false, lugar.Equals(pruebaLugar));
      lugar.Correo = "parquedeleste@gob.ve"; ;
      Assert.AreEqual(true, lugar.Equals(pruebaLugar));

      //Prueba Telefono de Lugar Turistico
      pruebaLugar.Telefono = 02123641438;
      Assert.AreEqual(false, lugar.Equals(pruebaLugar));
      lugar.Telefono = 02123641438;
      Assert.AreEqual(true, lugar.Equals(pruebaLugar));

      //Prueba Latitud de Lugar Turistico
      pruebaLugar.Latitud = 2.00;
      Assert.AreEqual(false, lugar.Equals(pruebaLugar));
      lugar.Latitud = 2.00;
      Assert.AreEqual(true, lugar.Equals(pruebaLugar));

      //Prueba Longitud de Lugar Turistico
      pruebaLugar.Longitud = 2.00;
      Assert.AreEqual(false, lugar.Equals(pruebaLugar));
      lugar.Longitud = 2.00;
      Assert.AreEqual(true, lugar.Equals(pruebaLugar));

      //Prueba Activar de Lugar Turistico
      //Prueba Latitud de Lugar Turistico
      pruebaLugar.Activar = false;
      Assert.AreEqual(false, lugar.Equals(pruebaLugar));
      lugar.Activar = false;
      Assert.AreEqual(true, lugar.Equals(pruebaLugar));
    }
  }
}
