using System;
using System.Collections.Generic;
using ApiRest_COCO_TRIP.Datos.Entity;
using ApiRest_COCO_TRIP.Datos.DAO.Interfaces;
using System.Data;
using NpgsqlTypes;
using ApiRest_COCO_TRIP.Comun.Excepcion;
using System.Reflection;
using Npgsql;
using System.Net.Sockets;
using NLog;

namespace ApiRest_COCO_TRIP.Datos.DAO
{
	public class DAOHorario : DAO , IDAOHorario
	{

		private static Logger log = LogManager.GetCurrentClassLogger();

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
			catch (NullReferenceException e)
			{

				log.Error(e.Message);
				throw new ReferenciaNulaExcepcion(e, "Parametros de entrada nulos en: "
				+ GetType().FullName + "." + MethodBase.GetCurrentMethod().Name + ". " + e.Message);

			}
			catch (InvalidCastException e)
			{

				log.Error("Casteo invalido en:"
				+ GetType().FullName + "." + MethodBase.GetCurrentMethod().Name + ". " + e.Message);
				throw new CasteoInvalidoExcepcion(e, "Ocurrio un casteo invalido en: "
				+ GetType().FullName + "." + MethodBase.GetCurrentMethod().Name + ". " + e.Message);

			}
			catch (NpgsqlException e)
			{

				log.Error("Ocurrio un error en la base de datos en: "
				+ GetType().FullName + "." + MethodBase.GetCurrentMethod().Name + ". " + e.Message);
				throw new BaseDeDatosExcepcion(e, "Ocurrio un error en la base de datos en: "
				+ GetType().FullName + "." + MethodBase.GetCurrentMethod().Name + ". " + e.Message);

			}
			catch (SocketException e)
			{

				log.Error("Ocurrio un error en la base de datos en: "
				+ GetType().FullName + "." + MethodBase.GetCurrentMethod().Name + ". " + e.Message);
				throw new SocketExcepcion(e, "Ocurrio un error en la base de datos en: "
				+ GetType().FullName + "." + MethodBase.GetCurrentMethod().Name + ". " + e.Message);

			}
			catch (Exception e)
			{

				log.Error("Ocurrio un error desconocido: "
				+ GetType().FullName + "." + MethodBase.GetCurrentMethod().Name + ". " + e.Message);
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
			Comando = base.SqlConexion.CreateCommand();
			Comando.CommandType = CommandType.StoredProcedure;
			Comando.CommandText = sp;

		}
	}
}