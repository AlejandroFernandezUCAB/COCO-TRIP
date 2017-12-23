using ApiRest_COCO_TRIP;
using ApiRest_COCO_TRIP.Datos.Entity;
using NUnit.Framework;
using ApiRest_COCO_TRIP.Datos.DAO;
using ApiRest_COCO_TRIP.Datos.Fabrica;
using ApiRest_COCO_TRIP.Controllers;
using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;

namespace ApiRestPruebas.M9
{
  [TestFixture]
  class PruebaCategoria
  {
    Categoria pCategoria;
    PeticionCategoria pPeticion;
    private M9_CategoriasController controller;
    private Dictionary<string, object> esperado = new Dictionary<string, object>();
    private Dictionary<string, object> respuesta = new Dictionary<string, object>();
    private DAO dao = FabricaDAO.CrearDAOCategoria();
    private JObject data;

    [OneTimeSetUp]
    protected void OTSU()
    {
      controller = new M9_CategoriasController();
      pCategoria = FabricaEntidad.CrearEntidadCategoria();

    }

/*    [SetUp]
    public void SetUp()
    {
      dao.Conectar();
      dao.Comando = dao.SqlConexion.CreateCommand();
      dao.Comando.CommandText = "INSERT INTO categoria values (250, 'prueba', 'descripcion de prueba', true, null, 1)";
      dao.Comando.ExecuteNonQuery();
      dao.Desconectar();
      dao = null;
      pCategoria = new Categoria()
      {
        Id = 250,
        Estatus = false
      };
    }*/

    [Test]
    public void PruebaModificarCategoria()
    {
      
       data = new JObject
          {
            { "id", 250 },
            { "nombre", "" },
            { "descripcion", "" },
            { "categoriaSuperior", null },
            { "nivel", 1 }
          };
      respuesta = (Dictionary<string, object>)controller.ModificarCategorias(data);
      esperado.Add("data", "Se actualizo de forma exitosa");
      var result = esperado.All(x => respuesta.Any(y => x.Value == y.Value));
      Assert.IsTrue(result);
    }

    [Test]
    public void PruebaModificarEstado()
    {
      data = new JObject
          {
            { "id", 250},
            { "estatus", false },
          };
      respuesta = (Dictionary<string,object>)controller.ActualizarEstatusCategoria(data);
      esperado.Add("data", "Se actualizo de forma ");
      var sortedDictionary1 = new SortedDictionary<string, object>(respuesta);
      var sortedDictionary2 = new SortedDictionary<string, object>(esperado);
      Assert.IsTrue(sortedDictionary1.SequenceEqual(sortedDictionary2));
    }

    /*    [TearDown]
        public void TearDown()
        {
          conn = new ConexionBase();
          conn.Conectar();
          conn.Comando = conn.SqlConexion.CreateCommand();
          conn.Comando.CommandText = "DELETE FROM categoria where ca_id = 250";
          conn.Comando.ExecuteNonQuery();
          conn.Desconectar();
        }*/


  
  }
}
