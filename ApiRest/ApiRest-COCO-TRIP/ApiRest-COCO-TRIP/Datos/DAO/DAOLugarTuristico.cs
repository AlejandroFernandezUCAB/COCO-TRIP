using System;
using System.Collections.Generic;
using ApiRest_COCO_TRIP.Datos.Entity;
using ApiRest_COCO_TRIP.Datos.DAO.Interfaces;
using System.Data;
using NpgsqlTypes;
using ApiRest_COCO_TRIP.Datos.Fabrica;
using Npgsql;

namespace ApiRest_COCO_TRIP.Datos.DAO
{
	public class DAOLugarTuristico : DAO, IDAOLugarTuristico
	{
		private LugarTuristico _lugarTuristico;
		private NpgsqlDataReader _datos;

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
				_datos = base.Comando.ExecuteReader();

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

			}
			catch (Exception e)
			{

			}
			return lugaresTuristicos;
		}

		public override Entidad ConsultarPorId(Entidad objeto)
		{
			throw new NotImplementedException();
		}

		public override void Eliminar(Entidad objeto)
		{
			throw new NotImplementedException();
		}

		public override void Insertar(Entidad objeto)
		{
			int success = 0 ;
			_lugarTuristico = (LugarTuristico)objeto;
			
			try
			{

				StoredProcedure("insertarlugarturistico");
				ParametrosAgregar(_lugarTuristico);
				success = base.Comando.ExecuteNonQuery();

			}
			catch(InvalidCastException e)
			{

			}
			catch(Exception e)
			{

			}
			finally
			{
				base.Desconectar();
			}
		}

		/// <summary>
		/// Para hacer la conexion y crear el stored procedure
		/// </summary>
		/// <param name="sp"></param>
		private void StoredProcedure(string sp)
		{
			base.Conectar();
			base.Comando = base.SqlConexion.CreateCommand();
			base.Comando.CommandType = CommandType.StoredProcedure;
			base.Comando.CommandText = sp;
		}

		/// <summary>
		/// Automatizando el llenado de los parametros de agregar
		/// </summary>
		/// <param name="lugarturistico"></param>
		private void ParametrosAgregar(LugarTuristico lugarTuristico)
		{
			base.Comando.Parameters.AddWithValue(NpgsqlDbType.Varchar, _lugarTuristico.Nombre);
			base.Comando.Parameters.AddWithValue(NpgsqlDbType.Numeric, _lugarTuristico.Costo);
			base.Comando.Parameters.AddWithValue(NpgsqlDbType.Varchar, _lugarTuristico.Descripcion);
			base.Comando.Parameters.AddWithValue(NpgsqlDbType.Varchar, _lugarTuristico.Direccion);
			base.Comando.Parameters.AddWithValue(NpgsqlDbType.Varchar, _lugarTuristico.Correo);
			base.Comando.Parameters.AddWithValue(NpgsqlDbType.Bigint, _lugarTuristico.Telefono);
			base.Comando.Parameters.AddWithValue(NpgsqlDbType.Numeric, _lugarTuristico.Latitud);
			base.Comando.Parameters.AddWithValue(NpgsqlDbType.Numeric, _lugarTuristico.Longitud);
			base.Comando.Parameters.AddWithValue(NpgsqlDbType.Boolean, _lugarTuristico.Activar);

		}
	}
}