using ApiRest_COCO_TRIP.Datos.Entity;
using System.Collections.Generic;

namespace ApiRest_COCO_TRIP.Datos.DAO.Interfaces
{
    interface IDAOCategoria
    {
        List<Entidad> ConsultarLista(string id);
    }
}
