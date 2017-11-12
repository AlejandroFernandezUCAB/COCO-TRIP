using System;
using NUnit.Framework;
using ApiRest_COCO_TRIP.Models.Dato;
using ApiRest_COCO_TRIP.Models.BaseDeDatos;

namespace ApiRestPruebas.M7.Base
{
  /// <summary>
  /// Pruebas Unitarias de la clase Peticion
  /// </summary>
  [TestFixture]
  public class PruebaPeticion
  {
    private PeticionLugarTuristico peticion;
    private LugarTuristico lugar;
    private Actividad actividad;
    private Horario horario;
    private Foto foto;

    //Identificador unico de cada objeto
    private int idLugar = 1;
    private int idActividad = 1;
    private int idHorario = 1;
    private int idFoto = 1;

    /// <summary>
    /// Instancia los objetos
    /// </summary>
    [SetUp]
    public void SetPeticion()
    {
      peticion = new PeticionLugarTuristico();

      lugar = new LugarTuristico();
      lugar.Id = idLugar;
      lugar.Nombre = "Parque Generalisimo de Miranda";
      lugar.Costo = 0;
      lugar.Descripcion = "Lugar al aire libre";
      lugar.Direccion = "Frente a la estacion Miranda, Caracas";
      lugar.Correo = "parquemiranda@gob.ve";
      lugar.Telefono = 02122732867;
      lugar.Latitud = 20.0;
      lugar.Longitud = 10.0;
      lugar.Activar = true;

      byte[] imagen = new byte[28480];

      actividad = new Actividad();
      actividad.Id = idActividad;
      actividad.Nombre = "Parque Generalisimo de Miranda";
      actividad.Duracion = new TimeSpan(2, 0, 0);
      actividad.Descripcion = "Lugar al aire libre";
      actividad.Foto.Contenido = imagen;
      actividad.Activar = true;

      horario = new Horario();
      horario.Id = idHorario;
      horario.DiaSemana = (int) DateTime.Now.DayOfWeek;
      horario.HoraApertura = new TimeSpan(8, 0, 0);
      horario.HoraCierre = new TimeSpan(17, 0, 0);

      foto = new Foto();
      foto.Id = idFoto;
      foto.Contenido = imagen;

      lugar.Actividad.Add(actividad);
      lugar.Horario.Add(horario);
      lugar.Foto.Add(foto);
    }

    /// <summary>
    /// Test del metodo InsertarLugarTuristico de la clase Peticion
    /// </summary>
    [Test]
    [Ignore("Evaluar si vale la pena")]
    public void TestInsertarLugarTuristico()
    {
      Assert.AreEqual(lugar.Id, peticion.InsertarLugarTuristico(lugar));
    }
  }
}
