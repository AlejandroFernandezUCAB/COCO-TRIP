using System;
using System.IO;
using System.Reflection;
using System.Collections.Generic;
using NUnit.Framework;
using Newtonsoft.Json;
using ApiRest_COCO_TRIP.Datos.DAO;
using ApiRest_COCO_TRIP.Datos.Entity;
using ApiRest_COCO_TRIP.Datos.Fabrica;

namespace ApiRestPruebas.M3.DAO
{
  /// <summary>
  /// Pruebas unitarias de DAOGrupo del Modulo 3
  /// </summary>
  [TestFixture]
  public class TestDAOGrupo
  {
    private string RutaArchivo = Path.GetDirectoryName(Uri.UnescapeDataString(new UriBuilder(Assembly.GetExecutingAssembly().CodeBase).Path)) + "\\PU\\";
    private const string ScriptsSetUp = "ScriptsSetUpTestDAOGrupo.txt";
    private const string DatoGrupo = "DatoGrupo.txt";
    private const string DatoUsuario = "DatoUsuario.txt";

    private DAOGrupo dao;

    private List<Grupo> listaGrupo;
    private List<Usuario> listaUsuario; 


    [SetUp]
    public void SetUp()
    {
      dao = FabricaDAO.CrearDAOGrupo();
      listaGrupo = new List<Grupo>();
      listaUsuario = new List<Usuario>();

      string[] datosGrupo = File.ReadAllLines(RutaArchivo + DatoGrupo);
      string[] datosUsuario = File.ReadAllLines(RutaArchivo + DatoUsuario);

      foreach(string linea in datosGrupo)
      {
        listaGrupo.Add(JsonConvert.DeserializeObject<Grupo>(linea));
      }

      foreach(string linea in datosUsuario)
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
    [Category("DAOGrupo")]
    [Test]
    public void TestInsertarId()
    {
      Grupo grupo = (Grupo) dao.InsertarId (listaGrupo[0]);
      Assert.AreEqual(true, grupo.Id != 0);
    }

    [Category("Modulo 3")]
    [Category("DAOGrupo")]
    [Test]
    public void TestAgregarIntegrante()
    {
      Assert.DoesNotThrow(AgregarIntegranteExitoso);
    }

    public void AgregarIntegranteExitoso()
    {
      dao.AgregarIntegrante(listaGrupo[0], listaUsuario[1]);
    }

    [Category("Modulo 3")]
    [Category("DAOGrupo")]
    [Test]
    public void TestConsultarPorId()
    {
      Grupo grupo = (Grupo) dao.ConsultarPorId (listaGrupo[0]);
      Assert.AreEqual(true, grupo.Nombre == listaGrupo[0].Nombre);
    }

    [Category("Modulo 3")]
    [Category("DAOGrupo")]
    [Test]
    public void TestConsultarLista()
    {
      List<Entidad> lista = dao.ConsultarLista(listaUsuario[0]);
      Assert.AreEqual(true, lista.Count == 2);
    }

    [Category("Modulo 3")]
    [Category("DAOGrupo")]
    [Test]
    public void TestConsultarUltimoGrupo()
    {
      Grupo grupo = (Grupo)dao.ConsultarUltimoGrupo(listaUsuario[0]);
      Assert.AreEqual(true, grupo.Nombre == listaGrupo[0].Nombre);
    }

    [Category("Modulo 3")]
    [Category("DAOGrupo")]
    [Test]
    public void TestConsultarMiembros()
    {
      List<Entidad> lista = dao.ConsultarMiembros(listaGrupo[0]);

      Usuario usuario = (Usuario) lista[0];
      Assert.AreEqual(true, usuario.NombreUsuario == listaUsuario[0].NombreUsuario && lista.Count == 1);
    }

    [Category("Modulo 3")]
    [Category("DAOGrupo")]
    [Test]
    public void TestConsultarMiembrosExceptoLider()
    {
      List<Entidad> lista = dao.ConsultarMiembrosExceptoLider(listaGrupo[0]);
      Assert.AreEqual(true, lista.Count == 0);
    }

    [Category("Modulo 3")]
    [Category("DAOGrupo")]
    [Test]
    public void TestConsultarMiembrosSinGrupo ()
    {
      List<Entidad> lista = dao.ConsultarMiembrosSinGrupo(listaGrupo[0], listaUsuario[0]);
      Assert.AreEqual(true, lista[0].Id == listaUsuario[1].Id && lista[1].Id == listaUsuario[2].Id && lista.Count == 2);
    }

    [Category("Modulo 3")]
    [Category("DAOGrupo")]
    [Test]
    public void TestConsultarLider ()
    {
      Usuario usuario = (Usuario) dao.ConsultarLider(listaGrupo[0]);
      Assert.AreEqual(true, usuario.NombreUsuario == listaUsuario[0].NombreUsuario);
    }

    [Category("Modulo 3")]
    [Category("DAOGrupo")]
    [Test]
    public void TestEliminar ()
    {
      Assert.DoesNotThrow(EliminarExitoso);
    }

    public void EliminarExitoso ()
    {
      dao.Eliminar(listaGrupo[0]);
    }

    [Category("Modulo 3")]
    [Category("DAOGrupo")]
    [Test]
    public void TestEliminarIntegrante ()
    {
      Assert.DoesNotThrow(EliminarIntegranteExitoso);
    }

    public void EliminarIntegranteExitoso ()
    {
      dao.EliminarIntegrante(listaGrupo[1], listaUsuario[0]);
    }

    [Category("Modulo 3")]
    [Category("DAOGrupo")]
    [Test]
    public void TestAbandonarGrupo()
    {
      Assert.DoesNotThrow(AbandonarGrupoExitoso);
    }

    public void AbandonarGrupoExitoso ()
    {
      dao.AbandonarGrupo(listaGrupo[1], listaUsuario[0]);
    }

    [Category("Modulo 3")]
    [Category("DAOGrupo")]
    [Test]
    public void TestActualizar ()
    {
      Assert.DoesNotThrow(ActualizarExitoso);
    }

    public void ActualizarExitoso ()
    {
      Grupo grupo = listaGrupo[0];
      grupo.Nombre = "Estudiantes de Ingenieria Informatica UCAB";
      dao.Actualizar(grupo);
    }
  }
}
