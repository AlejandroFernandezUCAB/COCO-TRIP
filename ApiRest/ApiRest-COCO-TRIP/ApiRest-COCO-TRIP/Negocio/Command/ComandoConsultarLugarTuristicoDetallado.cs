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

		}

		public override void Ejecutar()
		{
			//TODO hacer el metodo para traer el lugar turistico
			throw new NotImplementedException();
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