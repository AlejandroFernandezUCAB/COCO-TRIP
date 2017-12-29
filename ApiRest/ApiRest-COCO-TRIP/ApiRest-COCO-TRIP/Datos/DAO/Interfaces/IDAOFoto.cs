using ApiRest_COCO_TRIP.Datos.Entity;
using System.Collections.Generic;

namespace ApiRest_COCO_TRIP.Datos.DAO.Interfaces
{
    interface IDAOFoto
    {

        void InsertarFotoLugar(Entidad foto, int idLugar);

        List<Entidad> ConsultarLista(string id);

        void Eliminar(Entidad objeto);

        void Insertar(Entidad objeto);

		void Actualizar(Entidad objeto);

		List<Entidad> ConsultarLista(Entidad objeto);

		Entidad ConsultarPorId(Entidad objeto);

	}
}
