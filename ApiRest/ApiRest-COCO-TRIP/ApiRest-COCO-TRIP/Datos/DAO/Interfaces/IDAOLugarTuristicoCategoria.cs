using ApiRest_COCO_TRIP.Datos.Entity;

namespace ApiRest_COCO_TRIP.Datos.DAO.Interfaces
{
	public interface IDAOLugarTuristicoCategoria
	{
		void Insertar(Entidad categoria, Entidad lugarTuristico);
		        
		/// <summary>
        /// Metodo que obtiene las categorias de un lugar turistico.
        /// </summary>
        /// <param name="entidad">Objeto: LugarTuristico.</param>
        /// <returns>Lista de categorias</returns>
        List<Entidad> ObtenerCategoriaPorId(Entidad entidad);
	}
}
