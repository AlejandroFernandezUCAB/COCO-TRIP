using ApiRest_COCO_TRIP.Datos.Entity;

namespace ApiRest_COCO_TRIP.Datos.DAO.Interfaces
{
	interface IDAOLugarTuristicoCategoria
	{
		void Insertar(Entidad categoria, Entidad lugarTuristico);
	}
}
