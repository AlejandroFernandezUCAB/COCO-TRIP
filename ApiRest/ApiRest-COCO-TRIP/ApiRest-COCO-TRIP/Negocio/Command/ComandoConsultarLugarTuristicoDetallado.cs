using System;
using System.Collections.Generic;
using ApiRest_COCO_TRIP.Datos.Entity;
using Newtonsoft.Json.Linq;
using ApiRest_COCO_TRIP.Datos.Fabrica;
using ApiRest_COCO_TRIP.Datos.DAO.Interfaces;

namespace ApiRest_COCO_TRIP.Negocio.Command
{
	public class ComandoConsultarLugarTuristicoDetallado : Comando
	{
		private List<Entidad> _actividades;
		private List<Entidad> _horarios;
		private JObject _lugarTuristicoObject;
		private Entidad _lugarTuristico;
		private IDAOFoto iDAOFoto;
		private IDAOActividad iDAOActividad;
		private IDAOHorario iDAOHorario;
		private IDAOLugarTuristico iDAOLugarTuristico;

		public ComandoConsultarLugarTuristicoDetallado(JObject lugarTuristico)
		{

			_lugarTuristico = FabricaEntidad.CrearEntidadLugarTuristico();
			_lugarTuristico = lugarTuristico.ToObject<LugarTuristico>();
			_actividades = new List<Entidad>();
			_horarios = new List<Entidad>();

		}

		public override void Ejecutar()
		{
			
			//Traer lugar turistico detallado
			_lugarTuristico = iDAOLugarTuristico.ConsultarPorId(_lugarTuristico);

			//Traer actividades del lugar turistico
			_actividades = iDAOActividad.ConsultarLista(_lugarTuristico);
			foreach(Actividad actividad in _actividades)
			{
				((LugarTuristico)_lugarTuristico).Actividad.Add(actividad);
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