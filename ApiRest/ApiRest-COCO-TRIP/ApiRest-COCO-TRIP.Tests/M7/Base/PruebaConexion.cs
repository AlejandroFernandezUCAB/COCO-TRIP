using System;
using System.Linq;
using NUnit.Framework;
using ApiRest_COCO_TRIP.Models.M7.Base;
using ApiRest_COCO_TRIP.Models.M7.Dato;

namespace ApiRest_COCO_TRIP.Tests.M7.Base
{
  /// <summary>
  /// Pruebas Unitarias de la clase Conexion
  /// </summary>
  [TestFixture]
  public class PruebaConexion
  {
    private Conexion conexion;
    private LugarTuristico lugar;
    private Actividad actividad;
    private Horario horario;
    private Foto foto;

    //Falta excepciones de cada una

    /// <summary>
    /// Instancia los objetos
    /// </summary>
    [SetUp]
    public void SetConexion()
    {
      conexion = new Conexion();

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
      horario.DiaSemana = (int)Horario.Dia.Domingo;
      horario.HoraApertura = new TimeSpan(8, 0, 0);
      horario.HoraCierre = new TimeSpan(17, 0, 0);

      foto = new Foto();
      foto.Id = 1;
      foto.Contenido = imagen;
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
      Assert.AreEqual(true, conexion.InsertarLugarTuristico(lugar) > 0);
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
      Assert.AreEqual(true, conexion.InsertarActividad(actividad, lugar.Id) > 0);
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
      Assert.AreEqual(true, conexion.InsertarHorario(horario, lugar.Id) > 0);
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
      Assert.AreEqual(true, conexion.InsertarFoto(foto, lugar.Id) > 0);
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
      lugar.Id = 1;
      lugar.Nombre = "Parque Generalisimo de Miranda";
      lugar.Costo = 0;
      lugar.Descripcion = "Lugar al aire libre";
      lugar.Activar = true;

      conexion.Conectar();
      Assert.AreEqual(true, conexion.ConsultarListaLugarTuristico(1, 2).Contains(lugar));
      conexion.Desconectar();
    }

    /// <summary>
    /// Test del metodo ConsultarActividades de la clase Conexion
    /// </summary>
    [Test]
    [Category("Consultar")]
    public void TestConsultarActividades()
    {
      conexion.Conectar();
      Assert.AreEqual(true, conexion.ConsultarActividades(lugar.Id).Contains(actividad));
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
      Assert.AreEqual(true, horario.Equals(conexion.ConsultarDiaHorario(lugar.Id, (int)Horario.Dia.Domingo)));
      conexion.Desconectar();
    }

    /// <summary>
    /// Test del metodo ConsultarFotos de la clase Conexion
    /// </summary>
    [Test]
    [Category("Consultar")]
    public void TestConsultarFotos()
    {
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
    /// Test del metodo ActualizarActividad de la clase Conexion
    /// </summary>
    [Test]
    [Category("Actualizar")]
    public void TestActualizarActividad()
    {
      actividad.Foto.Contenido = new Byte[2];
      actividad.Nombre = "ABC";
      actividad.Duracion = new TimeSpan(19, 0, 0);
      actividad.Descripcion = "Haciendo PU simples";
      actividad.Activar = false;

      conexion.Conectar();
      conexion.ActualizarActividad(actividad);
      Assert.AreEqual(true, conexion.ConsultarActividades(lugar.Id).Contains(actividad));
      conexion.Desconectar();
    }

    /// <summary>
    /// Test del metodo ActualizarHorario de la clase Conexion
    /// </summary>
    [Test]
    [Category("Actualizar")]
    public void TestActualizarHorario()
    {
      horario.DiaSemana = (int)Horario.Dia.Lunes;
      horario.HoraApertura = new TimeSpan(22, 0, 0);
      horario.HoraCierre = new TimeSpan(24, 0, 0);

      conexion.Conectar();
      conexion.ActualizarHorario(horario);
      Assert.AreEqual(true, conexion.ConsultarHorarios(lugar.Id).Contains(horario));
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

      conexion.Conectar();
      conexion.ActualizarFoto(foto);
      Assert.AreEqual(true, conexion.ConsultarFotos(lugar.Id).Contains(foto));
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
        Assert.AreEqual(true, conexion.ConsultarActividades(lugar.Id)[0].Activar);
        conexion.Desconectar();

        conexion.Conectar();
        conexion.ActivarActividad(lugar.Id, false);
        Assert.AreEqual(false, conexion.ConsultarActividades(lugar.Id)[0].Activar);
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
