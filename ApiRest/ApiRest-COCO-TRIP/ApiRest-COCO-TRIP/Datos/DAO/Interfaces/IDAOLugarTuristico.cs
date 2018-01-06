using ApiRest_COCO_TRIP.Datos.Entity;
using System.Collections.Generic;

namespace ApiRest_COCO_TRIP.Datos.DAO.Interfaces
{
	public interface IDAOLugarTuristico
	{

        void Actualizar(Entidad objeto);

		List<Entidad> ConsultarLista(Entidad objeto);

		Entidad ConsultarPorId(Entidad objeto);

		void Eliminar(Entidad objeto);

		void Insertar(Entidad objeto);

		List<Entidad> ConsultarTodaLaLista();
	}
}
