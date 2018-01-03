using System;
using System.Collections.Generic;
using ApiRest_COCO_TRIP.Datos.Entity;
using ApiRest_COCO_TRIP.Datos.DAO.Interfaces;
using System.Data;
using NpgsqlTypes;
using Npgsql;
using ApiRest_COCO_TRIP.Datos.Fabrica;
using ApiRest_COCO_TRIP.Comun.Excepcion;
using System.Reflection;
using System.Net.Sockets;
using NLog;

namespace ApiRest_COCO_TRIP.Datos.DAO
{
	public class DAOActividad : DAO , IDAOActividad
	{
		private List<Entidad> _actividad;
		private NpgsqlDataReader _respuesta;
		private static Logger log = LogManager.GetCurrentClassLogger();

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

		/// <summary>
		/// Consulta la lista de actividades segun un lugar turistico
		/// </summary>
		/// <param name="objeto">Lugar turistico de las actividades que se quieren</param>
		/// <returns>Lugar turistico</returns>
		public override List<Entidad> ConsultarLista(Entidad objeto)
		{
			_actividad = new List<Entidad>();
			//Recordemos que el objeto es de un lugar turistico
			try
			{
				StoredProcedure("consultarActividades");
				Comando.Parameters.AddWithValue(NpgsqlDbType.Integer, objeto.Id);
				_respuesta = Comando.ExecuteReader();

				while (_respuesta.Read())
				{
					Actividad actividad;
					actividad = FabricaEntidad.CrearEntidadActividad();
					actividad.Id = _respuesta.GetInt32(0);
					actividad.Foto.Ruta = _respuesta.GetString(1);
					actividad.Nombre = _respuesta.GetString(2);
					actividad.Duracion = _respuesta.GetTimeSpan(3);
					actividad.Descripcion = _respuesta.GetString(4);
					actividad.Activar = _respuesta.GetBoolean(5);
					_actividad.Add(actividad);
				}

				return _actividad;

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
			Comando = SqlConexion.CreateCommand();
			Comando.CommandType = CommandType.StoredProcedure;
			Comando.CommandText = sp;
		}
	}
}