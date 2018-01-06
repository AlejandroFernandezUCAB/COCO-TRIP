using System;
using System.Collections.Generic;
using ApiRest_COCO_TRIP.Datos.Entity;
using NLog;
using ApiRest_COCO_TRIP.Datos.DAO.Interfaces;
using ApiRest_COCO_TRIP.Datos.Fabrica;
using ApiRest_COCO_TRIP.Comun.Excepcion;
using System.Reflection;
using Newtonsoft.Json.Linq;

namespace ApiRest_COCO_TRIP.Negocio.Command
{
	public class ComandoActualizarEstadoLT : Comando
	{
		private Entidad _lugarTuristico;
		private IDAOLugarTuristico _daoLugarTuristico;
		private static Logger log = LogManager.GetCurrentClassLogger();
		
		public ComandoActualizarEstadoLT(JObject datos)
		{
			_lugarTuristico = FabricaEntidad.CrearEntidadLugarTuristico();
			_lugarTuristico =  datos.ToObject<LugarTuristico>(); ;
			_daoLugarTuristico = FabricaDAO.CrearDAOLugarTuristico();
		} 

		public override void Ejecutar()
		{
			try
			{	
				//Con este if cambio a la condicion nueva. 
				if( ((LugarTuristico)_lugarTuristico).Activar == true )
				{
					((LugarTuristico)_lugarTuristico).Activar = false;
				}
				else
				{
					((LugarTuristico)_lugarTuristico).Activar = true;
				}

				_daoLugarTuristico.ActualizarEstado(_lugarTuristico);

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
	}
}