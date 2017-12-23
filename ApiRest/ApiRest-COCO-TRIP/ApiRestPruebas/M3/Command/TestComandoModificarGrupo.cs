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

namespace ApiRestPruebas.M3.Command
{
  /// <summary>
  /// Pruebas unitarias de ComandoModificarGrupo
  /// </summary>
  [TestFixture]
  public class TestComandoModificarGrupo
  {
    private string RutaArchivo = Path.GetDirectoryName(Uri.UnescapeDataString(new UriBuilder(Assembly.GetExecutingAssembly().CodeBase).Path)) + "\\PU\\";
    private const string ScriptsSetUp = "ScriptsSetUpTestComandoModificarGrupo.txt";
    private const string DatoGrupo = "DatoGrupo.txt";
    private const string DatoFoto = "PUFoto.jpg";

    private Comando comando;
    private List<Grupo> listaGrupo;
    private byte[] foto;

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

      foto = File.ReadAllBytes(RutaArchivo + DatoFoto);

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
    public void TestComandoModificarGrupoSinFotoExitoso()
    {
      Assert.DoesNotThrow(ComandoModificarGrupoSinFotoExitoso);
    }

    public void ComandoModificarGrupoSinFotoExitoso()
    {
      listaGrupo[1].Id = listaGrupo[0].Id;
      comando = FabricaComando.CrearComandoModificarGrupo(listaGrupo[1]);
      comando.Ejecutar();
    }

    [Category("Modulo 3")]
    [Category("Comando")]
    [Test]
    public void TestComandoModificarConFotoExitoso()
    {
      Assert.DoesNotThrow(ComandoModificarGrupoConFotoExitoso);
    }

    public void ComandoModificarGrupoConFotoExitoso()
    {
      listaGrupo[1].Id = listaGrupo[0].Id;
      listaGrupo[1].ContenidoFoto = foto;
      comando = FabricaComando.CrearComandoModificarGrupo(listaGrupo[1]);
      comando.Ejecutar();
    }
  }
}
