using System;
using System.Collections.Generic;
using ApiRest_COCO_TRIP.Datos.Entity;
using ApiRest_COCO_TRIP.Datos.DAO.Interfaces;
using System.Data;
using NpgsqlTypes;

namespace ApiRest_COCO_TRIP.Datos.DAO
{
	public class DAOHorario : DAO , IDAOHorario
	{
		public override void Actualizar(Entidad objeto)
		{
			throw new NotImplementedException();
		}

		public override List<Entidad> ConsultarLista(Entidad objeto)
		{
			throw new NotImplementedException();
		}

        /// <summary>
        /// Devuelve la lista de Horarios de un lugar turistico especifico
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<Entidad> ConsultarLista(string id)
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
		/// Insertar el horario relacionado con un lugar en la BDD  
		/// </summary>
		/// <param name="objeto">Horario</param>
		/// <param name="lugarTuristico">Lugar TUristico</param>
		public void Insertar(Entidad objeto, Entidad lugarTuristico)
		{
			Horario horario = (Horario)objeto;
			int success = 0;

			try
			{
				StoredProcedure("insertarhorario");
				//Asignando parametros al SP
				Comando.Parameters.AddWithValue(NpgsqlDbType.Integer, horario.DiaSemana);
				Comando.Parameters.AddWithValue(NpgsqlDbType.Time, horario.HoraApertura);
				Comando.Parameters.AddWithValue(NpgsqlDbType.Time, horario.HoraCierre);
				Comando.Parameters.AddWithValue(NpgsqlDbType.Integer, lugarTuristico.Id);
				//Ejecucion
				success = Comando.ExecuteNonQuery();

			}
			catch (InvalidCastException e)
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
			Comando = base.SqlConexion.CreateCommand();
			Comando.CommandType = CommandType.StoredProcedure;
			Comando.CommandText = sp;

		}
	}
}