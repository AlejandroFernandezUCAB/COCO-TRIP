using System;
using System.Collections.Generic;
using ApiRest_COCO_TRIP.Datos.Entity;
using ApiRest_COCO_TRIP.Datos.DAO.Interfaces;
using ApiRest_COCO_TRIP.Datos.Fabrica;
using ApiRest_COCO_TRIP.Comun.Excepcion;
using System.Reflection;
using NLog;

namespace ApiRest_COCO_TRIP.Negocio.Command
{
	public class ComandoLTAgregarActividad : Comando
	{
		private IDAOActividad _dao;
		private List<Entidad> _actividades;
		private Entidad _lugarTuristico;
		private static Logger log = LogManager.GetCurrentClassLogger();
		
		public ComandoLTAgregarActividad(Entidad lugarTuristico)
		{

			_dao = FabricaDAO.CrearDAOActividad();
			_lugarTuristico = lugarTuristico;
			_actividades = ((LugarTuristico)lugarTuristico).Actividad.ConvertAll(new Converter<Actividad, Entidad>(ConvertListActividad));

		}
		public override void Ejecutar()
		{
			try
			{	
			
				for (int i = 0; i < _actividades.Count; i++)
				{
					_dao.Insertar(_actividades[i], _lugarTuristico);
				}
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
		/// Convierte el objeto Actividad a Entidad
		/// </summary>
		/// <param name="input">Objeto Foto</param>
		/// <returns>Objeto Entidad</returns>
		private Entidad ConvertListActividad(Actividad input)
		{
			return input;
		}
	}
}