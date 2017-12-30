using System;
using System.Collections.Generic;
using ApiRest_COCO_TRIP.Datos.Entity;
using ApiRest_COCO_TRIP.Datos.DAO.Interfaces;

namespace ApiRest_COCO_TRIP.Datos.DAO
{
	public class DAOActividad : DAO , IDAOActividad
	{
        /// <summary>
        /// Devuelve la lista de Actividades de un lugar turistico especifico
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

		public override List<Entidad> ConsultarLista(Entidad objeto)
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

		public void InsertarActividadLugar(Entidad entidad, int id)
		{
			throw new NotImplementedException();
		}
	}
}