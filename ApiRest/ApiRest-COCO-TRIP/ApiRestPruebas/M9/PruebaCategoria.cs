using ApiRest_COCO_TRIP;
using ApiRest_COCO_TRIP.Datos.Entity;
using NUnit.Framework;
using ApiRest_COCO_TRIP.Datos.DAO;
using ApiRest_COCO_TRIP.Datos.Fabrica;
using ApiRest_COCO_TRIP.Controllers;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using ApiRest_COCO_TRIP.Negocio.Command;
using ApiRest_COCO_TRIP.Negocio.Fabrica;
using ApiRest_COCO_TRIP.Models.Excepcion;
using System.Collections.Generic;
using System;
using System.Linq;
using Npgsql;

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
    private Entidad respuesta2;

//////////////////////////////////////////////////////////////////////////////////////SETUPS////////////////////////////////////////////////////////////////////

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
      esperado = new Dictionary<string, object>();
      dao.Conectar();
      dao.Comando = dao.SqlConexion.CreateCommand();
      dao.Comando.CommandText = "INSERT INTO categoria values (1000, 'prueba', 'descripcion de prueba', true, null, 1)";
      dao.Comando.ExecuteNonQuery();
      dao.Comando = dao.SqlConexion.CreateCommand();
      dao.Comando.CommandText = "INSERT INTO categoria values (1001, 'prueba2', 'descripcion de prueba', true, 1000, 2)";
      dao.Comando.ExecuteNonQuery();
      dao.Comando = dao.SqlConexion.CreateCommand();
      dao.Comando.CommandText = "INSERT INTO categoria values (1002, 'prueba3', 'descripcion de prueba', true, 1001, 3)";
      dao.Comando.ExecuteNonQuery();
      dao.Desconectar();
    }
//////////////////////////////////////////////////////////////////////////////////////PRUEBAS CONTROLADOR////////////////////////////////////////////////////////////////////
    [Test]
    public void M9_PruebaModificarCategoria()
    {
      
       data = new JObject
          {
            { "id", 1000 },
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
    public void M9_PruebaNullModificarCategoria()
    {

      data = new JObject
          {
            { "id", 1000 },
            { "nombre", null },
            { "descripcion", "MODIFICAR" },
            { "categoriaSuperior", 0 },
            { "nivel", 1 }
          };
      respuesta = (Dictionary<string, object>)controller.ModificarCategorias(data);
      ParametrosNullException ex = new ParametrosNullException("nombre");
      esperado.Add("error", ex.Mensaje);
      var sortedDictionary1 = new SortedDictionary<string, object>(respuesta);
      var sortedDictionary2 = new SortedDictionary<string, object>(esperado);
      Assert.IsTrue(sortedDictionary1.SequenceEqual(sortedDictionary2));
    }

    [Test]
    public void M9_PruebaNombreDuplicadoModificarCategoria()
    {

      data = new JObject
          {
            { "id", 1001 },
            { "nombre", "prueba" },
            { "descripcion", "MODIFICAR" },
            { "categoriaSuperior", 1000 },
            { "nivel", 2 }
          };
      respuesta = (Dictionary<string, object>)controller.ModificarCategorias(data);
      NombreDuplicadoException ex = new NombreDuplicadoException("Esta Categoria id:1001 No se puede agregar con el nombre:prueba Porque este nombre ya existe");
      esperado.Add("error", ex.Mensaje);
      esperado.Add("MensajeError", "Este nombre de categoria ya existe");
      var sortedDictionary1 = new SortedDictionary<string, object>(respuesta);
      var sortedDictionary2 = new SortedDictionary<string, object>(esperado);
      Assert.IsTrue(sortedDictionary1.SequenceEqual(sortedDictionary2));
    }

    [Test]
    public void M9_PruebaDependenciaModificarCategoria()
    {

      data = new JObject
          {
            { "id", 1001 },
            { "nombre", "prueba2" },
            { "descripcion", "MODIFICAR" },
            { "categoriaSuperior", 0 },
            { "nivel", 1 }
          };
      respuesta = (Dictionary<string, object>)controller.ModificarCategorias(data);
      HijoConDePendenciaException ex = new HijoConDePendenciaException("Esta categoria id:1001 nombre:prueba2 tiene hijos");
      esperado.Add("error", ex.Mensaje);
      esperado.Add("MensajeError", "No se puede mover porque tiene categorias asociadas");
      var sortedDictionary1 = new SortedDictionary<string, object>(respuesta);
      var sortedDictionary2 = new SortedDictionary<string, object>(esperado);
      Assert.IsTrue(sortedDictionary1.SequenceEqual(sortedDictionary2));
    }

    [Test]
    public void M9_PruebaModificarEstadoCategoria()
    {
      data = new JObject
          {
            { "id", 1000},
            { "estatus", false },
          };
      respuesta = (Dictionary<string, object>)controller.ActualizarEstatusCategoria(data);
      esperado.Add("data", "Se actualizo de forma exitosa");
      var sortedDictionary1 = new SortedDictionary<string, object>(respuesta);
      var sortedDictionary2 = new SortedDictionary<string, object>(esperado);
      Assert.IsTrue(sortedDictionary1.SequenceEqual(sortedDictionary2));
    }

    [Test]
    public void M9_PruebaNullModificarEstadoCategoria()
    {
      data = new JObject
          {
            { "id", null},
            { "estatus", false },
          };
      respuesta = (Dictionary<string, object>)controller.ActualizarEstatusCategoria(data);
      ParametrosNullException ex = new ParametrosNullException("id");
      esperado.Add("error", ex.Mensaje);
      var sortedDictionary1 = new SortedDictionary<string, object>(respuesta);
      var sortedDictionary2 = new SortedDictionary<string, object>(esperado);
      Assert.IsTrue(sortedDictionary1.SequenceEqual(sortedDictionary2));
    }

//////////////////////////////////////////////////////////////////////////////////////PRUEBAS COMANDOS////////////////////////////////////////////////////////////////////
    [Test]
    public void M9_PruebaComandoModificarCategoria()
    {
      Categoria categoria = FabricaEntidad.CrearEntidadCategoria();
      categoria.Id = 1000;
      categoria.Nombre = "MODIFICAR";
      categoria.Descripcion = "MODIFICAR";
      categoria.CategoriaSuperior = 0;
      categoria.Nivel = 1;
      ComandoModificarCategoria com = FabricaComando.CrearComandoModificarCategoria(categoria);
      com.Ejecutar();
      ComandoObtenerCategoriaPorId com2 = FabricaComando.CrearComandoObtenerCategoriaPorId(categoria);
      com2.Ejecutar();
      respuesta2 = com2.RetornarLista2()[0];
      Categoria resp = (Categoria)respuesta2;
      Assert.AreEqual(categoria.Nombre, resp.Nombre);
    }
   
    public void PruebaExcepcionDuplicadoComandoModificarCategoria()
    {
      Categoria categoria = FabricaEntidad.CrearEntidadCategoria();
      categoria.Id = 1001;
      categoria.Nombre = "prueba";
      categoria.Descripcion = "MODIFICAR";
      categoria.CategoriaSuperior = 1000;
      categoria.Nivel = 2;
      ComandoModificarCategoria com = FabricaComando.CrearComandoModificarCategoria(categoria);
      com.Ejecutar();
    }

    public void PruebaExcepcionDependenciaComandoModificarCategoria()
    {
      Categoria categoria = FabricaEntidad.CrearEntidadCategoria();
      categoria.Id = 1001;
      categoria.Nombre = "MODIFICAR";
      categoria.Descripcion = "MODIFICAR";
      categoria.CategoriaSuperior = 0;
      categoria.Nivel = 1;
      ComandoModificarCategoria com = FabricaComando.CrearComandoModificarCategoria(categoria);
      com.Ejecutar();
    }

    [Test]
    public void M9_PruebaExcepcionComandoModificarCategoria()
    {
      Assert.Catch<NombreDuplicadoException>(PruebaExcepcionDuplicadoComandoModificarCategoria);
      Assert.Catch<HijoConDePendenciaException>(PruebaExcepcionDependenciaComandoModificarCategoria);
    }

    [Test]
    public void M9_PruebaComandoEstadoCategoria()
    {
      Categoria categoria = FabricaEntidad.CrearEntidadCategoria();
      categoria.Id = 1000;
      categoria.Estatus = false;
      ComandoEstadoCategoria com = FabricaComando.CrearComandoEstadoCategoria(categoria);
      com.Ejecutar();
      ComandoObtenerCategoriaPorId com2 = FabricaComando.CrearComandoObtenerCategoriaPorId(categoria);
      com2.Ejecutar();
      respuesta2 = com2.RetornarLista2()[0];
      Categoria resp = (Categoria)respuesta2;
      Assert.AreEqual(categoria.Estatus, resp.Estatus);
    }

//////////////////////////////////////////////////////////////////////////////////////PRUEBAS DAO////////////////////////////////////////////////////////////////////

    [Test]
    public void M9_PruebaDAOModificarCategoria()
    {
      Categoria categoria = FabricaEntidad.CrearEntidadCategoria();
      categoria.Id = 1000;
      categoria.Nombre = "MODIFICAR";
      categoria.Descripcion = "MODIFICAR";
      categoria.CategoriaSuperior = 0;
      categoria.Nivel = 1;
      dao.Actualizar(categoria);
      DAOCategoria daoc = (DAOCategoria)dao;
      respuesta2 = daoc.ObtenerCategoriaPorId(categoria)[0];
      Categoria resp = (Categoria)respuesta2;
      Assert.AreEqual(categoria.Nombre, resp.Nombre);
    }

    public void PruebaExcepcionDuplicadoDAOModificarCategoria()
    {
      Categoria categoria = FabricaEntidad.CrearEntidadCategoria();
      categoria.Id = 1001;
      categoria.Nombre = "prueba";
      categoria.Descripcion = "MODIFICAR";
      categoria.CategoriaSuperior = 1000;
      categoria.Nivel = 2;
      dao.Actualizar(categoria);
    }

    public void PruebaExcepcionDependenciaDAOModificarCategoria()
    {
      Categoria categoria = FabricaEntidad.CrearEntidadCategoria();
      categoria.Id = 1001;
      categoria.Nombre = "MODIFICAR";
      categoria.Descripcion = "MODIFICAR";
      categoria.CategoriaSuperior = 0;
      categoria.Nivel = 1;
      dao.Actualizar(categoria);
    }

    [Test]
    public void M9_PruebaExcepcionDAOModificarCategoria()
    {
      Assert.Catch<NombreDuplicadoException>(PruebaExcepcionDuplicadoDAOModificarCategoria);
      Assert.Catch<HijoConDePendenciaException>(PruebaExcepcionDependenciaDAOModificarCategoria);
    }

    [Test]
    public void M9_PruebaDAOEstadoCategoria()
    {
      Categoria categoria = FabricaEntidad.CrearEntidadCategoria();
      categoria.Id = 1000;
      categoria.Estatus = false;
      DAOCategoria daoc = (DAOCategoria)dao;
      daoc.ActualizarEstado(categoria);
      respuesta2 = daoc.ObtenerCategoriaPorId(categoria)[0];
      Categoria resp = (Categoria)respuesta2;
      Assert.AreEqual(categoria.Estatus, resp.Estatus);
    }


//////////////////////////////////////////////////////////////////////////////////////TEARDOWN////////////////////////////////////////////////////////////////////
    [TearDown]
        public void TearDown()
        {
          dao.Conectar();
          dao.Comando = dao.SqlConexion.CreateCommand();
          dao.Comando.CommandText = "DELETE FROM categoria where ca_id = 1002";
          dao.Comando.ExecuteNonQuery();
          dao.Comando = dao.SqlConexion.CreateCommand();
          dao.Comando.CommandText = "DELETE FROM categoria where ca_id = 1001";
          dao.Comando.ExecuteNonQuery();
          dao.Comando = dao.SqlConexion.CreateCommand();
          dao.Comando.CommandText = "DELETE FROM categoria where ca_id = 1000";
          dao.Comando.ExecuteNonQuery();
          dao.Desconectar();
        }
  
  }
}
