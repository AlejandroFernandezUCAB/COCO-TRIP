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
  /// Pruebas unitarias de ComandoEliminarAmigo
  /// </summary>
  [TestFixture]
  public class TestComandoEliminarAmigo
  {
    private string RutaArchivo = Path.GetDirectoryName(Uri.UnescapeDataString(new UriBuilder(Assembly.GetExecutingAssembly().CodeBase).Path)) + "\\PU\\";
    private const string ScriptsSetUp = "ScriptsSetUpTestComandoEliminarAmigo.txt";
    private const string DatoUsuario = "DatoUsuario.txt";

    private Comando comando;
    private List<Usuario> listaUsuario;

    [SetUp]
    public void SetUp()
    {
      DAOAmigo dao = FabricaDAO.CrearDAOAmigo();

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
    public void TestComandoEliminarAmigoExitoso()
    {
      Assert.DoesNotThrow(ComandoEliminarAmigoExitoso);
    }

    public void ComandoEliminarAmigoExitoso()
    {
      comando = FabricaComando.CrearComandoEliminarAmigo(listaUsuario[0].Id, listaUsuario[1].NombreUsuario);
      comando.Ejecutar();
    }

    [Category("Modulo 3")]
    [Category("Comando")]
    [Test]
    public void TestComandoEliminarAmigoExcepcion()
    {
      Assert.Catch<HttpResponseException>(ComandoEliminarAmigoExcepcion);
    }

    public void ComandoEliminarAmigoExcepcion()
    {
      comando = FabricaComando.CrearComandoEliminarAmigo(listaUsuario[0].Id, null);
      comando.Ejecutar();
    }
  }
}
