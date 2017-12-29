using ApiRest_COCO_TRIP.Datos.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiRest_COCO_TRIP.Datos.DAO.Interfaces
{
    interface IDAOCategoria
    {
        List<Entidad> ConsultarLista(string id);
    }
}
