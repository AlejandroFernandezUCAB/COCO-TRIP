using System;
using System.Collections.Generic;
using ApiRest_COCO_TRIP.Datos.Entity;
using ApiRest_COCO_TRIP.Datos.DAO.Interfaces;
using System.Data;
using NpgsqlTypes;

namespace ApiRest_COCO_TRIP.Datos.DAO
{
	public class DAOLugarTuristico : DAO, IDAOLugarTuristico
	{
		private LugarTuristico _lugarTuristico;

		public override void Actualizar(Entidad objeto)
		{
			throw new NotImplementedException();
		}

		public override List<Entidad> ConsultarLista(Entidad objeto)
		{
			throw new NotImplementedException();
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
				base.Comando.Parameters.AddWithValue(NpgsqlDbType.Varchar, _lugarTuristico.Nombre);
				base.Comando.Parameters.AddWithValue(NpgsqlDbType.Numeric, _lugarTuristico.Costo);
				base.Comando.Parameters.AddWithValue(NpgsqlDbType.Varchar, _lugarTuristico.Descripcion);
				base.Comando.Parameters.AddWithValue(NpgsqlDbType.Varchar, _lugarTuristico.Direccion);
				base.Comando.Parameters.AddWithValue(NpgsqlDbType.Varchar, _lugarTuristico.Correo);
				base.Comando.Parameters.AddWithValue(NpgsqlDbType.Bigint, _lugarTuristico.Telefono);
				base.Comando.Parameters.AddWithValue(NpgsqlDbType.Numeric, _lugarTuristico.Latitud);
				base.Comando.Parameters.AddWithValue(NpgsqlDbType.Numeric, _lugarTuristico.Longitud);
				base.Comando.Parameters.AddWithValue(NpgsqlDbType.Boolean, _lugarTuristico.Activar);
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
			base.Comando.Parameters.AddWithValue(NpgsqlDbType.Varchar, lugarTuristico.Nombre);
			base.Comando.Parameters.AddWithValue(NpgsqlDbType.Numeric, lugarTuristico.Costo);
			base.Comando.Parameters.AddWithValue(NpgsqlDbType.Varchar, lugarTuristico.Descripcion);
			base.Comando.Parameters.AddWithValue(NpgsqlDbType.Varchar, lugarTuristico.Direccion);
			base.Comando.Parameters.AddWithValue(NpgsqlDbType.Varchar, lugarTuristico.Correo);
			base.Comando.Parameters.AddWithValue(NpgsqlDbType.Bigint, lugarTuristico.Telefono);
			base.Comando.Parameters.AddWithValue(NpgsqlDbType.Numeric, lugarTuristico.Latitud);
			base.Comando.Parameters.AddWithValue(NpgsqlDbType.Numeric, lugarTuristico.Longitud);
			base.Comando.Parameters.AddWithValue(NpgsqlDbType.Boolean, lugarTuristico.Activar);

		}
	}
}