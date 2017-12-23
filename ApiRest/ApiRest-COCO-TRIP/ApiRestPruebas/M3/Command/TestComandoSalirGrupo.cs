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
  /// Pruebas unitarias de ComandoSalirGrupo
  /// </summary>
  [TestFixture]
  public class TestComandoSalirGrupo
  {
    private string RutaArchivo = Path.GetDirectoryName(Uri.UnescapeDataString(new UriBuilder(Assembly.GetExecutingAssembly().CodeBase).Path)) + "\\PU\\";
    private const string ScriptsSetUp = "ScriptsSetUpTestComandoSalirGrupo.txt";
    private const string DatoUsuario = "DatoUsuario.txt";
    private const string DatoGrupoo = "DatoGrupo.txt";

    private Comando comando;
    private List<Usuario> listaUsuario;
    private List<Grupo> listaGrupo;

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

      listaGrupo = new List<Grupo>();
      string[] datosGrupo = File.ReadAllLines(RutaArchivo + DatoGrupoo);

      foreach (string linea in datosGrupo)
      {
        listaGrupo.Add(JsonConvert.DeserializeObject<Grupo>(linea));
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
    public void TestComandoSalirGrupoMiembroExitoso ()
    {
      Assert.DoesNotThrow(ComandoSalirGrupoMiembroExitoso);
    }

    public void ComandoSalirGrupoMiembroExitoso()
    {
      comando = FabricaComando.CrearComandoSalirGrupo(listaGrupo[0].Id, listaUsuario[1].Id);
      comando.Ejecutar();
    }

    [Category("Modulo 3")]
    [Category("Comando")]
    [Test]
    public void TestComandoSalirGrupoLiderExitoso()
    {
      Assert.DoesNotThrow(ComandoSalirGrupoLiderExitoso);
    }

    public void ComandoSalirGrupoLiderExitoso()
    {
      comando = FabricaComando.CrearComandoSalirGrupo(listaGrupo[0].Id, listaUsuario[0].Id);
      comando.Ejecutar();
    }
  }
}
