using ApiRest_COCO_TRIP.Comun.Excepcion;
using ApiRest_COCO_TRIP.Datos.DAO;
using ApiRest_COCO_TRIP.Datos.DAO.Interfaces;
using ApiRest_COCO_TRIP.Datos.Entity;
using ApiRest_COCO_TRIP.Datos.Fabrica;
using ApiRest_COCO_TRIP.Negocio.Command;
using ApiRest_COCO_TRIP.Negocio.Fabrica;
using Moq;
using Newtonsoft.Json.Linq;
using Npgsql;
using NpgsqlTypes;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace ApiRestPruebas.M7
{
	[TestFixture]
	public class M7UnitTest
	{

		LugarTuristico _lugarTuristico;
		Foto _foto;
		Actividad _actividad;
		List<Entidad> _fotos;
		List<Entidad> _lugaresTuristicos;
		List<Entidad> _actividades;
		IDAOLugarTuristico iDAOLugarTuristico;
		DAOActividad daoActividad;
		IDAOFoto iDAOFoto;
        Comando _comandoA;
        Comando _comandoB;
		
		//SetUp
		#region 
		[OneTimeSetUp]
		public void SetUpAll()
		{
			DAO test = FabricaDAO.CrearDAOLugarTuristico();

			test.Conectar();
			test.Comando = new NpgsqlCommand("SELECT setval('seq_lugar_turistico', 1)", test.SqlConexion);
			test.Comando.ExecuteNonQuery();
			test.Desconectar();

			test.Conectar();
			test.Comando = new NpgsqlCommand("SELECT setval('seq_lt_foto', 1)", test.SqlConexion);
			test.Comando.ExecuteNonQuery();
			test.Desconectar();

			test.Conectar();
			test.Comando = new NpgsqlCommand("SELECT setval('seq_actividad', 1)", test.SqlConexion);
			test.Comando.ExecuteNonQuery();
			test.Desconectar();

			test.Conectar();
			test.Comando =  new NpgsqlCommand("Delete from lt_horario", test.SqlConexion);
			test.Comando.ExecuteNonQuery();
			test.Desconectar();

			test.Conectar();
			test.Comando = new NpgsqlCommand("Delete from lt_foto", test.SqlConexion);
			test.Comando.ExecuteNonQuery();
			test.Desconectar();

			test.Conectar();
			test.Comando = new NpgsqlCommand("Delete from lt_c", test.SqlConexion);
			test.Comando.ExecuteNonQuery();
			test.Desconectar();

			test.Conectar();
			test.Comando = new NpgsqlCommand("Delete from Actividad", test.SqlConexion);
			test.Comando.ExecuteNonQuery();
			test.Desconectar();

			test.Conectar();
			test.Comando = new NpgsqlCommand("Delete from lugar_turistico", test.SqlConexion);
			test.Comando.ExecuteNonQuery();
			test.Desconectar();
		}
		
		//SetUp

		[SetUp]
		public void SetUp()
		{
			_lugaresTuristicos = new List<Entidad>();
			_lugarTuristico = FabricaEntidad.CrearEntidadLugarTuristico();
			_lugarTuristico.Id = 2;
			_lugarTuristico.Nombre = "Parque Venezuela";
			_lugarTuristico.Costo = 2000;
			_lugarTuristico.Descripcion = "Parque creado en Venezuela";
			_lugarTuristico.Direccion = "Av. Principal Venezuela";
			_lugarTuristico.Correo = "venezuela@venezuela.com";
			_lugarTuristico.Telefono = 04142792806;
			_lugarTuristico.Latitud = 25;
			_lugarTuristico.Longitud = 25;
			_lugarTuristico.Activar = true;

			_lugaresTuristicos.Add( _lugarTuristico );

			_lugarTuristico = FabricaEntidad.CrearEntidadLugarTuristico();
			_lugarTuristico.Id = 3;
            _lugarTuristico.Nombre = "Parque Del este";
			_lugarTuristico.Costo = 2000;
			_lugarTuristico.Descripcion = "Parque natural en Venezuela";
			_lugarTuristico.Direccion = "En el este de caracas";
			_lugarTuristico.Correo = "parque@deleste.com";
			_lugarTuristico.Telefono = 04164444778;
			_lugarTuristico.Latitud = 25;
			_lugarTuristico.Longitud = 25;
			_lugarTuristico.Activar = true;

			_lugaresTuristicos.Add(_lugarTuristico);


            _fotos = new List<Entidad>();

            _foto = FabricaEntidad.CrearEntidadFoto();
			_foto.Id = 2;
			_foto.Ruta = "TEST";

            _fotos.Add(_foto);

            _foto = FabricaEntidad.CrearEntidadFoto();
            _foto.Id = 3;
            _foto.Ruta = "TEST2";

            _fotos.Add(_foto);

            // Guardo la primera foto de la lista
            _foto = (Foto)_fotos[0];

            _actividades = new List<Entidad>();

            _actividad = FabricaEntidad.CrearEntidadActividad();
			_actividad.Id = 2;
			_actividad.Nombre = "TEST";
			_actividad.Foto.Ruta = "TEST";
			_actividad.Duracion = new TimeSpan(2, 0, 0);
			_actividad.Descripcion = "TEST";
			_actividad.Activar = true;

            _actividades.Add(_actividad);

            _actividad = FabricaEntidad.CrearEntidadActividad();
            _actividad.Id = 3;
            _actividad.Nombre = "TREMENDOTEST";
            _actividad.Foto.Ruta = "CARACAS";
            _actividad.Duracion = new TimeSpan(2, 0, 0);
            _actividad.Descripcion = "THE GREATEST ACTIVITY";
            _actividad.Activar = true;

            _actividades.Add(_actividad);

        }
		#endregion

		//Pruebas de DAO
		#region
		
		//Lugar Turistico
		#region

        // Se le agrego un orden a las pruebas unitarias de manera de que puedan ser
        // ejecutadas en conjunto para realizar todas las pruebas del modulo que necesiten
        // tener uno o mas lugares turisticos insertados en la base de datos.
        // Esto evita insertar lo mismo varias veces y las inserciones son acumulativas.
        // En caso de necesitar correr una prueba unitaria especifica sera necesario
        // quitar el comentario en las lineas de insercion del lugar turistico,
        // si se presentara el caso.
        
		//Prueba de DAO de Lugar Turistico
		[Test, Order(1)]
		public void DAOInsertarLugarTuristico()
		{
			LugarTuristico resultado = FabricaEntidad.CrearEntidadLugarTuristico();

			iDAOLugarTuristico = FabricaDAO.CrearDAOLugarTuristico();

			iDAOLugarTuristico.Insertar( _lugaresTuristicos[0] );
			_lugaresTuristicos = iDAOLugarTuristico.ConsultarTodaLaLista();
			
			//Obtengo el ultimo lugar insertado
			foreach(LugarTuristico lugar in _lugaresTuristicos)
			{
				resultado = lugar;
			}

			Assert.AreEqual( 2 , resultado.Id);
			Assert.AreEqual( ((LugarTuristico)_lugaresTuristicos[0]).Nombre     , resultado.Nombre);
			Assert.AreEqual( ((LugarTuristico)_lugaresTuristicos[0]).Costo      , resultado.Costo);
			Assert.AreEqual( ((LugarTuristico)_lugaresTuristicos[0]).Descripcion, resultado.Descripcion);
			Assert.AreEqual( ((LugarTuristico)_lugaresTuristicos[0]).Direccion  , resultado.Direccion);
			Assert.AreEqual( ((LugarTuristico)_lugaresTuristicos[0]).Correo     , resultado.Correo);
			Assert.AreEqual( ((LugarTuristico)_lugaresTuristicos[0]).Telefono   , resultado.Telefono);
			Assert.AreEqual( ((LugarTuristico)_lugaresTuristicos[0]).Latitud    , resultado.Latitud);
			Assert.AreEqual( ((LugarTuristico)_lugaresTuristicos[0]).Longitud   , resultado.Longitud);
			Assert.AreEqual( ((LugarTuristico)_lugaresTuristicos[0]).Activar    , resultado.Activar);

		}

		[Test, Order(2)]
		public void DAOTodosLosLugaresTuristicos()
		{
			List<Entidad> resultado = new List<Entidad>();
			iDAOLugarTuristico = FabricaDAO.CrearDAOLugarTuristico();

                // Descomentar las siguientes lineas al ejecutar la pu de forma individual
            //iDAOLugarTuristico.Insertar(_lugaresTuristicos[0]);

            iDAOLugarTuristico.Insertar(_lugaresTuristicos[1]);

            resultado = iDAOLugarTuristico.ConsultarTodaLaLista();

			for (int i = 0; i < 2; i++)
			{
				Assert.AreEqual(_lugaresTuristicos[i].Id, resultado[i].Id);
				Assert.AreEqual(  ((LugarTuristico)_lugaresTuristicos[i]).Nombre, 
								  ((LugarTuristico)resultado[i]).Nombre);
				Assert.AreEqual(  ((LugarTuristico)_lugaresTuristicos[i]).Costo, 
								  ((LugarTuristico)resultado[i]).Costo);
				Assert.AreEqual(  ((LugarTuristico)_lugaresTuristicos[i]).Descripcion, 
								  ((LugarTuristico)resultado[i]).Descripcion);
				Assert.AreEqual(  ((LugarTuristico)_lugaresTuristicos[i]).Direccion, 
								  ((LugarTuristico)resultado[i]).Direccion);
				Assert.AreEqual(  ((LugarTuristico)_lugaresTuristicos[i]).Correo, 
								  ((LugarTuristico)resultado[i]).Correo);
				Assert.AreEqual(  ((LugarTuristico)_lugaresTuristicos[i]).Telefono, 
								  ((LugarTuristico)resultado[i]).Telefono);
				Assert.AreEqual(  ((LugarTuristico)_lugaresTuristicos[i]).Latitud, 
								  ((LugarTuristico)resultado[i]).Latitud);
				Assert.AreEqual(  ((LugarTuristico)_lugaresTuristicos[i]).Longitud, 
								  ((LugarTuristico)resultado[i]).Longitud);
				Assert.AreEqual(  ((LugarTuristico)_lugaresTuristicos[i]).Activar, 
								  ((LugarTuristico)resultado[i]).Activar);
			}

		}

		//Pruebas Excepciones de DAO

		[Test]
		public void PruebasExcepcionesDAOInsertarLugarTuristico()
		{

			Assert.Catch<BaseDeDatosExcepcion>(BaseDeDatosExcepcionDAOLugarTuristicoInsertar);
			Assert.Catch<SocketExcepcion>(SocketExcepcionDAOLugarTuristicoInsertar);
			Assert.Catch<ReferenciaNulaExcepcion>(ParametrosNulosDAOLugarTuristicoInsertar);
			Assert.Catch<CasteoInvalidoExcepcion>(CasteoInvalidoDAOLugarTuristicoInsertar);

		}

		[Test]
		public void PruebasExcepcionesDAOTodosLosLugaresTuristicos()
		{
			List<Entidad> lugarTuristico = new List<Entidad>();
			IDAOLugarTuristico iDAOLugarTuristico = FabricaDAO.CrearDAOLugarTuristico();
			lugarTuristico = iDAOLugarTuristico.ConsultarTodaLaLista();


		}
		#endregion

		//Foto
		#region
		[Test, Order(4)]
		public void DAOInsertarFoto()
		{
			iDAOFoto = FabricaDAO.CrearDAOFoto();

                // Descomentar las siguientes lineas al ejecutar la pu de forma individual
            //iDAOLugarTuristico = FabricaDAO.CrearDAOLugarTuristico();		
            //iDAOLugarTuristico.Insertar(_lugaresTuristicos[0]);

            //Inserto la foto
            iDAOFoto.Insertar(_foto, _lugaresTuristicos[0]);

            //Busco la foto
            _fotos = iDAOFoto.ConsultarLista(_lugaresTuristicos[0]);

			Assert.IsNotNull(iDAOFoto);
			Assert.IsNotNull(iDAOLugarTuristico);

			Assert.AreEqual( _foto.Id , _fotos[0].Id);
			Assert.AreEqual( _foto.Ruta+"2.jpg", ((Foto)_fotos[0]).Ruta);

		}

		[Test, Order(5)]
		public void DAOBuscarListaFoto()
		{

			iDAOFoto = FabricaDAO.CrearDAOFoto();
			iDAOLugarTuristico = FabricaDAO.CrearDAOLugarTuristico();

                // Descomentar las siguientes lineas al ejecutar la pu de forma individual
            //iDAOLugarTuristico.Insertar(_lugaresTuristicos[0]);
            //Inserto la foto
            //iDAOFoto.Insertar(_foto, _lugaresTuristicos[0]);


            //Busco la foto
            _fotos = iDAOFoto.ConsultarLista(_lugaresTuristicos[0]);

			Assert.IsNotNull(iDAOFoto);
			Assert.IsNotNull(iDAOLugarTuristico);

			Assert.AreEqual(_foto.Id, _fotos[0].Id);
			Assert.AreEqual(_foto.Ruta + "2.jpg", ((Foto)_fotos[0]).Ruta);

		}

        [Test, Order(6)]
        public void DAOEliminarFoto()
        {
            int idFoto = 0;
            int fotosAntesDeBorrar = 0;
            int fotosDespuesDeBorrar = 0;

                // Descomentar las siguientes lineas al ejecutar la pu de forma individual
            //iDAOFoto = FabricaDAO.CrearDAOFoto();
            //iDAOLugarTuristico = FabricaDAO.CrearDAOLugarTuristico();
            //iDAOLugarTuristico.Insertar(_lugaresTuristicos[0]);
            //iDAOFoto.Insertar(_fotos[0], _lugaresTuristicos[0]);


            //deberia ser la misma foto
            _fotos = iDAOFoto.ConsultarLista(_lugaresTuristicos[0]);
            fotosAntesDeBorrar = _fotos.Count;

            idFoto = _fotos[0].Id;


            Assert.AreEqual(idFoto, _fotos[0].Id);

            iDAOFoto.Eliminar(_fotos[0]);

            
            _fotos = iDAOFoto.ConsultarLista(_lugaresTuristicos[0]);
            fotosDespuesDeBorrar = _fotos.Count;

            Assert.Greater( fotosAntesDeBorrar , fotosDespuesDeBorrar );

        }
        #endregion

        //Actividad
        #region
        [Test, Order(7)]
		public void DAOInsertarActividad()
		{
			daoActividad = FabricaDAO.CrearDAOActividad();

                // Descomentar las siguientes lineas al ejecutar la pu de forma individual
            //iDAOLugarTuristico = FabricaDAO.CrearDAOLugarTuristico();
            //iDAOLugarTuristico.Insertar(_lugaresTuristicos[0]);

            daoActividad.Insertar( _actividades[0] , _lugaresTuristicos[0]);
			_actividades = daoActividad.ConsultarLista( _lugaresTuristicos[0]);

			foreach (Actividad actividad  in _actividades)
			{
				_actividad = actividad;
			}

			Assert.NotNull(_actividad);
			Assert.NotNull(_actividades);
			Assert.NotNull(_lugaresTuristicos[0]);
			Assert.NotNull(iDAOLugarTuristico);
			Assert.NotNull(daoActividad);

			Assert.AreEqual( _actividad.Id , _actividades[0].Id);
			Assert.AreEqual(_actividad.Nombre, ((Actividad)_actividades[0]).Nombre);
			Assert.AreEqual(_actividad.Descripcion, ((Actividad)_actividades[0]).Descripcion);
			Assert.AreEqual(_actividad.Duracion, ((Actividad)_actividades[0]).Duracion);
			Assert.AreEqual(_actividad.Activar, ((Actividad)_actividades[0]).Activar);

		}

        [Test, Order(8)]
        public void DAOConsultarActividades()
        {
            daoActividad = FabricaDAO.CrearDAOActividad();
            //Descomentar las siguientes lineas al ejecutar la pu de forma individual
            //iDAOLugarTuristico = FabricaDAO.CrearDAOLugarTuristico();
            // iDAOLugarTuristico.Insertar(_lugaresTuristicos[0]);
            // daoActividad.Insertar(_actividades[0], _lugaresTuristicos[0]);

            _actividad = (Actividad)_actividades[0];

            _actividades = daoActividad.ConsultarLista(_lugaresTuristicos[0]);

            Assert.AreEqual(_actividad.Id, _actividades[0].Id);
            Assert.AreEqual(_actividad.Nombre, ((Actividad)_actividades[0]).Nombre);
            Assert.AreEqual(_actividad.Descripcion, ((Actividad)_actividades[0]).Descripcion);
            Assert.AreEqual(_actividad.Duracion, ((Actividad)_actividades[0]).Duracion);
            Assert.AreEqual(_actividad.Activar, ((Actividad)_actividades[0]).Activar);

        }


        [Test, Order(9)]
        public void DAOEliminarActividad()
        {
            int actividadesAntesDeBorrar = 0;
            int actividadesDespuesDeBorrar = 0;

            daoActividad = FabricaDAO.CrearDAOActividad();

                // Descomentar las siguientes lineas al ejecutar la pu de forma individual
            //iDAOLugarTuristico = FabricaDAO.CrearDAOLugarTuristico();
            //iDAOLugarTuristico.Insertar(_lugaresTuristicos[0]);
            //daoActividad.Insertar(_actividades[0], _lugaresTuristicos[0]);
            //daoActividad.Insertar(_actividades[1], _lugaresTuristicos[0]);

            _actividades = daoActividad.ConsultarLista(_lugaresTuristicos[0]);
            actividadesAntesDeBorrar = _actividades.Count;

            _actividad = (Actividad)_actividades[0];
            daoActividad.Eliminar(_actividad);

            _actividades = daoActividad.ConsultarLista(_lugaresTuristicos[0]);
            actividadesDespuesDeBorrar = _actividades.Count;

            Assert.Greater(actividadesAntesDeBorrar, actividadesDespuesDeBorrar);

            Assert.False(_actividades.Contains(_actividad));

        }

        [Test, Order(10)]
        public void ComandoLTEAgregarActividad()
        {
            daoActividad = FabricaDAO.CrearDAOActividad();

            ((LugarTuristico)_lugaresTuristicos[0]).Actividad.Add( (Actividad)_actividades[0] );
            ((LugarTuristico)_lugaresTuristicos[0]).Actividad.Add((Actividad)_actividades[1]);

            // Descomentar las siguientes lineas al ejecutar la pu de forma individual
            iDAOLugarTuristico = FabricaDAO.CrearDAOLugarTuristico();
            iDAOLugarTuristico.Insertar(_lugaresTuristicos[0]);

            _comandoA = FabricaComando.CrearComandoLTAgregarActividad(_lugaresTuristicos[0]);
            _comandoA.Ejecutar();

            //lista de lugares Actividades
            _actividades = daoActividad.ConsultarLista(_lugaresTuristicos[0]);

            Assert.True( _actividades.Contains( _actividades[0]) );
            Assert.True( _actividades.Contains( _actividades[1]) );
        }

        [Test, Order(11)]
        public void ComandoLTEliminarActividad()
        {
            _comandoA = FabricaComando.CrearComandoLTEliminarActividad();
            //_comandoB = FabricaComando.CrearComandoLTAgregarActividad();

        }
        #endregion

        //Categoria
        #region
        #endregion

        //Horario
        #region
        #endregion

        #endregion



        //Auxiliares de Pruebas unitarias de las excepcions
        #region
        public void BaseDeDatosExcepcionDAOLugarTuristicoInsertar()
		{

			DAO dao= FabricaDAO.CrearDAOLugarTuristico();
			dao.SqlConexion = new NpgsqlConnection("Host = localhost; Port = 5432; " +
			"Username = admin_cocotrip; " +
			"Password = ds1718a; " +
			"Database = cocotip");
			dao.Insertar(_lugarTuristico);

		}

		public void SocketExcepcionDAOLugarTuristicoInsertar()
		{

			DAO dao = FabricaDAO.CrearDAOLugarTuristico();
			dao.SqlConexion = new NpgsqlConnection("Host = localhost; Port =5435; " +
			"Username = admin_cocotrip; " +
			"Password = ds1718a; " +
			"Database = cocotrip");
			dao.Insertar(_lugarTuristico);

		}

		public void ParametrosNulosDAOLugarTuristicoInsertar()
		{

			DAO dao = FabricaDAO.CrearDAOLugarTuristico();
			dao.Insertar(null);

		}

		public void CasteoInvalidoDAOLugarTuristicoInsertar()
		{

			DAO dao = FabricaDAO.CrearDAOLugarTuristico();
			_lugarTuristico.Descripcion = null; 
			dao.Insertar(_lugarTuristico);
			
		}
		#endregion

		//TearDown
		#region
		[TearDown]
		public void TearDown()
		{

			_lugarTuristico = null;
			_lugaresTuristicos = null;
		}
		#endregion
	}
}
