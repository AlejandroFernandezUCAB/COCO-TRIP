using ApiRest_COCO_TRIP.Datos.Entity;
using System.Collections.Generic;

namespace ApiRest_COCO_TRIP.Datos.DAO.Interfaces
{
    interface IDAOActividad
    {
        List<Entidad> ConsultarLista(string id);

		void Actualizar(Entidad objeto);

		List<Entidad> ConsultarLista(Entidad objeto);

		Entidad ConsultarPorId(Entidad objeto);

		void Eliminar(Entidad objeto);

		void Insertar(Entidad objeto);

		void Insertar(Entidad entidad, Entidad lugar);
	}
}
