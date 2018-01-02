using ApiRest_COCO_TRIP.Datos.Singleton;
using ApiRest_COCO_TRIP.Datos.Entity;
using NUnit.Framework;
using ApiRest_COCO_TRIP.Datos.DAO;
using ApiRest_COCO_TRIP.Datos.Fabrica;
using ApiRest_COCO_TRIP.Controllers;
using Newtonsoft.Json.Linq;
using ApiRest_COCO_TRIP.Negocio.Command;
using ApiRest_COCO_TRIP.Negocio.Fabrica;
using ApiRest_COCO_TRIP.Models.Excepcion;
using System.Collections.Generic;
using System;
using System.Linq;

namespace ApiRestPruebas.M9
{
    [TestFixture]
    class PruebaCategoria
    {
        private Entidad _categoria;
        private Comando _com;
        private M9_CategoriasController controller;
        private Dictionary<string, object> esperado = new Dictionary<string, object>();
        private Dictionary<string, object> respuesta = new Dictionary<string, object>();
        private DAO dao;
        private MensajeResultadoOperacion mensaje = MensajeResultadoOperacion.ObtenerInstancia();
        private JObject data;
        private Entidad _resp;

        ////////////////////////////////////////    SETUPS    ////////////////////////////////////////
        #region SetUp
        [OneTimeSetUp]
        protected void OTSU()
        {
            controller = new M9_CategoriasController();
            _categoria = FabricaEntidad.CrearEntidadCategoria();
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
        #endregion SetUp
        
        ////////////////////////////////////////    PRUEBAS CONTROLADOR    ///////////////////////////
        #region controlador
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
            esperado.Add("data", mensaje.ExitoModificar);
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
            esperado.Add("data", mensaje.ExitoModificar);
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
        #endregion controlador
        
        ////////////////////////////////////////    PRUEBAS COMANDOS     /////////////////////////////
        #region comandos
        [Test]
        public void M9_PruebaComandoModificarCategoria()
        {
            ((Categoria)_categoria).Id = 1000;
            ((Categoria)_categoria).Nombre = "MODIFICAR";
            ((Categoria)_categoria).Descripcion = "MODIFICAR";
            ((Categoria)_categoria).CategoriaSuperior = 0;
            ((Categoria)_categoria).Nivel = 1;
            _com = FabricaComando.CrearComandoModificarCategoria(((Categoria)_categoria));
            _com.Ejecutar();
            Comando __com = FabricaComando.CrearComandoObtenerCategoriaPorId(((Categoria)_categoria));
            __com.Ejecutar();
            _resp = ((ComandoObtenerCategoriaPorId)__com).RetornarLista()[0];
            Assert.AreEqual(((Categoria)_categoria).Nombre, ((Categoria)_resp).Nombre);
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
            ((Categoria)_categoria).Id = 1000;
            ((Categoria)_categoria).Estatus = false;
            _com = FabricaComando.CrearComandoEstadoCategoria(((Categoria)_categoria));
            _com.Ejecutar();
            _com = FabricaComando.CrearComandoObtenerCategoriaPorId(((Categoria)_categoria));
            _com.Ejecutar();
            _resp = ((ComandoObtenerCategoriaPorId)_com).RetornarLista()[0];
            Assert.AreEqual(((Categoria)_categoria).Estatus, ((Categoria)_resp).Estatus);
        }

        [Test]
        public void M9_PruebaComandoInsertarCategoria()
        {
            ((Categoria)_categoria).Nombre = "PRUEBA AGREGAR";
            ((Categoria)_categoria).Descripcion = "AGREGAR";
            ((Categoria)_categoria).CategoriaSuperior = 0;
            ((Categoria)_categoria).Nivel = 1;
            _com = FabricaComando.CrearComandoAgregarCategoria(_categoria);
            _com.Ejecutar();
            _com = FabricaComando.CrearComandoObtenerCategoriaPorNombre(_categoria);
            _com.Ejecutar();
            _resp = _com.Retornar();
            Borrar(((Categoria)_categoria).Nombre);
            Assert.AreEqual(((Categoria)_categoria).Nombre, ((Categoria)_resp).Nombre);
        }

        [Test]
        public void M9_PruebaExcepcionComandoAgregarCategoria()
        {
            Assert.Catch<NombreDuplicadoException>(PruebaExcepcionDuplicadoComandoAgregarCategoria);
        }

        [Test]
        public void M9_PruebaComandoObtenerPorId()
        {
            ((Categoria)_categoria).Id = 1001;
            _com = FabricaComando.CrearComandoObtenerCategoriaPorId(_categoria);
            _com.Ejecutar();
            _resp = ((ComandoObtenerCategoriaPorId)_com).RetornarLista()[0];
            Assert.AreEqual(((Categoria)_categoria).Id, ((Categoria)_resp).Id);
        }

        [Test]
        public void M9_PruebaComandoObtenerPorNombre()
        {
            ((Categoria)_categoria).Nombre = "prueba2";
            _com = FabricaComando.CrearComandoObtenerCategoriaPorNombre(_categoria);
            _com.Ejecutar();
            _resp = _com.Retornar();
            Assert.AreEqual(((Categoria)_categoria).Nombre, ((Categoria)_resp).Nombre);
        }

      /*  [Test]
        public void M9_PruebaComandoObtenerCategorias()
        {
            ((Categoria)_categoria).Id = 1002;
            _com = FabricaComando.CrearComandoObtenerCategorias(_categoria);
            _com.Ejecutar();
            _resp = ((ComandoObtenerCategorias)_com).RetornarLista2()[0];
            Assert.AreEqual(((Categoria)_categoria).Id, _resp.Id);
        }
        */

        [Test]
        public void M9_PruebaComandoObtenerCategoriasHabilitadas()
        {
            _com = FabricaComando.CrearComandoObtenerCategoriasHabilitadas();
            _com.Ejecutar();
            _resp = ((ComandoObtenerCategoriasHabilitadas)_com).RetornarLista()[0];
            foreach (Categoria _Entidad in ((ComandoObtenerCategoriasHabilitadas)_com).RetornarLista())
            {
                switch (_Entidad.Id)
                {
                    case 1000:
                        Assert.AreEqual(_Entidad.Id, 1000);
                        break;

                    case 1001:
                        Assert.AreEqual(_Entidad.Id, 1001);
                        break;

                    case 1002:
                        Assert.AreEqual(_Entidad.Id, 1002);
                        break;
                }
            }
            
        }
        #endregion comandos

        ////////////////////////////////////////    PRUEBAS DAO    ///////////////////////////////////
        #region DAOs
        [Test]
        public void M9_PruebaDAOModificarCategoria()
        {
            ((Categoria)_categoria).Id = 1000;
            ((Categoria)_categoria).Nombre = "MODIFICAR";
            ((Categoria)_categoria).Descripcion = "MODIFICAR";
            ((Categoria)_categoria).CategoriaSuperior = 0;
            ((Categoria)_categoria).Nivel = 1;
            dao.Actualizar(_categoria);
            _resp = ((DAOCategoria)dao).ObtenerCategoriaPorId(_categoria)[0];
            Assert.AreEqual(((Categoria)_categoria).Nombre, ((Categoria)_resp).Nombre);
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
            ((Categoria)_categoria).Id = 1000;
            ((Categoria)_categoria).Estatus = false;
            ((DAOCategoria)dao).ActualizarEstado(_categoria);
            _resp = ((DAOCategoria)dao).ObtenerCategoriaPorId(_categoria)[0];
            Assert.AreEqual(((Categoria)_categoria).Estatus, ((Categoria)_resp).Estatus);
        }

        [Test]
        public void M9_PruebaDAOInsertarCategoria()
        {
            ((Categoria)_categoria).Nombre = "AGREGAR PRUEBA 1";
            ((Categoria)_categoria).Descripcion = "abc";
            ((Categoria)_categoria).CategoriaSuperior = 0;
            ((Categoria)_categoria).Nivel = 1;
            dao.Insertar(_categoria);
            _resp = ((DAOCategoria)dao).ObtenerIdCategoriaPorNombre(_categoria);
            Borrar(((Categoria)_categoria).Nombre);
            Assert.AreEqual(((Categoria)_categoria).Nombre, ((Categoria)_resp).Nombre);
        }

        [Test]
        public void M9_PruebaExcepcionDuplicadoDAOInsertar()
        {
            Assert.Catch<NombreDuplicadoException>(PruebaExcepcionDuplicadoDAOInsertar);
        }

        /*[Test]
        public void M9_PruebaExcepcionBaseDatoDAOInsertar()
        {
          Assert.Catch<BaseDeDatosExcepcion>(PruebaExcepcionDuplicadoDAOInsertar);
          Borrar("XYZ");
        }
        */
        #endregion DAOs

        ////////////////////////////////////////    METODOS AUXILIARES DE EXCEPCIONES   //////////////
        #region auxiliaresExcepciones
        public void PruebaExcepcionBaseDatoDAOInsertar()
        {
            ((Categoria)_categoria).Nombre = "XYZ";
            ((Categoria)_categoria).CategoriaSuperior = 0;
            ((Categoria)_categoria).Nivel = 0;
            dao.Insertar(_categoria);
        }

        public void PruebaExcepcionDuplicadoDAOInsertar()
        {
            ((Categoria)_categoria).Nombre = "prueba2";
            ((Categoria)_categoria).Descripcion = "abc";
            ((Categoria)_categoria).CategoriaSuperior = 0;
            ((Categoria)_categoria).Nivel = 1;
            dao.Insertar(_categoria);
        }

        public void PruebaExcepcionDuplicadoDAOModificarCategoria()
        {
            ((Categoria)_categoria).Id = 1001;
            ((Categoria)_categoria).Nombre = "prueba";
            ((Categoria)_categoria).Descripcion = "MODIFICAR";
            ((Categoria)_categoria).CategoriaSuperior = 1000;
            ((Categoria)_categoria).Nivel = 2;
            dao.Actualizar(_categoria);
        }

        public void PruebaExcepcionDependenciaDAOModificarCategoria()
        {
            ((Categoria)_categoria).Id = 1001;
            ((Categoria)_categoria).Nombre = "MODIFICAR";
            ((Categoria)_categoria).Descripcion = "MODIFICAR";
            ((Categoria)_categoria).CategoriaSuperior = 0;
            ((Categoria)_categoria).Nivel = 1;
            dao.Actualizar(_categoria);
        }

        public void PruebaExcepcionDuplicadoComandoModificarCategoria()
        {
            ((Categoria)_categoria).Id = 1001;
            ((Categoria)_categoria).Nombre = "prueba";
            ((Categoria)_categoria).Descripcion = "MODIFICAR";
            ((Categoria)_categoria).CategoriaSuperior = 1000;
            ((Categoria)_categoria).Nivel = 2;
            _com = FabricaComando.CrearComandoModificarCategoria(_categoria);
            _com.Ejecutar();
        }

        public void PruebaExcepcionDuplicadoComandoAgregarCategoria()
        {
            ((Categoria)_categoria).Nombre = "AGREGAR PRUEBA";
            ((Categoria)_categoria).Descripcion = "PRUEBA";
            ((Categoria)_categoria).CategoriaSuperior = 10;
            ((Categoria)_categoria).Nivel = 2;
            _com = FabricaComando.CrearComandoAgregarCategoria(_categoria);
            _com.Ejecutar();
        }

        public void PruebaExcepcionDependenciaComandoModificarCategoria()
        {
            ((Categoria)_categoria).Id = 1001;
            ((Categoria)_categoria).Nombre = "MODIFICAR";
            ((Categoria)_categoria).Descripcion = "MODIFICAR";
            ((Categoria)_categoria).CategoriaSuperior = 0;
            ((Categoria)_categoria).Nivel = 1;
            _com = FabricaComando.CrearComandoModificarCategoria(_categoria);
            _com.Ejecutar();
        }
        //ItemNoEncontrado

        public void PruebaExcepcionBaseDedatosComandoObtenerCategoriaPorId()
        {
            //Implementar moq el metodo que llama a este metodo aun no esta implementado.
        }

        public void PruebaExcepcionBaseDedatosComandoObtenerCategorias()
        {
            //Implementar moq el metodo que llama a este metodo aun no esta implementado.
        }

        public void PruebaExcepcionBaseDedatosComandoObtenerCategoriasHabilitadas()
        {
            //Implementar moq el metodo que llama a este metodo aun no esta implementado.
        }

        #endregion auxiliaresExcepciones

        ////////////////////////////////////////    TEARDOWN   ////////////////////////////////////////
        #region TearDown
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
        #endregion TearDown

        ////////////////////////////////////////    METODOS AUXILIARES    /////////////////////////////
        #region metodosAuxiliares

        private void Borrar(String categoria)
        {
            dao.Conectar();
            dao.Comando = dao.SqlConexion.CreateCommand();
            dao.Comando.CommandText = "DELETE FROM categoria where ca_nombre ='" + categoria + "'";
            dao.Comando.ExecuteNonQuery();
            dao.Desconectar();
        }
        #endregion metodosAuxiliares
    }
}
