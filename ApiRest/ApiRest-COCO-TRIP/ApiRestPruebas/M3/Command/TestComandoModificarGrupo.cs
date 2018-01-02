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
  /// Pruebas unitarias de ComandoModificarGrupo
  /// </summary>
  [TestFixture]
  public class TestComandoModificarGrupo
  {
    private string RutaArchivo = Path.GetDirectoryName(Uri.UnescapeDataString(new UriBuilder(Assembly.GetExecutingAssembly().CodeBase).Path)) + "\\PU\\";
    private const string ScriptsSetUp = "ScriptsSetUpTestComandoModificarGrupo.txt";
    private const string DatoGrupo = "DatoGrupo.txt";
    private const string DatoUsuario = "DatoUsuario.txt";
    private const string DatoFoto = "PUFoto.jpg";

    private Comando comando;
    private List<Grupo> listaGrupo;
    private List<Usuario> listaUsuario;
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

      listaUsuario = new List<Usuario>();
      string[] datosUsuario = File.ReadAllLines(RutaArchivo + DatoUsuario);

      foreach (string linea in datosUsuario)
      {
        listaUsuario.Add(JsonConvert.DeserializeObject<Usuario>(linea));
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
    public void TestComandoModificarGrupoSinFotoLiderExitoso()
    {
      Assert.DoesNotThrow(ComandoModificarGrupoSinFotoLiderExitoso);
    }

    public void ComandoModificarGrupoSinFotoLiderExitoso()
    {
      listaGrupo[1].Id = listaGrupo[0].Id;
      comando = FabricaComando.CrearComandoModificarGrupo(listaGrupo[1], listaUsuario[0].Id);
      comando.Ejecutar();
    }

    [Category("Modulo 3")]
    [Category("Comando")]
    [Test]
    public void TestComandoModificarConFotoLiderExitoso()
    {
      Assert.DoesNotThrow(ComandoModificarGrupoConFotoLiderExitoso);
    }

    public void ComandoModificarGrupoConFotoLiderExitoso()
    {
      listaGrupo[1].Id = listaGrupo[0].Id;
      listaGrupo[1].ContenidoFoto = Convert.ToBase64String(foto);
      comando = FabricaComando.CrearComandoModificarGrupo(listaGrupo[1], listaUsuario[0].Id);
      comando.Ejecutar();
    }

    [Category("Modulo 3")]
    [Category("Comando")]
    [Test]
    public void TestComandoModificarGrupoSinFotoMiembroExitoso()
    {
      Assert.Catch<HttpResponseException>(ComandoModificarGrupoSinFotoMiembroExitoso);
    }

    public void ComandoModificarGrupoSinFotoMiembroExitoso()
    {
      listaGrupo[1].Id = listaGrupo[0].Id;
      comando = FabricaComando.CrearComandoModificarGrupo(listaGrupo[1], listaUsuario[1].Id);
      comando.Ejecutar();
    }

    [Category("Modulo 3")]
    [Category("Comando")]
    [Test]
    public void TestComandoModificarConFotoMiembroExitoso()
    {
      Assert.Catch<HttpResponseException>(ComandoModificarGrupoConFotoMiembroExitoso);
    }

    public void ComandoModificarGrupoConFotoMiembroExitoso()
    {
      listaGrupo[1].Id = listaGrupo[0].Id;
      listaGrupo[1].ContenidoFoto = Convert.ToBase64String(foto);
      comando = FabricaComando.CrearComandoModificarGrupo(listaGrupo[1], listaUsuario[1].Id);
      comando.Ejecutar();
    }

    [Category("Modulo 3")]
    [Category("Comando")]
    [Test]
    public void TestComandoModificarExcepcion()
    {
      Assert.Catch<HttpResponseException>(ComandoModificarExcepcion);
    }

    public void ComandoModificarExcepcion()
    {
      comando = FabricaComando.CrearComandoModificarGrupo(null, listaUsuario[0].Id);
      comando.Ejecutar();
    }
  }

}
