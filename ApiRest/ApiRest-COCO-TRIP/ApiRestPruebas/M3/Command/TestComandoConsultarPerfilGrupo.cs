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
  /// Pruebas unitarias de ComandoConsultarMiembroGrupo
  /// </summary>
  [TestFixture]
  public class TestComandoConsultarPerfilGrupo
  {
    private string RutaArchivo = Path.GetDirectoryName(Uri.UnescapeDataString(new UriBuilder(Assembly.GetExecutingAssembly().CodeBase).Path)) + "\\PU\\";
    private const string ScriptsSetUp = "ScriptsSetUpTestComandoConsultarPerfilGrupo.txt";
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
    public void TestComandoConsultarPerfilGrupoSinFotoExitoso()
    {
      comando = FabricaComando.CrearComandoConsultarPerfilGrupo(listaGrupo[0].Id);
      comando.Ejecutar();
      Grupo grupo = (Grupo) comando.Retornar();
      Assert.AreEqual(true, grupo.Nombre == listaGrupo[0].Nombre && grupo.CantidadIntegrantes == 3);
    }

    [Category("Modulo 3")]
    [Category("Comando")]
    [Test]
    public void TestComandoConsultarPerfilGrupoConFotoExitoso()
    {
      listaGrupo[0].ContenidoFoto = Convert.ToBase64String(foto);
      comando = FabricaComando.CrearComandoModificarGrupo(listaGrupo[0], 1);
      comando.Ejecutar();

      comando = FabricaComando.CrearComandoConsultarPerfilGrupo(listaGrupo[0].Id);
      comando.Ejecutar();
      Grupo grupo = (Grupo)comando.Retornar();
      Assert.AreEqual(true, grupo.Nombre == listaGrupo[0].Nombre && grupo.CantidadIntegrantes == 3 && grupo.RutaFoto != null);
    }
  }
}
