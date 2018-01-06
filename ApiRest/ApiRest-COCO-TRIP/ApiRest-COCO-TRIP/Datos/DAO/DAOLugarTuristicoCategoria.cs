using System;
using System.Collections.Generic;
using ApiRest_COCO_TRIP.Datos.Entity;
using ApiRest_COCO_TRIP.Datos.DAO.Interfaces;
using System.Data;
using NpgsqlTypes;
using NLog;
using ApiRest_COCO_TRIP.Comun.Excepcion;
using System.Reflection;
using Npgsql;
using System.Net.Sockets;
using ApiRest_COCO_TRIP.Datos.Fabrica;

namespace ApiRest_COCO_TRIP.Datos.DAO
{
	public class DAOLugarTuristicoCategoria : DAO, IDAOLugarTuristicoCategoria
	{
		private static Logger log = LogManager.GetCurrentClassLogger();
		private List<Entidad> _categorias;
		private IDAOCategoria iDAOCategoria;
		private NpgsqlDataReader _respuesta;

		public override void Actualizar(Entidad objeto)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Consulta lista de Categorias con un lugar turistico
		/// </summary>
		/// <param name="objeto"></param>
		/// <returns></returns>
		public override List<Entidad> ConsultarLista(Entidad objeto)
		{
			List<Entidad> categorias = new List<Entidad>();
			try
			{
				StoredProcedure("consultarcategorialugarturistico");
				Comando.Parameters.AddWithValue(NpgsqlDbType.Integer, objeto.Id);
				_respuesta = Comando.ExecuteReader();

				while (_respuesta.Read())
				{
					Categoria categoria;
					categoria = FabricaEntidad.CrearEntidadCategoria();
					categoria.Id = _respuesta.GetInt32(0);
					categoria.CategoriaSuperior = _respuesta.GetInt32(1);
					categoria.Nombre = _respuesta.GetString(2);

					categorias.Add(categoria);
				}

				return categorias;

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
		/// Metodo para Asociar una categoria a un lugar turistico
		/// </summary>
		/// <param name="categoria"></param>
		/// <param name="lugarTuristico"></param>
		public void Insertar(Entidad categoria, Entidad lugarTuristico)
		{
			int success = 0;
			try
			{
				StoredProcedure("insertarcategorialugarturistico");
				Comando.Parameters.AddWithValue(NpgsqlDbType.Integer, lugarTuristico.Id);
				Comando.Parameters.AddWithValue(NpgsqlDbType.Integer, categoria.Id);
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
		/// Metodo que obtiene las categorias de un lugar turistico.
		/// </summary>
        /// <param name="entidad">Objeto: LugarTuristico.</param>
        /// <returns>Lista de categorias</returns>
		public List<Entidad> ObtenerCategoriaPorId(Entidad objeto){
			_categorias = new List<Entidad>();
			try
			{
				StoredProcedure("ConsultarCategoriaLugarTuristico");
				Comando.Parameters.AddWithValue(NpgsqlDbType.Integer, objeto.Id);
				_respuesta = Comando.ExecuteReader();
				while (_respuesta.Read())
				{
					Categoria categoria = FabricaEntidad.CrearEntidadCategoria();
					categoria.Id = _respuesta.GetInt32(0);
					iDAOCategoria = FabricaDAO.CrearDAOCategoria();

					categoria = (Categoria)iDAOCategoria.ObtenerCategoriaPorId(categoria)[0];
					_categorias.Add(categoria);
				}
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

			return _categorias;
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