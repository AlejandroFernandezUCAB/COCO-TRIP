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
  /// Pruebas unitarias de ComandoEnviarNotificacionCorreo
  /// </summary>
  [TestFixture]
  public class TestComandoEnviarNotificacionCorreo
  {
    private string RutaArchivo = Path.GetDirectoryName(Uri.UnescapeDataString(new UriBuilder(Assembly.GetExecutingAssembly().CodeBase).Path)) + "\\PU\\";
    private const string ScriptsSetUp = "ScriptsSetUpTestComandoEnviarNotificacionCorreo.txt";
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
    public void TestComandoEnviarNotificacionCorreoExitoso()
    {
      Assert.DoesNotThrow(ComandoEnviarNotificacionCorreoExitoso);
    }

    public void ComandoEnviarNotificacionCorreoExitoso()
    {
      comando = FabricaComando.CrearComandoEnviarNotificacionCorreo(listaUsuario[1].Correo, listaUsuario[0].Id, listaUsuario[1].NombreUsuario);
      comando.Ejecutar();
    }

    [Category("Modulo 3")]
    [Category("Comando")]
    [Test]
    public void TestComandoEnviarNotificacionCorreoExcepcion()
    {
      Assert.Catch<HttpResponseException>(ComandoEnviarNotificacionCorreoExcepcion);
    }

    public void ComandoEnviarNotificacionCorreoExcepcion()
    {
      comando = FabricaComando.CrearComandoEnviarNotificacionCorreo(null, listaUsuario[0].Id, listaUsuario[1].NombreUsuario);
      comando.Ejecutar();
    }
  }

}
