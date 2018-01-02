using System.Collections.Generic;
using ApiRest_COCO_TRIP.Datos.Entity;
using ApiRest_COCO_TRIP.Datos.DAO;
using ApiRest_COCO_TRIP.Datos.Fabrica;
using System;
using ApiRest_COCO_TRIP.Comun.Excepcion;
using System.Reflection;
using NLog;

namespace ApiRest_COCO_TRIP.Negocio.Command
{
	public class ComandoLTAgregarHorario: Comando
	{
		private DAOHorario _daoHorario;
		private Entidad _lugarTuristico;
		private List<Entidad> _horarios;
		private static Logger log = LogManager.GetCurrentClassLogger();

		public ComandoLTAgregarHorario(Entidad lugarTuristico)
		{
			_lugarTuristico = lugarTuristico;
			_daoHorario = FabricaDAO.CrearDAOHorario();
			_horarios = ((LugarTuristico)lugarTuristico).Horario.ConvertAll(new Converter<Horario, Entidad>(ConvertListHorario)); 

		}

		public override void Ejecutar()
		{
			try
			{

				for (int i = 0; i < _horarios.Count; i++)
				{
					_daoHorario.Insertar(_horarios[i], _lugarTuristico);
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
			throw new System.NotImplementedException();
		}

		public override List<Entidad> RetornarLista()
		{
			throw new System.NotImplementedException();
		}

		/// <summary>
		/// Convierte el objeto Horario a Entidad
		/// </summary>
		/// <param name="input">Objeto Foto</param>
		/// <returns>Objeto Entidad</returns>
		private Entidad ConvertListHorario(Horario input)
		{
			return input;
		}


	}
}