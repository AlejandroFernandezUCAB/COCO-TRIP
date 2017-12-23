using System;
using System.IO;
using System.Reflection;
using System.Collections.Generic;
using NUnit.Framework;
using Newtonsoft.Json;
using ApiRest_COCO_TRIP.Negocio.Command;
using ApiRest_COCO_TRIP.Negocio.Fabrica;
using ApiRest_COCO_TRIP.Datos.Entity;

namespace ApiRestPruebas.M3.Command
{
  /// <summary>
  /// Pruebas unitarias de ComandoAgregarAmigo
  /// </summary>
  [TestFixture]
  public class TestComandoAgregarAmigo
  {
    private string RutaArchivo = Path.GetDirectoryName(Uri.UnescapeDataString(new UriBuilder(Assembly.GetExecutingAssembly().CodeBase).Path)) + "\\PU\\";
    private const string ScriptsSetUp = "ScriptsSetUpTestComandoAgregarAmigo.txt";
    private const string DatoUsuario = "DatoUsuario.txt";

    private Comando comando;
    private List<Usuario> listaUsuario;

    [SetUp]
    public void SetUp()
    {
      listaUsuario = new List<Usuario>();
      string[] datosUsuario = File.ReadAllLines(RutaArchivo + DatoUsuario);

      foreach (string linea in datosUsuario)
      {
        listaUsuario.Add(JsonConvert.DeserializeObject<Usuario>(linea));
      }
    }

    [Category("Modulo 3")]
    [Category("Comando")]
    [Test]
    public void TestComandoAgregarAmigoExitoso()
    {
      Assert.DoesNotThrow(ComandoAgregarAmigoExitoso);
    }

    public void ComandoAgregarAmigoExitoso()
    {
      comando = FabricaComando.CrearComandoAgregarAmigo(listaUsuario[1].Id, listaUsuario[2].NombreUsuario);
      comando.Ejecutar();
    }
  }

}
