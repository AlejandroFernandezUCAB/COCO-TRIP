using ApiRest_COCO_TRIP.Datos.Entity;
using System.Collections.Generic;

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

		List<Entidad> ConsultarLista(string id);

		void Eliminar(Entidad objeto);

		void Insertar(Entidad objeto);

		void Actualizar(Entidad objeto);

		List<Entidad> ConsultarLista(Entidad objeto);

		Entidad ConsultarPorId(Entidad objeto);
	}
}
