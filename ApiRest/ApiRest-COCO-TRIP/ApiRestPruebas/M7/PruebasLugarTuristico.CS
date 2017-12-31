using ApiRest_COCO_TRIP.Datos.DAO;
using ApiRest_COCO_TRIP.Datos.DAO.Interfaces;
using ApiRest_COCO_TRIP.Datos.Entity;
using ApiRest_COCO_TRIP.Datos.Fabrica;
using Newtonsoft.Json.Linq;
using Npgsql;
using NUnit.Framework;
using System.Collections.Generic;

namespace ApiRestPruebas.M7
{
	[TestFixture]
	public class PruebasLugarTuristico
	{

		LugarTuristico _lugarTuristico;
		List<Entidad> _lugaresTuristicos;
		IDAOLugarTuristico iDAOLugarTuristico;
		JObject objetoJSON;
		
		[OneTimeSetUp]
		public void SetUpAll()
		{
			DAO test = FabricaDAO.CrearDAOLugarTuristico();

			test.Conectar();
			test.Comando = new NpgsqlCommand("SELECT setval('seq_lugar_turistico', 1)", test.SqlConexion);
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
			_lugarTuristico.Nombre = "Parque Venezuela";
			_lugarTuristico.Costo = 2000;
			_lugarTuristico.Descripcion = "Parque creado en Venezuela";
			_lugarTuristico.Direccion = "Av. Principal Venezuela";
			_lugarTuristico.Correo = "venezuela@venezuela.com";
			_lugarTuristico.Telefono = 04142792806;
			_lugarTuristico.Latitud = 25;
			_lugarTuristico.Longitud = 25;
			_lugarTuristico.Activar = true;

			_lugaresTuristicos.Add(_lugarTuristico);

		}

		//Prueba de DAO de Lugar Turistico
		[Test]
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

			Assert.AreEqual(_lugaresTuristicos[0].Id, resultado.Id);
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

		[Test]
		public void DAOTodosLosLugaresTuristicos()
		{
			List<Entidad> resultado = new List<Entidad>();
			iDAOLugarTuristico = FabricaDAO.CrearDAOLugarTuristico();

			iDAOLugarTuristico.Insertar( _lugaresTuristicos[1] );
			resultado = iDAOLugarTuristico.ConsultarTodaLaLista();

			for (int i = 0; i < resultado.Count; i++)
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
	

	

		[TearDown]
		public void TearDown()
		{
			_lugarTuristico = null;
			_lugaresTuristicos = null;
		}
	}
}
