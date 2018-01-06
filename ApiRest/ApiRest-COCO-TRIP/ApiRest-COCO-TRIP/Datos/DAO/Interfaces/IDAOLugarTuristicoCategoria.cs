using ApiRest_COCO_TRIP.Datos.Entity;
using System.Collections.Generic;

namespace ApiRest_COCO_TRIP.Datos.DAO.Interfaces
{
	interface IDAOLugarTuristicoCategoria
	{
		void Insertar(Entidad categoria, Entidad lugarTuristico);

		List<Entidad> ConsultarLista(string id);

		void Eliminar(Entidad objeto);

		void Insertar(Entidad objeto);

		void Actualizar(Entidad objeto);

		List<Entidad> ConsultarLista(Entidad objeto);

		Entidad ConsultarPorId(Entidad objeto);
	}
}
