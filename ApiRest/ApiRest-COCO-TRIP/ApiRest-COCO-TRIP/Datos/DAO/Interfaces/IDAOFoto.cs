﻿using ApiRest_COCO_TRIP.Datos.Entity;
using System.Collections.Generic;

namespace ApiRest_COCO_TRIP.Datos.DAO.Interfaces
{
    public interface IDAOFoto
    {

        void Insertar(Entidad foto, Entidad lugar);

        List<Entidad> ConsultarLista(string id);

        void Eliminar(Entidad objeto);

        void Insertar(Entidad objeto);

		void Actualizar(Entidad objeto);

		List<Entidad> ConsultarLista(Entidad objeto);

		Entidad ConsultarPorId(Entidad objeto);

	}
}
