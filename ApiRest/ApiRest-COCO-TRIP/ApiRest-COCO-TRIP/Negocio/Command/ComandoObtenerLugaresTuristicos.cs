using System;
using System.Collections.Generic;
using ApiRest_COCO_TRIP.Datos.Entity;
using ApiRest_COCO_TRIP.Datos.Fabrica;
using ApiRest_COCO_TRIP.Datos.DAO.Interfaces;

namespace ApiRest_COCO_TRIP.Negocio.Command
{
	public class ComandoObtenerLugaresTuristicos : Comando
	{
		IDAOLugarTuristico _dao = FabricaDAO.CrearDAOLugarTuristico();
		List<Entidad> _lugaresTuristicos = new List<Entidad>();

		public override void Ejecutar()
		{
			try
			{
				_lugaresTuristicos = _dao.ConsultarTodaLaLista();
			}
			catch()
			{

			}
		}

		public override Entidad Retornar()
		{
			throw new NotImplementedException();
		}

		public override List<Entidad> RetornarLista()
		{
			return _lugaresTuristicos;
		}
	}
}