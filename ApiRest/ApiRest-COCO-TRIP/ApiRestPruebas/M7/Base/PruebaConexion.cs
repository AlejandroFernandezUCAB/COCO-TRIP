using System;
using NUnit.Framework;
using ApiRest_COCO_TRIP.Models.Dato;
using ApiRest_COCO_TRIP.Models.Excepcion;
using ApiRest_COCO_TRIP.Models.BaseDeDatos;
using ApiRest_COCO_TRIP.Models;

namespace ApiRestPruebas.M7.Base
{
  /// <summary>
  /// Pruebas Unitarias de la clase Conexion
  /// </summary>
  [TestFixture]
  public class PruebaConexion
  {
    private ConexionLugarTuristico conexion;
    private LugarTuristico lugar;
    private Actividad actividad;
    private Horario horario;
    private Foto foto;

    private Archivo archivo;

    //Identificador unico de cada objeto
    private int idLugar = 1;
    private int idActividad = 1;
    private int idHorario = 1;
    private int idFoto = 1;

    /// <summary>
    /// Instancia los objetos
    /// </summary>
    [SetUp]
    public void SetConexion()
    {
      conexion = new ConexionLugarTuristico();
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

    //Insert

    /// <summary>
    /// Test del metodo InsertarLugarTuristico de la clase Conexion
    /// </summary>
    [Test]
    [Category("Insertar")]
    public void TestInsertarLugarTuristico()
    {
      conexion.Conectar();
      idLugar = conexion.InsertarLugarTuristico(lugar);
      Assert.AreEqual(true, idLugar > 0);
      conexion.Desconectar();
    }

    /// <summary>
    /// Test de excepciones del metodo InsertarLugarTuristico
    /// </summary>
    [Test]
    [Category("Insertar")]
    public void TestExcepcionInsertarLugarTuristico()
    {
      lugar.Nombre = null;
      Assert.Catch<CasteoInvalidoExcepcion>(ExcepcionInsertarLugarTuristico);

      lugar = null;
      Assert.Catch<ReferenciaNulaExcepcion>(ExcepcionInsertarLugarTuristico);
    }

    /// <summary>
    /// Metodo que permite testear las excepciones
    /// </summary>
    private void ExcepcionInsertarLugarTuristico()
    {
      conexion.Conectar();
      conexion.InsertarLugarTuristico(lugar);
      conexion.Desconectar();
    }

    /// <summary>
    /// Test del metodo InsertarActividad de la clase Conexion
    /// </summary>
    [Test]
    [Category("Insertar")]
    public void TestInsertarActividad()
    {
      conexion.Conectar();
      idActividad = conexion.InsertarActividad(actividad, lugar.Id);
      Assert.AreEqual(true, idActividad > 0);
      conexion.Desconectar();
    }

    /// <summary>
    /// Test de excepciones del metodo InsertarActividad
    /// </summary>
    [Test]
    [Category("Insertar")]
    public void TestExcepcionInsertarActividad()
    {
      lugar.Id = 0;
      Assert.Catch<BaseDeDatosExcepcion>(ExcepcionInsertarActividad);

      lugar.Id = idLugar;
      actividad.Nombre = null;
      Assert.Catch<CasteoInvalidoExcepcion>(ExcepcionInsertarActividad);

      actividad = null;
      Assert.Catch<ReferenciaNulaExcepcion>(ExcepcionInsertarActividad);
    }

    /// <summary>
    /// Metodo que permite testear las excepciones
    /// </summary>
    private void ExcepcionInsertarActividad()
    {
      conexion.Conectar();
      conexion.InsertarActividad(actividad, lugar.Id);
      conexion.Desconectar();
    }

    /// <summary>
    /// Test del metodo InsertarHorario de la clase Conexion
    /// </summary>
    [Test]
    [Category("Insertar")]
    public void TestInsertarHorario()
    {
      conexion.Conectar();
      idHorario = conexion.InsertarHorario(horario, lugar.Id);
      Assert.AreEqual(true, idHorario > 0);
      conexion.Desconectar();
    }

    /// <summary>
    /// Test de excepciones del metodo InsertarHorario
    /// </summary>
    [Test]
    [Category("Insertar")]
    public void TestExcepcionInsertarHorario()
    {
      lugar.Id = 0;
      Assert.Catch<BaseDeDatosExcepcion>(ExcepcionInsertarHorario);

      lugar.Id = idLugar;
      horario.HoraApertura = TimeSpan.Zero;
      horario.HoraCierre = TimeSpan.Zero;
      Assert.DoesNotThrow(ExcepcionInsertarHorario);

      horario = null;
      Assert.Catch<ReferenciaNulaExcepcion>(ExcepcionInsertarHorario);
    }

    /// <summary>
    /// Metodo que permite testear las excepciones
    /// </summary>
    private void ExcepcionInsertarHorario()
    {
      conexion.Conectar();
      conexion.InsertarHorario(horario, lugar.Id);
      conexion.Desconectar();
    }

    /// <summary>
    /// Test del metodo InsertarFoto de la clase Conexion
    /// </summary>
    [Test]
    [Category("Insertar")]
    public void TestInsertarFoto()
    {
      conexion.Conectar();
      idFoto = conexion.InsertarFoto(foto, lugar.Id);
      Assert.AreEqual(true, idFoto > 0);
      conexion.Desconectar();
    }

    /// <summary>
    /// Test de excepciones del metodo InsertarFoto
    /// </summary>
    [Test]
    [Category("Insertar")]
    public void TestExcepcionInsertarFoto()
    {
      lugar.Id = 0;
      Assert.Catch<BaseDeDatosExcepcion>(ExcepcionInsertarFoto);

      lugar.Id = idLugar;
      foto.Contenido = null;
      Assert.Catch<ReferenciaNulaExcepcion>(ExcepcionInsertarFoto);

      foto = null;
      Assert.Catch<ReferenciaNulaExcepcion>(ExcepcionInsertarFoto);
    }

    /// <summary>
    /// Metodo que permite testear las excepciones
    /// </summary>
    private void ExcepcionInsertarFoto()
    {
      conexion.Conectar();
      conexion.InsertarFoto(foto, lugar.Id);
      conexion.Desconectar();
    }

    //Select

    /// <summary>
    /// Test del metodo ConsultarLugarTuristico de la clase Conexion
    /// </summary>
    [Test]
    [Category("Consultar")]
    public void TestConsultarLugarTuristico()
    {
      conexion.Conectar();

      Assert.AreEqual(true, lugar.Equals(conexion.ConsultarLugarTuristico(lugar.Id)));
      
      conexion.Desconectar();
    }

    /// <summary>
    /// Test del metodo ConsultarListaLugarTuristico de la clase Conexion
    /// </summary>
    [Test]
    [Category("Consultar")]
    public void TestConsultarListaLugarTuristico()
    {
      lugar = new LugarTuristico();
      lugar.Id = idLugar;
      lugar.Nombre = "Parque Generalisimo de Miranda";
      lugar.Costo = 0;
      lugar.Descripcion = "Lugar al aire libre";
      lugar.Activar = true;

      conexion.Conectar();
      Assert.AreEqual(true, conexion.ConsultarListaLugarTuristico(1, lugar.Id).Contains(lugar));
      conexion.Desconectar();
    }

    /// <summary>
    /// Test del metodo ConsultarActividades de la clase Conexion
    /// </summary>
    [Test]
    [Category("Consultar")]
    public void TestConsultarActividades()
    {
      actividad.Foto.Contenido = null;
      actividad.Foto.Ruta += "ac-" + actividad.Id;

      conexion.Conectar();
      Assert.AreEqual(true, conexion.ConsultarActividades(lugar.Id).Contains(actividad));
      conexion.Desconectar();
    }

    /// <summary>
    /// Test del metodo ConsultarActividad de la clase Conexion
    /// </summary>
    [Test]
    [Category("Consultar")]
    public void TestConsultarActividad()
    {
      actividad.Foto.Contenido = null;
      actividad.Foto.Ruta += "ac-" + actividad.Id;

      conexion.Conectar();
      Assert.AreEqual(true, actividad.Equals(conexion.ConsultarActividad(actividad.Id)));
      conexion.Desconectar();
    }

    /// <summary>
    /// Test del metodo ConsultarNombreActividades de la clase Conexion
    /// </summary>
    [Test]
    [Category("Consultar")]
    public void TestConsultarNombreActividades()
    {
      actividad = new Actividad();
      actividad.Nombre = "Parque Generalisimo de Miranda";

      conexion.Conectar();
      Assert.AreEqual(true, conexion.ConsultarNombreActividades(lugar.Id).Contains(actividad));
      conexion.Desconectar();
    }

    /// <summary>
    /// Test del metodo ConsultarHorarios de la clase Conexion
    /// </summary>
    [Test]
    [Category("Consultar")]
    public void TestConsultarHorarios()
    {
      conexion.Conectar();
      Assert.AreEqual(true, conexion.ConsultarHorarios(lugar.Id).Contains(horario));
      conexion.Desconectar();
    }

    /// <summary>
    /// Test del metodo ConsultarDiaHorario de la clase Conexion
    /// </summary>
    [Test]
    [Category("Consultar")]
    public void TestConsultarDiaHorario()
    {
      horario.Id = 0;
      horario.DiaSemana = 0;

      conexion.Conectar();
      Assert.AreEqual(true, horario.Equals(conexion.ConsultarDiaHorario(lugar.Id, (int)DateTime.Now.DayOfWeek)));
      conexion.Desconectar();
    }

    /// <summary>
    /// Test del metodo ConsultarFotos de la clase Conexion
    /// </summary>
    [Test]
    [Category("Consultar")]
    public void TestConsultarFotos()
    {
      foto.Contenido = null;
      foto.Ruta += "lt-fo-" + actividad.Id;

      conexion.Conectar();
      Assert.AreEqual(true, conexion.ConsultarFotos(lugar.Id).Contains(foto));
      conexion.Desconectar();
    }

    //Actualizar

    /// <summary>
    /// Test del metodo ActualizarLugarTuristico de la clase Conexion
    /// </summary>
    [Test]
    [Category("Actualizar")]
    public void TestActualizarLugarTuristico()
    {
      lugar.Nombre = "A";
      lugar.Costo = 500;
      lugar.Descripcion = "B";
      lugar.Direccion = "C";
      lugar.Correo = "probando@ucab.edu.ve";
      lugar.Telefono = 02123641438;
      lugar.Latitud = 8.99;
      lugar.Longitud = 9.99;
      lugar.Activar = false;

      conexion.Conectar();
      conexion.ActualizarLugarTuristico(lugar);
      Assert.AreEqual(true, lugar.Equals(conexion.ConsultarLugarTuristico(lugar.Id)));
      conexion.Desconectar();
    }

    /// <summary>
    /// Test de excepciones del metodo ActualizarLugarTuristico
    /// </summary>
    [Test]
    [Category("Actualizar")]
    public void TestExcepcionActualizarLugarTuristico()
    {
      lugar.Nombre = null;
      Assert.Catch<CasteoInvalidoExcepcion>(ExcepcionActualizarLugarTuristico);

      lugar = null;
      Assert.Catch<ReferenciaNulaExcepcion>(ExcepcionActualizarLugarTuristico);
    }

    /// <summary>
    /// Metodo que permite testear excepciones
    /// </summary>
    private void ExcepcionActualizarLugarTuristico()
    {
      conexion.Conectar();
      conexion.ActualizarLugarTuristico(lugar);
      conexion.Desconectar();
    }

    /// <summary>
    /// Test del metodo ActualizarActividad de la clase Conexion
    /// </summary>
    [Test]
    [Category("Actualizar")]
    public void TestActualizarActividad()
    {
      actividad.Foto.Contenido = new Byte[2];
      actividad.Foto.Ruta += "ac-" + actividad.Id;
      actividad.Nombre = "ABC";
      actividad.Duracion = new TimeSpan(19, 0, 0);
      actividad.Descripcion = "Haciendo PU simples";
      actividad.Activar = false;

      conexion.Conectar();
      conexion.ActualizarActividad(actividad);

      actividad.Foto.Contenido = null;

      Assert.AreEqual(true, conexion.ConsultarActividades(lugar.Id).Contains(actividad));
      conexion.Desconectar();
    }

    /// <summary>
    /// Test de excepciones del metodo ActualizarActividad
    /// </summary>
    [Test]
    [Category("Actualizar")]
    public void TestExcepcionActualizarActividad()
    {
      actividad.Nombre = null;
      Assert.Catch<CasteoInvalidoExcepcion>(ExcepcionActualizarActividad);

      actividad = null;
      Assert.Catch<ReferenciaNulaExcepcion>(ExcepcionActualizarActividad);
    }

    /// <summary>
    /// Metodo que permite testear excepciones
    /// </summary>
    private void ExcepcionActualizarActividad()
    {
      conexion.Conectar();
      conexion.ActualizarActividad(actividad);
      conexion.Desconectar();
    }

    /// <summary>
    /// Test del metodo ActualizarHorario de la clase Conexion
    /// </summary>
    [Test]
    [Category("Actualizar")]
    public void TestActualizarHorario()
    {
      horario.DiaSemana = (int) DateTime.Now.DayOfWeek + 1;
      horario.HoraApertura = new TimeSpan(22, 0, 0);
      horario.HoraCierre = new TimeSpan(24, 0, 0);

      conexion.Conectar();
      conexion.ActualizarHorario(horario);
      Assert.AreEqual(true, conexion.ConsultarHorarios(lugar.Id).Contains(horario));
      conexion.Desconectar();
    }

    /// <summary>
    /// Test de excepciones del metodo ActualizarHorario
    /// </summary>
    [Test]
    [Category("Actualizar")]
    public void TestExcepcionActualizarHorario()
    {
      horario.HoraApertura = TimeSpan.Zero;
      horario.HoraCierre = TimeSpan.Zero;
      Assert.DoesNotThrow(ExcepcionActualizarHorario);

      horario = null;
      Assert.Catch<ReferenciaNulaExcepcion>(ExcepcionActualizarHorario);
    }

    /// <summary>
    /// Metodo que permite testear excepciones
    /// </summary>
    private void ExcepcionActualizarHorario()
    {
      conexion.Conectar();
      conexion.ActualizarHorario(horario);
      conexion.Desconectar();
    }

    /// <summary>
    /// Test del metodo ActualizarFoto de la clase Conexion
    /// </summary>
    [Test]
    [Category("Actualizar")]
    public void TestActualizarFoto()
    {
      foto.Contenido = new Byte[12554];
      foto.Ruta += "lt-fo-" + foto.Id;

      conexion.Conectar();
      conexion.ActualizarFoto(foto);

      foto.Contenido = null;

      Assert.AreEqual(true, conexion.ConsultarFotos(lugar.Id).Contains(foto));
      conexion.Desconectar();
    }

    /// <summary>
    /// Test de excepciones del metodo ActualizarFoto
    /// </summary>
    [Test]
    [Category("Actualizar")]
    public void TestExcepcionActualizarFoto()
    {
      foto.Contenido = null;
      Assert.Catch<ReferenciaNulaExcepcion>(ExcepcionActualizarFoto);

      foto = null;
      Assert.Catch<ReferenciaNulaExcepcion>(ExcepcionActualizarFoto);
    }

    /// <summary>
    /// Metodo que permite testear excepciones
    /// </summary>
    private void ExcepcionActualizarFoto()
    {
      conexion.Conectar();
      conexion.ActualizarFoto(foto);
      conexion.Desconectar();
    }

    /// <summary>
    /// Test del metodo ActivarLugarTuristico de la clase Conexion
    /// </summary>
    [Test]
    [Category("Actualizar")]
    public void TestActivarLugarTuristico()
    {
      conexion.Conectar();
      conexion.ActivarLugarTuristico(lugar.Id, true);
      Assert.AreEqual(true, conexion.ConsultarLugarTuristico(lugar.Id).Activar);
      conexion.Desconectar();

      conexion.Conectar();
      conexion.ActivarLugarTuristico(lugar.Id, false);
      Assert.AreEqual(false, conexion.ConsultarLugarTuristico(lugar.Id).Activar);
      conexion.Desconectar();
    }

    /// <summary>
    /// Test del metodo ActivarActividad de la clase Conexion
    /// </summary>
    [Test]
    [Category("Actualizar")]
    public void TestActivarActividad()
    {
      try
      {
        conexion.Conectar();
        conexion.ActivarActividad(actividad.Id, true);
        Assert.AreEqual(true, conexion.ConsultarActividad(actividad.Id).Activar);
        conexion.Desconectar();

        conexion.Conectar();
        conexion.ActivarActividad(lugar.Id, false);

        Assert.AreEqual(false, conexion.ConsultarActividad(actividad.Id).Activar);
        conexion.Desconectar();
      }
      catch (ArgumentOutOfRangeException)
      {
        Assert.Fail("No hay actividades insertadas en la base de datos");
      }
    }

    //Eliminar

    /// <summary>
    /// Test del metodo EliminarActividad de la clase Conexion
    /// </summary>
    [Test]
    [Category("Eliminar")]
    public void TestEliminarActividad()
    {
      conexion.Conectar();
      conexion.EliminarActividad(actividad.Id);
      Assert.AreEqual(false, conexion.ConsultarActividades(lugar.Id).Contains(actividad));
      conexion.Desconectar();
    }

    /// <summary>
    /// Test del metodo EliminarActividad de la clase Conexion
    /// </summary>
    [Test]
    [Category("Eliminar")]
    public void TestEliminarFoto()
    {
      conexion.Conectar();
      conexion.EliminarFoto(foto.Id);
      Assert.AreEqual(false, conexion.ConsultarFotos(lugar.Id).Contains(foto));
      conexion.Desconectar();
    }

    /// <summary>
    /// Test del metodo EliminarHorario de la clase Conexion
    /// </summary>
    [Test]
    [Category("Eliminar")]
    public void TestEliminarHorario()
    {
      conexion.Conectar();
      conexion.EliminarFoto(horario.Id);
      Assert.AreEqual(false, conexion.ConsultarHorarios(lugar.Id).Contains(horario));
      conexion.Desconectar();
    }
  }
}
