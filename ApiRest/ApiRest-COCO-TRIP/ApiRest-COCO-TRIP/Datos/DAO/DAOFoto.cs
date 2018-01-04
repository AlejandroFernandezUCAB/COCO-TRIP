using System;
using System.Collections.Generic;
using ApiRest_COCO_TRIP.Datos.Entity;
using ApiRest_COCO_TRIP.Datos.DAO.Interfaces;
using System.Data;
using Npgsql;
using ApiRest_COCO_TRIP.Comun.Excepcion;
using System.Reflection;
using System.Net.Sockets;
using NLog;

namespace ApiRest_COCO_TRIP.Datos.DAO
{
	public class DAOFoto : DAO , IDAOFoto
	{
        private Foto _foto;
        private List<Entidad> _listaFotos;
        private NpgsqlCommand _comando;
        private NpgsqlDataReader _respuesta;
		private static Logger log = LogManager.GetCurrentClassLogger();

		/// <summary>
		/// Devuelve la lista de fotos de un lugar turistico especifico
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
        /// Devuelve la lista de fotos de un lugar turistico especifico.
        /// Recibe un lugar turistico.
        /// </summary>
        /// <param name="objeto"></param>
        /// <returns></returns>
		public override List<Entidad> ConsultarLista(Entidad objeto)
		{
            // Inicializamos la lista de fotos;
            _listaFotos = new List<Entidad>();
			// Se evita castear el objeto a un objeto lugar turistico
			// pues no es necesario.
			try
			{

				base.Conectar(); //Inicia una sesion con la base de datos


				_comando = new NpgsqlCommand("ConsultarFotos", base.SqlConexion);
				_comando.CommandType = CommandType.StoredProcedure;
				// Recordar que objeto es un lugar turistico
				_comando.Parameters.AddWithValue(NpgsqlTypes.NpgsqlDbType.Integer, objeto.Id);
				_respuesta = _comando.ExecuteReader();
				while (_respuesta.Read())
				{
					// Creo cada entidad Foto y la agrego a la lista
					Foto nuevaFoto;
					if (!_respuesta.IsDBNull(1))
					{
						nuevaFoto = new Foto(_respuesta.GetInt32(0), _respuesta.GetString(1));
						_listaFotos.Add(nuevaFoto);
					}
				}

				
				// Retorno la lista de entidades
				return _listaFotos;
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
			_foto = (Foto)objeto;
			try
			{
				Conectar(); //Inicia una sesion con la base de datos
				 _comando = new NpgsqlCommand("EliminarFoto", SqlConexion);
                _comando.CommandType = CommandType.StoredProcedure;
                _comando.Parameters.AddWithValue(NpgsqlTypes.NpgsqlDbType.Integer, _foto.Id);
                // El Store Procedure devuelve void
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
        /// Inserta una nueva foto.
        /// Requiere la id del Lugar Turistico al que pertenece
        /// </summary>
        /// <param name="foto"></param>
        /// <param name="idLugar"></param>
		public void Insertar(Entidad foto, Entidad lugar)
		{
            try
            {
                _foto = (Foto)foto;

                base.Conectar(); //Inicia una sesion con la base de datos


                _comando = new NpgsqlCommand("InsertarFoto", base.SqlConexion);
                _comando.CommandType = CommandType.StoredProcedure;
                _comando.Parameters.AddWithValue(NpgsqlTypes.NpgsqlDbType.Varchar, _foto.Ruta);
                _comando.Parameters.AddWithValue(NpgsqlTypes.NpgsqlDbType.Integer, lugar.Id);
                // _respuesta = _comando.ExecuteReader();
                // _respuesta.Read();
                // Esto Devuelve un id de base de datos
                // pero no hace falta utilizarlo aqui...

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

        public override void Insertar(Entidad objeto)
        {
            throw new NotImplementedException();
        }
    }
}