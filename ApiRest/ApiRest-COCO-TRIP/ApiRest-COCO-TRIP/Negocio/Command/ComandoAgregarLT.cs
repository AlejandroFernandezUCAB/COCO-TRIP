using System;
using System.Collections.Generic;
using ApiRest_COCO_TRIP.Datos.Entity;
using ApiRest_COCO_TRIP.Datos.Fabrica;
using Newtonsoft.Json.Linq;
using ApiRest_COCO_TRIP.Datos.DAO.Interfaces;
using NLog;
using ApiRest_COCO_TRIP.Comun.Excepcion;
using System.Reflection;
using ApiRest_COCO_TRIP.Negocio.Fabrica;

namespace ApiRest_COCO_TRIP.Negocio.Command
{	
	/// <summary>
	/// Comando que permite agregar el lugares turistico con su foto, actividad, horario y su categoria
	/// </summary>
	public class ComandoAgregarLT : Comando
	{
		private ComandoLTAgregarFoto _comandoAgregarFoto;
		private ComandoLTAgregarActividad _comandoAgregarActividad;
		private ComandoLTAgregarHorario _comandoAgregarHorario;
		private ComandoLTAgregarCategoria _comandoAgregarCategoria;
		private Entidad _lugarTuristico;
		private IDAOLugarTuristico _daoLugarTuristico;
		private static Logger log = LogManager.GetCurrentClassLogger();

		/// <summary>
		/// Creo el comando con la lista de datos ya deseralizada
		/// </summary>
		/// <param name="datos">JSON de Lugar turistico</param>
		public ComandoAgregarLT(JObject datos)
		{
			
			//Iniciando los objetos
			_lugarTuristico = FabricaEntidad.CrearEntidadLugarTuristico();
			_daoLugarTuristico = FabricaDAO.CrearDAOLugarTuristico();
			_lugarTuristico = datos.ToObject<LugarTuristico>();

			//Iniciando los comandos
			_comandoAgregarActividad = FabricaComando.CrearComandoLTAgregarActividad( _lugarTuristico );
			_comandoAgregarFoto = FabricaComando.CrearComandoLTAgregarFoto( _lugarTuristico );
			_comandoAgregarHorario = FabricaComando.CrearComandoLTAgregarHorario( _lugarTuristico );
			_comandoAgregarCategoria = FabricaComando.CrearComandoLTAgregarCategoria( _lugarTuristico );
			

		}

		/// <summary>
		/// Inserta un lugar turistico en la bsae de datos
		/// </summary>
		public override void Ejecutar()
		{

			try
			{
				_daoLugarTuristico.Insertar(_lugarTuristico);

				//En la siguiente linea se invoca al DAO para que devuelva la lista de todos los lugares turisticos,
				//Luego esta lista pasa a UltimoLugarTuristico y ese id que devuelve se lo pasa al lugar turistico anteriormente insertado.

				_lugarTuristico.Id = UltimoIdLugarTuristico( _daoLugarTuristico.ConsultarTodaLaLista() ); 

				_comandoAgregarFoto.Ejecutar();
				_comandoAgregarActividad.Ejecutar();
				_comandoAgregarHorario.Ejecutar();
				_comandoAgregarCategoria.Ejecutar();


			}

			catch (ReferenciaNulaExcepcion e)
			{
				log.Error(e.Mensaje);
				throw new ReferenciaNulaExcepcion(e, "Parametros de entrada nulos en: "
				+ GetType().FullName + "." + MethodBase.GetCurrentMethod().Name + ". " + e.Message);
			}
			catch (CasteoInvalidoExcepcion e)
			{

				log.Error("Casteo invalido en:"
				+ GetType().FullName + "." + MethodBase.GetCurrentMethod().Name + ". " + e.Message);
				throw new CasteoInvalidoExcepcion(e, "Casteo invalido en: "
				+ GetType().FullName + "." + MethodBase.GetCurrentMethod().Name + ". " + e.Message);

			}
			catch (BaseDeDatosExcepcion e)
			{

				log.Error("Ocurrio un error en la base de datos en: "
				+ GetType().FullName + "." + MethodBase.GetCurrentMethod().Name + ". " + e.Message);
				throw new BaseDeDatosExcepcion(e, "Ocurrio un error en la base de datos en: "
				+ GetType().FullName + "." + MethodBase.GetCurrentMethod().Name + ". " + e.Message);

			}
			catch (Excepcion e)
			{

				log.Error("Ocurrio un error desconocido: "
				+ GetType().FullName + "." + MethodBase.GetCurrentMethod().Name + ". " + e.Message);
				throw new Excepcion(e, "Ocurrio un error desconocido en: "
				+ GetType().FullName + "." + MethodBase.GetCurrentMethod().Name + ". " + e.Message);

			}

		}

		public override Entidad Retornar()
		{
			throw new NotImplementedException();
		}

		public override List<Entidad> RetornarLista()
		{
			throw new NotImplementedException();
		}


		/// <summary>
		/// Se busca el ID del ultimo objeto en la lista
		/// </summary>
		/// <param name="lugaresTuristicos">Lista de lugares turisticos</param>
		/// <returns>El ultimo id del ultimo objeto de la lista</returns>
		private int UltimoIdLugarTuristico(List<Entidad> lugaresTuristicos)
		{
			int cantidadDeLugares = lugaresTuristicos.Count - 1;
			return lugaresTuristicos[cantidadDeLugares].Id;
		}
	}
}