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
  /// Pruebas unitarias de ComandoAgregarGrupo
  /// </summary>
  [TestFixture]
  public class TestComandoAgregarGrupo
  {
    private string RutaArchivo = Path.GetDirectoryName(Uri.UnescapeDataString(new UriBuilder(Assembly.GetExecutingAssembly().CodeBase).Path)) + "\\PU\\";
    private const string ScriptsSetUp = "ScriptsSetUpTestComandoAgregarGrupo.txt";
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
    public void TestComandoAgregarGrupoSinFotoExitoso()
    {
      Assert.DoesNotThrow(ComandoAgregarGrupoSinFotoExitoso);
    }

    public void ComandoAgregarGrupoSinFotoExitoso()
    {
      comando = FabricaComando.CrearComandoAgregarGrupo(listaGrupo[0]);
      comando.Ejecutar();
    }

    [Category("Modulo 3")]
    [Category("Comando")]
    [Test]
    public void TestComandoAgregarGrupoConFotoExitoso()
    {
      Assert.DoesNotThrow(ComandoAgregarGrupoConFotoExitoso);
    }

    public void ComandoAgregarGrupoConFotoExitoso()
    {
      listaGrupo[0].ContenidoFoto = Convert.ToBase64String(foto);
      comando = FabricaComando.CrearComandoAgregarGrupo(listaGrupo[0]);
      comando.Ejecutar();
    }

    [Category("Modulo 3")]
    [Category("Comando")]
    [Test]
    public void TestComandoAgregarGrupoExcepcionCasteo()
    {
      Assert.Catch<HttpResponseException>(ComandoAgregarGrupoExcepcionCasteo);
    }

    public void ComandoAgregarGrupoExcepcionCasteo()
    {
      listaGrupo[0].Nombre = null;
      comando = FabricaComando.CrearComandoAgregarGrupo(listaGrupo[0]);
      comando.Ejecutar();
    }

    [Category("Modulo 3")]
    [Category("Comando")]
    [Test]
    public void TestComandoAgregarGrupoExcepcionReferenciaNula()
    {
      Assert.Catch<HttpResponseException>(ComandoAgregarGrupoExcepcionReferenciaNula);
    }

    public void ComandoAgregarGrupoExcepcionReferenciaNula()
    {
      comando = FabricaComando.CrearComandoAgregarGrupo(null);
      comando.Ejecutar();
    }
  }

}
