using System;
using System.Collections.Generic;
using ApiRest_COCO_TRIP.Datos.Entity;
using ApiRest_COCO_TRIP.Datos.DAO.Interfaces;
using System.Data;
using NpgsqlTypes;

namespace ApiRest_COCO_TRIP.Datos.DAO
{
	public class DAOLugarTuristico_Categoria : DAO, IDAOLugarTuristicoCategoria
	{
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
			throw new NotImplementedException();
		}

		/// <summary>
		/// Metodo para insertar un 
		/// </summary>
		/// <param name="categoria"></param>
		/// <param name="lugarTuristico"></param>
		public void Insertar(Entidad categoria, Entidad lugarTuristico)
		{
			int success = 0;
			try
			{
				StoredProcedure("insertarlugarturisticocategoria");
				Comando.Parameters.AddWithValue(NpgsqlDbType.Integer, lugarTuristico.Id);
				Comando.Parameters.AddWithValue(NpgsqlDbType.Integer, categoria.Id);
				success = Comando.ExecuteNonQuery();
			}
			catch (Exception e)
			{

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