using System;
using System.Net;
using System.Web.Http;
using NUnit.Framework;
using ApiRest_COCO_TRIP.Controllers;
using ApiRest_COCO_TRIP.Models.Dato;

namespace ApiRestPruebas.M7
{
  /// <summary>
  /// Pruebas unitarias del controlador del Modulo 7
  /// </summary>
  [TestFixture]
  public class PruebaController
  {
    private M7_LugaresTuristicosController controlador;
    private LugarTuristico lugar;
    private Actividad actividad;
    private Horario horario;
    private Foto foto;

    /// <summary>
    /// Instancia los objetos que se usaran en las pruebas unitarias
    /// </summary>
    [SetUp]
    public void SetControlador()
    {
      controlador = new M7_LugaresTuristicosController();

      lugar = new LugarTuristico();
      lugar.Id = 1;
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
      actividad.Id = 1;
      actividad.Nombre = "Parque Generalisimo de Miranda";
      actividad.Duracion = new TimeSpan(2, 0, 0);
      actividad.Descripcion = "Lugar al aire libre";
      actividad.Foto.Contenido = imagen;
      actividad.Activar = true;

      horario = new Horario();
      horario.Id = 1;
      horario.DiaSemana = (int)DateTime.Now.DayOfWeek;
      horario.HoraApertura = new TimeSpan(8, 0, 0);
      horario.HoraCierre = new TimeSpan(17, 0, 0);

      foto = new Foto();
      foto.Id = 1;
      foto.Contenido = imagen;
    }

    //GET

    /// <summary>
    /// Test del metodo GetLista
    /// </summary>
    [Category("Get")]
    [Test]
    public void TestGetLista()
    {
      lugar = new LugarTuristico();
      lugar.Id = 1;
      lugar.Nombre = "Parque Generalisimo de Miranda";
      lugar.Costo = 0;
      lugar.Descripcion = "Lugar al aire libre";
      lugar.Activar = true;

      horario.Id = 0;
      horario.DiaSemana = 0;

      lugar.Horario.Add(horario);
      lugar.Foto.Add(foto);

      Assert.AreEqual(true, controlador.GetLista(1, 2).Contains(lugar));
    }

    /// <summary>
    /// Test del metodo GetLugar
    /// </summary>
    [Category("Get")]
    [Test]
    public void TestGetLugar()
    {
      var pruebaActividad = new Actividad();
      pruebaActividad.Nombre = "Parque Generalisimo de Miranda";

      lugar.Actividad.Add(pruebaActividad);
      lugar.Foto.Add(foto);
      lugar.Horario.Add(horario);

      Assert.AreEqual(true, lugar.Equals(controlador.GetLugar(lugar.Id)));
    }

    /// <summary>
    /// Test del metodo GetLugarActividades
    /// </summary>
    [Category("Get")]
    [Test]
    public void TestGetLugarActividades()
    {
      lugar.Actividad.Add(actividad);
      lugar.Foto.Add(foto);
      lugar.Horario.Add(horario);

      Assert.AreEqual(true, lugar.Equals(controlador.GetLugarActividades(lugar.Id)));
    }

    /// <summary>
    /// Test del metodo GetActividades
    /// </summary>
    [Category("Get")]
    [Test]
    public void TestGetActividades()
    {
      Assert.AreEqual(true, controlador.GetActividades(lugar.Id).Contains(actividad));
    }

    /// <summary>
    /// Test del metodo GetActividad
    /// </summary>
    [Category("Get")]
    [Test]
    public void TestGetActividad()
    {
      Assert.AreEqual(true, actividad.Equals(controlador.GetActividad(actividad.Id)));
    }

    //POST

    /// <summary>
    /// Test del metodo PostLugar
    /// </summary>
    [Category("Post")]
    [Test]
    public void TestPostLugar()
    {
      lugar.Actividad.Add(actividad);
      lugar.Horario.Add(horario);
      lugar.Foto.Add(foto);

      controlador.PostLugar(lugar);

      Assert.AreEqual(true, lugar.Equals(controlador.GetLugarActividades(lugar.Id)));
    }

    /// <summary>
    /// Test del metodo PostActividad
    /// </summary>
    [Category("Post")]
    [Test]
    public void TestPostActividad()
    {
      controlador.PostActividad(actividad, lugar.Id);
      Assert.AreEqual(true, actividad.Equals(controlador.GetActividad(actividad.Id)));
    }

    /// <summary>
    /// Test del metodo PostHorario
    /// </summary>
    [Category("Post")]
    [Test]
    public void TestPostHorario()
    {
      controlador.PostHorario(horario, lugar.Id);
      Assert.AreEqual(true, controlador.GetLugar(lugar.Id).Horario.Contains(horario));
    }

    /// <summary>
    /// Test del metodo PostFoto
    /// </summary>
    [Category("Post")]
    [Test]
    public void TestPostFoto()
    {
      controlador.PostFoto(foto, lugar.Id);
      Assert.AreEqual(true, controlador.GetLugar(lugar.Id).Foto.Contains(foto));
    }

    //PUT

    /// <summary>
    /// Test del metodo PutLugar
    /// </summary>
    [Category("Put")]
    [Test]
    public void TestPutLugar()
    {
      lugar.Nombre = "Los Amigos Invisibles";

      lugar.Horario.Add(horario);
      lugar.Actividad.Add(actividad);
      lugar.Foto.Add(foto);

      controlador.PutLugar(lugar);

      Assert.AreEqual(true, lugar.Equals(controlador.GetLugarActividades(lugar.Id)));
    }

    /// <summary>
    /// Test del metodo PutActivarLugar
    /// </summary>
    [Category("Put")]
    [Test]
    public void TestPutActivarLugar()
    {
      lugar.Horario.Add(horario);
      lugar.Actividad.Add(actividad);
      lugar.Foto.Add(foto);

      lugar.Activar = false;
      controlador.PutActivarLugar(lugar.Id, lugar.Activar);
      Assert.AreEqual(true, lugar.Equals(controlador.GetLugarActividades(lugar.Id)));

      lugar.Activar = true;
      controlador.PutActivarLugar(lugar.Id, lugar.Activar);
      Assert.AreEqual(true, lugar.Equals(controlador.GetLugarActividades(lugar.Id)));
    }

    /// <summary>
    /// Test del metodo PutActivarActividad
    /// </summary>
    [Category("Put")]
    [Test]
    public void TestPutActivarActividad()
    {
      actividad.Activar = false;
      controlador.PutActivarActividad(actividad.Id, actividad.Activar);
      Assert.AreEqual(true, actividad.Equals(controlador.GetActividad(actividad.Id)));

      actividad.Activar = true;
      controlador.PutActivarActividad(actividad.Id, actividad.Activar);
      Assert.AreEqual(true, actividad.Equals(controlador.GetActividad(actividad.Id)));
    }

    /// <summary>
    /// Test del metodo DeleteActividad
    /// </summary>
    [Category("Delete")]
    [Test]
    public void TestDeleteActividad()
    {
      controlador.DeleteActividad(actividad.Id);
      Assert.Catch<HttpResponseException>(DeleteActividad);
    }

    /// <summary>
    /// Metodo que permite testear la excepcion retornada por un elemento no encontrado en GetActividad
    /// </summary>
    private void DeleteActividad()
    {
      Assert.AreEqual(false, controlador.GetActividad(actividad.Id));
    }

    /// <summary>
    /// Test del metodo DeleteFoto
    /// </summary>
    [Category("Delete")]
    [Test]
    public void TestDeleteFoto()
    {
      controlador.DeleteFoto(foto.Id);
      Assert.AreEqual(false, lugar.Equals(controlador.GetLugar(lugar.Id).Foto.Contains(foto)));
    }

    /// <summary>
    /// Test del metodo DeleteHorario
    /// </summary>
    [Category("Delete")]
    [Test]
    public void TestDeleteHorario()
    {
      controlador.DeleteHorario(horario.Id);
      Assert.AreEqual(false, lugar.Equals(controlador.GetLugar(lugar.Id).Horario.Contains(horario)));
    }
  }
}
