using System;
using System.Collections.Generic;
using ApiRest_COCO_TRIP.Datos.Entity;
using ApiRest_COCO_TRIP.Datos.DAO.Interfaces;

namespace ApiRest_COCO_TRIP.Datos.DAO
{
	public class DAOHorario : DAO , IDAOHorario
	{
		public override void Actualizar(Entidad objeto)
		{
			throw new NotImplementedException();
		}

		public override List<Entidad> ConsultarLista(Entidad objeto)
		{
			throw new NotImplementedException();
		}

        /// <summary>
        /// Devuelve la lista de Horarios de un lugar turistico especifico
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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
		/// Insertar el horario relacionado con un lugar en la BDD  
		/// </summary>
		/// <param name="entidad">Horario</param>
		/// <param name="id">Id lugar turistico</param>
		public void InsertarHorarioLugar(Entidad entidad, int id)
		{
			throw new NotImplementedException();
		}
	}
}