using System;
using System.Collections.Generic;
using ApiRest_COCO_TRIP.Datos.Entity;
using Newtonsoft.Json.Linq;
using ApiRest_COCO_TRIP.Datos.Fabrica;
using ApiRest_COCO_TRIP.Datos.DAO.Interfaces;
using NLog;
using ApiRest_COCO_TRIP.Comun.Excepcion;
using System.Reflection;

namespace ApiRest_COCO_TRIP.Negocio.Command
{
	public class ComandoConsultarLugarTuristicoDetallado : Comando
	{
		private List<Entidad> _actividades;
		private List<Entidad> _horarios;
		private List<Entidad> _categorias;
		private JObject _lugarTuristicoObject;
		private Entidad _lugarTuristico;
		private IDAOLugarTuristicoCategoria iDAOCategoria;
		private IDAOActividad iDAOActividad;
		private IDAOHorario iDAOHorario;
		private IDAOLugarTuristico iDAOLugarTuristico;
		private static Logger log = LogManager.GetCurrentClassLogger();

		public ComandoConsultarLugarTuristicoDetallado(JObject lugarTuristico)
		{
			iDAOCategoria = FabricaDAO.CrearDAOLugarTuristico_Categoria();
			iDAOActividad = FabricaDAO.CrearDAOActividad();
			iDAOHorario = FabricaDAO.CrearDAOHorario();
			iDAOLugarTuristico = FabricaDAO.CrearDAOLugarTuristico();
			_lugarTuristico = FabricaEntidad.CrearEntidadLugarTuristico();
			_lugarTuristico = lugarTuristico.ToObject<LugarTuristico>();
			_actividades = new List<Entidad>();
			_horarios = new List<Entidad>();
			_categorias = new List<Entidad>();

		}

		public override void Ejecutar()
		{
			try{ 
				//Traer lugar turistico detallado
				_lugarTuristico = iDAOLugarTuristico.ConsultarPorId(_lugarTuristico);

				//Traer actividades del lugar turistico
				_actividades = iDAOActividad.ConsultarLista(_lugarTuristico);

				foreach(Actividad actividad in _actividades)
				{
					((LugarTuristico)_lugarTuristico).Actividad.Add(actividad);
				}

				//Trayendo horarios
				_horarios = iDAOHorario.ConsultarLista(_lugarTuristico);

				foreach (Horario horario in _horarios)
				{
					((LugarTuristico)_lugarTuristico).Horario.Add(horario);
				}

				//Trayendo categorias
				_categorias = iDAOCategoria.ConsultarLista(_lugarTuristico);
			
				foreach (Categoria categoria in _categorias)
				{
					((LugarTuristico)_lugarTuristico).Categoria.Add(categoria);
				}

				log.Info("Lugar turistico:" + _lugarTuristico);
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
			return _lugarTuristico;
		}

		public override List<Entidad> RetornarLista()
		{
			throw new NotImplementedException();
		}
	}
}