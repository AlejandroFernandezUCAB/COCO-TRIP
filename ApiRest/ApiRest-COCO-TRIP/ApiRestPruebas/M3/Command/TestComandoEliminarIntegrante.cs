using System;
using System.IO;
using System.Reflection;
using System.Collections.Generic;
using NUnit.Framework;
using Newtonsoft.Json;
using ApiRest_COCO_TRIP.Negocio.Command;
using ApiRest_COCO_TRIP.Negocio.Fabrica;
using ApiRest_COCO_TRIP.Datos.Entity;
using ApiRest_COCO_TRIP.Datos.Fabrica;
using ApiRest_COCO_TRIP.Datos.DAO;
using System.Web.Http;

namespace ApiRestPruebas.M3.Command
{
  /// <summary>
  /// Pruebas unitarias de ComandoEliminarIntegrante
  /// </summary>
  [TestFixture]
  public class TestComandoEliminarIntegrante
  {
    private string RutaArchivo = Path.GetDirectoryName(Uri.UnescapeDataString(new UriBuilder(Assembly.GetExecutingAssembly().CodeBase).Path)) + "\\PU\\";
    private const string ScriptsSetUp = "ScriptsSetUpTestComandoEliminarIntegrante.txt";
    private const string DatoGrupo = "DatoGrupo.txt";
    private const string DatoUsuario = "DatoUsuario.txt";

    private Comando comando;
    private List<Grupo> listaGrupo;
    private List<Usuario> listaUsuario;

    [SetUp]
    public void SetUp()
    {
      DAOGrupo dao = FabricaDAO.CrearDAOGrupo();

      listaGrupo = new List<Grupo>();
      string[] datosGrupo = File.ReadAllLines(RutaArchivo + DatoGrupo);

      foreach (string linea in datosGrupo)
      {
        listaGrupo.Add(JsonConvert.DeserializeObject<Grupo>(linea));
      }

      listaUsuario = new List<Usuario>();
      string[] datosUsuario = File.ReadAllLines(RutaArchivo + DatoUsuario);

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
    [Category("Comando")]
    [Test]
    public void TestComandoEliminarIntegranteExitoso()
    {
      Assert.DoesNotThrow(ComandoEliminarIntegranteExitoso);
    }

    public void ComandoEliminarIntegranteExitoso()
    {
      comando = FabricaComando.CrearComandoEliminarIntegrante(listaGrupo[0].Id, listaUsuario[1].NombreUsuario);
      comando.Ejecutar();
    }

    [Category("Modulo 3")]
    [Category("Comando")]
    [Test]
    public void TestComandoEliminarIntegranteExcepcion()
    {
      Assert.Catch<HttpResponseException>(ComandoEliminarIntegranteExcepcion);
    }

    public void ComandoEliminarIntegranteExcepcion()
    {
      comando = FabricaComando.CrearComandoEliminarIntegrante(listaGrupo[0].Id, null);
      comando.Ejecutar();
    }
  }

}
