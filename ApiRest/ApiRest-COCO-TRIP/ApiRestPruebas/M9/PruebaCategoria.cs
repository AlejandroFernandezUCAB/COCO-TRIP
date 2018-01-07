using System;
using System.Collections.Generic;
using ApiRest_COCO_TRIP.Comun.Excepcion;
using ApiRest_COCO_TRIP.Datos.DAO;
using ApiRest_COCO_TRIP.Datos.Entity;
using ApiRest_COCO_TRIP.Datos.Fabrica;
using ApiRest_COCO_TRIP.Datos.Singleton;
using ApiRest_COCO_TRIP.Negocio.Command;
using ApiRest_COCO_TRIP.Negocio.Fabrica;
using Moq;
using NUnit.Framework;

namespace ApiRestPruebas.M9
{
    [TestFixture]
    class PruebaCategoria
    {
        private Entidad _categoria;
        private Comando _com;
        private Dictionary<string, object> esperado = new Dictionary<string, object>();
        private Dictionary<string, object> respuesta = new Dictionary<string, object>();
        private DAO dao;
        private MensajeResultadoOperacion mensaje = MensajeResultadoOperacion.ObtenerInstancia();
        private Entidad _resp;

        ////////////////////////////////////////    SETUPS    ////////////////////////////////////////
        #region SetUp

        /// <summary>
        /// Metodo OTSU, inicializando valores.
        /// </summary>
        [OneTimeSetUp]
        protected void OTSU()
        {
            _categoria = FabricaEntidad.CrearEntidadCategoria();
            dao = FabricaDAO.CrearDAOCategoria();
        }

        /// <summary>
        /// Metodo SetUp, inicializando valores de prueba.
        /// </summary>
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

        ////////////////////////////////////////    PRUEBAS COMANDOS     /////////////////////////////
        #region comandos

        #region Modificar

        /// <summary>
        /// Prueba para el comando modificar categoria.
        /// </summary>
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

        /// <summary>
        /// Prueba de excepcion de dependencia asociadas para el comando modificar categoria.
        /// </summary>
        [Test]
        public void M9_PruebaExcepcionDependenciaComandoModificarCategoria()
        {
            Assert.Catch<HijoConDePendenciaExcepcion>(PruebaExcepcionDependenciaComandoModificarCategoria);
        }

        /// <summary>
        /// Prueba de excepcion de nombre duplicado para el comando modificar categoria.
        /// </summary>
        [Test]
        public void M9_PruebaExcepcionDuplicadoComandoModificarCategoria()
        {
            Assert.Catch<NombreDuplicadoExcepcion>(PruebaExcepcionDuplicadoComandoModificarCategoria);
        }

        /// <summary>
        /// Prueba de excepcion de base de datos para el comando modificar categoria. 
        /// </summary>
        [Test]
        public void M9_PruebaExcepcionBaseDeDatosComandoModificarCategoria()
        {
            ((Categoria)_categoria).Id = 1000;
            ((Categoria)_categoria).Nombre = "MODIFICAR";
            ((Categoria)_categoria).Descripcion = "MODIFICAR";
            ((Categoria)_categoria).CategoriaSuperior = 0;
            ((Categoria)_categoria).Nivel = 1;

            Mock<ComandoModificarCategoria> _mockComandoModificarCategoria = new Mock<ComandoModificarCategoria>(_categoria);
            _mockComandoModificarCategoria.Setup(x => x.Ejecutar()).Throws(new BaseDeDatosExcepcion());

            Assert.Throws<BaseDeDatosExcepcion>(() => _mockComandoModificarCategoria.Object.Ejecutar());
        }

        /// <summary>
        /// Prueba de excepcion de parametros nulo para el comando modificar categoria.
        /// </summary>
        [Test]
        public void M9_PruebaExcepcionParametrosNuloComandoModificarCategoria()
        {
            Assert.Catch<ParametrosInvalidosExcepcion>(PruebaExcepcionParametrosNuloComandoModificarCategoria);
        }

        /// <summary>
        /// Prueba de excepcion de argumento nulo para el comando modificar categoria.
        /// </summary>
        [Test]
        public void M9_PruebaExcepcionArgumentoNuloComandoModificarCategoria()
        {
            ((Categoria)_categoria).Id = 1000;
            ((Categoria)_categoria).Nombre = "MODIFICAR";
            ((Categoria)_categoria).Descripcion = "MODIFICAR";
            ((Categoria)_categoria).CategoriaSuperior = 0;
            ((Categoria)_categoria).Nivel = 1;

            Mock<ComandoModificarCategoria> _mockComandoModificarCategoria = new Mock<ComandoModificarCategoria>(_categoria);
            _mockComandoModificarCategoria.Setup(x => x.Ejecutar()).Throws(new ArgumentoNuloExcepcion());

            Assert.Throws<ArgumentoNuloExcepcion>(() => _mockComandoModificarCategoria.Object.Ejecutar());
        }

        /// <summary>
        /// Prueba de excepcion general para el comando modificar categoria.
        /// </summary>
        [Test]
        public void M9_PruebaExcepcionGeneralComandoModificarCategoria()
        {
            ((Categoria)_categoria).Id = 1000;
            ((Categoria)_categoria).Nombre = "MODIFICAR";
            ((Categoria)_categoria).Descripcion = "MODIFICAR";
            ((Categoria)_categoria).CategoriaSuperior = 0;
            ((Categoria)_categoria).Nivel = 1;

            Mock<ComandoModificarCategoria> _mockComandoModificarCategoria = new Mock<ComandoModificarCategoria>(_categoria);
            _mockComandoModificarCategoria.Setup(x => x.Ejecutar()).Throws(new Excepcion());

            Assert.Throws<Excepcion>(() => _mockComandoModificarCategoria.Object.Ejecutar());
        }

        #endregion Modificar

        #region Estado

        /// <summary>
        /// Prueba para el comando estado categoria.
        /// </summary>
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

        /// <summary>
        /// Prueba de excepcion de base de datos para el comando estado categoria. 
        /// </summary>
        [Test]
        public void M9_PruebaExcepcionBaseDeDatosComandoEstadoCategoria()
        {
            ((Categoria)_categoria).Id = 1000;
            ((Categoria)_categoria).Estatus = false;

            Mock<ComandoEstadoCategoria> _mockComandoEstadoCategoria = new Mock<ComandoEstadoCategoria>(_categoria);
            _mockComandoEstadoCategoria.Setup(x => x.Ejecutar()).Throws(new BaseDeDatosExcepcion());

            Assert.Throws<BaseDeDatosExcepcion>(() => _mockComandoEstadoCategoria.Object.Ejecutar());
        }

        /// <summary>
        /// Prueba de excepcion general para el comando modificar categoria.
        /// </summary>
        [Test]
        public void M9_PruebaExcepcionGeneralComandoEstadoCategoria()
        {
            ((Categoria)_categoria).Id = 1000;
            ((Categoria)_categoria).Estatus = false;

            Mock<ComandoEstadoCategoria> _mockComandoEstadoCategoria = new Mock<ComandoEstadoCategoria>(_categoria);
            _mockComandoEstadoCategoria.Setup(x => x.Ejecutar()).Throws(new Excepcion());

            Assert.Throws<Excepcion>(() => _mockComandoEstadoCategoria.Object.Ejecutar());
        }

        #endregion Estado

        #region Insertar

        /// <summary>
        /// Prueba para el comando agregar categoria.
        /// </summary>
        [Test]
        public void M9_PruebaComandoInsertarCategoria()
        {
            ((Categoria)_categoria).Nombre = "AGREGARPRUEB";
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

        /// <summary>
        /// Prueba de excepcion de nombre duplicado para el comando agregar categoria. 
        /// </summary>
        [Test]
        public void M9_PruebaExcepcionNombreDuplicadoComandoAgregarCategoria()
        {
            Assert.Catch<NombreDuplicadoExcepcion>(PruebaExcepcionDuplicadoComandoAgregarCategoria);
        }

        /// <summary>
        /// Prueba de excepcion de base de datos para el comando agregar categoria. 
        /// </summary>
        [Test]
        public void M9_PruebaExcepcionBaseDeDatosComandoAgregarCategoria()
        {
            ((Categoria)_categoria).Nombre = "AGREGAR PRUEBA EXCEPCION";
            ((Categoria)_categoria).Descripcion = "DESCRIPCION";
            ((Categoria)_categoria).CategoriaSuperior = 0;
            ((Categoria)_categoria).Nivel = 1;

            Mock<ComandoAgregarCategoria> _mockComandoAgregarCategoria = new Mock<ComandoAgregarCategoria>(_categoria);
            _mockComandoAgregarCategoria.Setup(x => x.Ejecutar()).Throws(new BaseDeDatosExcepcion());

            Assert.Throws<BaseDeDatosExcepcion>(() => _mockComandoAgregarCategoria.Object.Ejecutar());
        }

        /// <summary>
        /// Prueba de excepcion de argumento nulo para el comando agregar categoria.
        /// </summary>
        [Test]
        public void M9_PruebaExcepcionParametrosInvalidosComandoAgregarCategoria()
        {
            Assert.Catch<ParametrosInvalidosExcepcion>(PruebaExcepcionParametrosInvalidosComandoAgregarCategoria);
        }

        /// <summary>
        /// Prueba de excepcion general para el comando agregar categoria.
        /// </summary>
        [Test]
        public void M9_PruebaExcepcionGeneralComandoAgregarCategoria()
        {
            ((Categoria)_categoria).Nombre = "AGREGAR PRUEBA EXCEPCION";
            ((Categoria)_categoria).Descripcion = "DESCRIPCION";
            ((Categoria)_categoria).CategoriaSuperior = 0;
            ((Categoria)_categoria).Nivel = 1;

            Mock<ComandoAgregarCategoria> _mockComandoAgregarCategoria = new Mock<ComandoAgregarCategoria>(_categoria);
            _mockComandoAgregarCategoria.Setup(x => x.Ejecutar()).Throws(new Excepcion());

            Assert.Throws<Excepcion>(() => _mockComandoAgregarCategoria.Object.Ejecutar());
        }

        #endregion Insertar

        #region ObtenerPorId

        /// <summary>
        /// Prueba para el comando obtener categoria por id.
        /// </summary>
        [Test]
        public void M9_PruebaComandoObtenerPorId()
        {
            ((Categoria)_categoria).Id = 1001;
            _com = FabricaComando.CrearComandoObtenerCategoriaPorId(_categoria);
            _com.Ejecutar();
            _resp = ((ComandoObtenerCategoriaPorId)_com).RetornarLista()[0];
            Assert.AreEqual(((Categoria)_categoria).Id, ((Categoria)_resp).Id);
        }

        /// <summary>
        /// Prueba de excepcion de base de datos para el comando obtener categoria por id.
        /// </summary>
        [Test]
        public void M9_PruebaExcepcionBaseDeDatosComandoObtenerCategoriasPorId()
        {
            ((Categoria)_categoria).Id = 1001;

            Mock<ComandoObtenerCategoriaPorId> _mockComandoObtenerCategoriasPorId = new Mock<ComandoObtenerCategoriaPorId>(_categoria);
            _mockComandoObtenerCategoriasPorId.Setup(x => x.Ejecutar()).Throws(new BaseDeDatosExcepcion());

            Assert.Throws<BaseDeDatosExcepcion>(() => _mockComandoObtenerCategoriasPorId.Object.Ejecutar());
        }

        /// <summary>
        /// Prueba de excepcion general para el comando obtener categoria por id.
        /// </summary>
        [Test]
        public void M9_PruebaExcepcionGeneralComandoObtenerCategoriasPorId()
        {
            ((Categoria)_categoria).Id = 1001;

            Mock<ComandoObtenerCategoriaPorId> _mockComandoObtenerCategoriasPorId = new Mock<ComandoObtenerCategoriaPorId>(_categoria);
            _mockComandoObtenerCategoriasPorId.Setup(x => x.Ejecutar()).Throws(new Excepcion());

            Assert.Throws<Excepcion>(() => _mockComandoObtenerCategoriasPorId.Object.Ejecutar());
        }

        #endregion ObtenerPorId

        #region ObtenerPorNombre

        /// <summary>
        /// Prueba para el comando obtener categoria por nombre.
        /// </summary>
        [Test]
        public void M9_PruebaComandoObtenerPorNombre()
        {
            ((Categoria)_categoria).Nombre = "prueba2";
            _com = FabricaComando.CrearComandoObtenerCategoriaPorNombre(_categoria);
            _com.Ejecutar();
            _resp = _com.Retornar();
            Assert.AreEqual(((Categoria)_categoria).Nombre, ((Categoria)_resp).Nombre);
        }

        /// <summary>
        /// Prueba de excepcion de base de datos para el comando obtener categoria por nombre.
        /// </summary>
        [Test]
        public void M9_PruebaExcepcionBaseDeDatosComandoObtenerCategoriasPorNombre()
        {
            ((Categoria)_categoria).Nombre = "prueba2";

            Mock<ComandoObtenerCategoriaPorNombre> _mockComandoObtenerCategoriasPorNombre = new Mock<ComandoObtenerCategoriaPorNombre>(_categoria);
            _mockComandoObtenerCategoriasPorNombre.Setup(x => x.Ejecutar()).Throws(new BaseDeDatosExcepcion());

            Assert.Throws<BaseDeDatosExcepcion>(() => _mockComandoObtenerCategoriasPorNombre.Object.Ejecutar());
        }

        /// <summary>
        /// Prueba de excepcion general para el comando obtener categoria por nombre.
        /// </summary>
        [Test]
        public void M9_PruebaExcepcionGeneralComandoObtenerCategoriasPorNombre()
        {
            ((Categoria)_categoria).Id = 1000;

            Mock<ComandoObtenerCategoriaPorNombre> _mockComandoObtenerCategoriasPorNombre = new Mock<ComandoObtenerCategoriaPorNombre>(_categoria);
            _mockComandoObtenerCategoriasPorNombre.Setup(x => x.Ejecutar()).Throws(new Excepcion());

            Assert.Throws<Excepcion>(() => _mockComandoObtenerCategoriasPorNombre.Object.Ejecutar());
        }

        #endregion ObtenerPorNombre

        #region Obtener

        /// <summary>
        /// Prueba para el comando obtener categoria.
        /// </summary>
        [Test]
        public void M9_PruebaComandoObtenerCategorias()
        {
            ((Categoria)_categoria).Id = 1000;
            _com = FabricaComando.CrearComandoObtenerCategorias(_categoria);
            _com.Ejecutar();
            _resp = ((ComandoObtenerCategorias)_com).RetornarLista()[0];
            Assert.AreEqual(((Categoria)_categoria).Id, ((Categoria)_resp).CategoriaSuperior);
        }

        /// <summary>
        /// Prueba de excepcion de base de datos para el comando obtener categoria.
        /// </summary>
        [Test]
        public void M9_PruebaExcepcionBaseDeDatosComandoObtenerCategorias()
        {
            ((Categoria)_categoria).Id = 1000;

            Mock<ComandoObtenerCategorias> _mockComandoObtenerCategorias = new Mock<ComandoObtenerCategorias>(_categoria);
            _mockComandoObtenerCategorias.Setup(x => x.Ejecutar()).Throws(new BaseDeDatosExcepcion());

            Assert.Throws<BaseDeDatosExcepcion>(() => _mockComandoObtenerCategorias.Object.Ejecutar());
        }

        /// <summary>
        /// Prueba de excepcion general para el comando obtener categoria.
        /// </summary>
        [Test]
        public void M9_PruebaExcepcionGeneralComandoObtenerCategorias()
        {
            ((Categoria)_categoria).Id = 1000;

            Mock<ComandoObtenerCategorias> _mockComandoObtenerCategorias = new Mock<ComandoObtenerCategorias>(_categoria);
            _mockComandoObtenerCategorias.Setup(x => x.Ejecutar()).Throws(new Excepcion());

            Assert.Throws<Excepcion>(() => _mockComandoObtenerCategorias.Object.Ejecutar());
        }

        #endregion Obtener

        #region ObtenerHabilitadas

        /// <summary>
        /// Prueba para el comando obtener categorias Habilitadas.
        /// </summary>
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

        /// <summary>
        /// Prueba de excepcion de base de datos para el comando obtener categorias Habilitadas.
        /// </summary>
        [Test]
        public void M9_PruebaExcepcionBaseDeDatosComandoObtenerCategoriasHabilitadas()
        {
            Mock<ComandoObtenerCategoriasHabilitadas> _mockComandoObtenerCategoriasHabilitadas = new Mock<ComandoObtenerCategoriasHabilitadas>();
            _mockComandoObtenerCategoriasHabilitadas.Setup(x => x.Ejecutar()).Throws(new BaseDeDatosExcepcion());

            Assert.Throws<BaseDeDatosExcepcion>(() => _mockComandoObtenerCategoriasHabilitadas.Object.Ejecutar());
        }

        /// <summary>
        /// Prueba de excepcion general para el comando obtener categorias Habilitadas.
        /// </summary>
        [Test]
        public void M9_PruebaExcepcionGeneralComandoObtenerCategoriasHabilitadas()
        {
            Mock<ComandoObtenerCategoriasHabilitadas> _mockComandoObtenerCategoriasHabilitadas = new Mock<ComandoObtenerCategoriasHabilitadas>();
            _mockComandoObtenerCategoriasHabilitadas.Setup(x => x.Ejecutar()).Throws(new Excepcion());

            Assert.Throws<Excepcion>(() => _mockComandoObtenerCategoriasHabilitadas.Object.Ejecutar());
        }

        #endregion ObtenerHabilitadas

        #endregion comandos

        ////////////////////////////////////////    PRUEBAS DAO    ///////////////////////////////////
        #region DAOs

        #region Actualizar

        /// <summary>
        /// Prueba para el DAO Actualizar categoria.
        /// </summary>
        [Test]
        public void M9_PruebaDAOActualizarCategoria()
        {
            ((Categoria)_categoria).Id = 1000;
            ((Categoria)_categoria).Nombre = "MODIFICAR";
            ((Categoria)_categoria).Descripcion = "MODIFICAR";
            ((Categoria)_categoria).CategoriaSuperior = 0;
            ((Categoria)_categoria).Nivel = 1;
            ((DAOCategoria)dao).Actualizar(_categoria);
            _resp = ((DAOCategoria)dao).ObtenerCategoriaPorId(_categoria)[0];
            Assert.AreEqual(((Categoria)_categoria).Nombre, ((Categoria)_resp).Nombre);
        }

        /// <summary>
        /// Prueba de excepcion de nombre duplicado para el DAO Actualizar categoria. 
        /// </summary>
        [Test]
        public void M9_PruebaExcepcionNombreDuplicadoDAOActualizar()
        {
            Assert.Catch<NombreDuplicadoExcepcion>(PruebaExcepcionDuplicadoDAOModificarCategoria);
        }

        /// <summary>
        /// Prueba de excepcion de dependencias asociadas para el DAO Actualizar categoria. 
        /// </summary>
        [Test]
        public void M9_PruebaExcepcionHijoConDePendenciaExcepcionDAOActualizar()
        {
            Assert.Catch<HijoConDePendenciaExcepcion>(PruebaExcepcionDependenciaDAOModificarCategoria);
        }

        /// <summary>
        /// Prueba de excepcion de base de datos para el DAO Actualizar categoria. 
        /// </summary>
        [Test]
        public void M9_PruebaExcepcionBaseDeDatosDaoActualizar()
        {
            ((Categoria)_categoria).Nombre = "AGREGAR PRUEBA EXCEPCION";
            ((Categoria)_categoria).Descripcion = "DESCRIPCION";
            ((Categoria)_categoria).CategoriaSuperior = 0;
            ((Categoria)_categoria).Nivel = 1;

            Mock<DAOCategoria> _mockDaoCategoria = new Mock<DAOCategoria>();
            _mockDaoCategoria.Setup(x => x.Actualizar(It.IsAny<Categoria>())).Throws(new BaseDeDatosExcepcion());

            Assert.Throws<BaseDeDatosExcepcion>(() => _mockDaoCategoria.Object.Actualizar(_categoria));
        }

        /// <summary>
        /// Prueba de excepcion de argumento nulo para el DAO Actualizar categoria. 
        /// </summary>
        [Test]
        public void M9_PruebaExcepcionArgumentoNuloDaoActualizar()
        {
            ((Categoria)_categoria).Nombre = "";
            ((Categoria)_categoria).Descripcion = "";
            ((Categoria)_categoria).CategoriaSuperior = 0;
            ((Categoria)_categoria).Nivel = 1;

            Mock<DAOCategoria> _mockDaoCategoria = new Mock<DAOCategoria>();
            _mockDaoCategoria.Setup(x => x.Actualizar(It.IsAny<Categoria>())).Throws(new BaseDeDatosExcepcion());

            Assert.Throws<BaseDeDatosExcepcion>(() => _mockDaoCategoria.Object.Actualizar(_categoria));
        }

        /// <summary>
        /// Prueba de excepcion general para el DAO Actualizar categoria. 
        /// </summary>
        [Test]
        public void M9_PruebaExcepcionGeneralDaoActualizar()
        {
            ((Categoria)_categoria).Nombre = "AGREGAR PRUEBA EXCEPCION";
            ((Categoria)_categoria).Descripcion = "DESCRIPCION";
            ((Categoria)_categoria).CategoriaSuperior = 0;
            ((Categoria)_categoria).Nivel = 1;

            Mock<DAOCategoria> _mockDaoCategoria = new Mock<DAOCategoria>();
            _mockDaoCategoria.Setup(x => x.Actualizar(It.IsAny<Categoria>())).Throws(new Excepcion());

            Assert.Throws<Excepcion>(() => _mockDaoCategoria.Object.Actualizar(_categoria));
        }

        #endregion Actualizar

        #region Estado

        /// <summary>
        /// Prueba para el DAO estado categoria.
        /// </summary>
        [Test]
        public void M9_PruebaDAOEstadoCategoria()
        {
            ((Categoria)_categoria).Id = 1000;
            ((Categoria)_categoria).Estatus = false;
            ((DAOCategoria)dao).ActualizarEstado(_categoria);
            _resp = ((DAOCategoria)dao).ObtenerCategoriaPorId(_categoria)[0];
            Assert.AreEqual(((Categoria)_categoria).Estatus, ((Categoria)_resp).Estatus);
        }

        /// <summary>
        /// Prueba de excepcion de base de datos para el DAO estado categoria. 
        /// </summary>
        [Test]
        public void M9_PruebaExcepcionBaseDeDatosDaoEstadoCategoria()
        {            
            ((Categoria)_categoria).Id = 1000;
            ((Categoria)_categoria).Estatus = false;

            Mock<DAOCategoria> _mockDaoCategoria = new Mock<DAOCategoria>();
            _mockDaoCategoria.Setup(x => x.ActualizarEstado(It.IsAny<Categoria>())).Throws(new BaseDeDatosExcepcion());

            Assert.Throws<BaseDeDatosExcepcion>(() => _mockDaoCategoria.Object.ActualizarEstado(_categoria));
        }

        /// <summary>
        /// Prueba de excepcion general para el DAO estado categoria. 
        /// </summary>
        [Test]
        public void M9_PruebaExcepcionGeneralDaoEstadoCategoria()
        {
            ((Categoria)_categoria).Id = 1000;
            ((Categoria)_categoria).Estatus = false;

            Mock<DAOCategoria> _mockDaoCategoria = new Mock<DAOCategoria>();
            _mockDaoCategoria.Setup(x => x.ActualizarEstado(It.IsAny<Categoria>())).Throws(new Excepcion());

            Assert.Throws<Excepcion>(() => _mockDaoCategoria.Object.ActualizarEstado(_categoria));
        }

        #endregion Estado

        #region Insertar

        /// <summary>
        /// Prueba para el DAO insertar categoria.
        /// </summary>
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

        /// <summary>
        /// Prueba de excepcion de base de datos para el DAO insertar categoria. 
        /// </summary>
        [Test]
        public void M9_PruebaExcepcionBaseDeDatosDaoInsertar()
        {
            ((Categoria)_categoria).Nombre = "AGREGAR PRUEBA EXCEPCION";
            ((Categoria)_categoria).Descripcion = "DESCRIPCION";
            ((Categoria)_categoria).CategoriaSuperior = 0;
            ((Categoria)_categoria).Nivel = 1;

            Mock<DAOCategoria> _mockDaoCategoria = new Mock<DAOCategoria>();
            _mockDaoCategoria.Setup(x => x.Insertar(It.IsAny<Categoria>())).Throws(new BaseDeDatosExcepcion());

            Assert.Throws<BaseDeDatosExcepcion>(() => _mockDaoCategoria.Object.Insertar(_categoria));
        }

        /// <summary>
        /// Prueba de excepcion general para el DAO insertar categoria. 
        /// </summary>
        [Test]
        public void M9_PruebaExcepcionGeneralDaoInsertar()
        {
            ((Categoria)_categoria).Nombre = "AGREGAR PRUEBA EXCEPCION";
            ((Categoria)_categoria).Descripcion = "DESCRIPCION";
            ((Categoria)_categoria).CategoriaSuperior = 0;
            ((Categoria)_categoria).Nivel = 1;

            Mock<DAOCategoria> _mockDaoCategoria = new Mock<DAOCategoria>();
            _mockDaoCategoria.Setup(x => x.Insertar(It.IsAny<Categoria>())).Throws(new Excepcion());

            Assert.Throws<Excepcion>(() => _mockDaoCategoria.Object.Insertar(_categoria));
        }

        /// <summary>
        /// Prueba de excepcion de nombre duplicado para el DAO insertar categoria. 
        /// </summary>
        [Test]
        public void M9_PruebaExcepcionDuplicadoDAOInsertar()
        {
            Assert.Catch<NombreDuplicadoExcepcion>(PruebaExcepcionDuplicadoDAOInsertar);
        }

        #endregion Insertar

        #region ObtenerHabilitadas

        /// <summary>
        /// Prueba de excepcion base de datos para el DAO obtener categoria Habilitadas. 
        /// </summary>
        [Test]
        public void M9_PruebaExcepcionBaseDeDatosDaoObtenerCategoriasHabilitadas()
        {
             Mock<DAOCategoria> _mockDaoCategoria = new Mock<DAOCategoria>();
            _mockDaoCategoria.Setup(x => x.ObtenerCategoriasHabilitadas()).Throws(new BaseDeDatosExcepcion());

            Assert.Throws<BaseDeDatosExcepcion>(() => _mockDaoCategoria.Object.ObtenerCategoriasHabilitadas());
        }

        /// <summary>
        /// Prueba de excepcion General para el DAO obtener categoria Habilitadas. 
        /// </summary>
        [Test]
        public void M9_PruebaExcepcionGeneralDaoObtenerCategoriasHabilitadas()
        {
            Mock<DAOCategoria> _mockDaoCategoria = new Mock<DAOCategoria>();
            _mockDaoCategoria.Setup(x => x.ObtenerCategoriasHabilitadas()).Throws(new Excepcion());

            Assert.Throws<Excepcion>(() => _mockDaoCategoria.Object.ObtenerCategoriasHabilitadas());
        }

        #endregion ObtenerHabilitadas

        #region ObtenerPorId

        /// <summary>
        /// Prueba de excepcion base de datos para el DAO obtener categoria por id. 
        /// </summary>
        [Test]
        public void M9_PruebaExcepcionBaseDeDatosDaoObtenerCategoriasPorId()
        {
            ((Categoria)_categoria).Id = 1000;

            Mock<DAOCategoria> _mockDaoCategoria = new Mock<DAOCategoria>();
            _mockDaoCategoria.Setup(x => x.ObtenerCategoriaPorId(It.IsAny<Categoria>())).Throws(new BaseDeDatosExcepcion());

            Assert.Throws<BaseDeDatosExcepcion>(() => _mockDaoCategoria.Object.ObtenerCategoriaPorId(_categoria));
        }

        /// <summary>
        /// Prueba de excepcion General para el DAO obtener categoria por id. 
        /// </summary>
        [Test]
        public void M9_PruebaExcepcionGeneralDaoObtenerCategoriasPorId()
        {
            ((Categoria)_categoria).Id = 1000;

            Mock<DAOCategoria> _mockDaoCategoria = new Mock<DAOCategoria>();
            _mockDaoCategoria.Setup(x => x.ObtenerCategoriaPorId(It.IsAny<Categoria>())).Throws(new Excepcion());

            Assert.Throws<Excepcion>(() => _mockDaoCategoria.Object.ObtenerCategoriaPorId(_categoria));
        }

        #endregion ObtenePorId

        #region ObtenerPorNombre

        /// <summary>
        /// Prueba de excepcion base de datos para el DAO obtener categoria por nombre. 
        /// </summary>
        [Test]
        public void M9_PruebaExcepcionBaseDeDatosDaoObtenerCategoriasPorNombre()
        {
            ((Categoria)_categoria).Nombre = "prueba";

            Mock<DAOCategoria> _mockDaoCategoria = new Mock<DAOCategoria>();
            _mockDaoCategoria.Setup(x => x.ObtenerIdCategoriaPorNombre(It.IsAny<Categoria>())).Throws(new BaseDeDatosExcepcion());

            Assert.Throws<BaseDeDatosExcepcion>(() => _mockDaoCategoria.Object.ObtenerIdCategoriaPorNombre(_categoria));
        }

        /// <summary>
        /// Prueba de excepcion General para el DAO obtener categoria por por nombre. 
        /// </summary>
        [Test]
        public void M9_PruebaExcepcionGeneralDaoObtenerCategoriasPorNombre()
        {
            ((Categoria)_categoria).Nombre = "prueba";

            Mock<DAOCategoria> _mockDaoCategoria = new Mock<DAOCategoria>();
            _mockDaoCategoria.Setup(x => x.ObtenerIdCategoriaPorNombre(It.IsAny<Categoria>())).Throws(new Excepcion());

            Assert.Throws<Excepcion>(() => _mockDaoCategoria.Object.ObtenerIdCategoriaPorNombre(_categoria));
        }

        #endregion ObtenerPorNombre

        #region Obtener

        /// <summary>
        /// Prueba de excepcion base de datos para el DAO obtener categoria. 
        /// </summary>
        [Test]
        public void M9_PruebaExcepcionBaseDeDatosDaoObtenerCategorias()
        {
            ((Categoria)_categoria).Id = 1000;

            Mock<DAOCategoria> _mockDaoCategoria = new Mock<DAOCategoria>();
            _mockDaoCategoria.Setup(x => x.ObtenerCategorias(It.IsAny<Categoria>())).Throws(new BaseDeDatosExcepcion());

            Assert.Throws<BaseDeDatosExcepcion>(() => _mockDaoCategoria.Object.ObtenerCategorias(_categoria));
        }

        /// <summary>
        /// Prueba de excepcion base de datos para el DAO obtener categoria. 
        /// </summary>
        [Test]
        public void M9_PruebaExcepcionGeneralDaoObtenerCategorias()
        {
            ((Categoria)_categoria).Id = 1000;

            Mock<DAOCategoria> _mockDaoCategoria = new Mock<DAOCategoria>();
            _mockDaoCategoria.Setup(x => x.ObtenerCategorias(It.IsAny<Categoria>())).Throws(new Excepcion());

            Assert.Throws<Excepcion>(() => _mockDaoCategoria.Object.ObtenerCategorias(_categoria));
        }

        #endregion Obtener

        #endregion DAOs

        ////////////////////////////////////////    METODOS AUXILIARES DE EXCEPCIONES   //////////////
        #region auxiliaresExcepciones

        #region DaoInsertar

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

        #endregion DaoInsertar

        #region DaoModificar

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
            ((Categoria)_categoria).Nombre = "modificar";
            ((Categoria)_categoria).Descripcion = "modificar";
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

        #endregion DaoModificar

        #region ComandoAgregar

        public void PruebaExcepcionDuplicadoComandoAgregarCategoria()
        {
            ((Categoria)_categoria).Nombre = "AGREGARPRUEB";
            ((Categoria)_categoria).Descripcion = "PRUEBA";
            ((Categoria)_categoria).CategoriaSuperior = 10;
            ((Categoria)_categoria).Nivel = 2;
            _com = FabricaComando.CrearComandoAgregarCategoria(_categoria);
            _com.Ejecutar();
        }

        public void PruebaExcepcionParametrosInvalidosComandoAgregarCategoria()
        {
            ((Categoria)_categoria).Nombre = "";
            ((Categoria)_categoria).Descripcion = "";
            ((Categoria)_categoria).CategoriaSuperior = 0;
            ((Categoria)_categoria).Nivel = 1;
            _com = FabricaComando.CrearComandoAgregarCategoria(_categoria);
            _com.Ejecutar();
        }

        #endregion ComandoAgregar

        #region ComandoModificar

        public void PruebaExcepcionDependenciaComandoModificarCategoria()
        {
            ((Categoria)_categoria).Id = 1001;
            ((Categoria)_categoria).Nombre = "modificar";
            ((Categoria)_categoria).Descripcion = "modificar";
            ((Categoria)_categoria).CategoriaSuperior = 0;
            ((Categoria)_categoria).Nivel = 1;
            _com = FabricaComando.CrearComandoModificarCategoria(_categoria);
            _com.Ejecutar();
        }

        public void PruebaExcepcionParametrosNuloComandoModificarCategoria()
        {
            ((Categoria)_categoria).Id = 1000;
            ((Categoria)_categoria).Nombre = "";
            ((Categoria)_categoria).Descripcion = "";
            ((Categoria)_categoria).CategoriaSuperior = 0;
            ((Categoria)_categoria).Nivel = 1;

            _com = FabricaComando.CrearComandoModificarCategoria(_categoria);
            _com.Ejecutar();
        }

        #endregion ComandoModificar

        #region ComandoObtenerPorId

        public void PruebaExcepcionBaseDedatosComandoObtenerCategoriaPorId()
        {
            //Implementar moq el metodo que llama a este metodo aun no esta implementado.
        }

        #endregion ComandoObtenerPorId

        #region ComandoObtenerCategorias

        public void PruebaExcepcionBaseDedatosComandoObtenerCategorias()
        {
            //Implementar moq el metodo que llama a este metodo aun no esta implementado.
        }

        #endregion ComandoObtenerCategorias

        #region ComandoObtenerHabilitadas

        public void PruebaExcepcionBaseDedatosComandoObtenerCategoriasHabilitadas()
        {
            //Implementar moq el metodo que llama a este metodo aun no esta implementado.
        }

        #endregion ComandoObtenerHabilitadas
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
