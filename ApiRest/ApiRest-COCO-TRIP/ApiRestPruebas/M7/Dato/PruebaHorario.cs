using System;
using NUnit.Framework;
using ApiRest_COCO_TRIP.Models.M7.Dato;

namespace ApiRestPruebas.M7.Dato
{
  /// <summary>
  /// Pruebas unitarias de la clase Horario
  /// </summary>
  [TestFixture]
  public class PruebaHorario
  {
    //Objeto 1 de la clase que se esta testeando
    private Horario horario;
    //Objeto 2 de la clase que se esta testeando
    private Horario pruebaHorario;

    /// <summary>
    /// Instancia los objetos
    /// </summary>
    [SetUp]
    public void SetHorario()
    {
      horario = new Horario();
      horario.Id = 1;
      horario.DiaSemana = (int) DateTime.Now.DayOfWeek;
      horario.HoraApertura = new TimeSpan(1, 0, 0);
      horario.HoraCierre = new TimeSpan(2, 0, 0);

      pruebaHorario = new Horario();
      pruebaHorario.Id = 1;
      pruebaHorario.DiaSemana = (int) DateTime.Now.DayOfWeek;
      pruebaHorario.HoraApertura = new TimeSpan(1, 0, 0);
      pruebaHorario.HoraCierre = new TimeSpan(2, 0, 0);
    }

    /// <summary>
    /// Test del metodo Equal de la clase Horario
    /// </summary>
    [Test]
    [Category("Equal")]
    public void TestEqualHorario()
    {
      //Prueba null y un objeto distinto
      Assert.AreEqual(false, horario.Equals(null));
      Assert.AreEqual(false, horario.Equals(new Foto()));

      //Prueba ID del Horario
      pruebaHorario.Id = 2;
      Assert.AreEqual(false, horario.Equals(pruebaHorario));
      horario.Id = 2;
      Assert.AreEqual(true, horario.Equals(pruebaHorario));

      //Prueba DiaSemana del Horario
      pruebaHorario.DiaSemana = (int) DateTime.Now.DayOfWeek + 1;
      Assert.AreEqual(false, horario.Equals(pruebaHorario));
      horario.DiaSemana = (int) DateTime.Now.DayOfWeek + 1;
      Assert.AreEqual(true, horario.Equals(pruebaHorario));

      //Prueba HoraApertura que tambien aplica para el atributo HoraCierre del Horario
      pruebaHorario.HoraApertura = pruebaHorario.HoraApertura.Add(new TimeSpan(1, 0, 0));
      Assert.AreEqual(false, horario.Equals(pruebaHorario));
      horario.HoraApertura = horario.HoraApertura.Add(new TimeSpan(1, 0, 0));
      Assert.AreEqual(true, horario.Equals(pruebaHorario));
    }
  }
}
