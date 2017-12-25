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
    private DAO dao;
    private JObject data;

    [OneTimeSetUp]
    protected void OTSU()
    {
      controller = new M9_CategoriasController();
      pCategoria = FabricaEntidad.CrearEntidadCategoria();
      dao = FabricaDAO.CrearDAOCategoria();

    }

    [SetUp]
    public void SetUp()
    {
      dao.Conectar();
      dao.Comando = dao.SqlConexion.CreateCommand();
      dao.Comando.CommandText = "INSERT INTO categoria values (250, 'prueba', 'descripcion de prueba', true, null, 1)";
      dao.Comando.ExecuteNonQuery();
      dao.Desconectar();
    }

    [Test]
    public void PruebaModificarCategoria()
    {
      
       data = new JObject
          {
            { "id", 250 },
            { "nombre", "MODIFICAR" },
            { "descripcion", "MODIFICAR" },
            { "categoriaSuperior", 0 },
            { "nivel", 1 }
          };
      respuesta = (Dictionary<string, object>)controller.ModificarCategorias(data);
      esperado.Add("data", "Se actualizo de forma exitosa");
      var sortedDictionary1 = new SortedDictionary<string, object>(respuesta);
      var sortedDictionary2 = new SortedDictionary<string, object>(esperado);
      Assert.IsTrue(sortedDictionary1.SequenceEqual(sortedDictionary2));
    }

    [Test]
    public void PruebaModificarEstadoCategoria()
    {
      data = new JObject
          {
            { "id", 250},
            { "estatus", false },
          };
      respuesta = (Dictionary<string,object>)controller.ActualizarEstatusCategoria(data);
      esperado.Add("data", "Se actualizo de forma exitosa");
      var sortedDictionary1 = new SortedDictionary<string, object>(respuesta);
      var sortedDictionary2 = new SortedDictionary<string, object>(esperado);
      Assert.IsTrue(sortedDictionary1.SequenceEqual(sortedDictionary2));
    }

        [TearDown]
        public void TearDown()
        {
          dao.Conectar();
          dao.Comando = dao.SqlConexion.CreateCommand();
          dao.Comando.CommandText = "DELETE FROM categoria where ca_id = 250";
          dao.Comando.ExecuteNonQuery();
          dao.Desconectar();
        }


  
  }
}
