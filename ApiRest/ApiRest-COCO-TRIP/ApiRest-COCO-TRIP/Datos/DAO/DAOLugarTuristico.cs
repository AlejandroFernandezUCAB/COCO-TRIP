using System;
using System.Collections.Generic;
using ApiRest_COCO_TRIP.Datos.Entity;
using ApiRest_COCO_TRIP.Datos.DAO.Interfaces;
using System.Data;
using NpgsqlTypes;
using ApiRest_COCO_TRIP.Datos.Fabrica;
using Npgsql;
using ApiRest_COCO_TRIP.Comun.Excepcion;
using System.Reflection;
using NLog;

namespace ApiRest_COCO_TRIP.Datos.DAO
{
	public class DAOLugarTuristico : DAO, IDAOLugarTuristico
	{
		private LugarTuristico _lugarTuristico;
		private NpgsqlDataReader _datos;
		private static Logger log = LogManager.GetCurrentClassLogger();

		public override void Actualizar(Entidad objeto)
		{
			
		}

		public override List<Entidad> ConsultarLista(Entidad objeto)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Metodo que se trae toda la lista de lugares turisticos
		/// </summary>
		/// <returns>Lista de lugares turisticos completa</returns>
		public List<Entidad> ConsultarTodaLaLista()
		{
			List<Entidad> lugaresTuristicos = new List<Entidad>(); // Estos new me hacen ruido aqui.
			 
			try
			{
				StoredProcedure("ConsultarLugaresTuristico");
				_datos = Comando.ExecuteReader();

				while (_datos.Read())
				{
					LugarTuristico lugarTuristico = FabricaEntidad.CrearEntidadLugarTuristico();
					lugarTuristico.Id = _datos.GetInt32(0);
					lugarTuristico.Nombre = _datos.GetString(1);
					lugarTuristico.Costo = _datos.GetDouble(2);
					lugarTuristico.Descripcion = _datos.GetString(3);
					lugarTuristico.Direccion = _datos.GetString(4);
					lugarTuristico.Correo = _datos.GetString(5);
					lugarTuristico.Telefono = _datos.GetInt64(6);
					lugarTuristico.Latitud = _datos.GetDouble(7);
					lugarTuristico.Longitud = _datos.GetDouble(8);
					lugarTuristico.Activar = _datos.GetBoolean(9);
					lugaresTuristicos.Add(lugarTuristico);
				}

				return lugaresTuristicos;
			}
			catch (Exception e)
			{
				return null;
			}
			finally
			{
				base.Desconectar();
			}

			
		}

		public override Entidad ConsultarPorId(Entidad objeto)
		{
			throw new NotImplementedException();
		}

		public override void Eliminar(Entidad objeto)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Insertar un lugar turistico en la base de datos
		/// </summary>
		/// <param name="objeto">Objeto lugar turistico</param>
		public override void Insertar(Entidad objeto)
		{
			int success = 0 ;
			_lugarTuristico = (LugarTuristico)objeto;
			
			try
			{

				StoredProcedure("insertarlugarturistico");
				//Seteando los parametros al SP
				Comando.Parameters.AddWithValue(NpgsqlDbType.Varchar, _lugarTuristico.Nombre);
				Comando.Parameters.AddWithValue(NpgsqlDbType.Numeric, _lugarTuristico.Costo);
				Comando.Parameters.AddWithValue(NpgsqlDbType.Varchar, null);
				Comando.Parameters.AddWithValue(NpgsqlDbType.Varchar, _lugarTuristico.Direccion);
				Comando.Parameters.AddWithValue(NpgsqlDbType.Varchar, _lugarTuristico.Correo);
				Comando.Parameters.AddWithValue(NpgsqlDbType.Bigint, _lugarTuristico.Telefono);
				Comando.Parameters.AddWithValue(NpgsqlDbType.Numeric, _lugarTuristico.Latitud);
				Comando.Parameters.AddWithValue(NpgsqlDbType.Numeric, _lugarTuristico.Longitud);
				Comando.Parameters.AddWithValue(NpgsqlDbType.Boolean, _lugarTuristico.Activar);
				//Ejecucion
				success = Comando.ExecuteNonQuery();

			}
			catch(NullReferenceException e)
			{
				
				throw new ReferenciaNulaExcepcion(e, "Parametros de entrada nulos en: "
				+ GetType().FullName + "." + MethodBase.GetCurrentMethod().Name + ". " + e.Message);

			}
			catch(InvalidCastException e)
			{

				throw new CasteoInvalidoExcepcion(e, "Ocurrio un casteo invalido en: "
				+ GetType().FullName + "." + MethodBase.GetCurrentMethod().Name + ". " + e.Message);

			}
			catch(NpgsqlException e)
			{
				
				throw new BaseDeDatosExcepcion(e, "Ocurrio un error en la base de datos en: "
				+ GetType().FullName + "." + MethodBase.GetCurrentMethod().Name + ". " + e.Message);

			}
			catch(Exception e)
			{

				throw new Excepcion(e, "Ocurrio un error desconocido en: "
				+ GetType().FullName + "." + MethodBase.GetCurrentMethod().Name + ". " + e.Message);

			}
			finally
			{
				Desconectar();
			}
		}

		/// <summary>
		/// Para hacer la conexion y crear el stored procedure
		/// </summary>
		/// <param name="sp"></param>
		private void StoredProcedure(string sp)
		{
			Conectar();
			Comando = SqlConexion.CreateCommand();
			Comando.CommandType = CommandType.StoredProcedure;
			Comando.CommandText = sp;
		}

	}
}