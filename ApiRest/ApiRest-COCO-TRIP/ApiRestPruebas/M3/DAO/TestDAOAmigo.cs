using System;
using System.IO;
using System.Reflection;
using System.Collections.Generic;
using NUnit.Framework;
using Newtonsoft.Json;
using ApiRest_COCO_TRIP.Datos.DAO;
using ApiRest_COCO_TRIP.Datos.Entity;
using ApiRest_COCO_TRIP.Datos.Fabrica;
using ApiRest_COCO_TRIP.Comun.Excepcion;

namespace ApiRestPruebas.M3.DAO
{
  /// <summary>
  /// Pruebas unitarias de DAOAmigo del Modulo 3
  /// </summary>
  [TestFixture]
  public class TestDAOAmigo
  {
    private string RutaArchivo = Path.GetDirectoryName(Uri.UnescapeDataString(new UriBuilder(Assembly.GetExecutingAssembly().CodeBase).Path)) + "\\PU\\";
    private const string ScriptsSetUp = "ScriptsSetUpTestDAOAmigo.txt";
    private const string DatoAmigo = "DatoAmigo.txt";
    private const string DatoUsuario = "DatoUsuario.txt";

    private DAOAmigo dao;

    private List<Amigo> listaAmigo;
    private List<Usuario> listaUsuario;

    [SetUp]
    public void SetUp()
    {
      dao = FabricaDAO.CrearDAOAmigo();
      listaAmigo = new List<Amigo>();
      listaUsuario = new List<Usuario>();

      string[] datosAmigo = File.ReadAllLines(RutaArchivo + DatoAmigo);
      string[] datosUsuario = File.ReadAllLines(RutaArchivo + DatoUsuario);

      foreach (string linea in datosAmigo)
      {
        listaAmigo.Add(JsonConvert.DeserializeObject<Amigo>(linea));
      }

      foreach (string linea in datosUsuario)
      {
        listaUsuario.Add(JsonConvert.DeserializeObject<Usuario>(linea));
      }

      dao.Conectar();
      dao.Comando = dao.SqlConexion.CreateCommand();
      dao.Comando.CommandText = File.ReadAllText(RutaArchivo + ScriptsSetUp);
      dao.Comando.CommandType = System.Data.CommandType.Text;
      dao.Comando.ExecuteNonQuery();
      dao.Desconectar();
    }

    [Category("Modulo 3")]
    [Category("DAOAmigo")]
    [Test]
    public void TestInsertar()
    {
      Assert.DoesNotThrow(InsertarExitoso);
    }

    public void InsertarExitoso()
    {
      Amigo amigo = FabricaEntidad.CrearEntidadAmigo();
      amigo.Activo = listaUsuario[1].Id;
      amigo.Pasivo = listaUsuario[2].Id;
      dao.Insertar(amigo);
    }

    [Category("Modulo 3")]
    [Category("DAOAmigo")]
    [Test]
    public void TestInsertarExcepcionReferenciaNula()
    {
      Assert.Catch<ReferenciaNulaExcepcion>(InsertarExcepcionReferenciaNula);
    }

    public void InsertarExcepcionReferenciaNula()
    {
      dao.Insertar(null);
    }

    [Category("Modulo 3")]
    [Category("DAOAmigo")]
    [Test]
    public void TestInsertarExcepcionBaseDeDatos()
    {
      Assert.Catch<BaseDeDatosExcepcion>(InsertarExcepcionBaseDeDatos);
    }

    public void InsertarExcepcionBaseDeDatos()
    {
      Amigo amigo = FabricaEntidad.CrearEntidadAmigo();
      amigo.Activo = listaUsuario[0].Id;
      amigo.Pasivo = 0;
      dao.Insertar(amigo);
    }

    [Category("Modulo 3")]
    [Category("DAOAmigo")]
    [Test]
    public void TestBuscarAmigosConNombreUsuario()
    {
      Usuario usuario = listaUsuario[1];
      Usuario buscando = listaUsuario[2];

      usuario.Nombre = buscando.NombreUsuario;
      List<Entidad> lista = dao.BuscarAmigos(usuario);

      usuario = (Usuario) lista[0];
      Assert.AreEqual(true, lista.Count == 1 && usuario.NombreUsuario == buscando.NombreUsuario);
    }

    [Category("Modulo 3")]
    [Category("DAOAmigo")]
    [Test]
    public void TestBuscarAmigosConNombre()
    {
      Usuario usuario = listaUsuario[1];
      Usuario buscando = listaUsuario[2];

      usuario.Nombre = buscando.Nombre;
      List<Entidad> lista = dao.BuscarAmigos(usuario);

      usuario = (Usuario)lista[0];
      Assert.AreEqual(true, lista.Count == 1 && usuario.NombreUsuario == buscando.NombreUsuario);
    }

    [Category("Modulo 3")]
    [Category("DAOAmigo")]
    [Test]
    public void TestBuscarAmigosConApellido()
    {
      Usuario usuario = listaUsuario[1];
      Usuario buscando = listaUsuario[2];

      usuario.Nombre = buscando.Apellido;
      List<Entidad> lista = dao.BuscarAmigos(usuario);

      usuario = (Usuario)lista[0];
      Assert.AreEqual(true, lista.Count == 1 && usuario.NombreUsuario == buscando.NombreUsuario);
    }

    [Category("Modulo 3")]
    [Category("DAOAmigo")]
    [Test]
    public void TestBuscarAmigosExcepcionCasteo()
    {
      Assert.Catch<CasteoInvalidoExcepcion>(BuscarAmigosExcepcionCasteo);
    }

    public void BuscarAmigosExcepcionCasteo ()
    {
      listaUsuario[0].Nombre = null;
      dao.BuscarAmigos(listaUsuario[0]);
    }

    [Category("Modulo 3")]
    [Category("DAOAmigo")]
    [Test]
    public void TestBuscarAmigosExcepcionReferenciaNula()
    {
      Assert.Catch<ReferenciaNulaExcepcion>(BuscarAmigosExcepcionReferenciaNula);
    }

    public void BuscarAmigosExcepcionReferenciaNula()
    {
      dao.BuscarAmigos(null);
    }

    [Category("Modulo 3")]
    [Category("DAOAmigo")]
    [Test]
    public void TestConsultarPorId()
    {
      Entidad resultado = dao.ConsultarPorId(listaAmigo[0]);
      Assert.AreEqual(true, resultado.Id != 0);
    }

    [Category("Modulo 3")]
    [Category("DAOAmigo")]
    [Test]
    public void TestConsultarPorIdExcepcion()
    {
      Assert.Catch<ReferenciaNulaExcepcion>(ConsultarPorIdExcepcion);
    }

    public void ConsultarPorIdExcepcion()
    {
      dao.ConsultarPorId(null);
    }

    [Category("Modulo 3")]
    [Category("DAOAmigo")]
    [Test]
    public void TestConsultarLista()
    {
      List<Entidad> lista = dao.ConsultarLista(listaUsuario[0]);
      Usuario usuario = (Usuario) lista[0];
      Assert.AreEqual(true, lista.Count == 1 && usuario.NombreUsuario == listaUsuario[1].NombreUsuario);
    }

    [Category("Modulo 3")]
    [Category("DAOAmigo")]
    [Test]
    public void TestConsultarListaExcepcion()
    {
      Assert.Catch<ReferenciaNulaExcepcion>(ConsultarListaExcepcion);
    }

    public void ConsultarListaExcepcion()
    {
      dao.ConsultarLista(null);
    }

    [Category("Modulo 3")]
    [Category("DAOAmigo")]
    [Test]
    public void TestConsultarListaNotificaciones()
    {
      List<Entidad> lista = dao.ConsultarListaNotificaciones(listaUsuario[2]);
      Usuario usuario = (Usuario) lista[0];
      Assert.AreEqual(true, lista.Count == 1 && usuario.NombreUsuario == listaUsuario[0].NombreUsuario);
    }

    [Category("Modulo 3")]
    [Category("DAOAmigo")]
    [Test]
    public void TestConsultarListaNotificacionesExcepcion()
    {
      Assert.Catch<ReferenciaNulaExcepcion>(ConsultarListaNotificacionesExcepcion);
    }

    public void ConsultarListaNotificacionesExcepcion()
    {
      dao.ConsultarListaNotificaciones(null);
    }

    [Category("Modulo 3")]
    [Category("DAOAmigo")]
    [Test]
    public void TestRechazarNotificacion()
    {
      Assert.DoesNotThrow(RechazarNotificacionExitoso);
    }

    public void RechazarNotificacionExitoso()
    {
      Amigo amigo = FabricaEntidad.CrearEntidadAmigo();
      amigo.Pasivo = listaUsuario[2].Id;
      amigo.Activo = listaUsuario[0].Id;
      dao.RechazarNotificacion(amigo);
    }

    [Category("Modulo 3")]
    [Category("DAOAmigo")]
    [Test]
    public void TestRechazarNotificacionExcepcion()
    {
      Assert.Catch<ReferenciaNulaExcepcion>(RechazarNotificacionExcepcion);
    }

    public void RechazarNotificacionExcepcion()
    {
      dao.RechazarNotificacion(null);
    }

    [Category("Modulo 3")]
    [Category("DAOAmigo")]
    [Test]
    public void TestAceptarNotificacion()
    {
      Assert.DoesNotThrow(AceptarNotificacionExitoso);
    }

    public void AceptarNotificacionExitoso()
    {
      Amigo amigo = FabricaEntidad.CrearEntidadAmigo();
      amigo.Pasivo = listaUsuario[2].Id;
      amigo.Activo = listaUsuario[0].Id;
      dao.AceptarNotificacion(amigo);
    }

    [Category("Modulo 3")]
    [Category("DAOAmigo")]
    [Test]
    public void TestAceptarNotificacionExcepcion()
    {
      Assert.Catch<ReferenciaNulaExcepcion>(AceptarNotificacionExcepcion);
    }

    public void AceptarNotificacionExcepcion()
    {
      dao.AceptarNotificacion(null);
    }

    [Category("Modulo 3")]
    [Category("DAOAmigo")]
    [Test]
    public void TestEliminar()
    {
      Assert.DoesNotThrow(EliminarExitoso);
    }

    public void EliminarExitoso()
    {
      Amigo amigo = FabricaEntidad.CrearEntidadAmigo();
      amigo.Pasivo = listaUsuario[1].Id;
      amigo.Activo = listaUsuario[0].Id;
      dao.Eliminar(amigo);
    }

    [Category("Modulo 3")]
    [Category("DAOAmigo")]
    [Test]
    public void TestEliminarExcepcion()
    {
      Assert.Catch<ReferenciaNulaExcepcion>(EliminarExcepcion);
    }

    public void EliminarExcepcion()
    {
      dao.Eliminar(null);
    }
  }

}
