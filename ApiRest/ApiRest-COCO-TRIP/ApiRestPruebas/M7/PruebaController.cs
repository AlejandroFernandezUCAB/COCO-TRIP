using System;
using System.Web.Http;
using NUnit.Framework;
using ApiRest_COCO_TRIP.Controllers;
using ApiRest_COCO_TRIP.Models.Dato;
using ApiRest_COCO_TRIP.Models;

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
    private Categoria categoria;

    private Archivo archivo;

    //Identificador unico de cada objeto
    private int idLugar = 1;
    private int idActividad = 1;
    private int idHorario = 1;
    private int idFoto = 1;

    /// <summary>
    /// Instancia los objetos que se usaran en las pruebas unitarias
    /// </summary>
    [SetUp]
    public void SetControlador()
    {
      controlador = new M7_LugaresTuristicosController();
      archivo = new Archivo();

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

      categoria = new Categoria();
      categoria.Id = 1;
      categoria.Nombre = "Categoria 1";
      lugar.Categoria.Add(categoria);

      categoria = new Categoria();
      categoria.Id = 2;
      categoria.Nombre = "Categoria 2";
      lugar.Categoria.Add(categoria);

      categoria = new Categoria();
      categoria.Id = 3;
      categoria.CategoriaSuperior = 1;
      categoria.Nombre = "Categoria 1.2";
      lugar.SubCategoria.Add(categoria);

      byte[] imagen = new byte[28480];

      actividad = new Actividad();
      actividad.Id = idActividad;
      actividad.Nombre = "Parque Generalisimo de Miranda";
      actividad.Duracion = new TimeSpan(2, 0, 0);
      actividad.Descripcion = "Lugar al aire libre";
      actividad.Foto.Contenido = imagen;
      actividad.Foto.Ruta = archivo.Ruta;
      actividad.Activar = true;

      horario = new Horario();
      horario.Id = idHorario;
      horario.DiaSemana = (int)DateTime.Now.DayOfWeek;
      horario.HoraApertura = new TimeSpan(8, 0, 0);
      horario.HoraCierre = new TimeSpan(17, 0, 0);

      foto = new Foto();
      foto.Id = idFoto;
      foto.Contenido = imagen;
      foto.Ruta = archivo.Ruta;
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
      lugar.Id = idLugar;
      lugar.Nombre = "Parque Generalisimo de Miranda";
      lugar.Costo = 0;
      lugar.Descripcion = "Lugar al aire libre";
      lugar.Activar = true;

      horario.Id = 0;
      horario.DiaSemana = 0;

      foto.Ruta += "lt-fo-" + foto.Id + ".jpg";
      foto.Contenido = null;

      lugar.Horario.Add(horario);
      lugar.Foto.Add(foto);

      Assert.AreEqual(true, controlador.GetLista(1, lugar.Id).Contains(lugar));
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

      foto.Ruta += "lt-fo-" + foto.Id + ".jpg";
      foto.Contenido = null;

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
      foto.Ruta += "lt-fo-" + foto.Id + ".jpg";
      foto.Contenido = null;

      actividad.Foto.Ruta += "ac-" + actividad.Id + ".jpg";
      actividad.Foto.Contenido = null;

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
      actividad.Foto.Ruta += "ac-" + actividad.Id + ".jpg";
      actividad.Foto.Contenido = null;

      Assert.AreEqual(true, controlador.GetActividades(lugar.Id).Contains(actividad));
    }

    /// <summary>
    /// Test del metodo GetActividad
    /// </summary>
    [Category("Get")]
    [Test]
    public void TestGetActividad()
    {
      actividad.Foto.Ruta += "ac-" + actividad.Id + ".jpg";
      actividad.Foto.Contenido = null;

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

      //idLugar = controlador.PostLugar(lugar);
      Assert.AreEqual(true, idLugar > 0);
    }

    /// <summary>
    /// Metodo para testear las excepciones del metodo PostLugar
    /// </summary>
    [Category("Post")]
    [Test]
    public void TestExcepcionPostLugar()
    {
      lugar.Actividad.Add(actividad);
      lugar.Horario.Add(horario);
      lugar.Foto.Add(foto);

      lugar.Nombre = null;
      Assert.Catch<HttpResponseException>(ExcepcionPostLugar);

      lugar = null;
      Assert.Catch<HttpResponseException>(ExcepcionPostLugar);
    }

    /// <summary>
    /// Metodo para testear las excepciones
    /// </summary>
    public void ExcepcionPostLugar()
    {
      //controlador.PostLugar(lugar);
    }

    /// <summary>
    /// Test del metodo PostActividad
    /// </summary>
    [Category("Post")]
    [Test]
    public void TestPostActividad()
    {
      idActividad = controlador.PostActividad(actividad, lugar.Id);
      Assert.AreEqual(true, idActividad > 0);
    }

    /// <summary>
    /// Metodo para testear las excepciones del metodo PostActividad
    /// </summary>
    [Category("Post")]
    [Test]
    public void TestExcepcionPostActividad()
    {
      lugar.Id = 0;
      Assert.Catch<HttpResponseException>(ExcepcionPostActividad);

      lugar.Id = idLugar;
      actividad.Nombre = null;
      Assert.Catch<HttpResponseException>(ExcepcionPostActividad);

      actividad = null;
      Assert.Catch<HttpResponseException>(ExcepcionPostActividad);
    }

    /// <summary>
    /// Metodo para testear las excepciones
    /// </summary>
    public void ExcepcionPostActividad()
    {
      controlador.PostActividad(actividad, lugar.Id);
    }

    /// <summary>
    /// Test del metodo PostHorario
    /// </summary>
    [Category("Post")]
    [Test]
    public void TestPostHorario()
    {
      idHorario = controlador.PostHorario(horario, lugar.Id);
      Assert.AreEqual(true, idHorario > 0);
    }

    /// <summary>
    /// Metodo para testear las excepciones del metodo PostHorario
    /// </summary>
    [Category("Post")]
    [Test]
    public void TestExcepcionPostHorario()
    {
      lugar.Id = 0;
      Assert.Catch<HttpResponseException>(ExcepcionPostHorario);

      lugar.Id = idLugar;
      horario.HoraApertura = TimeSpan.Zero;
      horario.HoraCierre = TimeSpan.Zero;
      Assert.DoesNotThrow(ExcepcionPostHorario);

      horario = null;
      Assert.Catch<HttpResponseException>(ExcepcionPostHorario);
    }

    /// <summary>
    /// Metodo para testear las excepciones
    /// </summary>
    public void ExcepcionPostHorario()
    {
      controlador.PostHorario(horario, lugar.Id);
    }

    /// <summary>
    /// Test del metodo PostFoto
    /// </summary>
    [Category("Post")]
    [Test]
    public void TestPostFoto()
    {
      idFoto = controlador.PostFoto(foto, lugar.Id);
      Assert.AreEqual(true, idFoto > 0);
    }

    /// <summary>
    /// Metodo para testear las excepciones del metodo PostFoto
    /// </summary>
    [Category("Post")]
    [Test]
    public void TestExcepcionPostFoto()
    {
      lugar.Id = 0;
      Assert.Catch<HttpResponseException>(ExcepcionPostFoto);

      lugar.Id = idLugar;
      foto.Contenido = null;
      Assert.Catch<HttpResponseException>(ExcepcionPostFoto);

      foto = null;
      Assert.Catch<HttpResponseException>(ExcepcionPostFoto);
    }

    /// <summary>
    /// Metodo para testear las excepciones
    /// </summary>
    public void ExcepcionPostFoto()
    {
      controlador.PostFoto(foto, lugar.Id);
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

      actividad.Foto.Ruta += "ac-" + actividad.Id;
      foto.Ruta += "lt-fo-" + foto.Id;

      lugar.Horario.Add(horario);
      lugar.Actividad.Add(actividad);
      lugar.Foto.Add(foto);

      controlador.PutLugar(lugar);

      lugar.Actividad[0].Foto.Contenido = null;
      lugar.Foto[0].Contenido = null;

      Assert.AreEqual(true, lugar.Equals(controlador.GetLugarActividades(lugar.Id)));
    }

    /// <summary>
    /// Test para las excepciones del metodo PutLugar
    /// </summary>
    [Category("Put")]
    [Test]
    public void TestExcepcionPutLugar()
    {
      lugar.Nombre = null;
      Assert.Catch<HttpResponseException>(ExcepcionPutLugar);

      lugar = null;
      Assert.Catch<HttpResponseException>(ExcepcionPutLugar);
    }

    /// <summary>
    /// Metodo que permite testear excepciones
    /// </summary>
    public void ExcepcionPutLugar()
    {
      controlador.PutLugar(lugar);
    }

    /// <summary>
    /// Test del metodo PutActivarLugar
    /// </summary>
    [Category("Put")]
    [Test]
    public void TestPutActivarLugar()
    {
      lugar.Activar = false;
      controlador.PutActivarLugar(lugar.Id, lugar.Activar);
      Assert.AreEqual(false, controlador.GetLugar(lugar.Id).Activar);

      lugar.Activar = true;
      controlador.PutActivarLugar(lugar.Id, lugar.Activar);
      Assert.AreEqual(true, controlador.GetLugar(lugar.Id).Activar);
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
      Assert.AreEqual(false, controlador.GetActividad(actividad.Id).Activar);

      actividad.Activar = true;
      controlador.PutActivarActividad(actividad.Id, actividad.Activar);
      Assert.AreEqual(true, controlador.GetActividad(actividad.Id).Activar);
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
      controlador.GetActividad(actividad.Id);
    }

    /// <summary>
    /// Test del metodo DeleteFoto
    /// </summary>
    //[Category("Delete")]
    //[Test]
    //public void TestDeleteFoto()
    //{
    //  foto.Contenido = null;

    //  controlador.DeleteFoto(foto.Id);
    //  Assert.AreEqual(false, lugar.Equals(controlador.GetLugar(lugar.Id).Foto.Contains(foto)));
    //}

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
