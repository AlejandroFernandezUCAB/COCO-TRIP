using System;
using System.Collections.Generic;
using ApiRest_COCO_TRIP.Datos.Entity;
using ApiRest_COCO_TRIP.Datos.DAO.Interfaces;
using System.Data;
using NpgsqlTypes;

namespace ApiRest_COCO_TRIP.Datos.DAO
{
	public class DAOActividad : DAO , IDAOActividad
	{
        /// <summary>
        /// Devuelve la lista de Actividades de un lugar turistico especifico
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<Entidad> ConsultarLista(string id)
        {
            throw new NotImplementedException();
        }

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
		/// Insertar una actividad referenciado con el lugarturistico
		/// </summary>
		/// <param name="entidad">Objeto: Actividad</param>
		/// <param name="lugarTuristico">Objeto: LugarTuristico</param>
		public void Insertar(Entidad entidad, Entidad lugarTuristico )
		{
			Actividad actividad = (Actividad)entidad;
			int success = 0;
			try
			{
				StoredProcedure("insertarActividad");
				Comando.Parameters.AddWithValue(NpgsqlDbType.Varchar, actividad.Nombre);
				Comando.Parameters.AddWithValue(NpgsqlDbType.Varchar, actividad.Foto.Ruta);
				Comando.Parameters.AddWithValue(NpgsqlDbType.Time, actividad.Duracion);
				Comando.Parameters.AddWithValue(NpgsqlDbType.Varchar, actividad.Descripcion);
				Comando.Parameters.AddWithValue(NpgsqlDbType.Boolean, actividad.Activar);
				Comando.Parameters.AddWithValue(NpgsqlDbType.Integer, lugarTuristico.Id);
				success = Comando.ExecuteNonQuery();

			}catch(InvalidCastException e)
			{

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